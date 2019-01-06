using System.Web.Http;
using Microsoft.Practices.Unity;

namespace RastreioFacil.Library.Web
{
    public class WebBaseApi : ApiController
    {
        private readonly UnityContainer container;


        public WebBaseApi()
        {
            container = new UnityContainer();
        }

        public UnityContainer Container
        {
            get { return container; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && container != null)
            {
                container.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
