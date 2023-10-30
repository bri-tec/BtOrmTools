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
    public class DatabaseObjectWriter : IDatabaseObjectWriter
    {
        IDatabaseConnection _connection;     
        ILogger? _logger;


        /// <summary>
        /// Allows objects to be written to the database.
        /// An object with an ID=0 calls Insert.
        /// Objects with an ID set calls Update.
        /// The DatabaseConnection object must be passed at construction.
        /// </summary>
        /// <param name="databaseConnection"></param>
        public DatabaseObjectWriter(IDatabaseConnection databaseConnection, ILogger? logger = null) 
        {
            _connection = databaseConnection;
            _logger = logger;
        }

        public int Write(object model)
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
                if ((int)idProperty.GetValue(model) == 0)
                {
                    int newID = Insert(model);
                    idProperty.SetValue(model, newID);
                    return newID;
                }
                else
                    Update(model);
            }
            else
            {
                throw new Exception("No ID Property Found!");
            }

            return 0;
        }

        private string GetFieldEncapsulation(PropertyInfo propertyInfo, object model)
        {
            if (propertyInfo.PropertyType == typeof(string) || propertyInfo.PropertyType == typeof(bool))
                return "'" + propertyInfo.GetValue(model) + "'";
            else if (propertyInfo.PropertyType == typeof(DateTime))
                return "'" + propertyInfo.GetValue(model).ToString().Replace("'","''") + "'";
            else
                return propertyInfo.GetValue(model).ToString();             
        }

        private bool Update(object model)
        {
            Type type = model.GetType();
            string sql = $"UPDATE {type.Name} SET ";
            StringBuilder updates = new ("");
            string whereClause = "";

            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                var attributes = propertyInfo.GetCustomAttribute<DatabaseAttributes>();
                if (attributes != null)
                {
                    whereClause = $"WHERE {propertyInfo.Name} = {GetFieldEncapsulation(propertyInfo, model)}";
                }
                else
                {
                    updates.Append(updates.Length ==0 ? propertyInfo.Name + "=" + GetFieldEncapsulation(propertyInfo, model) + " " : "," + propertyInfo.Name + "=" + GetFieldEncapsulation(propertyInfo, model) + " ");
                }
            }

            if (whereClause != "")
            {
                try
                {
                    int result = _connection.GetSqlConnection().ExecuteNonQuery(sql.ToString() + updates + whereClause, true, WriteLog);
                    if (result == 1)
                        return true;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }
            }

            return false;
        }

        private int Insert(object model)
        {
            Type type = model.GetType();
            StringBuilder sql = new StringBuilder($"INSERT INTO {type.Name} ");
            StringBuilder valuesString = new StringBuilder();
            StringBuilder fieldsString = new StringBuilder();
            PropertyInfo? idPropertyInfo = null;

            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                
                var attributes = propertyInfo.GetCustomAttribute<DatabaseAttributes>();
                if (attributes == null)
                {                 
                    if (valuesString.Length == 0)
                    {
                        fieldsString.Append(propertyInfo.Name);
                        valuesString.Append(GetFieldEncapsulation(propertyInfo, model));
                    }
                    else
                    {
                        fieldsString.Append("," + propertyInfo.Name);
                        valuesString.Append("," + GetFieldEncapsulation(propertyInfo, model));
                    }
                }
                else
                {
                    idPropertyInfo = propertyInfo;
                }
            }

            if (idPropertyInfo != null)
            {
                try
                {
                    object insertedID = _connection.GetSqlConnection().ExecuteScalar(sql.ToString() + "(" + fieldsString.ToString() + $") OUTPUT INSERTED.{idPropertyInfo.Name} VALUES (" + valuesString.ToString() + ")", true, WriteLog);
                    if (insertedID != null)
                        return (int)insertedID;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }
            }

            return 0;
        }

        public void WriteLog(string message, object source)
        {
            if (_logger != null)
                _logger.Log(DateTime.Now, message, source);
        }
    }
}
