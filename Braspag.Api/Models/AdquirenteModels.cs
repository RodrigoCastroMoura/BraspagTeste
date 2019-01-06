using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Braspag.Api.Models
{
    public class AdquirenteModels
    {
        public string adquirentes { get; set; }

        public decimal? visa { get; set; }

        public decimal? master { get; set; }

        public decimal? elo { get; set; }
    }
}