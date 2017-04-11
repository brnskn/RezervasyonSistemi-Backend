using RezervasyonSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace RezervasyonSistemi.Controllers
{
    /// <summary>  
    ///  Kullanıcının bilgilerini düzenler.
    /// </summary>  
    public class KullaniciController : ApiController
    {
        private RezervasyonSistemiContext db = new RezervasyonSistemiContext();

        /// <summary>
        /// Giriş yapan kullanıcının bilgilerini getirir.
        /// </summary>
        /// <returns>Kullanıcı bilgileri</returns>
        [Authorize(Roles = "True")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
            Kullanicilar userIsletme = db.Kullanicilars.Where(x => x.ID == currentUser).FirstOrDefault();
            return Ok(userIsletme);
        }
        /// <summary>
        /// Giriş yapan kullanıcının işletmesini düzenler.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpPut]
        public IHttpActionResult Put(Kullanicilar kullanici)
        {
            if (kullanici != null && ModelState.IsValid)
            {
                ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
                int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
                try
                {
                    Kullanicilar updatedKullanici = db.Kullanicilars.Single(x => x.ID == currentUser);
                    if (updatedKullanici == null)
                    {
                        return BadRequest("Kullanici ID is not valid");
                    }
                    updatedKullanici.Isim = kullanici.Isim;
                    updatedKullanici.Soyisim = kullanici.Soyisim;
                    updatedKullanici.KullaniciAdi = kullanici.KullaniciAdi;
                    updatedKullanici.KullaniciTipi = kullanici.KullaniciTipi;
                    updatedKullanici.Mail = kullanici.Mail;
                    updatedKullanici.TelefonNumarasi = kullanici.TelefonNumarasi;
                    db.Entry(updatedKullanici).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Ok(updatedKullanici);
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
