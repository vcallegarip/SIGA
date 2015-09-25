//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data;
//using System.Diagnostics;
//using System.Data.SqlClient;
//using System.Xml;
//using System.Configuration;
//using System.Text.RegularExpressions;

//namespace RegistroIntegral_BLL.Helpers
//{
//    public static class SqlHelper

//    {

//        /// <summary> this enum is used to indicate whether the connection was provided by the caller, or created by SqlHelper, so that we can set the appropriate CommandBehavior when calling ExecuteReader() 
//        /// </summary>

//        public enum SqlConnectionOwnership

//        {

//            Internal,//Connection is owned and managed by SqlHelper
//            External//Connection is owned and managed by the caller

//        }


//        #region Data Members


//        private static Dictionary<string, string> _connectionStringOverrides;

//        //Singleton Property

//        private static Dictionary<string, string> ConnectionStringOverrides

//        {

//            get

//            {

//                if (_connectionStringOverrides == null) { _connectionStringOverrides = new Dictionary<string, string>(); }

//                return _connectionStringOverrides;

//            }

//        }


//        #endregion

//        #region Utility methods


//        /// <summary> In a case where configuration is missing or incomplete (eg a testing project or development project) this method adds the connection string to the operative local collection so that it can be retrieved in the normal process

//        /// </summary>

//        public static void AddConnectionStringOverride(string name, string connectionString)

//        {

//            name = name.ToLower().Trim();


//            if (ConnectionStringOverrides.ContainsKey(name))

//            {

//                ConnectionStringOverrides[name] = connectionString;

//            }

//            else

//            {

//                ConnectionStringOverrides.Add(name, connectionString);

//            }

//        }


//        /// <summary> In a case where configuration is missing or incomplete (eg a testing project or development project) this method allows for management of the connection string within the operative local collection

//        /// </summary>

//        public static void RemoveConnectionStringOverride(string name)

//        {

//            name = name.ToLower().Trim();

//            if (ConnectionStringOverrides.ContainsKey(name))

//            {

//                ConnectionStringOverrides.Remove(name);

//            }

//        }


//        /// <summary> This function returns an XML string by wrapping the StringWriter object and the DataSet.WriteXml() method, also removing some default XML markup

//        /// </summary>

//        public static string GetXmlOfDataset(System.Data.DataSet set)

//        {

//            if (set == null) { return null; }


//            DataTable table0 = set.Tables[0];

//            table0.TableName = "r";


//            //foreach (DataRow row in table0.Rows) 

//            //{

//            //    foreach (DataColumn c in table0.Columns)

//            //    {

//            //        if (row.IsNull(c))

//            //        {


//            //               // if(row.GetType = null ){}

//            //            //(row.GetType)

//            //            switch (c) {

//            //                case 1:

//            //                    row[c] = string.Empty;

//            //                    break;

//            //                case 2:

//            //                    row[c] = string.Empty;

//            //                    break;

//            //                default:

//            //                    row[c] = string.Empty;

//            //                    break;

//            //            }


//            //        };

//            //    }

//            //}

            


//            using (var writer = new System.IO.StringWriter())

//            {

//                table0.WriteXml(writer, System.Data.XmlWriteMode.IgnoreSchema);

                

//                string result = writer.ToString();


//                result = result.Substring(12,result.Length - 12 -13);


//                result = "<table0>" + result + "</table0>";


//                return result.Replace(" xml:space=\"preserve\"", "");

//            }

//        }


 

 

//        /// <summary>  Retrieves a connection string by its name, leveraging a local collection of overrides or else the native configuration 

//        /// </summary>

//        private static SqlConnection GetConnection(string connectionStringName)

//        {


//            string connectionString = null;

//            connectionStringName = connectionStringName.ToLower().Trim();


//            //check for overrides (eg from Console Application that has no Web.config

//            if (ConnectionStringOverrides.ContainsKey(connectionStringName))

//            {

//                connectionString = ConnectionStringOverrides[connectionStringName];

//            }


//            //check for Web.config connection

//            if (connectionString.IsEmpty())

//            {

//                connectionString = Convert.ToString(ConfigurationManager.ConnectionStrings[connectionStringName]);

//            }


//            return new SqlConnection(connectionString);


//        }


 

//        // This method is used to attach array of SqlParameters to a SqlCommand.

//        // This method will assign a value of DbNull to any parameter with a direction of

//        // InputOutput and a value of null.  

//        // This behavior will prevent default values from being used, but

//        // this will be the less common case than an intended pure output parameter (derived as InputOutput)

//        // where the user provided no input value.

//        // Parameters:

//        // -command - The command to which the parameters will be added

//        // -commandParameters - an array of SqlParameters tho be added to command

//        private static void AttachParameters(SqlCommand command, IEnumerable<SqlParameter> parameters)

//        {

//            //TraceCommand(command, parameters);


//            foreach (SqlParameter param in parameters)

//            {

//                if (param == null)

//                {

//                    throw new NullReferenceException("A Null SQL Parameter was sent to " + command.CommandText);

//                }

//                //check for derived output value with no value assigned

//                if (param.Direction == ParameterDirection.InputOutput & param.Value == null)

