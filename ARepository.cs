using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YemekListesi.API.Models;

namespace YemekListesi.API.Data
{
    public class ARepository : IARepository
    {
        private DataContext _context;

        public ARepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public List<Yemek> GetYemekler()
        {
            var yemeklers = _context.Yemeklers.Include(c=>c.Fotograflar).ToList();
            return yemeklers;
        }

        public Yemek GetYemekId(int yemekId)
        {
            var yemek = _context.Yemeklers.Include(c => c.Fotograflar).FirstOrDefault(c => c.Id == yemekId);
            return yemek;
        }

        public Foto GetFoto(int id) 
        {
            var foto = _context.Fotograflars.FirstOrDefault(p => p.Id == id);
            return foto;
        }

        public List<Foto> GetFotoYemek(int yemekId)
        {
            var foto = _context.Fotograflars.Where(p => p.YemekId == yemekId).ToList();
            return foto;
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}

