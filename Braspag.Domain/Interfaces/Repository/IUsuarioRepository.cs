using Braspag.Domain.DTO;
using Braspag.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Braspag.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Usuario Autenticacao(UsuarioDto dto);
    }
}