//                {

//                    param.Value = null; // NOTE! this looks like redundant code because the param.Value is already null, perhaps this was intended to set to  DbNull.Value ?? ~DavB (unchanged for now)

//                }

//                command.Parameters.Add(param);

//            }

//        }


 

//        /// <summary> This method opens (if necessary) and assigns a connection, transaction, command type and parameters  to the provided command.

//        /// </summary>

//        /// <param name="command">the SqlCommand to be prepared</param>

//        /// <param name="connection">a valid SqlConnection, on which to execute this command</param>

//        /// <param name="transaction">pass null or a valid SqlTransaction</param>

//        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>

//        /// <param name="commandText">the stored procedure name or T-SQL command</param>

//        /// <param name="parameters">an array, List<>, etc. of SqlParameters to be associated with the command or 'null' if no parameters are required</param>

//        private static SqlCommand PrepareCommand(

//            SqlConnection connection

//            , SqlTransaction transaction

//            , CommandType commandType

//            , string commandText

//            , IEnumerable<SqlParameter> parameters)

//        {

//            if (connection == null)

//            {

//                throw new ArgumentException("Null connection while attempting to access database.");

//            }


//            //if the provided connection is not open, we will open it

//            if (connection.State != ConnectionState.Open)

//            {

//                connection.Open();

//            }


//            SqlCommand command = new System.Data.SqlClient.SqlCommand();


//            //associate the connection with the command

//            command.Connection = connection;


//            //set the command text (stored procedure name or SQL statement)

//            command.CommandText = commandText;


//            //if we were provided a transaction, assign it.

//            if ((transaction != null))

//            {

//                command.Transaction = transaction;

//            }


//            //set the command type

//            command.CommandType = commandType;


//            //attach the command parameters if they are provided

//            if (parameters != null)

//            {

//                AttachParameters(command, parameters);

//            }


//            return command;

//        }


//        /// <summary>  This method validates that a connection string name is in the right format. This is added since previous code passed the connection sstring itself instead of jsut it's name

//        /// </summary>

//        private static void ValidateConnectionStringName(string connectionStringName)

//        {

//            if (string.IsNullOrEmpty(connectionStringName))

//            {

//                throw new ArgumentException("A connection string name is expected but missing");

//            }


//            if (!Regex.IsMatch(connectionStringName, @"^\w+$"))  // containing nothing but word characters (one or more)    eg [a=zA-Z0-9_] 

//            {

//                throw new ArgumentException("A connection string name should contain only word characters");

//            }

//        }


//        /// <summary>

//        /// Removes all line breaks in string fields of a table

//        /// </summary>

//        public static DataTable RemoveDataTableLineBreaks(DataTable dt)

//        {

//            for (int r = 0; r < dt.Rows.Count; r++)

//            {

//                for (int c = 0; c < dt.Columns.Count; c++)

//                {

//                    Type colType = dt.Columns[c].DataType;

//                    if (colType == typeof(System.String))

//                    {

//                        if (dt.Rows[r][c] != DBNull.Value)

//                        {


//                            dt.Rows[r][c] = Regex.Replace(dt.Rows[r][c].ToString(), @"\r\n?|\n", " ");


//                        }

//                    }

               

//                }

//            }


//            return dt;

//        }


//        /// <summary>

//        /// Use this method to remove any null values from a table. This is only important when using the XMLString format.

//        /// </summary>

//        public static DataTable RemoveDataTableNulls(DataTable dt)

//        {

//            for (int r = 0; r < dt.Rows.Count; r++)

//            {

//                for (int c = 0; c < dt.Columns.Count; c++)

//                {

//                    if (dt.Rows[r][c] == DBNull.Value)

//                    {

//                        //Type colType = dt.Columns[c].DataType;

//                        //dt.Columns[c].ReadOnly = false;

//                        dt.Rows[r][c] = GetEmptyValueForColumn(dt.Columns[c]);


//                        //if (colType == typeof(System.String))

//                        //{

//                        //    dt.Rows[r][c] = "";

//                        //}

//                        //else if (colType == typeof(System.DateTime))

//                        //{

//                        //    dt.Rows[r][c] = "";

//                        //}

//                        //else if (colType == typeof(System.Boolean))

//                        //{

//                        //    dt.Rows[r][c] = false;

//                        //}

//                        //else if (colType == typeof(System.Decimal))

//                        //{

//                        //    dt.Rows[r][c] = 0;

//                        //}

//                        //else if (colType == typeof(System.Single))

//                        //{

//                        //    dt.Rows[r][c] = 0;

//                        //}

//                        //else if (colType == typeof(System.Double))

//                        //{

//                        //    dt.Rows[r][c] = 0;

//                        //}

//                        //else if (colType == typeof(System.Int32))

//                        //{

//                        //    dt.Rows[r][c] = 0;

//                        //}

//                        //else if (colType == typeof(System.Int16))

//                        //{

//                        //    dt.Rows[r][c] = 0;

//                        //}

//                        //else if (colType == typeof(System.Byte))

//                        //{

//                        //    dt.Rows[r][c] = 0;

//                        //}

