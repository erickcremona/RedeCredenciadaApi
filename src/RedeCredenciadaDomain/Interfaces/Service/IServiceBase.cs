using FluentValidation;
using FluentValidation.Results;
using RedeCredenciadaDomain.Entities;

namespace RedeCredenciadaDomain.Interfaces.Service
{
    public interface IServiceBase<TEntity> where TEntity : Entity
    {
        void Notificate(ValidationResult validationResult);
        void Notificate(string message);
        bool RunValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : class;
        bool HasNotification();
    }
}