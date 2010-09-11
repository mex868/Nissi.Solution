using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Diagnostics;

namespace Nissi.DataAccess
{
    /// <summary>
    /// Classe base para as classes de acesso a dados. Essa classe possui os métodos necessários para a 
    /// conexão com o banco e execução de stored procedures no banco de dados. Essa classe não está associada
    /// a nenhum banco de dados específico.
    /// </summary>
    public class NissiBaseData
    {
        #region Atributos globais
        private SqlDatabase database;
        private DbCommand command;
        private DbTransaction currentTransaction = null;
        private string DatabaseNameNissi = "NissiDataBase";
        #endregion

        #region Métodos para uso de Query Analyzer Web (Tela de Acesso Direto)

        /// <summary>
        /// Instancia e retorna um objeto SqlDatabase que representa a conexão com o banco de dados.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public SqlDatabase GetDatabase(string connectionString)
        {
            string databaseName = ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
            SqlDatabase db = new SqlDatabase(databaseName);
            return db;
        }

        /// <summary>
        /// Chamada de proc via tela de Execução
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="hasReturnValue"></param>
        /// <param name="connectionString"></param>
        public void OpenCommand(string procedureName, string connectionString)
        {
            if (database != null && command != null)
                CloseCommand();

            database = GetDatabase(connectionString);
            command = database.GetStoredProcCommand(procedureName);
        }

        #endregion

        #region Métodos para criar a Conexão com o banco de dados

        /// <summary>
        /// Instancia e retorna um objeto SqlDatabase que representa a conexão com o banco de dados.
        /// </summary>
        private SqlDatabase GetDatabase()
        {
            string databaseName = ConfigurationManager.ConnectionStrings[DatabaseNameNissi].ConnectionString;
            SqlDatabase db = new SqlDatabase(databaseName);
            return db;
        }
        #endregion

        #region Gera o Debug com o comando necessário para executar a proc no Query Analyzer

        /// <summary>
        /// Gera um debug com a string necessária para executar a proc no query analyzer 
        /// com os parâmetros informados na execução. Essa string é usada para facilitar o 
        /// teste em caso de erro.
        /// </summary>
        /// <param name="comando">object DbCommand com o qual deverá ser gerada a string de execução</param>
        [System.Diagnostics.Conditional("DEBUG")]
        private void DebugProcedure(DbCommand comando)
        {
            string comandoSql = "[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "] "
                                    + MontaStringExecucaoProcedure(comando);
            Debug.WriteLine(comandoSql);
        }

