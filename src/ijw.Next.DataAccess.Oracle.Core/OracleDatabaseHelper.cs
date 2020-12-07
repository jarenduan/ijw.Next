using ijw.Next.Collection;
using ijw.Next.Log;

using Oracle.ManagedDataAccess.Client;

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ijw.Next.DataAccess.Oracle.Core {
    /// <summary>
    /// Oracle 数据库 Helper
    /// </summary>
    public class OracleDatabaseHelper {
        private readonly string _connString;
        private readonly ILogger? _logger;

        /// <summary>
        /// Oracle 数据库访问
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="logger">日志记录器</param>
        public OracleDatabaseHelper(string connString, ILogger? logger = null) {
            connString.ShouldBeNotNullArgument(nameof(connString));
            _connString = connString;
            _logger = logger;
        }

        /// <summary>
        /// 查询标量字符串
        /// </summary>
        /// <param name="cmdtxt">SQL命令文本</param>
        /// <param name="parameters">Oracle sql命令参数</param>
        /// <returns>查询的字符串</returns>
        public string? QueryScalar(string cmdtxt, params OracleParameter[] parameters) {
            var re = queryScalar(cmdtxt, parameters);
            return (re is DBNull) ? null : (string)re;
        }

        /// <summary>
        /// 查询标量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdtxt">SQL命令文本</param>
        /// <param name="parameters">Oracle sql命令参数</param>
        /// <returns>查询的标量</returns>
        public T? QueryScalar<T>(string cmdtxt, params OracleParameter[] parameters) where T : struct {
            var re = queryScalar(cmdtxt, parameters);
            return (re is DBNull) ? null : (T?)re;
        }

        /// <summary>
        /// 查询标量
        /// </summary>
        /// <param name="cmdtxt">SQL命令文本</param>
        /// <param name="parameters">Oracle sql命令参数</param>
        /// <returns>查询的标量</returns>
        public async Task<string?> QueryScalarAsync(string cmdtxt, params OracleParameter[] parameters)
            => await QueryScalarAsync(cmdtxt, CancellationToken.None, parameters);

        /// <summary>
        /// 查询标量
        /// </summary>
        /// <typeparam name="T">标量的类型</typeparam>
        /// <param name="cmdtxt">SQL命令文本</param>
        /// <param name="parameters">Oracle sql命令参数</param>
        /// <returns>查询的标量</returns>
        public async Task<T?> QueryScalarAsync<T>(string cmdtxt, params OracleParameter[] parameters) where T : struct 
            => await QueryScalarAsync<T>(cmdtxt, CancellationToken.None, parameters);

        /// <summary>
        /// 查询标量
        /// </summary>
        /// <param name="cmdtxt">SQL命令文本</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <param name="parameters">Oracle sql命令参数</param>
        /// <returns>查询的标量</returns>
        public async Task<string?> QueryScalarAsync(string cmdtxt, CancellationToken cancellationToken, params OracleParameter[] parameters) {
            var re = await queryScalarAsync(cmdtxt, cancellationToken, parameters);
            return re is DBNull ? null : (string)re;
        }

        /// <summary>
        /// 查询标量集合
        /// </summary>
        /// <typeparam name="T">标量的类型</typeparam>
        /// <param name="cmdtxt">SQL命令文本</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <param name="parameters">Oracle sql命令参数</param>
        /// <returns>查询的标量集合</returns>
        public async Task<T?> QueryScalarAsync<T>(string cmdtxt, CancellationToken cancellationToken, params OracleParameter[] parameters) where T : struct {
            var re = await queryScalarAsync(cmdtxt, cancellationToken, parameters);
            return re is DBNull ? null : (T?)(T)re;
        }

        /// <summary>
        /// 查询字符串标量集合
        /// </summary>
        /// <typeparam name="T">标量的类型</typeparam>
        /// <param name="cmdtxt">SQL命令文本</param>
        /// <param name="parameters">Oracle sql命令参数</param>
        /// <returns>查询的标量集合</returns>
        public List<T?> QueryScalarList<T>(string cmdtxt, params OracleParameter[] parameters)
            where T : struct => QueryEntities<T?>(cmdtxt, (r) => r.FieldCount == 0 || r[0] is null || r[0] is DBNull ? null : (T)r[0], parameters);

        /// <summary>
        /// 查询字符串标量集合
        /// </summary>
        /// <param name="cmdtxt">SQL命令文本</param>
        /// <param name="parameters">Oracle sql命令参数</param>
        /// <returns>查询到的标量集合</returns>
        public List<string?> QueryScalarList(string cmdtxt, params OracleParameter[] parameters)
            => QueryEntities(cmdtxt, (r) => r.FieldCount == 0 || r[0] is null || r[0] is DBNull ? null : r[0].ToString(), parameters);

        /// <summary>
        /// 异步地查询标量集合
        /// </summary>
        /// <typeparam name="T">标量类型, 只能是struct</typeparam>
        /// <param name="cmdtxt">SQL命令文本</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <param name="parameters">Oracle sql命令参数</param>
        /// <returns>查询到的标量集合</returns>
        public async Task<List<T?>> QueryScalarListAsync<T>(string cmdtxt, CancellationToken cancellationToken, params OracleParameter[] parameters)
            where T : struct => await QueryEntitiesAsync<T?>(cmdtxt,
                                                            () => default(T),
                                                            (e, r) => r.FieldCount == 0 || r[0] is null || r[0] is DBNull ? null : (T)r[0],
                                                            cancellationToken,
                                                            parameters);

        /// <summary>
        /// 异步地查询字符串标量集合
        /// </summary>
        /// <param name="cmdtxt">SQL命令文本</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <param name="parameters">Oracle sql命令参数</param>
        /// <returns>标量集合</returns>
        public async Task<List<string?>> QueryScalarListAsync(string cmdtxt, CancellationToken cancellationToken, params OracleParameter[] parameters)
            => await QueryEntitiesAsync<string?>(cmdtxt,
                                                () => null,
                                                (e, r) => r.FieldCount == 0 || r[0] is null || r[0] is DBNull ? null : r[0].ToString(),
                                                cancellationToken,
                                                parameters);

        /// <summary>
        /// 查询实体集合
        /// </summary>
        /// <typeparam name="T">标量的类型</typeparam>
        /// <param name="cmdtxt">SQL命令文本</param>
        /// <param name="fillEntitiesFunc">填充实体的函数</param>
        /// <param name="parameters">Oracle sql命令参数</param>
        /// <returns>查询结果实体</returns>
        public List<T> QueryEntities<T>(string cmdtxt, Func<IDataRecord, T> fillEntitiesFunc, params OracleParameter[] parameters) { 
            fillEntitiesFunc.ShouldBeNotNullArgument(nameof(fillEntitiesFunc));

            List<T> result = new List<T>();
            using var oc = new OracleConnection(_connString);
            oc.Open();
            using var cmd = new OracleCommand(cmdtxt, oc);
            if (parameters != null) {
                cmd.Parameters.AddRange(parameters);
            }

            _logger?.WriteTrace("[OracleDBHelper] QueryEntities: " + cmd.CommandText);
            _logger?.WriteTrace("[OracleDBHelper]   w/ paras: " + parameters?.ToAllEnumStrings(e => $"{e.ParameterName} = {e.Value}") ?? " None.");

            using var odr = cmd.ExecuteReader();
            var count = 0;
            while (odr.Read()) {
                count++;
                result.Add(fillEntitiesFunc(odr));
            }

            return result;
        }

        /// <summary>
        /// 查询实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdtxt">SQL命令文本</param>
        /// <param name="parameters">Oracle sql命令参数</param>
        /// <returns>查询结果, 实体列表</returns>
        public async Task<List<T>> QueryEntitiesAsync<T>(string cmdtxt, params OracleParameter[] parameters)
            where T : class, new()
            => await QueryEntitiesAsync<T>(cmdtxt, CancellationToken.None, parameters);

        /// <summary>
        /// 查询实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdtxt">SQL命令文本</param>
        /// <param name="cancellationToken">取消标志</param>
        /// <param name="parameters">Oracle sql命令参数</param>
        /// <returns>查询结果, 实体列表</returns>
        public async Task<List<T>> QueryEntitiesAsync<T>(string cmdtxt, CancellationToken cancellationToken, params OracleParameter[] parameters)
            where T : class, new()
            => await QueryEntitiesAsync(cmdtxt, () => new T(), parameters);

        /// <summary>
        /// 使用指定的创建函数与默认的填充函数, 查询实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdtxt">SQL命令文本</param>
        /// <param name="createNewEntity">创建实体新实例的函数</param>
        /// <param name="parameters">Oracle sql命令参数</param>
        /// <returns>查询结果, 实体列表</returns>
        public async Task<List<T>> QueryEntitiesAsync<T>(string cmdtxt, Func<T> createNewEntity, params OracleParameter[] parameters)
            where T : class => await QueryEntitiesAsync(cmdtxt, createNewEntity, CancellationToken.None, parameters);

        /// <summary>
        /// 使用指定的创建函数与默认的填充函数, 查询实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdtxt">SQL命令文本</param>
        /// <param name="createNewEntity">创建实体新实例的函数</param>
        /// <param name="cancellationToken">取消标志</param>
        /// <param name="parameters">Oracle sql命令参数</param>
        /// <returns>查询结果, 实体列表</returns>
        public async Task<List<T>> QueryEntitiesAsync<T>(string cmdtxt, Func<T> createNewEntity, CancellationToken cancellationToken, params OracleParameter[] parameters)
            where T : class => await QueryEntitiesAsync(cmdtxt, createNewEntity, (e, dr) => e.FillPropertiesOfBasicType(dr), cancellationToken, parameters);

        /// <summary>
        /// 使用指定的创建与填充函数, 查询实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdtxt">SQL命令文本</param>
        /// <param name="createNewEntity">创建实体新实例的函数</param>
        /// <param name="fillEntitiesByDataRecordFunc">从数据记录填充实体的函数</param>
        /// <param name="parameters">Oracle sql命令参数</param>
        /// <returns>查询结果, 实体列表</returns>
        public async Task<List<T>> QueryEntitiesAsync<T>(string cmdtxt, Func<T> createNewEntity, Func<T, IDataRecord, T> fillEntitiesByDataRecordFunc, params OracleParameter[] parameters)
            where T : class => await QueryEntitiesAsync(cmdtxt, createNewEntity, fillEntitiesByDataRecordFunc, CancellationToken.None, parameters);

        /// <summary>
        /// 使用指定的创建与填充函数, 查询实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdtxt">SQL命令文本</param>
        /// <param name="createNewEntity">创建实体新实例的函数</param>
        /// <param name="fillEntitiesByDataRecordFunc">从数据记录填充实体的函数</param>
        /// <param name="cancelToken">取消标志</param>
        /// <param name="parameters">Oracle sql命令参数</param>
        /// <returns>查询结果, 实体列表</returns>
        public async Task<List<T>> QueryEntitiesAsync<T>(string cmdtxt, Func<T> createNewEntity, Func<T, IDataRecord, T> fillEntitiesByDataRecordFunc, CancellationToken cancelToken, params OracleParameter[] parameters) {
            fillEntitiesByDataRecordFunc.ShouldBeNotNullArgument(nameof(fillEntitiesByDataRecordFunc));

            List<T> result = new List<T>();
            using var oc = new OracleConnection(_connString);
            oc.Open();
            using var cmd = new OracleCommand(cmdtxt, oc);
            if (parameters != null) {
                cmd.Parameters.AddRange(parameters);
            }

            _logger?.WriteTrace("[OracleDBHelper] QueryEntitiesAsync:" + cmd.CommandText);
            _logger?.WriteTrace("[OracleDBHelper]   w/ paras: " + parameters?.ToAllEnumStrings(e => $"{e.ParameterName} = {e.Value}") ?? " None.");

            using var odr = await cmd.ExecuteReaderAsync(cancelToken);

            while (await odr.ReadAsync()) {
                T e = createNewEntity();
                fillEntitiesByDataRecordFunc(e, odr);
                result.Add(e);
            }

            return result;
        }
        
        /// <summary>
        /// 执行非查询SQL命令
        /// </summary>
        /// <param name="cmdtxt">SQL命令文本</param>
        /// <param name="parameters">Oracle sql命令参数</param>
        /// <returns>影响的行数</returns>
        public int ExecuteNonQuery(string cmdtxt, params OracleParameter[] parameters) {
            using var oc = new OracleConnection(_connString);
            oc.Open();
            using var cmd = new OracleCommand(cmdtxt, oc);
            if (parameters != null) {
                cmd.Parameters.AddRange(parameters);
            }

            _logger?.WriteTrace("[OracleDBHelper] ExecuteNonQuery: " + cmd.CommandText);
            _logger?.WriteTrace("[OracleDBHelper]   w/ paras: " + parameters?.ToAllEnumStrings(e => $"{e.ParameterName} = {e.Value}") ?? " None.");
            
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 异步执行非查询SQL命令
        /// </summary>
        /// <param name="cmdtxt">SQL命令文本</param>
        /// <param name="parameters">Oracle sql命令参数</param>
        /// <returns>影响的行数</returns>
        public async Task<int> ExecuteNonQueryAsync(string cmdtxt, params OracleParameter[] parameters) 
            => await ExecuteNonQueryAsync(cmdtxt, CancellationToken.None, parameters);

        /// <summary>
        /// 异步执行非查询SQL命令
        /// </summary>
        /// <param name="cmdtxt">SQL命令文本</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <param name="parameters">Oracle sql命令参数</param>
        /// <returns>影响的行数</returns>
        public async Task<int> ExecuteNonQueryAsync(string cmdtxt, CancellationToken cancellationToken, params OracleParameter[] parameters) {
            using var oc = new OracleConnection(_connString);
            oc.Open();
            using var cmd = new OracleCommand(cmdtxt, oc);
            if (parameters != null) {
                cmd.Parameters.AddRange(parameters);
            }

            _logger?.WriteTrace("[OracleDBHelper] ExecuteNonQueryAsync: " + cmd.CommandText);
            _logger?.WriteTrace("[OracleDBHelper]   w/ paras: " + parameters?.ToAllEnumStrings(e => $"{e.ParameterName} = {e.Value}") ?? " None.");

            return await cmd.ExecuteNonQueryAsync(cancellationToken);
        }

        private object queryScalar(string cmdtxt, OracleParameter[] parameters) {
            using var oc = new OracleConnection(_connString);
            oc.Open();
            using var cmd = new OracleCommand(cmdtxt, oc);
            if (parameters != null) {
                cmd.Parameters.AddRange(parameters);
            }

            _logger?.WriteTrace("[OracleDBHelper] QueryScalar: " + cmd.CommandText);
            _logger?.WriteTrace("[OracleDBHelper]   w/ paras: " + parameters?.ToAllEnumStrings(e => $"{e.ParameterName} = {e.Value}") ?? " None.");

            return cmd.ExecuteScalar();
        }

        private async Task<object> queryScalarAsync(string cmdtxt, CancellationToken cancellationToken, params OracleParameter[] parameters) {
            using var oc = new OracleConnection(_connString);
            oc.Open();
            using var cmd = new OracleCommand(cmdtxt, oc);
            if (parameters != null) {
                cmd.Parameters.AddRange(parameters);
            }
            
            _logger?.WriteTrace("[OracleDBHelper] QueryScalarAsync: " + cmd.CommandText);
            _logger?.WriteTrace("[OracleDBHelper]   w/ paras: " + parameters?.ToAllEnumStrings(e => $"{e.ParameterName} = {e.Value}") ?? " None.");

            return await cmd.ExecuteScalarAsync(cancellationToken);
        }
    }
}