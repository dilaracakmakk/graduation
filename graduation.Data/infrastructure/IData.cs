using graduation.Data.infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace graduation.Data.infrastructure
{
    public interface IData<T>
    {
        DataResult Insert(T t);
        DataResult Update(T t);
        DataResult Delete(T t);
        DataResult DeleteByKey(int id);

        T GetByKey(int id);
        List<T> GetAll();
        List<T> GetAll(string orderBy, bool isDesc = false);
        List<T> GetBy(Expression<Func<T, bool>> predicate);
        List<T> GetBy(Expression<Func<T, bool>> predicate, string orderBy, bool isDesc = false);
        List<T> GetByPage(int pageNumber, int pageCount, string orderBy = "id", bool isDesc = false);
        List<T> GetByPage(Expression<Func<T, bool>> predicate, int pageNumber, int pageCount, string orderBy = "id", bool isDesc = false);

        T FirstOrDefault(Expression<Func<T, bool>> predicate, bool asNoTracking = false);
        T FirstOrDefault(Expression<Func<T, bool>> predicate, string orderBy = "id", bool isDesc = false, bool asNoTracking = false);
        void DetachAllEntities();

        DataResult InsertBulk(List<T> ts, bool validateAndIgnoredBefore = false);

        int GetCount();
        int GetCount(Expression<Func<T, bool>> predicate);



    }
}
