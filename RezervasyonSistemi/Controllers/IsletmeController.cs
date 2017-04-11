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
    ///  Kullanıcının işletme bilgilerini düzenler.
    /// </summary>  
    public class IsletmeController : ApiController
    {
        private RezervasyonSistemiContext db = new RezervasyonSistemiContext();

        /// <summary>
        /// Giriş yapan kullanıcının işletmesini getirir.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpGet]
        public List<Isletmeler> Get()
        {
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
            var userIsletme = db.Isletmelers.Where(x => x.KullaniciID == currentUser).ToList();
            return userIsletme;
        }

        /// <summary>
        /// Giriş yapan kullanıcıya yeni bir işletme ekler.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpPost]
        public IHttpActionResult Post(Isletmeler isletme)
        {
            if (isletme != null && ModelState.IsValid)
            {
                ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
                int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
                isletme.KullaniciID = currentUser;
                isletme.KatBilgileris = null;
                isletme.Kullanicilar = null;
                db.Isletmelers.Add(isletme);
                db.SaveChanges();
                return Ok(isletme);
            }else
            {
                return BadRequest(ModelState);
            }

        }

        /// <summary>
        /// Giriş yapan kullanıcının işletmesini düzenler.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpPut]
        public IHttpActionResult Put(Isletmeler isletme)
        {
            if (isletme != null && ModelState.IsValid)
            {
                ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
                int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
                try
                {
                    Isletmeler updatedIsletme = db.Isletmelers.Single(x => x.KullaniciID == currentUser && x.ID == isletme.ID);
                    if (updatedIsletme == null)
                    {
                        return BadRequest("Isletme ID is not valid");
                    }
                    updatedIsletme.ID = updatedIsletme.ID;
                    updatedIsletme.IsletmeAciklamasi = isletme.IsletmeAciklamasi;
                    updatedIsletme.IsletmeAdresi = isletme.IsletmeAdresi;
                    updatedIsletme.IsletmeIsmi = isletme.IsletmeIsmi;
                    updatedIsletme.IsletmeKrokiResim = isletme.IsletmeKrokiResim;
                    updatedIsletme.IsletmeNumarasi = isletme.IsletmeNumarasi;
                    updatedIsletme.KullaniciID = currentUser;
                    db.Entry(updatedIsletme).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Ok(isletme);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Giriş yapan kullanıcının işletmesini düzenler.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpDelete]
        public IHttpActionResult Delete(int ID)
        {
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
            try
            {
                Isletmeler deletedIsletme = db.Isletmelers.Single(x => x.KullaniciID == currentUser && x.ID == ID);
                if (deletedIsletme == null)
                {
                    return BadRequest("Isletme ID is not valid");
                }
                db.Isletmelers.Remove(deletedIsletme);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