        /// <summary>
        /// Monta uma string que pode ser utilizada para executar o comando passado como parâmetro
        /// no Query Analyzer, para testes e depuração.
        /// </summary>
        /// <param name="comando">object DbCommand com o qual deverá ser gerada a string de execução</param>
        /// <returns>String de execução da procedure</returns>
        private string MontaStringExecucaoProcedure(DbCommand comando)
        {
            // Obtêm o separador decimal
            System.Globalization.CultureInfo cult = System.Globalization.CultureInfo.CurrentCulture;
            string Sep = cult.NumberFormat.NumberDecimalSeparator;

            StringBuilder sb = new StringBuilder();

            sb.Append("TJSP2005 - ").Append(DatabaseNameNissi);
            sb.Append(": exec ").Append(comando.CommandText).Append(" ");

            for (int iPar = 0; iPar < comando.Parameters.Count; iPar++)
            {
                sb.Append(comando.Parameters[iPar].ParameterName);
                sb.Append(" = ");

                // Valores nulos
                if (comando.Parameters[iPar].Value == DBNull.Value || comando.Parameters[iPar].Value == null)
                    sb.Append("null");
                else
                {
                    switch (comando.Parameters[iPar].DbType)
                    {
                        case DbType.Byte:
                        case DbType.Int16:
                        case DbType.Int32:
                        case DbType.Int64:
                        case DbType.SByte:
                        case DbType.UInt16:
                        case DbType.UInt32:
                        case DbType.UInt64:
                            sb.Append(comando.Parameters[iPar].Value.ToString());
                            break;
                        case DbType.Date:
                        case DbType.DateTime:
                            DateTime data = (DateTime)comando.Parameters[iPar].Value;
                            sb.Append("'").Append(data.ToString("yyyy-MM-dd HH:mm:ss.fff")).Append("'");
                            break;
                        case DbType.Decimal:
                        case DbType.Currency:
                        case DbType.Double:
                            double decValor = Convert.ToDouble(comando.Parameters[iPar].Value);
                            sb.Append(decValor.ToString("#######0.00##").Replace(Sep, "."));
                            break;
                        case DbType.StringFixedLength:
                        case DbType.String:
                        case DbType.AnsiString:
                            string strValor = comando.Parameters[iPar].Value.ToString();
                            sb.Append("'").Append(strValor.Replace("'", "''")).Append("'");
                            break;
                        case DbType.Boolean:
                            bool blnValor = (bool)comando.Parameters[iPar].Value;
                            sb.Append(blnValor ? "1" : "0");
                            break;
                        default:
                            sb.Append(" {Erro! Tipo não tratado: ");
                            sb.Append(comando.Parameters[iPar].DbType.ToString());
                            sb.Append("}");
                            break;
                    }
                }
                if (comando.Parameters[iPar].Direction != ParameterDirection.Input)
                    sb.Append(" (out)");

                sb.Append(", ");
            }
            if (comando.Parameters.Count > 0)
                sb.Remove(sb.Length - 2, 2);

            return (sb.ToString());
        }

        #endregion

        #region Métodos para execução das procedures

        /// <summary>
        /// Cria os objetos necessários para a execução da procedure informada.
        /// Esse método não executa a procedure, apenas prepara o comando. Após a execução da procedure 
        /// deve ser chamado o método CloseCommand(). 
        /// </summary>
        /// <param name="procedureName">Nome da procedure a ser executada</param>
        protected void OpenCommand(string procedureName)
        {
            OpenCommand(procedureName, false);
        }

        /// <summary>
        /// Cria os objetos necessários para a execução da procedure informada.
        /// Esse método não executa a procedure, apenas prepara o comando. Após a execução da procedure 
        /// deve ser chamado o método CloseCommand(). 
        /// </summary>
        /// <param name="procedureName">Nome da procedure a ser executada</param>
        /// <param name="hasReturnValue">Booleano que indica se a procedure retornará um valor 
        /// de retorno através do comando 'return'</param>
        protected void OpenCommand(string procedureName, bool hasReturnValue)
        {
            if (database != null && command != null)
                CloseCommand();

            database = GetDatabase();

            command = database.GetStoredProcCommand(procedureName);

            if (hasReturnValue)
                database.AddParameter(command, "@RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue, "",
                        DataRowVersion.Current, null);
        }

        /// <summary>
        //É USADO ESTRITAMENTE PARA TESTE
        //ESSE METODO EXECUTA UM COMANDO SQL
        /// </summary>
        /// <param name="procedureName">Consulta sql a ser executada</param>
        protected void OpenCommandSQL(string sql)
        {
            if (database != null && command != null)
                CloseCommand();

            database = GetDatabase();
            command = database.GetSqlStringCommand(sql);
        }

        /// <summary>
        /// Encerra e libera o comando criado para a execução da procedure. Deve sempre ser 
        /// chamado após a execução da procedure para liberar os recursos utilizados.
        /// IMPORTANTE: Se estiver sendo utilizado um DataReader, esse método deve ser chamado 
        /// apenas quando o DataReader não estiver mais sendo utilizado.
        /// </summary>
        protected void CloseCommand()
        {
            if (command != null)
                command.Dispose();
            command = null;
            database = null;
        }

