using MySql.Data.MySqlClient;
using System.Data;

namespace TaskSystem
{
    public class BC_MySqlUtils
    {
        
        // 连接字符串
        private static string ConnStr = "server=127.0.0.1;user id=root;password=990815;database=tasksms;charset=utf8; SslMode=None";

        // 建立MySQL数据库连接
        public static MySqlConnection GetMysqlConnection()
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);
            return conn;
        }

        // 执行增删改方法
        public static int ExecuteSQL(string sql)
        {
            int result = 0;
            MySqlConnection conn = GetMysqlConnection();
            conn.Open();
            MySqlCommand command = new MySqlCommand(sql, conn);
            result = command.ExecuteNonQuery();
            command.Dispose();
            conn.Close();
            conn.Dispose();
            return result;
        }

        // 查询单个值
        public static object ExecuteSQLGetScalar(string sql)
        {
            object result = null;
            MySqlConnection conn = GetMysqlConnection();
            conn.Open();
            MySqlCommand command = new MySqlCommand(sql, conn);
            result = command.ExecuteScalar();
            command.Dispose();
            conn.Close();
            conn.Dispose();
            return result;
        }

        // 执行查询,获得MySqlDataReader对象
        public static MySqlDataReader ExecuteSQLGetRS(string sql)
        {
            MySqlConnection conn = GetMysqlConnection();
            conn.Open();
            MySqlCommand  command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        // 关闭资源
        public static void CloseResource(MySqlConnection conn, MySqlDataReader reader)
        {
            // 先开后关
            if (reader != null)
            {
                reader.Close();
                reader.Dispose();
            }
            // if (command != null)
            // {
            //     command.Dispose();
            // }
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}