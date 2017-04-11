using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;
using System.Security.Claims;
using RezervasyonSistemi.Models;
using System.Web.Http;


namespace RezervasyonSistemi.OAuth.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private RezervasyonSistemiContext db = new RezervasyonSistemiContext();
        // OAuthAuthorizationServerProvider sınıfının client erişimine izin verebilmek için ilgili ValidateClientAuthentication metotunu override ediyoruz.
        public override async System.Threading.Tasks.Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        // OAuthAuthorizationServerProvider sınıfının kaynak erişimine izin verebilmek için ilgili GrantResourceOwnerCredentials metotunu override ediyoruz.
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // CORS ayarlarını set ediyoruz.
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            // Kullanıcının access_token alabilmesi için gerekli validation işlemlerini yapıyoruz.
            Kullanicilar kullanici = UserValidate.Validate(context.UserName, context.Password);
            if (kullanici!=null)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("ID", kullanici.ID.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Role, kullanici.KullaniciTipi.ToString()));

                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "Kullanıcı adı veya şifre yanlış.");
            }
        }
    }
}