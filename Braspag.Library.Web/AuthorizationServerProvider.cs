using System.Collections.Generic;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Braspag.Service;
using Braspag.Domain.Interfaces.Services;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using System;

namespace Braspag.Library.Web
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private  IUsuarioServices usuario;
        
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
               var Container = new UnityContainer();
               IoC.IoC.Resolve(Container);
               usuario = Container.Resolve<IUsuarioServices>();

               var user = usuario.Autenticacao(context.UserName, context.Password);
               usuario.Dispose();
                Container.Dispose();

                if (user == null)
                {
                    context.SetError("invalid_grant", "Login ou senha está incorreto");
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Role, "Usuario"));
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("role", "user"));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.login));
          

                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                  { "Hora", DateTime.Now.ToString()}
                  

                });

                var ticket = new AuthenticationTicket(identity, props);

                context.Validated(ticket);
            }
            catch (Exception ex)
            {
                context.SetError("invalid_grant", ex.Message);
            }
            

        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}
