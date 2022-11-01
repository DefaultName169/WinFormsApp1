using Dapper;
using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Reflection;

namespace SynceOToHTLT.DataAccess.Common
{
    /// <summary>
    /// Base class connect db
    /// </summary>
    public class DbContext
    {
        private readonly string _connStr;

        public DbContext(string connections)
        {
            _connStr = connections;
        }
        /// <summary>
        /// Get data from table with condition
        /// query null return all.
        /// </summary>
        /// <typeparam name="T">Type of data</typeparam>
        /// <param name="query">sql query</param>
        /// <param name="param">data filter</param>
        /// <returns></returns>
        public List<T> Get<T>(string query = null, object param = null, string connstr = null) where T : class
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return ExecuteCommand<IEnumerable<T>>(connstr ?? _connStr,
                    conn => conn.GetAll<T>()).ToList();
            }
            else
            {
                return ExecuteCommand<IEnumerable<T>>(connstr ?? _connStr,
                    conn => conn.Query<T>(query, param)).ToList();
            }
        }

        public IEnumerable<T> GetSQLServer<T>(string query = null, object param = null, string connstr = null) where T : class
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return ExecuteCommandSqlServer<IEnumerable<T>>(connstr ?? _connStr,
                    conn => conn.GetAll<T>()).ToList();
            }
            else
            {
                return ExecuteCommandSqlServer<IEnumerable<T>>(connstr ?? _connStr,
                    conn => conn.Query<T>(query, param)).ToList();
            }
        }

        /// <summary>
        /// return a record
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public T SingleOrDefault<T>(string query, object param = null, string connstr = null) where T : class
        {
            return ExecuteCommand<T>(connstr ?? _connStr, conn =>
                    conn.Query<T>(query, param).SingleOrDefault());
        }
        /// <summary>
        /// return a record sqlserver
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <param name="connstr"></param>
        /// <returns></returns>
        public T SingleOrDefaultSQLSERVER<T>(string query, object param = null, string connstr = null) where T : class
        {
            return ExecuteCommandSqlServer<T>(connstr ?? _connStr, conn =>
                    conn.Query<T>(query, param).SingleOrDefault());
        }

        /// <summary>
        /// Insert an object to db
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns>ID of record inserted</returns>
        public long Insert<T>(T obj, string connstr = null) where T : class
        {
            return ExecuteCommand<long>(connstr ?? _connStr, conn =>
            {
                return conn.Insert<T>(obj);
            });
        }

        /// <summary>
        /// Insert a number of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns>Count of record inserted</returns>
        public long Inserts<T>(IEnumerable<T> list, string connstr = null) where T : class
        {
            return ExecuteCommand<long>(connstr ?? _connStr, conn =>
            {
                return conn.Insert(list);
            });
        }

        public bool CheckTableExist(string tbName, string connstr = null)
        {
            return this.Get<dynamic>($"SHOW TABLES LIKE '{tbName.ToLower()}'", null, connstr ?? _connStr).ToList().Count > 0;
        }

        public bool CheckTableExistSQLSERVER(string tbName, string connstr = null)
        {
            return this.ExecuteScalarSQLSERVER<int>($"if OBJECT_ID('[{tbName.ToLower()}]') is not null BEGIN SELECT count(*) FROM information_schema.columns WHERE table_name = '{tbName.ToLower()}' END", null, connstr) > 0;
        }

        public long InsertsSQLSERVER<T>(IEnumerable<T> list, string connstr = null) where T : class
        {
            return ExecuteCommandSqlServer<long>(connstr ?? _connStr, conn =>
            {
                return conn.Insert(list);
            });
        }

        /// <summary>
        /// get an object by id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">Id</param>
        /// <returns></returns>
        public T GetById<T>(int id, string connstr = null) where T : class
        {
            return ExecuteCommand<T>(connstr ?? _connStr, conn =>
            {
                return conn.Get<T>(id);
            });
        }

        /// <summary>
        /// delete and object by id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Delete<T>(T obj, string connstr = null) where T : class
        {
            return ExecuteCommand<bool>(connstr ?? _connStr, conn =>
            {
                return conn.Delete<T>(obj);
            });
        }

        /// <summary>
        /// delete list of objecst
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Deleted<T>(IEnumerable<T> list, string connstr = null) where T : class
        {
            return ExecuteCommand<bool>(connstr ?? _connStr, conn =>
            {
                return conn.Delete(list);
            });
        }

        /// <summary>
        /// Delete from table by condition
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void DeleteByCondition<T>(string condition, object param = null, string connstr = null) where T : class
        {
            ExecuteNonQuery($"delete from {TableName<T>()} where {condition} ", param, connstr);
        }

        /// <summary>
        /// update an exists obj by id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Update<T>(T obj, string connstr = null) where T : class
        {
            return ExecuteCommand<bool>(connstr ?? _connStr, conn =>
            {
                return conn.Update<T>(obj);
            });
        }


        /// <summary>
        /// Update a list of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Updates<T>(IEnumerable<T> list, string connstr = null) where T : class
        {
            return ExecuteCommand<bool>(connstr ?? _connStr, conn =>
            {
                return conn.Update(list);
            });
        }


        /// <summary>
        /// check if condition exist
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool Exists<T>(string condition, object param = null, string connstr = null)
        {
            return ExecuteCommand<bool>(connstr ?? _connStr, conn =>
            {
                return conn.ExecuteScalar<bool>($"select count(1) from {TableName<T>()} where {condition}", param);
            });
        }

        /// <summary>
        /// Count number of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Count<T>(string condition, object param = null, string connstr = null)
        {
            return ExecuteCommand<int>(connstr ?? _connStr, conn =>
            {
                return conn.ExecuteScalar<int>($"select count(1) from {TableName<T>()} where {condition}", param);
            });
        }

        /// <summary>
        /// return a string, a number or a boolen....
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public T ExecuteScalar<T>(string condition, object param = null, string connstr = null)
        {
            return ExecuteCommand<T>(connstr ?? _connStr, conn =>
            {
                return conn.ExecuteScalar<T>(condition, param);
            });
        }

        public T ExecuteScalarSQLSERVER<T>(string condition, object param = null, string connstr = null)
        {
            return ExecuteCommandSqlServer<T>(connstr ?? _connStr, conn =>
            {
                return conn.ExecuteScalar<T>(condition, param);
            });
        }

        /// <summary>
        /// execute a script
        /// </summary>
        /// <param name="script"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public void ExecuteScript(string script, string delimiter, string connstr = null)
        {
            ExecuteCommand(connstr ?? _connStr, conn =>
            {
                MySqlScript cmd = new MySqlScript(conn);
                cmd.Query = script;
                cmd.Delimiter = delimiter;
                cmd.Execute();
            });
        }

        /// <summary>
        /// execute a store without query
        /// </summary>
        /// <param name="store"></param>
        /// <param name="param"></param>
        /// <param name="constr"></param>
        public void ExecuteStoreNonQuery(string store, object param = null, string constr = null)
        {
            ExecuteCommand(constr ?? _connStr, conn =>
            {
                conn.Execute(store,
                    param,
                    commandType: System.Data.CommandType.StoredProcedure);
            });
        }

        /// <summary>
        /// execute non query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <param name="connstr"></param>
        public void ExecuteNonQuery(string query, object param = null, string connstr = null)
        {
            ExecuteCommand(connstr ?? _connStr, conn =>
            {
                conn.Execute(query, param);
            });
        }

        /// <summary>
        /// execute nonquery sql server
        /// </summary>
        /// <param name="query"></param>
        /// <param name="param"></param
        /// <param name="connstr"></param>
        public void ExecuteNonQuerySQLServer(string query, object param = null, string connstr = null)
        {
            ExecuteCommandSqlServer(connstr ?? _connStr, conn =>
            {
                conn.Execute(query, param);
            });
        }

        public System.Data.DataTable GetDataTable(string query, string connstr = null)
        {
            using (var conn = new MySqlConnection(connstr ?? _connStr))
            {
                conn.Open();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Load(conn.ExecuteReader(query));
                return dt;
            }
        }

        public System.Data.DataTable GetDataTableSQLSERVER(string query, string connstr = null)
        {
            using (var conn = new SqlConnection(connstr ?? _connStr))
            {
                conn.Open();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Load(conn.ExecuteReader(query));
                return dt;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public System.Data.DataTable GetTableSchema(string query, string connstr = null)
        {
            using (var conn = new MySqlConnection(connstr ?? _connStr))
            {
                conn.Open();

                return conn.ExecuteReader(query).GetSchemaTable();
            }
        }


        public System.Data.DataTable GetTableSchemaSQLServer(string query, string connstr = null)
        {
            using (var conn = new SqlConnection(connstr ?? _connStr))
            {
                conn.Open();

                return conn.ExecuteReader(query).GetSchemaTable();
            }
        }
        /// <summary>
        ///
        /// 
        /// </summary>
        /// <param name="connstr"></param>
        /// <returns></returns>
        public System.Data.DataTable GetDBSchema(string connstr = null)
        {
            using (var conn = new MySqlConnection(connstr ?? _connStr))
            {
                conn.Open();

                return conn.GetSchema("Tables");
            }
        }

        public System.Data.DataTable GetDataTableLayout(string tableName, string connectionString = null)
        {
            System.Data.DataTable table = new System.Data.DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString ?? _connStr))
            {
                connection.Open();
                // Select * is not a good thing, but in this cases is is very usefull to make the code dynamic/reusable 
                // We get the tabel layout for our DataTable
                string query = $"SELECT * FROM `" + tableName + "` limit 0";
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                {
                    adapter.Fill(table);
                };
            }

            return table;
        }

        public string CreateMYSQLTABLE(string tableName, System.Data.DataTable table)
        {
            string sqlsc = "CREATE TABLE `" + tableName + "`(";
            for (int i = 0; i < table.Columns.Count; i++)
            {
                sqlsc += "\n `" + table.Columns[i].ColumnName + "` ";
                string columnType = table.Columns[i].DataType.ToString();
                switch (columnType)
                {
                    case "System.Int32":
                        sqlsc += " int ";
                        break;
                    case "System.Int64":
                        sqlsc += " bigint ";
                        break;
                    case "System.Int16":
                        sqlsc += " smallint";
                        break;
                    case "System.Byte":
                        sqlsc += " tinyint";
                        break;
                    case "System.Decimal":
                        sqlsc += " decimal ";
                        break;
                    case "System.DateTime":
                        sqlsc += " datetime ";
                        break;
                    case "System.Boolean":
                        sqlsc += " bool ";
                        break;
                    case "System.String":
                    default:
                        if (table.Columns[i].MaxLength == -1 || table.Columns[i].MaxLength > 20000)
                        {
                            sqlsc += " MEDIUMTEXT ";
                        }
                        else
                        {
                            sqlsc += string.Format(" varchar({0}) ", table.Columns[i].MaxLength);
                        }
                        break;
                }
                sqlsc += ",";
            }
            return sqlsc.Substring(0, sqlsc.Length - 1) + "\n)";
        }

        public static string CreateSQLTABLE(string tableName, System.Data.DataTable table)
        {
            string sqlsc = "CREATE TABLE [" + tableName + "] (";
            for (int i = 0; i < table.Columns.Count; i++)
            {
                sqlsc += "\n [" + table.Columns[i].ColumnName + "] ";
                string columnType = table.Columns[i].DataType.ToString();
                switch (columnType)
                {
                    case "System.Int32":
                        sqlsc += " int ";
                        break;
                    case "System.Int64":
                        sqlsc += " bigint ";
                        break;
                    case "System.Int16":
                        sqlsc += " smallint";
                        break;
                    case "System.Byte":
                        sqlsc += " tinyint";
                        break;
                    case "System.Decimal":
                        sqlsc += " decimal ";
                        break;
                    case "System.DateTime":
                        sqlsc += " datetime ";
                        break;
                    case "System.String":
                    default:
                        sqlsc += string.Format(" nvarchar({0}) ", table.Columns[i].MaxLength == -1 ? "max" : table.Columns[i].MaxLength.ToString());
                        break;
                }
                if (table.Columns[i].AutoIncrement)
                    sqlsc += " IDENTITY(" + table.Columns[i].AutoIncrementSeed.ToString() + "," + table.Columns[i].AutoIncrementStep.ToString() + ") ";
                if (!table.Columns[i].AllowDBNull)
                    sqlsc += " NOT NULL ";
                sqlsc += ",";
            }
            return sqlsc.Substring(0, sqlsc.Length - 1) + "\n)";
        }

        /// <summary>
        /// Execute Command return none
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="task"></param>
        private void ExecuteCommand(string connStr, Action<MySqlConnection> task)
        {
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                task(conn);
            }
        }

        #region helper
        /// <summary>
        /// Execute Command return none
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="task"></param>
        private void ExecuteCommandSqlServer(string connStr, Action<SqlConnection> task)
        {
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                task(conn);
            }
        }

        /// <summary>
        /// Execute command return data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connStr"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        private T ExecuteCommand<T>(string connStr, Func<MySqlConnection, T> task)
        {
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                return task(conn);
            }
        }
        /// <summary>
        /// execute sqlserver query return data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connStr"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        private T ExecuteCommandSqlServer<T>(string connStr, Func<SqlConnection, T> task)
        {
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();

                return task(conn);
            }
        }

        /// <summary>
        /// return name of table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public string TableName<T>()
        {
            if (SqlMapperExtensions.TableNameMapper != null)
                return SqlMapperExtensions.TableNameMapper(typeof(T));
            string getTableName = "GetTableName";
            MethodInfo getTableNameMethod = typeof(SqlMapperExtensions).GetMethod(getTableName, BindingFlags.NonPublic | BindingFlags.Static);

            if (getTableNameMethod == null)
                throw new ArgumentOutOfRangeException($"Method '{getTableName}' is not found in '{nameof(SqlMapperExtensions)}' class.");
            var tableName = getTableNameMethod.Invoke(null, new object[] { typeof(T) }) as string;
            return getTableNameMethod.Invoke(null, new object[] { typeof(T) }) as string;
        }
        #endregion
    }
}
