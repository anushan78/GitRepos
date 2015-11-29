using System.Collections.Generic;

namespace Evil.Repository
{
    public interface IDataContext<TEntity> where TEntity : class
    {
        List<TEntity> Retrieve();
    }
}