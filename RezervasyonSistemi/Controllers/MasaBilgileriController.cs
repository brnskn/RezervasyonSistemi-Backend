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
    ///  İşletmenin masa bilgileri ile ilgili işlemler.
    /// </summary>  
    public class MasaBilgileriController : ApiController
    {
        private RezervasyonSistemiContext db = new RezervasyonSistemiContext();

        /// <summary>
        /// İlgili kat bilgisine göre masa bilgilerini getirir.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpGet]
        public List<MasaBilgileri> Get(int KatID)
        {
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
            var IsletmeID = db.KatBilgileris.Find(KatID).IsletmeID;
            if (db.Isletmelers.Where(x => x.KullaniciID == currentUser && x.ID == IsletmeID).First() == null)
            {
                return null;
            }
            var userMasaBilgileri = db.MasaBilgileris.Where(x => x.KatID == KatID).ToList();
            return userMasaBilgileri;
        }


        /// <summary>
        /// İlgili kat bilgisine göre masa bilgisi ekler.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpPost]
        public IHttpActionResult Post(MasaBilgileri MasaBilgileri)
        {
            if (MasaBilgileri != null && ModelState.IsValid)
            {
                ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
                int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
                var IsletmeID = db.KatBilgileris.Find(MasaBilgileri.KatID).IsletmeID;
                if (db.Isletmelers.Where(x => x.KullaniciID == currentUser && x.ID == IsletmeID).First() == null)
                {
                    return BadRequest("KatID is not valid.");
                }
                MasaBilgileri.KatBilgileri = null;
                db.MasaBilgileris.Add(MasaBilgileri);
                db.SaveChanges();
                return Ok(MasaBilgileri);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }


        /// <summary>
        /// İlgili kat bilgisine göre masa bilgisini düzenler.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpPut]
        public IHttpActionResult Put(MasaBilgileri MasaBilgileri)
        {
            if (MasaBilgileri != null && ModelState.IsValid)
            {
                ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
                int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
                var IsletmeID = db.KatBilgileris.Find(MasaBilgileri.KatID).IsletmeID;
                if (db.Isletmelers.Where(x => x.KullaniciID == currentUser && x.ID == IsletmeID).First() == null)
                {
                    return BadRequest("KatID is not valid.");
                }
                try
                {
                    MasaBilgileri updatedMasaBilgileri = db.MasaBilgileris.Single(x => x.ID == MasaBilgileri.ID);
                    if (updatedMasaBilgileri == null)
                    {
                        return BadRequest("Isletme ID is not valid");
                    }
                    updatedMasaBilgileri.KatID = MasaBilgileri.KatID;
                    updatedMasaBilgileri.MasaIsmi = MasaBilgileri.MasaIsmi;
                    updatedMasaBilgileri.MasaNumarasi = MasaBilgileri.MasaNumarasi;
                    db.Entry(updatedMasaBilgileri).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Ok(updatedMasaBilgileri);
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
        /// İlgili kat bilgisine göre masa bilgisini siler.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpDelete]
        public IHttpActionResult Delete(int ID)
        {
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
            try
            {
                var KatID = db.MasaBilgileris.Find(ID).KatID;
                var IsletmeID = db.KatBilgileris.Find(KatID).IsletmeID;
                if (db.Isletmelers.Where(x => x.KullaniciID == currentUser && x.ID == IsletmeID).First() == null)
                {
                    return BadRequest("IsletmeID is not valid.");
                }

                MasaBilgileri deletedMasaBilgileri = db.MasaBilgileris.Single(x => x.ID == ID);
                if (deletedMasaBilgileri == null)
                {
                    return BadRequest("KatBilgileriID is not valid");
                }
                db.MasaBilgileris.Remove(deletedMasaBilgileri);
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
