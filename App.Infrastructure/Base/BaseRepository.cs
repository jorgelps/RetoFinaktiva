
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace App.Infrastructure.Base
{
    public interface IBaseRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();
        TEntity FindById(object id);
        TEntity Create(TEntity entity, bool autoSave = true);
        TEntity Update(TEntity entity, bool autoSave = true);
        void Delete(TEntity entity, bool autoSave = true);
        void SaveChanges();



    }

    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected DbContext _database;
        protected DbSet<TEntity> _table;

        public BaseRepository(DbContext context)
        {
            _database = context;
            _table = _database.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _table;
        }

        public virtual TEntity FindById(object id)
        {
            var newId = CastPrimaryKey(id);
            return _table.Find(newId);
        }

        public virtual TEntity Create(TEntity entity, bool autoSave = true)
        {
            _table.Add(entity);

            if (autoSave)
            {
                _database.SaveChanges();
            }

            return entity;
        }

        public virtual TEntity Update(TEntity entity, bool autoSave = true)
        {
            if (entity == null)
            {
                return null;
            }
            TEntity exist = _table.Find(GetValuePrimaryKey(entity));
            if (exist != null)
            {
                _database.Entry(exist).CurrentValues.SetValues(entity);
                if (autoSave)
                {
                    _database.SaveChanges();
                }
            }

            return exist;
        }

        public virtual void Delete(TEntity entity, bool autoSave = true)
        {
            _table.Remove(entity);
            if (autoSave)
            {
                _database.SaveChanges();
            }
        }

        public virtual void SaveChanges()
        {
            _database.SaveChanges();
        }

        protected object CastPrimaryKey(object id)
        {
            string keyName = GetPrimaryKeyName();
            Type keyType = typeof(TEntity).GetProperty(keyName).PropertyType;
            return Convert.ChangeType(id, keyType);
        }

        protected object GetValuePrimaryKey(TEntity entity)
        {
            string keyName = GetPrimaryKeyName();
            object value = typeof(TEntity).GetProperty(keyName).GetValue(entity);
            return value;
        }

        protected string GetPrimaryKeyName()
        {
            var keyNames = _database.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties.Select(x => x.Name);
            string keyName = keyNames.FirstOrDefault();

            if (keyNames.Count() > 1)
            {
                throw new ApplicationException("Error admin");
            }

            if (keyName == null)
            {
                throw new ApplicationException("Error admin");
            }

            return keyName;
        }


    }
}
