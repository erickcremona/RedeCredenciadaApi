using FluentValidation;
using RedeCredenciadaDomain.DTOs;

namespace RedeCredenciadaDomain.Validations
{
    public class SubstituicaoValidation : AbstractValidator<BaseFilterConsultaDTO>
    {
        public SubstituicaoValidation()
        {
            RuleFor(p => p.Cidade)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(p => p.Estado)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido");
                
            When(p => p.CodigoPlano == null && p.RegistroANS == null, () =>
            {
                RuleFor(p => p.CodigoPlano)
                .NotNull().WithMessage("Código do Plano ou Registro ANS deve ser informado");
            });

        }
    }
}