//                        //else if (colType == typeof(System.Guid))

//                        //{

//                        //    dt.Rows[r][c] = Guid.Empty.ToString();

//                        //}

//                        //else

//                        //{

//                        //    dt.Rows[r][c] = "";

//                        //}

//                    }

//                }

//            }


//            return dt;

//        }


//        /// <summary>

//        /// Use this method if you are unsure if your datatable has nulls

//        /// </summary>

//        public static string ConvertDataTableToXML(DataTable dt, string rootNodeName, string rowNodeName)

//        {

//            DataSet ds = new DataSet();

//            ds.DataSetName = rootNodeName;

//            dt.TableName = rowNodeName;

//            ds.Tables.Add(dt);

//            return ds.GetXml();

//        }


//        public static  object GetEmptyValueForColumn(DataColumn dc)

//        {

//            Type colType = dc.DataType;

//            //dc.ReadOnly = false;


//            if (colType == typeof(System.String))

//            {

//               return "";

//            }

//            else if (colType == typeof(System.DateTime))

//            {

//                return "";

//            }

//            else if (colType == typeof(System.Boolean))

//            {

//                return false;

//            }

//            else if (colType == typeof(System.Decimal))

//            {

//                return 0;

//            }

//            else if (colType == typeof(System.Single))

//            {

//                return 0;

//            }

//            else if (colType == typeof(System.Double))

//            {

//                return 0;

//            }

//            else if (colType == typeof(System.Int32))

//            {

//                return 0;

//            }

//            else if (colType == typeof(System.Int16))

//            {

//                return 0;

//            }

//            else if (colType == typeof(System.Byte))

//            {

//                return 0;

//            }

//            else if (colType == typeof(System.Guid))

//            {

//                return Guid.Empty.ToString();

//            }

//            else

//            {

//                return "";

//            }

//        }


 

//        #endregion

//        #region EXECUTION Methods


//        /// <summary> Returns a integer of rows affected from the supplied Sql command (note that some SPs turn RowCount off)

//        /// </summary>

//        /// <param name="connectionStringName">The Name of the Connection String in the application's configuration</param>

//        /// <param name="parameters"> an Array/List<>/etc. of 0 or more SqlParameter objects passed as needed</param>

//        /// <param name="commandText">The Stored Procedure Name (default) or inline SQL String (requires that you specify the command type)</param>

//        /// <param name="commandType">null defaults to "StoredProcedure", or else specify "Text" according to the type of command being sent. </param>

//        /// <returns></returns>

//        public static int ExecuteNonQuery(string connectionStringName, IEnumerable<SqlParameter> parameters, string commandText, CommandType commandType = CommandType.StoredProcedure)

//        {

//            ValidateConnectionStringName(connectionStringName);


//            using (SqlConnection connection = GetConnection(connectionStringName))

//            {

//                return ExecuteNonQuery(connection, parameters, commandText, commandType);

//            }

//        }


//        /// <summary> Returns a integer of rows affected from the supplied Sql command (note that some SPs turn RowCount off)

//        /// This overload allows passing the same Connection/Transaction if passing multiple requests. This requires handling the disposal of the connection and the Commit/Rollback of the transaction

//        /// </summary>

//        /// <param name="connection">A SqlConnection Object NOTE!! THE CALLING FUNCTION MUST DISPOSE THIS OBJECT! eg with a 'using' statement. (The connection will be opened if not alreaady)</param>

//        /// <param name="parameters"> an Array/List<>/etc. of 0 or more SqlParameter objects passed as needed</param>

//        /// <param name="commandText">The Stored Procedure Name (default) or inline SQL String (requires that you specify the command type)</param>

//        /// <param name="commandType">null defaults to "StoredProcedure", or else specify "Text" according to the type of command being sent. </param>

//        /// <param name="transaction">Pass in a SqlTransaction object if containing multiple DB calls together. NOTE!!! The caller must call COMMIT/ROLLBACK depending on success/failure. </param>

//        /// <returns></returns>

//        public static int ExecuteNonQuery(SqlConnection connection, IEnumerable<SqlParameter> parameters, string commandText, CommandType commandType = CommandType.StoredProcedure, SqlTransaction transaction = null)

//        {

//            //sample usage: see also http://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqltransaction.aspx

//            //

//            //          int resultCount = 0;

//            //

//            //          using (SqlConnection connection = new SqlConnection(connectionString))

//            //          {

//            //              connection.Open();

//            //              SqlTransaction transaction = connection.BeginTransaction("SampleTransaction");

//            //          

//            //              try

//            //              {

//            // -->call this method here --> resultCount = ExecuteScalar(connection, parameters, "dbo.spDoWork", null, transaction)

//            //          

//            //                  transaction.Commit();

//            //              }

//            //              catch (Exception ex)

//            //              {

//            //                  try // Attempt to roll back the transaction. 

//            //                  {

//            //                      transaction.Rollback();

//            //                  }

//            //                  catch (Exception ex2)

//            //                  {

//            //                      // This catch block will handle any errors that may have occurred 

//            //                      // on the server that would cause the rollback to fail, such as 

//            //                      // a closed connection.

