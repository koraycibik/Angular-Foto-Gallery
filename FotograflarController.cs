using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using YemekListesi.API.Data;
using YemekListesi.API.Dtos;
using YemekListesi.API.Helpers;
using YemekListesi.API.Models;

namespace YemekListesi.API.Controllers
{
    [Produces("application/json")]
    [Route("api/yemekler/{yemekid}/fotolar")]
    public class FotograflarController : Controller
    {
        private IARepository _appRepo;
        private IMapper _map;
        private IOptions<CloudinarySettings> _cloudinaryC;

        private Cloudinary _cloudinary;

        public FotograflarController(IARepository appRepo, IMapper map, IOptions<CloudinarySettings> cloudinaryC)
        {
            _appRepo = appRepo;
            _map = map;
            _cloudinaryC = cloudinaryC;

            Account account = new Account(
                _cloudinaryC.Value.CloudName, 
                _cloudinaryC.Value.ApiKey, 
                _cloudinaryC.Value.ApiSecret);

            _cloudinary=new Cloudinary(account);
        }

        [HttpPost]
        public ActionResult AddFotoYemek(int yemekid, [FromForm]FotoDto fotoDto)
        {
            var yemek = _appRepo.GetFotoById(yemek);

            if (yemek==null)
            {
                return BadRequest("Could not find the city");
            }

            var cUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (cUserId!=yemek.UserId)
            {
                return Unauthorized();
            }

            var filefor = fotoDto.Dosya;

            var uResult = new ImageUploadResult();

            if (filefor.Length>0)
            {
                using (var strm = filefor.OpenReadStream())
                {
                    var uParams = new ImageUploadParams
                    {
                        File = new FileDescription(filefor.Name, strm)
                    };

                    uResult = _cloudinary.Upload(uParams);
                }
            }

            fotoDto.Url = uploadResult.Uri.ToString();
            fotoCDto.GenelId = uResult.GenelId;

            var foto = _map.Map<Foto>(fotoCDto);
            foto.Yemek = yemek;

            if (!yemek.Fotolar.Any(p=>p.VarMi))
            {
                foto.VarMi = true;
            }

            yemek.Fotolar.Add(foto);

            if (_appRepo.SaveAll())
            {
                var fotoR = _mapper.Map<fotoDto>(foto);
                return CreatedAtRoute("GetFoto", new {id = foto.Id}, fotoR);

            }
            return BadRequest("Could not add the photo");
        }

        [HttpGet("{id}", Name = "GetFoto")]
        public ActionResult GetFoto(int id)
        {
            var FotoDb = _appRepo.GetFoto(id);
            var foto = _mapper.Map<fotoR>(FotoDb);

            return Ok(foto);
        }
    }
}