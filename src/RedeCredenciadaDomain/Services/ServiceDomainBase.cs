using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using RedeCredenciadaDomain.Entities;
using RedeCredenciadaDomain.Interfaces.Repositoty;
using RedeCredenciadaDomain.Interfaces.Service;
using RedeCredenciadaDomain.Notifications;

namespace RedeCredenciadaDomain.Services
{
    public class ServiceDomainBase<TEntity> : IServiceBase<TEntity> where TEntity : Entity
    {
        private readonly INotification _notification;
        private readonly ILogger<ServiceDomainBase<TEntity>> _logger;

        public ServiceDomainBase(IRepositoryBase<TEntity> repositoryBase,
                                              INotification notification,
                              ILogger<ServiceDomainBase<TEntity>> logger)
        {
            _notification = notification;
            _logger = logger;
        }

        public void Notificate(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificate(error.ErrorMessage);
                _logger.LogError($"[{nameof(ServiceDomainBase<TEntity>)}] erro de parâmetro - {error.ErrorMessage}");
            }

        }

        public void Notificate(string message)
            => _notification.Handle(new Notification(message));

        public bool RunValidation<TV, TE>(TV validation, TE entity)
            where TV : AbstractValidator<TE>
            where TE : class
        {
            var validator = validation.Validate(entity);
            Notificate(validator);
            return validator.IsValid;
        }

        public bool HasNotification()
            => _notification.HasNotification();
    }
}
