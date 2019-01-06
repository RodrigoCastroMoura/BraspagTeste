using Braspag.Domain.Commands;
using Braspag.Domain.DTO;
using Braspag.Domain.Entities;
using Braspag.Domain.Interfaces.Repository;
using Braspag.Domain.Interfaces.Services;
using Braspag.Domain.Interfaces.UnityOfWork;
using Braspag.Domain.Service;

namespace Braspag.Service
{
    public class UsuarioAppSevice : AppService, IUsuarioServices
    {
        private readonly IUsuarioRepository iosuarioRepository;

        public UsuarioAppSevice(
            IUnitiOfWork unitOfWork,
            IUsuarioRepository iosuarioRepository)
            : base(unitOfWork)
        {
            this.iosuarioRepository = iosuarioRepository;
        }

        public Usuario Autenticacao(string login, string senha)
        {
            var dto = new UsuarioDto()
            {
                login = login,
                senha = senha
            };

            dto.ValidaUsuario();

            return iosuarioRepository.Autenticacao(dto);
        }
    }
}
