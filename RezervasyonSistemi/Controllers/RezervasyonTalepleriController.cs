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
    ///  İşletmenin rezervasyon işlemleri.
    /// </summary>  
    public class RezervasyonTalepleriController : ApiController
    {
        private RezervasyonSistemiContext db = new RezervasyonSistemiContext();

        /// <summary>
        /// İşletmenin rezervasyon bilgilerini getirir.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpGet]
        public List<RezervasyonTalepleri> Get(int IsletmeID)
        {
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
            if (db.Isletmelers.Where(x => x.KullaniciID == currentUser && x.ID == IsletmeID).First() == null)
            {
                return null;
            }
            var userRezervasyonTalepleri = db.RezervasyonTalepleris.Where(x=> x.MasaBilgileri.KatBilgileri.IsletmeID == IsletmeID).ToList();
            return userRezervasyonTalepleri;
        }

        /// <summary>
        /// İşletmenin rezervasyon bilgilerini masaya göre getirir.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpGet]
        public List<RezervasyonTalepleri> Get(int IsletmeID, int MasaID)
        {
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
            if (db.Isletmelers.Where(x => x.KullaniciID == currentUser && x.ID == IsletmeID).First() == null)
            {
                return null;
            }
            var userRezervasyonTalepleri = db.RezervasyonTalepleris.Where(x => x.MasaBilgileri.KatBilgileri.IsletmeID == IsletmeID && x.MasaID == MasaID).ToList();
            return userRezervasyonTalepleri;
        }

        /// <summary>
        /// İşletmenin rezervasyon bilgilerini onaylar.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpPut]
        public IHttpActionResult Put(RezervasyonTalepleri RezervasyonTalepleri)
        {
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
            try
            {
                RezervasyonTalepleri updatedRezervasyonTalepleri = db.RezervasyonTalepleris.Single(x => x.ID == RezervasyonTalepleri.ID && x.MasaBilgileri.KatBilgileri.Isletmeler.KullaniciID == currentUser);
                if (updatedRezervasyonTalepleri == null)
                {
                    return BadRequest("ID is not valid");
                }
                updatedRezervasyonTalepleri.OnayDurumu = RezervasyonTalepleri.OnayDurumu;
                db.Entry(updatedRezervasyonTalepleri).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Ok(updatedRezervasyonTalepleri);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// İşletmenin rezervasyon bilgilerini siler.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpDelete]
        public IHttpActionResult Delete(int ID)
        {
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
            try
            {
                RezervasyonTalepleri deletedRezervasyonTalepleri = db.RezervasyonTalepleris.Single(x => x.ID == ID && x.MasaBilgileri.KatBilgileri.Isletmeler.KullaniciID == currentUser);
                if (deletedRezervasyonTalepleri == null)
                {
                    return BadRequest("ID is not valid");
                }
                db.RezervasyonTalepleris.Remove(deletedRezervasyonTalepleri);
                db.SaveChanges();
                return Ok(deletedRezervasyonTalepleri);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
