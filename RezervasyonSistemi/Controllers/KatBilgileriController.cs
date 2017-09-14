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
    ///  İşletmenin kat bilgileri ile ilgili işlemler.
    /// </summary>  
    public class KatBilgileriController : ApiController
    {
        private RezervasyonSistemiContext db = new RezervasyonSistemiContext();
        /// <summary>
        /// İşletmenin kat bilgilerini getirir.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpGet]
        public List<KatBilgileri> Get(int IsletmeID)
        {
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
            if (db.Isletmelers.Where(x => x.KullaniciID == currentUser && x.ID == IsletmeID).First()==null)
            {
                return null;
            }
            var userKatBilgileri = db.KatBilgileris.Where(x => x.IsletmeID == IsletmeID).ToList();
            return userKatBilgileri;
        }


        /// <summary>
        /// İşletme için yeni bir kat bilgisi ekler.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpPost]
        public IHttpActionResult Post(KatBilgileri KatBilgileri)
        {
            if (KatBilgileri != null && ModelState.IsValid)
            {
                ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
                int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
                if (db.Isletmelers.Where(x => x.KullaniciID == currentUser && x.ID == KatBilgileri.IsletmeID).First() == null)
                {
                    return BadRequest("IsletmeID is not valid.");
                }
                KatBilgileri.Isletmeler = null;
                KatBilgileri.MasaBilgileris = null;
                db.KatBilgileris.Add(KatBilgileri);
                db.SaveChanges();
                return Ok(KatBilgileri);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }


        /// <summary>
        /// İşletme için eklenen bir kat bilgisi düzenler.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpPut]
        public IHttpActionResult Put(KatBilgileri KatBilgileri)
        {
            if (KatBilgileri != null && ModelState.IsValid)
            {
                ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
                int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
                if (db.Isletmelers.Where(x => x.KullaniciID == currentUser && x.ID == KatBilgileri.IsletmeID).First() == null)
                {
                    return BadRequest("IsletmeID is not valid.");
                }
                try
                {
                    KatBilgileri updatedKatBilgileri = db.KatBilgileris.Single(x => x.ID == KatBilgileri.ID);
                    if (updatedKatBilgileri == null)
                    {
                        return BadRequest("Isletme ID is not valid");
                    }
                    updatedKatBilgileri.KatIsmi = KatBilgileri.KatIsmi;
                    db.Entry(updatedKatBilgileri).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Ok(updatedKatBilgileri);
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
        /// İşletme için eklenen bir kat bilgisini siler.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpDelete]
        public IHttpActionResult Delete(int ID)
        {
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
            try
            {
                var IsletmeID = db.KatBilgileris.Find(ID).IsletmeID;
                if (db.Isletmelers.Where(x => x.KullaniciID == currentUser && x.ID == IsletmeID).First() == null)
                {
                    return BadRequest("IsletmeID is not valid.");
                }

                KatBilgileri deletedKatBilgileri = db.KatBilgileris.Single(x => x.ID == ID);
                if (deletedKatBilgileri == null)
                {
                    return BadRequest("KatBilgileriID is not valid");
                }
                db.KatBilgileris.Remove(deletedKatBilgileri);
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
