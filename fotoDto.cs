using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberi.API.Dtos
{
    public class fotoDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Aciklama { get; set; }
        public DateTime EklemeZamani { get; set; }
        public bool VarMi { get; set; }
        public string GenelId { get; set; }
    }
}
