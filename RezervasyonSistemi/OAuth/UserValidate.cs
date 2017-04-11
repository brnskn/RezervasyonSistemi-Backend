using RezervasyonSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RezervasyonSistemi.OAuth
{
    public class UserValidate
    {
        public static Kullanicilar Validate(string username, string password)
        {
            RezervasyonSistemiContext db = new RezervasyonSistemiContext();
            return db.Kullanicilars.FirstOrDefault(x=>x.KullaniciAdi==username && x.Sifre==password);
        }
    }
}