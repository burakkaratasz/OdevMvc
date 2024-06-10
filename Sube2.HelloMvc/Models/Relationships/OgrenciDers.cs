using Sube2.HelloMvc.Models;

namespace Sube2.HelloMvc.Models.Relationships
{
    public class OgrenciDers
    {
        public int Ogrenciid { get; set; }
        public Ogrenci? Ogrenci { get; set; }

        public int Dersid { get; set; }
        public Ders? Ders { get; set; }
    }
}