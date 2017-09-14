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
    ///  İşletmenin plan bilgileri ile ilgili işlemler.
    /// </summary>  
    public class PlanController : ApiController
    {
        private RezervasyonSistemiContext db = new RezervasyonSistemiContext();

        /// <summary>
        /// İşletmenin plan bilgilerini getirir.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpGet]
        public List<Planlar> Get(int IsletmeID)
        {
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
            if (db.Isletmelers.Where(x => x.KullaniciID == currentUser && x.ID == IsletmeID).First() == null)
            {
                return null;
            }
            var userPlanlar = db.Planlars.Where(x => x.IsletmeID == IsletmeID).ToList();
            return userPlanlar;
        }

        /// <summary>
        /// İşletme için yeni bir plan bilgisi ekler.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpPost]
        public IHttpActionResult Post(Planlar Planlar)
        {
            if (Planlar != null && ModelState.IsValid)
            {
                ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
                int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
                if (db.Isletmelers.Where(x => x.KullaniciID == currentUser && x.ID == Planlar.IsletmeID).First() == null)
                {
                    return BadRequest("IsletmeID is not valid.");
                }
                Planlar.Isletmeler = null;
                Planlar.PlanDetaylaris = null;
                db.Planlars.Add(Planlar);
                db.SaveChanges();
                return Ok(Planlar);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        /// <summary>
        /// İşletme için eklenen bir plan bilgisi düzenler.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpPut]
        public IHttpActionResult Put(Planlar Planlar)
        {
            if (Planlar != null && ModelState.IsValid)
            {
                ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
                int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
                if (db.Isletmelers.Where(x => x.KullaniciID == currentUser && x.ID == Planlar.IsletmeID).First() == null)
                {
                    return BadRequest("IsletmeID is not valid.");
                }
                try
                {
                    Planlar updatedPlanlar = db.Planlars.Single(x => x.ID == Planlar.ID);
                    if (updatedPlanlar == null)
                    {
                        return BadRequest("Plan ID is not valid");
                    }
                    updatedPlanlar.PlanIsmi = Planlar.PlanIsmi;
                    updatedPlanlar.PlanAciklamasi = Planlar.PlanAciklamasi;
                    db.Entry(updatedPlanlar).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Ok(updatedPlanlar);
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
        /// İşletme için eklenen bir plan bilgisini siler.
        /// </summary>
        [Authorize(Roles = "True")]
        [HttpDelete]
        public IHttpActionResult Delete(int ID)
        {
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            int currentUser = Convert.ToInt32(claimsIdentity.FindFirst("ID").Value);
            try
            {
                var IsletmeID = db.Planlars.Find(ID).IsletmeID;
                if (db.Isletmelers.Where(x => x.KullaniciID == currentUser && x.ID == IsletmeID).First() == null)
                {
                    return BadRequest("IsletmeID is not valid.");
                }

                Planlar deletedPlanlar = db.Planlars.Single(x => x.ID == ID);
                if (deletedPlanlar == null)
                {
                    return BadRequest("PlanlarID is not valid");
                }
                db.Planlars.Remove(deletedPlanlar);
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