//            //                  }

//            //              }

//            //          }


 

//            SqlCommand cmd = PrepareCommand(connection, transaction, commandType, commandText, parameters);


//            //finally, execute the command.

//            int result = cmd.ExecuteNonQuery();


//            return result;

//        }


 

 

//        /// <summary> Returns a DataSet from the supplied Sql command (possibly multiple tables returned)

//        /// </summary>

//        /// <param name="connectionStringName">The Name of the Connection String in the application's configuration</param>

//        /// <param name="parameters"> an Array/List<>/etc. of 0 or more SqlParameter objects passed as needed</param>

//        /// <param name="commandText">The Stored Procedure Name (default) or inline SQL String (requires that you specify the command type)</param>

//        /// <param name="commandType">null defaults to "StoredProcedure", or else specify "Text" according to the type of command being sent. </param>

//        /// <returns></returns>

//        public static DataSet ExecuteDataset(

//            string connectionStringName

//            , IEnumerable<SqlParameter> parameters

//            , string commandText

//            , CommandType commandType = CommandType.StoredProcedure)

//        {

//            ValidateConnectionStringName(connectionStringName);


//            using (SqlConnection connection = GetConnection(connectionStringName))

//            {

//                return ExecuteDataset(connection, parameters, commandText, commandType);

//            }

//        }


//        /// <summary> Returns a DataSet from the supplied Sql command (possibly multiple tables returned) 

//        /// This overload allows passing the same Connection/Transaction if passing multiple requests. This requires handling the disposal of the connection and the Commit/Rollback of the transaction

//        /// </summary>

//        /// <param name="connection">A SqlConnection Object NOTE!! THE CALLING FUNCTION MUST DISPOSE THIS OBJECT! eg with a 'using' statement. (The connection will be opened if not alreaady)</param>

//        /// <param name="parameters"> an Array/List<>/etc. of 0 or more SqlParameter objects passed as needed</param>

//        /// <param name="commandText">The Stored Procedure Name (default) or inline SQL String (requires that you specify the command type)</param>

//        /// <param name="commandType">null defaults to "StoredProcedure", or else specify "Text" according to the type of command being sent. </param>

//        /// <param name="transaction">Pass in a SqlTransaction object if containing multiple DB calls together. NOTE!!! The caller must call COMMIT/ROLLBACK depending on success/failure. </param>

//        /// <returns></returns>

//        public static DataSet ExecuteDataset(

//            SqlConnection connection

//            , IEnumerable<SqlParameter> parameters

//            , string commandText

//            , CommandType commandType = CommandType.StoredProcedure

//            , SqlTransaction transaction = null)

//        {

//            //sample usage: see also http://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqltransaction.aspx

//            //

//            //          DataSet set = null;

//            //

//            //          using (SqlConnection connection = new SqlConnection(connectionString))

//            //          {

//            //              connection.Open();

//            //              SqlTransaction transaction = connection.BeginTransaction("SampleTransaction");

//            //          

//            //              try

//            //              {

//            // -->call this method here --> set = ExecuteDataset(connection, parameters, "dbo.spDoWork", null, transaction)

//            //          

//            //                  transaction.Commit();

//            //              }

//            //              catch (Exception ex)

//            //              {

//            //                  try // Attempt to roll back the transaction. 

//            //                  {

//            //                      transaction.Rollback();

//            //                  }

//            //                  catch (Exception ex2)

//            //                  {

//            //                      // This catch block will handle any errors that may have occurred 

//            //                      // on the server that would cause the rollback to fail, such as 

//            //                      // a closed connection.

//            //                  }

//            //              }

//            //          }


 

 

 

//            //create a command and prepare it for execution

//            SqlCommand command = PrepareCommand(connection, transaction, commandType, commandText, parameters);


//            SqlDataAdapter adapter = new SqlDataAdapter(command);


//            DataSet set = new DataSet();

//            //fill the DataSet using default values for DataTable names, etc.

//            adapter.Fill(set);


//            return set;

//        }


 

//        /// <summary> Returns a SqlDataReader from the supplied Sql command

//        /// </summary>

//        /// <param name="connectionStringName">The Name of the Connection String in the application's configuration</param>

//        /// <param name="parameters"> an Array/List<>/etc. of 0 or more SqlParameter objects passed as needed</param>

//        /// <param name="commandText">The Stored Procedure Name (default) or inline SQL String (requires that you specify the command type)</param>

//        /// <param name="commandType">null defaults to "StoredProcedure", or else specify "Text" according to the type of command being sent. </param>

//        public static SqlDataReader ExecuteReader(

//            string connectionStringName

//            , IEnumerable<SqlParameter> parameters

//            , string commandText

//            , CommandType commandType = CommandType.StoredProcedure)

//        {

//            ValidateConnectionStringName(connectionStringName);


//            using (SqlConnection connection = GetConnection(connectionStringName))

//            {

//                return ExecuteReader(connection, parameters, commandText, commandType, null, SqlConnectionOwnership.Internal); // always internal when using the 'connectinoStringName' ... causes immediate close of connection

//            }

//        }