        /// <summary>
        /// Adiciona uma parâmetro de entrada para a procedure.
        /// </summary>
        /// <param name="name">Nome do parâmetro (Ex: '@NumProtocolo')</param>
        /// <param name="type">Tipo do parâmetro (Ex: DBType.Int32, DBType.String etc)</param>
        /// <param name="value">Valor do parâmetro</param>
        protected void AddInParameter(string name, DbType type, object value)
        {
            database.AddInParameter(command, name, type, value);
        }

        /// <summary>
        /// Adiciona um parâmetro do tipo varbinary, um sequência de bytes
        /// </summary>
        /// <param name="name">Nome do parâmetro (Com o '@')</param>
        /// <param name="value">valor do parâmetro</param>
        protected void AddBinaryParameter(string name, byte[] value)
        {
            database.AddInParameter(command, name, SqlDbType.VarBinary, value);
        }

        /// <summary>
        /// Adiciona um parâmetro de saída para a procedure. Se o parâmetro for uma string, 
        /// o tamanho do parâmetro deve ser informado.
        /// </summary>
        /// <param name="name">Nome do parâmetro (Ex: '@NumProtocolo')</param>
        /// <param name="type">Tipo do parâmetro (Ex: DBType.Int32, DBType.String etc)</param>
        protected void AddOutParameter(string name, DbType type)
        {
            AddOutParameter(name, type, 50);
        }

        /// <summary>
        /// Adiciona um parâmetro de saída para a procedure. Se o parâmetro for uma string, 
        /// o tamanho do parâmetro deve ser informado.
        /// </summary>
        /// <param name="name">Nome do parâmetro (Ex: '@NumProtocolo')</param>
        /// <param name="type">Tipo do parâmetro (Ex: DBType.Int32, DBType.String etc)</param>
        /// <param name="size">Tamanho do parâmetro (Somente em caso de strings)</param>
        protected void AddOutParameter(string name, DbType type, int size)
        {
            database.AddOutParameter(command, name, type, size);
        }

        /// <summary>
        /// Adiciona um parâmetro de entrada e saída para a procedure. Se o parâmetro for uma string, 
        /// o tamanho do parâmetro deve ser informado. 
        /// </summary>
        /// <param name="name">Nome do parâmetro (Ex: '@NumProtocolo')</param>
        /// <param name="type">Tipo do parâmetro (Ex: DBType.Int32, DBType.String etc)</param>
        /// <param name="value">Valor do parâmetro</param>
        protected void AddInOutParameter(string name, DbType type, object value)
        {
            AddInOutParameter(name, type, 50, value);
        }

        /// <summary>
        /// Adiciona um parâmetro de entrada e saída para a procedure. Se o parâmetro for uma string, 
        /// o tamanho do parâmetro deve ser informado. 
        /// </summary>
        /// <param name="name">Nome do parâmetro (Ex: '@NumProtocolo')</param>
        /// <param name="type">Tipo do parâmetro (Ex: DBType.Int32, DBType.String etc)</param>
        /// <param name="size">Tamanho do parâmetro (Somente em caso de strings)</param>
        /// <param name="value">Valor do parâmetro</param>
        protected void AddInOutParameter(string name, DbType type, int size, object value)
        {
            database.AddOutParameter(command, name, type, size);
            command.Parameters[name].Direction = ParameterDirection.InputOutput;
            command.Parameters[name].Value = value;
        }

        /// <summary>
        /// Retorna o valor do parâmetro informado. Esse método é chamado após a execução 
        /// da procedure para acessar o valor dos parâmetros de saída.
        /// IMPORTANTE: Se houver um DataReader ativo, os valores dos parâmetros
        /// de saída só estarão disponíveis após o DataReader ser fechado.
        /// </summary>
        /// <param name="name">Nome do parâmetro (Ex: '@NumProtocolo')</param>
        /// <returns>Valor do parâmetro</returns>
        protected object GetParameterValue(string name)
        {
            return command.Parameters[name].Value;
        }

