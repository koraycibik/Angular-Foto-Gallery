using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YemekListesi.API.Models;

namespace YemekListesi.API.Data
{
    public interface IARepository
    {
        void Add<T>(T entity) where T:class ;
        void Delete<T>(T entity) where T : class;
        bool SaveAll();

        List<Yemek> GetYemekler();
        List<Foto> GetFotoYemek(int yemekid);
        Yemek GetYemekId(int yemekid);
        Foto GetFoto(int id);

    }
}
