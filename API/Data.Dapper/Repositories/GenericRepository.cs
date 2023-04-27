using Dapper;
using Dapper.Contrib.Extensions;
using Data.Dapper.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using Pluralize.NET;
using System.Collections;
using System.Data;
using System.Reflection;

namespace Data.Dapper.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected IDbConnection EstablishConnection()
        {
            var conn = DapperContext.CreateConnection();
            conn.Open();
            return conn;
        }
        public async Task DeleteRowsByDynamicValuesAsync(dynamic item, string table = null)
        {
            var sqlBuilder = new SqlBuilder();
            var template = sqlBuilder.AddTemplate($"DELETE FROM {table ?? Table} /**where**/");
            object itemAsObj = item;
            var dynamicProps = itemAsObj.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var prop in dynamicProps)
            {
                var thisProp = prop.GetValue(item);
                if (thisProp == null)
                {
                    continue;
                }
                else if (prop.PropertyType != typeof(string) && prop.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)))
                {
                    var pluralizer = new Pluralizer();
                    if (pluralizer.IsPlural(prop.Name))
                    {
                        sqlBuilder.Where($"[{pluralizer.Singularize(prop.Name)}] IN @{prop.Name}");
                    }
                    else
                    {
                        sqlBuilder.Where($"[{prop.Name}] IN @{prop.Name}");
                    }
                }
                else
                {
                    sqlBuilder.Where($"[{prop.Name}] = @{prop.Name}");
                }
                parameters.Add($"@{prop.Name}", thisProp);
            }

            using (var db = EstablishConnection())
            {
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        await db.ExecuteAsync(template.RawSql, parameters, trans);
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                    }
                }
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var db = EstablishConnection())
            {
                var rows = await db.GetAllAsync<T>();
                return rows;
            }
        }

        public async Task<T> GetAsync(Guid id)
        {
            using (var db = EstablishConnection())
            {
                var row = await db.GetAsync<T>(id);
                return row;
            }
        }

        public async Task<List<TT>> GetFromSql<TT>(string query, object parameter = null)
        {
            using (var db = EstablishConnection())
            {
                List<TT> result;
                if (parameter != null)
                {
                    result = (await db.QueryAsync<TT>(query, parameter)).ToList<TT>();
                }
                else
                {
                    result = (await db.QueryAsync<TT>(query)).ToList<TT>();
                }
                return result;
            }
        }

        public List<TT> GetFromSqlNoAsync<TT>(string query, object parameter = null)
        {
            using (var db = EstablishConnection())
            {
                List<TT> result;
                if (parameter != null)
                {
                    result = (db.Query<TT>(query, parameter)).ToList<TT>();
                }
                else
                {
                    result = (db.Query<TT>(query)).ToList<TT>();
                }
                return result;
            }
        }

        public async Task<T> GetFirstOrDefaultByDynamicAsync(dynamic item, string table = null)
        {
            var sqlBuilder = new SqlBuilder();
            var template = sqlBuilder.AddTemplate($"SELECT * FROM {table ?? Table} /**where**/");
            object itemAsObj = item;
            var dynamicProps = itemAsObj.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var prop in dynamicProps)
            {
                var thisProp = prop.GetValue(item);
                if (thisProp == null)
                {
                    continue;
                }
                else if (prop.PropertyType != typeof(string) && prop.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)))
                {
                    var pluralizer = new Pluralizer();
                    if (pluralizer.IsPlural(prop.Name))
                    {
                        sqlBuilder.Where($"[{pluralizer.Singularize(prop.Name)}] IN @{prop.Name}");
                    }
                    else
                    {
                        sqlBuilder.Where($"[{prop.Name}] IN @{prop.Name}");
                    }
                }
                else
                {
                    sqlBuilder.Where($"[{prop.Name}] = @{prop.Name}");
                }
                parameters.Add($"@{prop.Name}", thisProp);
            }

            using (var db = EstablishConnection())
            {
                try
                {
                    var row = await db.QueryFirstOrDefaultAsync<T>(template.RawSql, parameters);
                    return row;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public async Task<int> RowCount(dynamic item, string table = null)
        {
            var sqlBuilder = new SqlBuilder();
            var template = sqlBuilder.AddTemplate($"SELECT count(*) as Rows FROM  {table ?? Table} /**where**/");

            using (var db = EstablishConnection())
            {
                try
                {
                    var row = await db.ExecuteScalarAsync<int>(template.RawSql);
                    return row;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<T>> GetByDynamicAsync(dynamic item, string table = null)
        {
            var sqlBuilder = new SqlBuilder();
            var template = sqlBuilder.AddTemplate($"SELECT * FROM {table ?? Table} /**where**/");
            object itemAsObj = item;
            var dynamicProps = itemAsObj.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var prop in dynamicProps)
            {
                var thisProp = prop.GetValue(item);
                if (thisProp == null)
                {
                    continue;
                }
                else if (prop.PropertyType != typeof(string) && prop.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)))
                {
                    var pluralizer = new Pluralizer();
                    if (pluralizer.IsPlural(prop.Name))
                    {
                        sqlBuilder.Where($"[{pluralizer.Singularize(prop.Name)}] IN @{prop.Name}");
                    }
                    else
                    {
                        sqlBuilder.Where($"[{prop.Name}] IN @{prop.Name}");
                    }
                }
                else
                {
                    sqlBuilder.Where($"[{prop.Name}] = @{prop.Name}");
                }
                parameters.Add($"@{prop.Name}", thisProp);
            }

            using (var db = EstablishConnection())
            {
                return await db.QueryAsync<T>(template.RawSql, parameters);
            }
        }
        public async Task<IEnumerable<T>> GetByDynamicAsync(dynamic item, int pageNumber, int pageSize, string order, string table = null)
        {
            int offset = (pageNumber - 1) * pageSize;
            var sqlBuilder = new SqlBuilder();
            var template = sqlBuilder.AddTemplate($"SELECT * FROM {table ?? Table} /**where**/");
            object itemAsObj = item;
            var dynamicProps = itemAsObj.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var prop in dynamicProps)
            {
                var thisProp = prop.GetValue(item);
                if (thisProp == null)
                {
                    continue;
                }
                else if (prop.PropertyType != typeof(string) && prop.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)))
                {
                    var pluralizer = new Pluralizer();
                    if (pluralizer.IsPlural(prop.Name))
                    {
                        sqlBuilder.Where($"[{pluralizer.Singularize(prop.Name)}] IN @{prop.Name}");
                    }
                    else
                    {
                        sqlBuilder.Where($"[{prop.Name}] IN @{prop.Name}");
                    }
                }
                else
                {
                    sqlBuilder.Where($"[{prop.Name}] = @{prop.Name}");
                }
                parameters.Add($"@{prop.Name}", thisProp);
            }

            string sql = $"{template.RawSql} ORDER BY {order} Limit {offset},{pageSize}";
            using (var db = EstablishConnection())
            {
                var rows = await db.QueryAsync<T>(sql, parameters);

                return rows;
            }
        }

        public async Task<int> InsertAsync(T item)
        {
            using (var db = EstablishConnection())
            {
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        var inserted = await db.InsertAsync<T>(item, trans);
                        trans.Commit();
                        return inserted;
                    }
                    catch
                    {
                        trans.Rollback();
                        return -1;
                    }
                }
            }
        }

        public async Task<int> InsertManyAsync(IEnumerable<T> items)
        {
            using (var db = EstablishConnection())
            {
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        var inserted = await db.InsertAsync(items, trans);
                        trans.Commit();
                        return inserted;
                    }
                    catch
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }
        }

        public async Task UpdateAsync(T item)
        {
            using (var db = EstablishConnection())
            {
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        await db.UpdateAsync<T>(item, trans);
                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        throw e;
                    }
                }
            }
        }

        public async Task SetManyToRemovedAsync()
        {
            using (var db = EstablishConnection())
            {
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        await db.ExecuteAsync($"UPDATE {Table} SET Removed = 1 WHERE Removed = 0", transaction: trans);
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                    }
                }
            }
        }

        public async Task SetToRemovedByDynamicAsync(dynamic item)
        {
            var sqlBuilder = new SqlBuilder();
            var template = sqlBuilder.AddTemplate($"UPDATE {Table} SET Removed = 1 /**where**/");
            object itemAsObj = item;
            var dynamicProps = itemAsObj.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var prop in dynamicProps)
            {
                var thisProp = prop.GetValue(item);
                if (thisProp == null)
                {
                    continue;
                }
                else if (prop.PropertyType != typeof(string) && prop.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)))
                {
                    var pluralizer = new Pluralizer();
                    if (pluralizer.IsPlural(prop.Name))
                    {
                        sqlBuilder.Where($"[{pluralizer.Singularize(prop.Name)}] IN @{prop.Name}");
                    }
                    else
                    {
                        sqlBuilder.Where($"[{prop.Name}] IN @{prop.Name}");
                    }
                }
                else
                {
                    sqlBuilder.Where($"[{prop.Name}] = @{prop.Name}");
                }
                parameters.Add($"@{prop.Name}", thisProp);
            }

            using (var db = EstablishConnection())
            {
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        await db.ExecuteAsync(template.RawSql, parameters, trans);
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                    }
                }
            }
        }

        public async Task<T> GetAsync(string id)
        {
            using (var db = EstablishConnection())
            {
                var row = await db.GetAsync<T>(id);
                return row;
            }
        }

        private string Table => typeof(T).GetCustomAttribute<TableAttribute>(false)?.Name;
    }
}