using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Configuration;
using System.Collections.Concurrent;
using System.Configuration;
using System.Data;


namespace Banana.Dal
{
    public abstract class DalBase : IConnectionManager
    {
        protected static readonly ConcurrentDictionary<string, ConnectionStringSettings> connectionList = null;
        private string currentConnStringName = null;

        static DalBase()
        {
            //初始化数据库连接
            connectionList = new System.Collections.Concurrent.ConcurrentDictionary<string, ConnectionStringSettings>();
            foreach (ConnectionStringSettings cs in ConfigurationManager.ConnectionStrings)
            {
                if (cs.Name.CompareTo("LocalSqlServer") == 0) continue;
                connectionList.AddOrUpdate(cs.Name, cs, (key, val) => { return val; });
            }

        }

        public DalBase()
        {
            SetConnection();
        }

        private static IDbConnection GetConnection(string connectionString, string dbType)
        {
            IDbConnection conn = null;
            dbType = dbType.ToUpper();
            switch (dbType)
            {
                case "SQLSERVER":
                case "SYSTEM.DATA.SQLCLIENT": conn = new System.Data.SqlClient.SqlConnection(connectionString); break;
                case "ORACLE": conn = new System.Data.OracleClient.OracleConnection(connectionString); break;
                case "ACCESS": conn = new System.Data.OleDb.OleDbConnection(connectionString); break;
                case "DB2": conn = new System.Data.Odbc.OdbcConnection(connectionString); break;
                default: conn = new System.Data.OleDb.OleDbConnection(connectionString); break;
            }
            return conn;
        }
        /// <summary>
        /// 获取配置文件中第一个数据库连接字符串，然后创建一个数据库连接
        /// </summary>
        /// <returns></returns>
        protected IDbConnection OpenConnection()
        {
            IDbConnection conn = OpenConnection(currentConnStringName);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            return conn;
        }

        /// <summary>
        /// 获取配置文件中指定名称的数据库连接字符串，然后创建一个数据库连接
        /// </summary>
        /// <returns></returns>
        private IDbConnection OpenConnection(string connName)
        {
            if (connectionList.Count <= 0)
                throw new ConfigurationErrorsException("请在配置文件添加数据库连接字符串");
            if (!connectionList.ContainsKey(connName))
                throw new ArgumentException("提供的数据库连接字符串名称在配置文件中找不到");

            ConnectionStringSettings cs = connectionList[connName];
            IDbConnection conn = GetConnection(cs.ConnectionString, cs.ProviderName);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            return conn;
        }

        #region IConnectionManager 成员

        public void SetConnection()
        {
            if (connectionList.Count <= 0)
                throw new ConfigurationErrorsException("请在配置文件添加数据库连接字符串");

            ConnectionStringSettings firstConnSetting = connectionList.OrderBy(c => c.Key).First().Value;
            this.currentConnStringName = firstConnSetting.Name;
        }

        public void SetConnection(string connStringName)
        {
            this.currentConnStringName = connStringName;
        }

        public IDbTransaction GetTransaction(string connectionStringName)
        {
            IDbConnection conn = OpenConnection(connectionStringName);
            return conn.BeginTransaction();
        }

        public IDbTransaction GetTransaction()
        {
            IDbConnection conn = OpenConnection();
            return conn.BeginTransaction();
        }

        #endregion
    }
}
