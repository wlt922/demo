using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DbContext
    {
        public SqlSugarClient Db { get; private set; }

        public DbContext()
        {
            // 创建 SqlSugarClient 实例  
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Server=localhost;port=3306; Database=mydatabase;Uid=root;Pwd=123456;", // 替换为你的连接字符串
                DbType = DbType.MySql, // 数据库类型  
                IsAutoCloseConnection = true, // 自动关闭连接  
            });
        }
    }
}
