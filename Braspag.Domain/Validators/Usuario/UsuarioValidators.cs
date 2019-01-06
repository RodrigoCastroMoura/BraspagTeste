using FluentValidation;
using Braspag.Domain.DTO;


namespace Braspag.Domain.Validators.Usuario
{
    public class UsuarioValidators : AbstractValidator<UsuarioDto>
    {
        public UsuarioValidators()
        {

            RuleFor(usuario => usuario.login)
                .NotEmpty()
                .NotNull()
                .WithMessage("Login não informado");

            RuleFor(usuario => usuario.senha)
                .NotEmpty()
                .NotNull()
                .WithMessage("Senha não informado");
        }

    }
}
