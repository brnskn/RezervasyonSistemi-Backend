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
    ///  İşletmenin plan detay bilgileri ile ilgili işlemler.
    /// </summary>  
    public class PlanDetaylariController : ApiController
    {
        private RezervasyonSistemiContext db = new RezervasyonSistemiContext();

        /// <summary>
        /// İşletmenin plan detay bilgilerini getirir.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpGet]
        public List<PlanDetaylari> Get(int PlanID)
        {
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
            var IsletmeID = db.Planlars.Find(PlanID).IsletmeID;
            if (db.Isletmelers.Where(x => x.KullaniciID == currentUser && x.ID == IsletmeID).First() == null)
            {
                return null;
            }
            var userPlanDetaylari = db.PlanDetaylaris.Where(x => x.PlanID == PlanID).ToList();
            return userPlanDetaylari;
        }

        /// <summary>
        /// İşletme için yeni bir plan detay bilgisi ekler.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpPost]
        public IHttpActionResult Post(PlanDetaylari PlanDetaylari)
        {
            if (PlanDetaylari != null && ModelState.IsValid)
            {
                ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
                int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
                var IsletmeID = db.Planlars.Find(PlanDetaylari.PlanID).IsletmeID;
                if (db.Isletmelers.Where(x => x.KullaniciID == currentUser && x.ID == IsletmeID).First() == null)
                {
                    return BadRequest("IsletmeID is not valid.");
                }
                PlanDetaylari.Planlar = null;
                db.PlanDetaylaris.Add(PlanDetaylari);
                db.SaveChanges();
                return Ok(PlanDetaylari);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        /// <summary>
        /// İşletme için eklenen bir plan detay bilgisi düzenler.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpPut]
        public IHttpActionResult Put(PlanDetaylari PlanDetaylari)
        {
            if (PlanDetaylari != null && ModelState.IsValid)
            {
                ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
                int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
                var IsletmeID = db.Planlars.Find(PlanDetaylari.PlanID).IsletmeID;
                if (db.Isletmelers.Where(x => x.KullaniciID == currentUser && x.ID == IsletmeID).First() == null)
                {
                    return BadRequest("IsletmeID is not valid.");
                }
                try
                {
                    PlanDetaylari updatedPlanDetaylari = db.PlanDetaylaris.Single(x => x.ID == PlanDetaylari.ID);
                    if (updatedPlanDetaylari == null)
                    {
                        return BadRequest("Plan ID is not valid");
                    }
                    updatedPlanDetaylari.HaftaninGunu = PlanDetaylari.HaftaninGunu;
                    updatedPlanDetaylari.BaslangicSaati = PlanDetaylari.BaslangicSaati;
                    updatedPlanDetaylari.BitisSaati = PlanDetaylari.BitisSaati;
                    db.Entry(updatedPlanDetaylari).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Ok(updatedPlanDetaylari);
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
        /// İşletme için eklenen bir plan detay bilgisini siler.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpDelete]
        public IHttpActionResult Delete(int ID)
        {
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
            try
            {
                var PlanID = db.PlanDetaylaris.Find(ID).PlanID;
                var IsletmeID = db.Planlars.Find(PlanID).IsletmeID;
                if (db.Isletmelers.Where(x => x.KullaniciID == currentUser && x.ID == IsletmeID).First() == null)
                {
                    return BadRequest("IsletmeID is not valid.");
                }

                PlanDetaylari deletedPlanDetaylari = db.PlanDetaylaris.Single(x => x.ID == ID);
                if (deletedPlanDetaylari == null)
                {
                    return BadRequest("PlanlarID is not valid");
                }
                db.PlanDetaylaris.Remove(deletedPlanDetaylari);
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
