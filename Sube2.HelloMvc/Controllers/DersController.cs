using Sube2.HelloMvc.Models;
using Sube2.HelloMvc.Models.Relationships;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Sube2.HelloMvc.Controllers
{
    public class DersController : Controller
    {
        
        public IActionResult Index() //Dersler tablosundaki tüm kayıtları listelemek için
        {
            //veritabanından bir referans oluştur
            using (var ctx = new OkulDbContext())
            {
                //kayıtları listeye çek ve listeyi index view'a gönder
                var lst = ctx.Dersler.ToList();
                return View(lst);
            }
        }

        [HttpGet]   // Get metodu ile AddDers view'ini göstermek için
        public IActionResult AddDers()
        {
            return View();
        }

        [HttpPost] // Post metodu ile yeni bir ders eklemek için
        public IActionResult AddDers(Ders ders)
        {
            if (ders != null) // Gönderilen ders objesi null değilse
            {
                using (var ctx = new OkulDbContext())
                {
                    ctx.Dersler.Add(ders);
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult EditDers(int id) 
        {
            using (var ctx = new OkulDbContext())
            {
                var ders = ctx.Dersler.Find(id); //id'ye göre dersi çek
                return View(ders);
            }
        }

        [HttpPost]
        public IActionResult EditDers(Ders ders)
        {
            if (ders != null)
            {
                using (var ctx = new OkulDbContext())
                {
                    ctx.Entry(ders).State = EntityState.Modified; // Dersin durumunu güncellenmiş olarak işaretliyoruz
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteDers(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                var ders = ctx.Dersler.Find(id);
                if (ders != null)
                {
                    ctx.Dersler.Remove(ders);
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult KayitliOgrenciler(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                var ders = ctx.Dersler.Include(d => d.OgrenciDersler!).ThenInclude(od => od.Ogrenci!).FirstOrDefault(d => d.Dersid == id); //OgrenciDersler ve Ogrenci ilişkisini dahil et
                if (ders == null)
                {
                    return NotFound();
                }

                if (ders.OgrenciDersler == null) //ilişki yok ise yeni liste oluştur
                {
                    ders.OgrenciDersler = new List<OgrenciDers>();
                }

                return View(ders); //ders view a gönder
            }
        }
    }
}