//        /// <summary> Execute a SqlDataReader that returns a result set . 

//        /// This overload allows passing the same Connection/Transaction if passing multiple requests. This requires handling the disposal of the connection and the Commit/Rollback of the transaction

//        /// NOTE The connectionOwnership variable.

//        /// </summary>

//        /// <param name="connection">A SqlConnection Object NOTE!! THE CALLING FUNCTION MUST DISPOSE THIS OBJECT! eg with a 'using' statement. (The connection will be opened if not alreaady)</param>

//        /// <param name="parameters"> an Array/List<>/etc. of 0 or more SqlParameter objects passed as needed</param>

//        /// <param name="commandText">The Stored Procedure Name (default) or inline SQL String (requires that you specify the command type)</param>

//        /// <param name="commandType">null defaults to "StoredProcedure", or else specify "Text" according to the type of command being sent. </param>

//        /// <param name="transaction">Pass in a SqlTransaction object if containing multiple DB calls together. NOTE!!! The caller must call COMMIT/ROLLBACK depending on success/failure. </param>

//        /// <param name="connectionOwnership">Teh connectionOwnership variable is unique to the ExecuteReader function, telling the connection to close immediately or else stay open in case a different command is about to be passed in. Interal = close immediately, Externale = keep open</param>

//        /// <returns></returns>

//        public static SqlDataReader ExecuteReader(

//            SqlConnection connection

//            , IEnumerable<SqlParameter> parameters

//            , string commandText

//            , CommandType commandType = CommandType.StoredProcedure

//            , SqlTransaction transaction = null

//            , SqlConnectionOwnership connectionOwnership = SqlConnectionOwnership.External) //assume that  that the connectioni sexternal for this overload, unless explicitly set otherwise, so that connection remains open for multiple DB hits.

//        {

//            //sample usage: see also http://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqltransaction.aspx

//            //

//            //          SqlDataReader reader = null;

//            //

//            //          using (SqlConnection connection = new SqlConnection(connectionString))

//            //          {

//            //              connection.Open();

//            //              SqlTransaction transaction = connection.BeginTransaction("SampleTransaction");

//            //          

//            //              try

//            //              {

//            // -->call this method here --> reader = ExecuteReader(connection, parameters, "dbo.spDoWork", null, transaction)

//            //          

//            //                  transaction.Commit();

//            //              }

//            //              catch (Exception ex)

//            //              {

//            //                  try // Attempt to roll back the transaction. 

//            //                  {

//            //                      transaction.Rollback();

//            //                  }

//            //                  catch (Exception ex2)

//            //                  {

//            //                      // This catch block will handle any errors that may have occurred 

//            //                      // on the server that would cause the rollback to fail, such as 

//            //                      // a closed connection.

//            //                  }

//            //              }

//            //          }


 

//            //create a reader

//            SqlDataReader reader = null;


//            //create a command and prepare it for execution

//            SqlCommand command = PrepareCommand(connection, transaction, commandType, commandText, parameters);


//            // call ExecuteReader with the appropriate CommandBehavior

//            if (connectionOwnership == SqlConnectionOwnership.External)

//            {

//                reader = command.ExecuteReader();

//            }

//            else

//            {

//                reader = command.ExecuteReader(CommandBehavior.CloseConnection);

//            }


//            //detach the SqlParameters from the command object, so they can be used again

//            command.Parameters.Clear();


 

//            return reader;

//        }


 

//        /// <summary> Returns a The contents of the first column of the first row from the supplied Sql command. Cast the result objec tot he expected type

//        /// </summary>

//        /// <param name="connectionStringName">The Name of the Connection String in the application's configuration</param>

//        /// <param name="parameters"> an Array/List<>/etc. of 0 or more SqlParameter objects passed as needed</param>

//        /// <param name="commandText">The Stored Procedure Name (default) or inline SQL String (requires that you specify the command type)</param>

//        /// <param name="commandType">null defaults to "StoredProcedure", or else specify "Text" according to the type of command being sent. </param>

//        public static object ExecuteScalar(

//            string connectionStringName

//            , IEnumerable<SqlParameter> parameters

//            , string commandText

//            , CommandType commandType = CommandType.StoredProcedure)

//        {

//            ValidateConnectionStringName(connectionStringName);


//            using (SqlConnection connection = GetConnection(connectionStringName))

//            {

//                return ExecuteScalar(connection, parameters, commandText, commandType);

//            }

//        }


//        /// <summary> Returns a The contents of the first column of the first row from the supplied Sql command. Cast the result objec tot he expected type

//        /// This overload allows passing the same Connection/Transaction if passing multiple requests. This requires handling the disposal of the connection and the Commit/Rollback of the transaction

//        /// </summary>

//        /// <param name="connection">A SqlConnection Object NOTE!! THE CALLING FUNCTION MUST DISPOSE THIS OBJECT! eg with a 'using' statement. (The connection will be opened if not alreaady)</param>

//        /// <param name="parameters"> an Array/List<>/etc. of 0 or more SqlParameter objects passed as needed</param>

//        /// <param name="commandText">The Stored Procedure Name (default) or inline SQL String (requires that you specify the command type)</param>

