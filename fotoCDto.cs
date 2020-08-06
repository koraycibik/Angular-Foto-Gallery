using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace YemekListesi.API.Dtos
{
    public class FotoCDto
    {
        public FotoCDto()
        {
            DateAdded=DateTime.Now;
        }
        public string Url { get; set; }
        public IFormFile Dosya { get; set; }
        public string Aciklama { get; set; }
        public DateTime EklemeZamani { get; set; }
        public string GenelId { get; set; }

    }
}
