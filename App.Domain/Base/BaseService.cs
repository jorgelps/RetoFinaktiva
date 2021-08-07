using App.Infrastructure.Base;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace App.Domain.Base
{
    public interface IBaseService<TDTO>
        where TDTO : class
    {
        TDTO Create(TDTO dto, bool autoSave = true);
        void Delete(object id, bool autoSave = true);
        TDTO FindById(object Id);
        TDTO Update(TDTO dto, bool autoSave = true);
        IEnumerable<TDTO> GetAll();

        void SaveChanges();
    }

    public abstract class BaseService<TDTO, TEntity> : IBaseService<TDTO> where TDTO : class where TEntity : class
    {

        public IBaseRepository<TEntity> _repository;
        public IMapper _mapperDependency;
        public IConfiguration _configuration;

        public BaseService(IBaseRepository<TEntity> repository, IMapper mapperDependency, IConfiguration configuration)
        {
            _repository = repository;
            _mapperDependency = mapperDependency;
            _configuration = configuration;
        }

        #region CRUD

        public virtual TDTO Create(TDTO dto, bool autoSave = true)
        {
            TEntity entity = _mapperDependency.Map<TEntity>(dto);
            entity = _repository.Create(entity, autoSave);
            return _mapperDependency.Map<TDTO>(entity);
        }

        public virtual void Delete(object id, bool autoSave = true)
        {
            TEntity entity = _repository.FindById(id);
            _repository.Delete(entity, autoSave);
        }

        public virtual TDTO FindById(object Id)
        {
            var result = _repository.FindById(Id);
            return _mapperDependency.Map<TDTO>(result);
        }

        public virtual TDTO Update(TDTO dto, bool autoSave = true)
        {
            TEntity entity = _mapperDependency.Map<TEntity>(dto);
            entity = _repository.Update(entity, autoSave);
            return _mapperDependency.Map<TDTO>(entity);
        }

        public virtual IEnumerable<TDTO> GetAll()
        {
            List<TDTO> list;
            list = _mapperDependency.Map<List<TDTO>>(_repository.GetAll());
            return list;
        }

     


        public virtual void SaveChanges()
        {
            _repository.SaveChanges();
        }

        #endregion
    }
}