//        /// <param name="commandType">null defaults to "StoredProcedure", or else specify "Text" according to the type of command being sent. </param>

//        /// <param name="transaction">Pass in a SqlTransaction object if containing multiple DB calls together. NOTE!!! The caller must call COMMIT/ROLLBACK depending on success/failure. </param>

//        public static object ExecuteScalar(

//            SqlConnection connection

//            , IEnumerable<SqlParameter> parameters

//            , string commandText

//            , CommandType commandType = CommandType.StoredProcedure

//            , SqlTransaction transaction = null)

//        {

//            //sample usage: see also http://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqltransaction.aspx

//            //

//            //          object result = null;

//            //

//            //          using (SqlConnection connection = new SqlConnection(connectionString))

//            //          {

//            //              connection.Open();

//            //              SqlTransaction transaction = connection.BeginTransaction("SampleTransaction");

//            //          

//            //              try

//            //              {

//            // -->call this method here --> result = ExecuteScalar(connection, parameters, "dbo.spDoWork", null, transaction)

//            //          

//            //                  transaction.Commit();

//            //              }

//            //              catch (Exception ex)

//            //              {

//            //                  try // Attempt to roll back the transaction. 

//            //                  {

//            //                      transaction.Rollback();

//            //                  }

//            //                  catch (Exception ex2)

//            //                  {

//            //                      // This catch block will handle any errors that may have occurred 

//            //                      // on the server that would cause the rollback to fail, such as 

//            //                      // a closed connection.

//            //                  }

//            //              }

//            //          }


//            //create a command and prepare it for execution

//            SqlCommand cmd = PrepareCommand(connection, transaction, commandType, commandText, parameters);


//            return cmd.ExecuteScalar(); //execute the command & return the first cell of the first row

//        }


 

 

//        /// <summary> Returns an XmlReader from the supplied Sql command

//        /// </summary>

//        /// <param name="connectionStringName">The Name of the Connection String in the application's configuration</param>

//        /// <param name="parameters"> an Array/List<>/etc. of 0 or more SqlParameter objects passed as needed</param>

//        /// <param name="commandText">The Stored Procedure Name (default) or inline SQL String (requires that you specify the command type)</param>

//        /// <param name="commandType">null defaults to "StoredProcedure", or else specify "Text" according to the type of command being sent. </param>

//        public static XmlReader ExecuteXmlReader(

//            string connectionStringName

//            , IEnumerable<SqlParameter> parameters

//            , string commandText

//            , CommandType commandType = CommandType.StoredProcedure)

//        {

//            ValidateConnectionStringName(connectionStringName);


//            using (SqlConnection connection = GetConnection(connectionStringName))

//            {

//                return ExecuteXmlReader(connection, parameters, commandText, commandType);

//            }

//        }


//        /// <summary> Returns an XmlReader from the supplied Sql command

//        /// This overload allows passing the same Connection/Transaction if passing multiple requests. This requires handling the disposal of the connection and the Commit/Rollback of the transaction

//        /// </summary>

//        /// <param name="connection">A SqlConnection Object NOTE!! THE CALLING FUNCTION MUST DISPOSE THIS OBJECT! eg with a 'using' statement. (The connection will be opened if not alreaady)</param>

//        /// <param name="parameters"> an Array/List<>/etc. of 0 or more SqlParameter objects passed as needed</param>

//        /// <param name="commandText">The Stored Procedure Name (default) or inline SQL String (requires that you specify the command type)</param>

//        /// <param name="commandType">null defaults to "StoredProcedure", or else specify "Text" according to the type of command being sent. </param>

//        /// <param name="transaction">Pass in a SqlTransaction object if containing multiple DB calls together. NOTE!!! The caller must call COMMIT/ROLLBACK depending on success/failure. </param>

//        public static XmlReader ExecuteXmlReader(

//            SqlConnection connection

//            , IEnumerable<SqlParameter> parameters

//            , string commandText

//            , CommandType commandType = CommandType.StoredProcedure

//            , SqlTransaction transaction = null)

//        {

//            //sample usage: see also http://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqltransaction.aspx

//            //

//            //          XmlReader reader = 0;

//            //

//            //          using (SqlConnection connection = new SqlConnection(connectionString))

//            //          {

//            //              connection.Open();

//            //              SqlTransaction transaction = connection.BeginTransaction("SampleTransaction");

//            //          

//            //              try

//            //              {

//            // -->call this method here --> reader = ExecuteXmlReader(connection, parameters, "dbo.spDoWork", null, transaction)

//            //          

//            //                  transaction.Commit();

//            //              }

//            //              catch (Exception ex)

//            //              {

//            //                  try // Attempt to roll back the transaction. 

//            //                  {

//            //                      transaction.Rollback();

//            //                  }

//            //                  catch (Exception ex2)

//            //                  {

//            //                      // This catch block will handle any errors that may have occurred 

//            //                      // on the server that would cause the rollback to fail, such as 

//            //                      // a closed connection.

//            //                  }

//            //              }

//            //          }


//            //create a command and prepare it for execution

//            SqlCommand cmd = PrepareCommand(connection, transaction, commandType, commandText, parameters);