        /// <summary>
        /// Retorna o valor de retorno da procedure. Esse valor é o que a proc retorna através da 
        /// expressão "return". Para chamar esse método, é necessário chamar o método AddReturnValueParameter 
        /// antes da execução.
        /// </summary>        
        /// <returns>Valor de retorno da procedure</returns>
        protected int GetReturnValue()
        {
            if (command.Parameters["@RETURN_VALUE"].Value == DBNull.Value)
                return int.MinValue;
            else
                return (int)command.Parameters["@RETURN_VALUE"].Value;
        }
        /// <summary>
        /// Retorna o valor de retorno da procedure. Esse valor é o que a proc retorna através da 
        /// expressão "return". Para chamar esse método, é necessário chamar o método AddReturnValueParameter 
        /// antes da execução.
        /// </summary>        
        /// <returns>Valor de retorno da procedure</returns>
        protected T GetReturnValue<T>()
        {
            if (command.Parameters["@RETURN_VALUE"].Value == DBNull.Value)
                return default(T);
            else
                return (T)command.Parameters["@RETURN_VALUE"].Value;
        }

        /// <summary>
        /// Executa o comando preparado previamente sem retornar nenhum valor.
        /// Se houverem parâmetros de saída, os valores SERÃO retornados normalmente.
        /// </summary>
        /// <returns>Quantidade de registros afetados</returns>
        protected int ExecuteNonQuery()
        {
            try
            {
                DebugProcedure(command);

                if (currentTransaction == null)
                    return database.ExecuteNonQuery(command);
                else
                    return database.ExecuteNonQuery(command, currentTransaction);
            }
            catch (Exception ex)
            {
                ex.HelpLink = MontaStringExecucaoProcedure(command);
                throw ex;
            }
        }

        /// <summary>
        /// Executa o comando preparado anteriormente e retorna um DataReader.
        /// </summary>
        protected IDataReader ExecuteReader()
        {
            try
            {
                DebugProcedure(command);
                if (currentTransaction == null)
                    return database.ExecuteReader(command);
                else
                    return database.ExecuteReader(command, currentTransaction);
            }
            catch (Exception ex)
            {
                ex.HelpLink = MontaStringExecucaoProcedure(command);
                throw ex;
            }
        }

        /// <summary>
        /// Executa o comando preparado anteriormente e retorna o valor do primeiro campo 
        /// da primeira linha retornada pela procedure.
        /// </summary>
        /// <typeparam name="T">Tipo do dado a ser retornado</typeparam>
        /// <returns>Retorna o valor do primeiro campo da primeira linha retornada pela 
        /// procedure. Se o campo for nulo, retornará nulo quanto o tipo T aceitar nulo, 
        /// ou 0, caso contrário</returns>
        protected T ExecuteScalar<T>()
        {
            try
            {
                DebugProcedure(command);
                object retorno;
                if (currentTransaction == null)
                    retorno = database.ExecuteScalar(command);
                else
                    retorno = database.ExecuteScalar(command, currentTransaction);

                if (retorno == DBNull.Value)
                    return default(T);
                else
                    return (T)retorno;
            }
            catch (Exception ex)
            {
                ex.HelpLink = MontaStringExecucaoProcedure(command);
                throw ex;
            }
        }

        /// <summary>
        /// Executa o comando preparado e retorna o resultado em uma DataTable.
        /// </summary>
        /// <returns>DataTable com o resultado da execução do comando</returns>
        protected DataTable ExecuteDataTable()
        {
            return ExecuteDataTable<DataTable>();
        }

