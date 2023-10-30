using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BtOrmCore
{
    public static class BasicSQLFunctions
    {
        public delegate void LogWriterDel(string message, object source);

        /// <summary>
        /// Executes a non-query on an SQL connection and 
        /// returns the number of rows affected.
        /// </summary>
        /// <param name="conn">SQL Connection</param>
        /// <param name="sql">SQL to be executed</param>
        /// <param name="closeConnection">Whether to close the connection after execution (default is true).</param>
        /// <returns>The number of rows affected.</returns>
        /// <exception cref="Exception"></exception>
        public static int ExecuteNonQuery(this SqlConnection conn, string sql, bool closeConnection = true, LogWriterDel? logWriter = null)
        {
            int result = 0;

            try
            {
                if (conn == null)
                    throw new ArgumentNullException("SQL connection can't be null.");

                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery();

                if (logWriter != null)
                {
                    logWriter($"SQL Executed: {sql}", conn);
                }
            }
            catch (Exception e) 
            {
                if (logWriter != null)
                {
                    logWriter($"SQL Failed: {sql}", conn);
                }
                throw new Exception("Command not executed: " + e.Message);
            }
            finally
            {
                if (closeConnection && conn.State == System.Data.ConnectionState.Open)                    
                    conn.Close();                
            }

            return result;
        }

        /// <summary>
        /// Executes the supplied SQL query on the supplied connection and returns the first result.
        /// </summary>
        /// <param name="conn">SQL Connection</param>
        /// <param name="sql">SQL to be run.</param>
        /// <param name="closeConnection">Whether to close the connection after execution (default is true).</param>
        /// <returns>An object containing the first result of the query.</returns>
        /// <exception cref="Exception"></exception>
        public static object ExecuteScalar(this SqlConnection conn, string sql, bool closeConnection = true, LogWriterDel? logWriter = null)
        {
            object result = null;

            try
            {
                if (conn == null)
                    throw new ArgumentNullException("SQL connection can't be null.");

                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                result = cmd.ExecuteScalar();

                if (logWriter != null)
                {
                    logWriter($"SQL Executed: {sql}", conn);
                }
            }            
            catch (Exception e)
            {
                if (logWriter != null)
                {
                    logWriter($"SQL Failed: {sql}", conn);
                }
                throw new Exception("Command not executed: " + e.Message);
            }
            finally
            {
                if (closeConnection && conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Executes a select statement and returns the rulsts as a Datatable
        /// </summary>
        /// <param name="conn">SQL Connection</param>
        /// <param name="sql">The SQL Select statement</param>
        /// <param name="closeConnection">Whether to close the connection after execution (default is true).</param>
        /// <returns>A Datatable with the results of the select statement</returns>
        /// <exception cref="Exception"></exception>
        public static DataTable SelectToDataTable(this SqlConnection conn, string sql, bool closeConnection = true, LogWriterDel? logWriter = null)
        {
            DataTable result = new DataTable();

            try
            {
                if (conn == null)
                    throw new ArgumentNullException("SQL connection can't be null.");

                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }


                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn))
                {
                    sqlDataAdapter.Fill(result);

                    if (logWriter != null)
                    {
                        logWriter($"SQL Executed: {sql}", conn);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Command not executed: " + e.Message);
            }
            finally
            {
                if (closeConnection && conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }

            return result;
        }
    }    
}
