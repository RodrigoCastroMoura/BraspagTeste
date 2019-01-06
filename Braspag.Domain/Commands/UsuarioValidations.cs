using Braspag.Domain.DTO;
using Braspag.Domain.Validators.Usuario;
using FluentValidation;


namespace Braspag.Domain.Commands
{
    public static  class UsuarioValidations
    {
        public static void ValidaUsuario(this UsuarioDto dto)
        {
            UsuarioValidators validator = new UsuarioValidators();
            validator.ValidateAndThrow(dto);
        }
    }
}