        /// <summary>
        /// Executa o comando preparado e retorna o resultado em uma DataTable. Como esse método
        /// usa Generics, a DataTable retornada será do tipo especificado no parâmetro TDataTable
        /// </summary>
        /// <typeparam name="TDataTable">Tipo da DataTable a ser criada, para ser especificada
        /// em caso de Dataset tipado</typeparam>
        /// <returns>DataTable com o resultado da execução do comando</returns>
        protected TDataTable ExecuteDataTable<TDataTable>() where TDataTable : DataTable, new()
        {
            try
            {
                DebugProcedure(command);

                DbDataAdapter adapter = database.GetDataAdapter();
                adapter.SelectCommand = command;
                if (currentTransaction == null)
                    command.Connection = database.CreateConnection();
                else
                {
                    command.Connection = currentTransaction.Connection;
                    command.Transaction = currentTransaction;
                }

                TDataTable tbl = new TDataTable();
                adapter.Fill(0, Convert.ToInt32(ConfigurationManager.AppSettings["TJSP2005.QtdeMaxRegistro"]), tbl);

                return tbl;
            }
            catch (Exception ex)
            {
                ex.HelpLink = MontaStringExecucaoProcedure(command);
                throw ex;
            }
        }

        /// <summary>
        /// Executa o comando preparado e retorna os valores do primeiro campo em um array 
        /// com o tipo especificado. Essa função não realiza conversão de tipos, portanto, 
        /// se o valor retornado pelo campo não for do tipo especificado irá ocorrer um erro
        /// </summary>
        /// <typeparam name="T">Tipo dos itens do array</typeparam>
        /// <returns>Um array com os valores retornados pelo banco</returns>
        protected T[] ExecuteArray<T>()
        {
            DataTable dtbResult = ExecuteDataTable();

            if (dtbResult == null || dtbResult.Rows.Count == 0)
                return new T[0];
            else
            {
                T[] arrResultado = new T[dtbResult.Rows.Count];
                for (int i = 0; i < dtbResult.Rows.Count; i++)
                    arrResultado[i] = (T)dtbResult.Rows[i][0];

                return arrResultado;
            }
        }

        #endregion

        #region Métodos auxiliares

        /// <summary>
        /// Converte o campo desejado de um DataReader para Binary (Image)
        /// </summary>
        /// <param name="reader">DataReader</param>
        /// <param name="name">Nome do Campo</param>
        /// <returns></returns>
        protected T GetReaderValue<T>(IDataReader reader, string name)
        {
            if (reader[name] == System.DBNull.Value)
                return default(T);
            else
                return (T)reader[name];
        }

        /// <summary>
        /// Converte o campo desejado de um DataReader para Binary (Image)
        /// </summary>
        /// <param name="reader">DataReader</param>
        /// <param name="index">Indice do Campo</param>
        /// <returns></returns>
        protected T GetReaderValue<T>(IDataReader reader, int index)
        {
            if (reader[index] == System.DBNull.Value)
                return default(T);
            else
                return (T)reader[index];
        }

        /// <summary>
        /// Converte o campo desejado de um DataReader para Booleano
        /// </summary>
        /// <param name="name">Nome do Campo</param>
        /// <param name="reader">DataReader</param>
        /// <returns></returns>
        public bool GetAsBoolean(string name, IDataReader reader)
        {
            string result = reader[name] == System.DBNull.Value || reader[name].ToString().Trim().Equals("") ? "N" : reader[name].ToString();
            return result.Equals("S") ? true : false;
        }

        /// <summary>
        /// Transforma um valor booleano em string com respostas "S" ou "N"
        /// </summary>
        /// <param name="resposta">Váriavel do tipo boolean de entrada</param>
        /// <returns></returns>
        public string BoolToString(bool resposta)
        {
            return resposta ? "S" : "N";
        }

        /// <summary>
        /// Transforma um valor string "S" ou "N" em booleano
        /// </summary>
        /// <param name="resposta">Váriavel do tipo string contendo "S" ou "N"</param>
        /// <returns></returns>
        public bool StringToBool(string resposta)
        {
            bool bretorno = false;
            if (resposta.Equals("1") || resposta.Equals("0"))
                bretorno = resposta.Equals("1") ? true : false;
            else if (resposta.ToLower().Equals("true") || resposta.ToLower().Equals("false"))
                bretorno = resposta.ToLower().Equals("true") ? true : false;
            else
                bretorno = resposta.Equals("S") ? true : false;

            return bretorno;
        }

        #endregion
    }
}
