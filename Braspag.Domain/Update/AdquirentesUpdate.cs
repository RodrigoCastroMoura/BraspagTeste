using System;
using System.Collections.Generic;
using Braspag.Domain.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Braspag.Domain.Update
{
    public static class AdquirentesUpdate
    {
        public static UpdateDocument UpDateAdquirentes(this Adquirentes adquirente)
        {
            var update = new UpdateDocument();

            if (adquirente.adquirentes != null)
                update.Set("adquirentes", adquirente.adquirentes);

            if (adquirente.visa != 0)
                update.Set("visa", adquirente.visa.ToString().Replace(",","."));

            if (adquirente.master != 0)
                update.Set("master", adquirente.master.ToString().Replace(",", "."));

            if (adquirente.elo != 0)
                update.Set("elo", adquirente.elo.ToString().Replace(",", "."));

            return update;
        }
    }
}