//            //create the DataAdapter & DataSet

//            return cmd.ExecuteXmlReader();


//        }


//        #endregion

//        #region Parameter Methods


//        public static System.Data.SqlClient.SqlParameter AddNewOutputParameter(List<SqlParameter> parameters, string parameterName, SqlDbType sqlDbType, int size = -1)

//        {

//            SqlParameter parameter = null;

//            if (size > -1)

//            {

//                parameter = new SqlParameter(parameterName, sqlDbType, size);

//            }

//            else

//            {

//                parameter = new SqlParameter(parameterName, sqlDbType);

//            }

//            parameter.Direction = ParameterDirection.Output;

//            parameters.Add(parameter);

//            return parameter;

//        }


//        #endregion

//        #region Extension Methods


 

//        //Extension Methods

//        /// <summary>  Gets a column value as string. If column is missing or NULL it returns the default. Optional bool indicates if empty strings should receive the default (by passing false, empty strings are replaced with the default)

//        /// </summary>

//        /// <param name="row">any DataRow object</param>

//        /// <param name="columnName">the name of the column inthe DataRow</param>

//        /// <param name="defaultIfNull">the default value in case the column is null or missing</param>

//        /// <param name="emptyStringsAllowed">pass false to force empty strings to get the default. true indicates that an empty string is a permitted return value.</param>

//        /// <returns></returns>

//        public static string GetStringOr(

//            this DataRow row // This is an extension method of of a DataRow object ... row.GetStringOr("asdf", "qwer")

//            , string columnName

//            , string defaultIfNull = "" // default is an empty string if no other string is specified

//            , bool emptyStringsAllowed = true)

//        {

//            //didn't pass original value by ref, since it may need to be assigned to a class property which cant be passed by ref


//            if (string.IsNullOrEmpty(columnName) || row.Table.Columns[columnName] == null)

//            {

//                return defaultIfNull;

//            }

//            //validate non-null column

//            string newValue = Convert.ToString(row[columnName]);

//            if (!emptyStringsAllowed && string.IsNullOrEmpty(newValue))

//            {

//                return defaultIfNull;

//            }

//            //validate whether empty string are allowed etc

//            return newValue;

//        }


//        /// <summary>  Gets a column value as integer. If column is missing or NULL it returns the default. Optional minvalue  that the result must be higher or else you will receive the default (by passing a minvalue of 0, a parse of -1 or -2 will be replaced with the default, but 0 will return ok)

//        /// </summary>

//        /// <param name="row">any DataRow object</param>

//        /// <param name="columnName">the name of the column inthe DataRow</param>

//        /// <param name="defaultIfNull">the default value in case the column is null or missing</param>

//        /// <param name="minimumAllowedValue">if supplied, the resulting int must be above this min value or else the default is returned (eg. 0 to prevent a return of -1)</param>

//        /// <returns></returns>

//        public static int GetIntOr(

//            this DataRow row // This is an extension method of of a DataRow object ... row.GetIntOr("asdf", -1)

//            , string columnName

//            , int defaultIfNull // do not set a default here, force the caller to choose one

//            , System.Nullable<int> minimumAllowedValue = null)

//        {

//            if (columnName.IsEmpty() || row.Table.Columns[columnName.Trim()] == null)

//            {

//                return defaultIfNull;

//            }

//            //validate non-null column

//            int resultInt = 0;


//            if (object.ReferenceEquals(row.Table.Columns[columnName].DataType, typeof(int)))

//            {

//                if (object.ReferenceEquals(row[columnName], DBNull.Value))

//                {

//                    return defaultIfNull;

//                }

//                //simple conversion

//                resultInt = Convert.ToInt32(row[columnName]);

//            }

//            else if (!int.TryParse(Convert.ToString(row[columnName]), out resultInt))

//            {

//                //We were unable to parse the string value to an int, so return default

//                return defaultIfNull;

//            }


//            //we have a parsed int


 

//            if (minimumAllowedValue != null && resultInt < minimumAllowedValue)

//            {

//                //Doesn't match minimum requirement (eg. must be above -1)

//                return defaultIfNull;

//            }


//            return resultInt;

//            //successful return

//        }


//        /// <summary>  Gets a column value as boolean. If column is missing or NULL it returns the default. If Column is not null, but also not a bit, it tries to convert to string and parse as bool using "1" "true" "t" "y" "yes" etc. Optional optionalTrueString allows the caller to specify a string that should return TRUE if the record contains it. Likewise, the optionalFalseString.

//        /// </summary>

//        /// <param name="row">any DataRow object</param>

//        /// <param name="columnName">the name of the column inthe DataRow</param>

//        /// <param name="defaultIfNull">the default value in case the column is null or missing</param>

//        /// <param name="optionalTrueString">If supplied, this will cause a cell value matching this string to return as true. Built-in alternates exist, such as "true" "yes" "1" "t" "y"</param>

//        /// <param name="optionalFalseString">If supplied, this will cause a cell value matching this string to return as false. Built-in alternates exist, such as "false" "no" "0" "f" "n"</param>

//        /// <returns></returns>

//        public static bool GetBoolOr(

