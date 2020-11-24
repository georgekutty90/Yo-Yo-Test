using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace Yo_Yo_App.DataAccess
{
    public class DBRepository<T> : IDBRepository<T> where T : class
    {
        private readonly string _connectionString;
        public DBRepository(IConfiguration config)
        {
            _connectionString = config["Database:connectionString"];//Add connection string in appsettings.json
        }
        public int ExecuteCommand(string query, object parameters)
        {
            using (var connection = ConnectionManager.GetSqlConnection(_connectionString))
            {
                try
                {
                    var cmd = new CommandDefinition(query, parameters, commandType: CommandType.StoredProcedure);
                    var result = connection.Execute(cmd);                   
                    return result;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }
        }

        public IEnumerable<T> SelectByParams(string query, object parameters)
        {
            using (var connection = ConnectionManager.GetSqlConnection(_connectionString))
            {
                try
                {
                    var cmd = new CommandDefinition(query, parameters, commandType: CommandType.StoredProcedure);
                    return connection.Query<T>(cmd);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }

        public IEnumerable<T> Select(string query)
        {
            using (var connection = ConnectionManager.GetSqlConnection(_connectionString))
            {
                try
                {
                    var cmd = new CommandDefinition(query,  commandType: CommandType.StoredProcedure);
                    return connection.Query<T>(cmd);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }
    }
}
