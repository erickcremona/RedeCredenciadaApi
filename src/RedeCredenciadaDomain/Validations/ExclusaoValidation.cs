using FluentValidation;
using RedeCredenciadaDomain.DTOs;

namespace RedeCredenciadaDomain.Validations
{
    public class ExclusaoValidation : AbstractValidator<BaseFilterConsultaDTO>
    {
        public ExclusaoValidation()
        {
            RuleFor(p => p.Cidade)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(p => p.Estado)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido");

            When(p => p.RegistroANS == null && p.CodigoPlano == null, () =>
            {
                RuleFor(p => p.CodigoPlano)
                .NotNull().WithMessage("Código do Plano ou Registro ANS deve ser informado");
            });
        }
    }
}