//            this DataRow row // This is an extension method of of a DataRow object ... row.GetBoolOr("asdf", false)

//            , string columnName

//            , bool defaultIfNull // do not set a default here, force the caller to choose one

//            , string optionalTrueString = null // see notes above.  eg. "IsMember" if the database stores strings that are meant to act like bools

//            , string optionalFalseString = null) // see notes above.  eg. "NotMember" if the database stores strings that are meant to act like bools

//        {

//            if (columnName.IsEmpty() || row.Table.Columns[columnName.Trim()] == null)

//            {

//                return defaultIfNull;

//            }

//            //validate non-null column


//            if (object.ReferenceEquals(row.Table.Columns[columnName].DataType, typeof(bool)))

//            {

//                if (object.ReferenceEquals(row[columnName], DBNull.Value))

//                {

//                    return defaultIfNull;

//                }

//                return Convert.ToBoolean(row[columnName]);

//            }


//            //not a standard bool column, so eval as string

//            string boolValue = Convert.ToString(row[columnName]).ToLower();


//            if (optionalFalseString != null && boolValue == optionalFalseString)

//            {

//                return false;

//            }

//            if (optionalTrueString != null && boolValue == optionalTrueString)

//            {

//                return true;

//            }


//            switch (boolValue) // already .ToLower() from above

//            {

//                case "true":

//                case "1":

//                case "t":

//                case "y":

//                case "yes":

//                    return true;

//                case "false":

//                case "0":

//                case "f":

//                case "n":

//                case "no":

//                    return false;

//                default:

//                    return defaultIfNull;

//            }

//        }


//        /// <summary>  Gets a column value as DateTime. If column is missing or NULL it returns the default. If datetime parsing fails, it returns the default.

//        /// </summary>

//        /// <param name="row">any DataRow object</param>

//        /// <param name="columnName">the name of the column inthe DataRow</param>

//        /// <param name="defaultIfNull">the default value in case the column is null or missing</param>

//        /// <returns></returns>

//        public static DateTime GetDateTimeOr(

//            this DataRow row // This is an extension method of of a DataRow object ... row.GetDateTimeOr("asdf", DateTime.Now)

//            , string columnName

//            , DateTime defaultIfNull) // do not set a default here, force the caller to choose one

//        {

//            if (columnName.IsEmpty() || row.Table.Columns[columnName] == null || object.ReferenceEquals(row[columnName], DBNull.Value))

//            {

//                return defaultIfNull;

//            }

//            //validate non-null column

//            DateTime resultDateTime = DateTime.MinValue;

//            if (DateTime.TryParse(Convert.ToString(row[columnName]), out resultDateTime))

//            {

//                return resultDateTime;

//            }

//            return defaultIfNull;

//        }


//        /// <summary>  Gets a column value as DateTime? (nullable DateTime). If column is missing or NULL this returns null. If datetime parsing fails, it returns null.

//        /// </summary>

//        /// <param name="row">any DataRow object</param>

//        /// <param name="columnName">the name of the column inthe DataRow</param>

//        /// <returns></returns>

//        public static DateTime? GetNullableDateTimeOr(

//            this DataRow row // This is an extension method of of a DataRow object ... row.GetDateTimeOr("asdf", DateTime.Now)

//            , string columnName) // do not set a default here, force the caller to choose one

//        {

//            if (columnName.IsEmpty() || row.Table.Columns[columnName] == null || object.ReferenceEquals(row[columnName], DBNull.Value))

//            {

//                return null;

//            }

//            //validate non-null column

//            DateTime resultDateTime = DateTime.MinValue;

//            if (DateTime.TryParse(Convert.ToString(row[columnName]), out resultDateTime))

//            {

//                return resultDateTime;

//            }

//            return null;

//        }


//        /// <summary>  Gets a column value as Decimal. If column is missing or NULL it returns the default. 

//        /// </summary>

//        /// <param name="row">any DataRow object</param>

//        /// <param name="columnName">the name of the column inthe DataRow</param>

//        /// <param name="defaultIfNull">the default value in case the column is null or missing</param>

//        /// <returns></returns>

//        public static decimal GetDecimalOr(

//            this DataRow row

//            , string columnName

//            , decimal defaultIfNull) // do not set a default here, force the caller to choose one

//        {

//            if (columnName.IsEmpty() || row.Table.Columns[columnName.Trim()] == null)

//            {

//                return defaultIfNull;

//            }

//            //validate non-null column

//            if (object.ReferenceEquals(row.Table.Columns[columnName].DataType, typeof(decimal)))

//            {

//                if (object.ReferenceEquals(row[columnName], DBNull.Value))

//                {

//                    return defaultIfNull;

//                }

//                //simple conversion, return

//                return Convert.ToDecimal(row[columnName]);

//            }


//            decimal resultDecimal = 0;

//            if (decimal.TryParse(Convert.ToString(row[columnName]), out resultDecimal))

//            {

//                //We were able to parse the string value to an int, so return it

//                return resultDecimal;

//            }


//            return defaultIfNull;

//            //parse failed, return default

//        }


//        #endregion


//    }

//}

 