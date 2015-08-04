using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using MongoDB.Driver.Linq;
using MongoRepository;

namespace Neat.Data.Mongo
{
    public class MongoRepository<T> : IRepository<T> where T : class, IEntity<string>, new()
    {
        private readonly MongoRepository.IRepository<T> _repository;

        public MongoRepository()
        {
            _repository = new MongoRepository.MongoRepository<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _repository.Collection.AsQueryable();
        }

        public T GetById(string id)
        {
            return _repository.GetById(id);
        }

        public T Add(T entity)
        {
            return _repository.Add(entity);
        }

        public void Update(T entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                throw new DataException("Entity must provide an Id for an Update!");
            }
            var existingEntity = GetById(entity.Id);
            if (existingEntity == null)
            {
                throw new DataException(string.Format("No entity exists with Id {0}!", entity.Id));
            }
            _repository.Update(entity);
        }

        public void Delete(T entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                throw new DataException("Entity must provide an Id for an Delete!");
            }
            var existingEntity = GetById(entity.Id);
            if (existingEntity == null)
            {
                throw new DataException(string.Format("No entity exists with Id {0}!", entity.Id));
            }
            _repository.Delete(entity);
        }

        public void Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new DataException("Entity must provide an Id for a Delete!");
            }
            var existingEntity = GetById(id);
            if (existingEntity == null)
            {
                throw new DataException(string.Format("No entity exists with Id {0}!", id));
            }
            _repository.Delete(id);
        }
    }

    public class GenericMongoRepository : IGenericRepository
    {
        public IQueryable<object> GetAll(Type type)
        {
            var method = GetType().GetMethod("GetAllTyped");
            var generic = method.MakeGenericMethod(type);

            return generic.Invoke(this, new object[0]) as IQueryable<object>;
        }

        public IQueryable<T> GetAllTyped<T>() where T : class, IEntity<string>, new()
        {
            var mongoRepository = new MongoRepository<T>();

            return mongoRepository.GetAll();
        }

        public object GetById(Type type, string id)
        {
            var method = GetType().GetMethod("GetByIdTyped");
            var generic = method.MakeGenericMethod(type);

            return generic.Invoke(this, new object[] { id });
        }

        public T GetByIdTyped<T>(string id) where T : class, IEntity<string>, new()
        {
            var mongoRepository = new MongoRepository<T>();

            return mongoRepository.GetById(id);
        }

        public object Add(Type type, object entity)
        {
            var method = GetType().GetMethod("AddTyped");
            var generic = method.MakeGenericMethod(type);

            return generic.Invoke(this, new [] { entity });
        }

        public T AddTyped<T>(T entity) where T : class, IEntity<string>, new()
        {
            var mongoRepository = new MongoRepository<T>();

            return mongoRepository.Add(entity);
        }

        public void Update(Type type, object entity)
        {
            var method = GetType().GetMethod("UpdateTyped");
            var generic = method.MakeGenericMethod(type);

            generic.Invoke(this, new[] { entity });
        }

        public void UpdateTyped<T>(T entity) where T : class, IEntity<string>, new()
        {
            var mongoRepository = new MongoRepository<T>();

            mongoRepository.Update(entity);
        }

        public void Delete(Type type, object entity)
        {
            var method = GetType().GetMethod("DeleteTypedByEntity");
            var generic = method.MakeGenericMethod(type);

            generic.Invoke(this, new[] { entity });
        }

        public void DeleteTypedByEntity<T>(T entity) where T : class, IEntity<string>, new()
        {
            var mongoRepository = new MongoRepository<T>();

            mongoRepository.Delete(entity);
        }

        public void Delete(Type type, string id)
        {
            var method = GetType().GetMethod("DeleteTypedById");
            var generic = method.MakeGenericMethod(type);

            generic.Invoke(this, new object[] { id });
        }

        public void DeleteTypedById<T>(string id) where T : class, IEntity<string>, new()
        {
            var mongoRepository = new MongoRepository<T>();

            mongoRepository.Delete(id);
        }
    }
}