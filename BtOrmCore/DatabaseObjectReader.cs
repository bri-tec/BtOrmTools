using Loggers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BtOrmCore
{
    public class DatabaseObjectReader : IDatabaseObjectReader
    {
        IDatabaseConnection _connection;
        ILogger? _logger;
        /// <summary>
        /// The DatabaseConnection object must be passed at construction.
        /// </summary>
        /// <param name="databaseConnection"></param>
        public DatabaseObjectReader(IDatabaseConnection databaseConnection, ILogger? logger = null)
        {
            _connection = databaseConnection;
            _logger = logger;
        }

        public bool Read(object model)
        {
            Type type = model.GetType();
            PropertyInfo idProperty = null;

            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                var attributes = propertyInfo.GetCustomAttribute<DatabaseAttributes>();
                if (attributes != null)
                {
                    idProperty = propertyInfo;
                    break;
                }                
            }             
            
            if (idProperty != null)
            {
                try
                {
                    DataTable returnedObjects = _connection.GetSqlConnection().SelectToDataTable($"SELECT * FROM {type.Name} WHERE {idProperty.Name}={idProperty.GetValue(model).ToString()}", true, WriteLog);

                    if (returnedObjects.Rows.Count == 1)
                    {
                        foreach (DataColumn col in returnedObjects.Columns)
                        {
                            PropertyInfo colProp = type.GetProperty(col.ColumnName);

                            if (colProp != null)
                            {
                                colProp.SetValue(model, returnedObjects.Rows[0][col.ColumnName]);
                            }
                        }

                        return true;
                    }
                    else
                    {
                        return false; //not found
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }
            }
            else
            {
                throw new Exception("No ID Property Found!");
            }

             
        }

        public T? Read<T>(object id) where T : class, new()
        {
            T model = new T();
            Type type = model.GetType();

            PropertyInfo idProperty = null;

            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                var attributes = propertyInfo.GetCustomAttribute<DatabaseAttributes>();
                if (attributes != null)
                {
                    idProperty = propertyInfo;
                    break;
                }
            }

            if (idProperty != null)
            {
                try
                {
                    DataTable returnedObjects = _connection.GetSqlConnection().SelectToDataTable($"SELECT * FROM {type.Name} WHERE {idProperty.Name}={id.ToString()}", true, WriteLog);

                    if (returnedObjects.Rows.Count == 1)
                    {
                        foreach (DataColumn col in returnedObjects.Columns)
                        {
                            PropertyInfo colProp = type.GetProperty(col.ColumnName);

                            if (colProp != null)
                            {
                                colProp.SetValue(model, returnedObjects.Rows[0][col.ColumnName]);
                            }
                        }

                        return model;
                    }
                    else
                    {
                        return null; //not found
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }
            }
            else
            {
                throw new Exception("No ID Property Found!");
            }

        }


        public bool Read(object model, object id)
        {
            Type type = model.GetType();
            PropertyInfo idProperty = null;

            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                var attributes = propertyInfo.GetCustomAttribute<DatabaseAttributes>();
                if (attributes != null)
                {
                    idProperty = propertyInfo;
                    break;
                }
            }

            if (idProperty != null)
            {
                try
                {
                    DataTable returnedObjects = _connection.GetSqlConnection().SelectToDataTable($"SELECT * FROM {type.Name} WHERE {idProperty.Name}={id.ToString()}", true, WriteLog);

                    if (returnedObjects.Rows.Count == 1)
                    {
                        foreach (DataColumn col in returnedObjects.Columns)
                        {
                            PropertyInfo colProp = type.GetProperty(col.ColumnName);

                            if (colProp != null)
                            {
                                colProp.SetValue(model, returnedObjects.Rows[0][col.ColumnName]);
                            }
                        }

                        return true;
                    }
                    else
                    {
                        return false; //not found
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }
            }
            else
            {
                throw new Exception("No ID Property Found!");
            }

        }

        public void WriteLog(string message, object source)
        {
            if (_logger != null)
                _logger.Log(DateTime.Now, message, source);
        }
    }
}
