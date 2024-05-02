using Microsoft.Extensions.Logging;
using RedeCredenciadaDomain.Entities;
using RedeCredenciadaDomain.Interfaces.Repositoty;
using RedeCredenciadaInfraData.Common;
using TopDown.Core.Data;

namespace RedeCredenciadaInfraData.Repository
{
    public class RepositoryBase<TEntity> : BaseRepository, IRepositoryBase<TEntity> where TEntity : Entity
    {
        public RepositoryBase(BaseData baseData,
                 ILogger<BaseRepository> logger)
                     : base(baseData, logger) { }
    }
}
