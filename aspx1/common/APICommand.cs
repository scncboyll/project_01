
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace TaskSystem
{
    public class BC_APICommand
    {
        private static JObject PostData;
        // 实现方法
        public static async Task ProcessAPIResult(HttpContext content, IApplicationBuilder builder)
        {
            string returnStr = "";
            // 获取POST参数
            string parameters = "{}";
            using (StreamReader sr = new StreamReader(content.Request.Body, Encoding.UTF8))
            {
                //string content = sr.ReadToEnd();  //.Net Core 3.0 默认不再支持
                parameters = sr.ReadToEndAsync().Result;
            }
            PostData = JObject.Parse(parameters);
            // 获取APICommand
            string APICommand = GetParameterByName("APICommand");
            StringBuilder sqlB = new StringBuilder();
            switch (APICommand)
            {
                case "demo":
                    {
                        returnStr = BC_APIResult.GetAPIResult("", (int)BC_APIResultStatus.UN_KNOW, "示例请求");
                        break;
                    }
                case "Login":
                    {
                        string userName = GetParameterByName("UserName");
                        string userPwd = GetParameterByName("UserPwd");

                        sqlB.Length = 0;
                        sqlB.AppendLine($"SELECT UserID FROM user WHERE UserName='{userName}' AND UserPwd='{userPwd}'");
                        string sql = sqlB.ToString();
                        System.Console.WriteLine(sql);

                        int userID = (int)BC_MySqlUtils.ExecuteSQLGetScalar(sql);

                        returnStr = BC_APIResult.GetAPIResult(userID, (int)BC_APIResultStatus.UN_KNOW, "实例请求");
                        break;
                    }
                case "userLogin":   //用户登录
                    {
                        sqlB.Length = 0;

                        sqlB.AppendLine("SELECT ");
                        sqlB.AppendLine(" user.UserName ");
                        sqlB.AppendLine(" ,user.UserPwd ");
                        sqlB.AppendLine(" FROM user ");
                        sqlB.AppendLine(" LIMIT 30 ");
                        sqlB.AppendLine(";");

                        MySqlDataReader reader = BC_MySqlUtils.ExecuteSQLGetRS(sqlB.ToString());

                        //把所有的行装进list
                        List<object> taskList = new List<object>();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //吧每一行每一列装成一个字典
                                Dictionary<string, string> taskListDic = new Dictionary<string, string>();
                                taskListDic.Add("UserName", reader["UserName"].ToString());
                                taskListDic.Add("UserPwd", reader["UserPwd"].ToString());

                                taskList.Add(taskListDic);
                            }
                        }
                        reader.Close();

                        returnStr = BC_APIResult.GetAPIResult(taskList, (int)BC_APIResultStatus.FAIL, "执行查询Task的API!");

                        break;
                    }

                case "userRegister":  //用户注册
                    {
                        string userName = GetParameterByName("UserName");
                        string userPwd = GetParameterByName("UserPwd");
                        string email = GetParameterByName("Email");
                        string tname = GetParameterByName("Tname");
                        string sex = GetParameterByName("Sex");
                        string addr = GetParameterByName("Addr");
                        string tel = GetParameterByName("Tel");


                        sqlB.Length = 0;
                        sqlB.AppendLine("INSERT INTO ");
                        sqlB.AppendLine(" user(");
                        sqlB.AppendLine(" user.UserName ");
                        sqlB.AppendLine(", user.UserPwd ");
                        sqlB.AppendLine(", user.Email ");
                        sqlB.AppendLine(", user.Tname ");
                        sqlB.AppendLine(", user.Sex ");
                        sqlB.AppendLine(", user.Addr ");
                        sqlB.AppendLine(", user.Tel ");
                        sqlB.AppendLine(", user.Delstatus ");
                        sqlB.AppendLine(", user.SaveTime ");
                        sqlB.AppendLine(")");
                        sqlB.AppendLine(" VALUES(");
                        sqlB.AppendLine($" '{userName}' ");
                        sqlB.AppendLine($", '{userPwd}' ");
                        sqlB.AppendLine($", '{email}' ");
                        sqlB.AppendLine($", '{tname}' ");
                        sqlB.AppendLine($", '{sex}' ");
                        sqlB.AppendLine($", '{addr}' ");
                        sqlB.AppendLine($", '{tel}' ");
                        sqlB.AppendLine($", '否' ");
                        sqlB.AppendLine($", NOW() ");
                        sqlB.AppendLine(")");
                        sqlB.AppendLine(";");

                        int affectedRow = BC_MySqlUtils.ExecuteSQL(sqlB.ToString());

                        returnStr = BC_APIResult.GetAPIResult(affectedRow, (int)BC_APIResultStatus.FAIL, "执行我们注册的API!");


                        break;
                    }
                case "QueryTask":
                    {
                        sqlB.Length = 0;
                        sqlB.AppendLine("SELECT ");
                        sqlB.AppendLine(" tasks.TaskID ");
                        sqlB.AppendLine(" ,tasks.ProjectID ");
                        sqlB.AppendLine(" ,tasks.Task ");
                        sqlB.AppendLine(" ,tasks.AssignedToUserID ");
                        sqlB.AppendLine(" ,tasks.TimeUpdated ");
                        sqlB.AppendLine(" ,tasks.UpdatedBy ");
                        sqlB.AppendLine(" FROM tasks ");
                        sqlB.AppendLine(" LIMIT 30 ");
                        sqlB.AppendLine(";");

                        MySqlDataReader reader = BC_MySqlUtils.ExecuteSQLGetRS(sqlB.ToString());

                        //把所有的行装进list
                        List<object> taskList = new List<object>();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //吧每一行每一列装成一个字典
                                Dictionary<string, string> taskListDic = new Dictionary<string, string>();
                                taskListDic.Add("TaskID", reader["TaskID"].ToString());
                                taskListDic.Add("ProjectID", reader["ProjectID"].ToString());
                                taskListDic.Add("Task", reader["Task"].ToString());
                                taskListDic.Add("AssignedToUserID", reader["AssignedToUserID"].ToString());
                                taskListDic.Add("TimeUpdated", reader["TimeUpdated"].ToString());
                                taskListDic.Add("UpdatedBy", reader["UpdatedBy"].ToString());

                                taskList.Add(taskListDic);
                            }
                        }
                        reader.Close();

                        returnStr = BC_APIResult.GetAPIResult(taskList, (int)BC_APIResultStatus.FAIL, "执行查询Task的API!");

                        break;
                    }
                case "AdminRegister":  //管理员注册
                    {
                        string adminName = GetParameterByName("AdminName");
                        string adminPwd = GetParameterByName("AdminPwd");
                        string realName = GetParameterByName("RealName");
                        string sex = GetParameterByName("Sex");
                        string idCard = GetParameterByName("IdCard");
                        string tel = GetParameterByName("Tel");
                        string addr = GetParameterByName("Addr");
                        string email = GetParameterByName("Email");


                        sqlB.Length = 0;
                        sqlB.AppendLine("INSERT INTO ");
                        sqlB.AppendLine(" admin(");
                        sqlB.AppendLine(" admin.AdminName ");
                        sqlB.AppendLine(", admin.Adminpwd ");
                        sqlB.AppendLine(", admin.RealName ");
                        sqlB.AppendLine(", admin.Sex ");
                        sqlB.AppendLine(", admin.IdCard ");
                        sqlB.AppendLine(", admin.Tel ");
                        sqlB.AppendLine(", admin.Addr ");
                        sqlB.AppendLine(", admin.Email ");
                        sqlB.AppendLine(")");
                        sqlB.AppendLine(" VALUES(");
                        sqlB.AppendLine($" '{adminName}' ");
                        sqlB.AppendLine($", '{adminPwd}' ");
                        sqlB.AppendLine($", '{realName}' ");
                        sqlB.AppendLine($", '{sex}' ");
                        sqlB.AppendLine($", '{idCard}' ");
                        sqlB.AppendLine($", '{tel}' ");
                        sqlB.AppendLine($", '{addr}' ");
                        sqlB.AppendLine($", '{email}' ");
                        sqlB.AppendLine(")");
                        sqlB.AppendLine(";");

                        int affectedRow = BC_MySqlUtils.ExecuteSQL(sqlB.ToString());

                        returnStr = BC_APIResult.GetAPIResult(affectedRow, (int)BC_APIResultStatus.FAIL, "执行我们注册的API!");

                        break;
                    }
                case "AdminLogin":   //管理员登录
                    {
                        sqlB.Length = 0;

                        sqlB.AppendLine("SELECT ");
                        sqlB.AppendLine(" admin.AdminName ");
                        sqlB.AppendLine(" ,admin.AdminPwd ");
                        sqlB.AppendLine(" FROM admin ");
                        sqlB.AppendLine(" LIMIT 30 ");
                        sqlB.AppendLine(";");

                        MySqlDataReader reader = BC_MySqlUtils.ExecuteSQLGetRS(sqlB.ToString());

                        //把所有的行装进list
                        List<object> taskList = new List<object>();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //吧每一行每一列装成一个字典
                                Dictionary<string, string> taskListDic = new Dictionary<string, string>();
                                taskListDic.Add("AdminName", reader["AdminName"].ToString());
                                taskListDic.Add("AdminPwd", reader["AdminPwd"].ToString());

                                taskList.Add(taskListDic);
                            }
                        }
                        reader.Close();

                        returnStr = BC_APIResult.GetAPIResult(taskList, (int)BC_APIResultStatus.FAIL, "执行查询Task的API!");

                        break;
                    }
                //从数据库中获取user表的信息
                case "AdminIndex":
                    {
                        sqlB.Length = 0;
                        sqlB.AppendLine("SELECT ");
                        sqlB.AppendLine(" user.UserID ");
                        sqlB.AppendLine(" ,user.UserName ");
                        sqlB.AppendLine(" ,user.UserPwd ");
                        sqlB.AppendLine(" ,user.Tname ");
                        sqlB.AppendLine(" ,user.Sex ");
                        sqlB.AppendLine(" ,user.Tel ");
                        sqlB.AppendLine(" ,user.Email ");
                        sqlB.AppendLine(" ,user.Addr ");
                        sqlB.AppendLine(" ,user.SaveTime ");
                        // sqlB.AppendLine(" ,user.Delstatus ");
                        sqlB.AppendLine(" FROM user ");
                        sqlB.AppendLine(" LIMIT 30 ");
                        sqlB.AppendLine(";");

                        MySqlDataReader reader = BC_MySqlUtils.ExecuteSQLGetRS(sqlB.ToString());

                        //把所有的行装进list
                        List<object> userList = new List<object>();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //吧每一行每一列装成一个字典
                                Dictionary<string, string> userListDic = new Dictionary<string, string>();
                                userListDic.Add("UserID", reader["UserID"].ToString());
                                userListDic.Add("UserName", reader["UserName"].ToString());
                                userListDic.Add("UserPwd", reader["UserPwd"].ToString());
                                userListDic.Add("Tname", reader["Tname"].ToString());
                                userListDic.Add("Sex", reader["Sex"].ToString());
                                userListDic.Add("Tel", reader["Tel"].ToString());
                                userListDic.Add("Email", reader["Email"].ToString());
                                userListDic.Add("Addr", reader["Addr"].ToString());
                                userListDic.Add("SaveTime", reader["SaveTime"].ToString());
                                // userListDic.Add("Delstatus", reader["Delstatus"].ToString());

                                userList.Add(userListDic);
                            }
                        }
                        reader.Close();

                        returnStr = BC_APIResult.GetAPIResult(userList, (int)BC_APIResultStatus.FAIL, "执行查询Task的API!");

                        break;
                    }
                case "AddTask":
                    {
                        string task = GetParameterByName("Task");

                        sqlB.Length = 0;

                        sqlB.AppendLine("INSERT INTO ");
                        sqlB.AppendLine(" tasks(");
                        sqlB.AppendLine(" tasks.Task ");
                        sqlB.AppendLine(", tasks.TimeCreated ");
                        sqlB.AppendLine(", tasks.TimeUpdated ");
                        sqlB.AppendLine(")");
                        sqlB.AppendLine(" VALUES(");
                        sqlB.AppendLine($" '{task}' ");
                        sqlB.AppendLine($", NOW() ");
                        sqlB.AppendLine($", NOW() ");
                        sqlB.AppendLine(")");
                        sqlB.AppendLine(";");

                        int affectedRow = BC_MySqlUtils.ExecuteSQL(sqlB.ToString());

                        returnStr = BC_APIResult.GetAPIResult(affectedRow, (int)BC_APIResultStatus.FAIL, "执行我们注册的API!");

                        break;
                    }
                case "del":
                    {
                        string id = GetParameterByName("ID");

                        sqlB.Length = 0;

                        sqlB.AppendLine("DELETE ");
                        sqlB.AppendLine("   FROM  ");
                        sqlB.AppendLine("  tasks ");
                        sqlB.AppendLine("  WHERE ");
                        sqlB.AppendLine("  TaskID ");
                        sqlB.AppendLine("  = ");
                        sqlB.AppendLine($"    '{int.Parse(id)}' ");
                        sqlB.AppendLine(";");

                        string sql = sqlB.ToString();


                        int affectedRow = BC_MySqlUtils.ExecuteSQL(sqlB.ToString());

                        returnStr = BC_APIResult.GetAPIResult(affectedRow, (int)BC_APIResultStatus.FAIL, "执行删除的API!");


                        break;


                    }
                //查找要修改用户的信息
                case "find":
                    {
                        string id = GetParameterByName("ID");

                        sqlB.Length = 0;

                        sqlB.AppendLine("SELECT ");
                        sqlB.AppendLine(" user.UserName ");
                        sqlB.AppendLine(" ,user.UserID ");
                        sqlB.AppendLine(" ,user.UserPwd ");
                        sqlB.AppendLine(" ,user.Tname ");
                        sqlB.AppendLine(" ,user.Sex ");
                        sqlB.AppendLine(" ,user.Tel ");
                        sqlB.AppendLine(" ,user.Email ");
                        sqlB.AppendLine(" ,user.Addr ");
                        sqlB.AppendLine(" FROM user ");
                        sqlB.AppendLine(" WHERE ");
                        sqlB.AppendLine(" UserID ");
                        sqlB.AppendLine(" = ");
                        sqlB.AppendLine($"    '{int.Parse(id)}' ");
                        sqlB.AppendLine(";");

                        MySqlDataReader reader = BC_MySqlUtils.ExecuteSQLGetRS(sqlB.ToString());

                        //把所有的行装进list
                        List<object> taskList = new List<object>();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //吧每一行每一列装成一个字典
                                Dictionary<string, string> taskListDic = new Dictionary<string, string>();
                                taskListDic.Add("UserName", reader["UserName"].ToString());
                                taskListDic.Add("UserPwd", reader["UserPwd"].ToString());
                                taskListDic.Add("Tname", reader["Tname"].ToString());
                                taskListDic.Add("Sex", reader["Sex"].ToString());
                                taskListDic.Add("Tel", reader["Tel"].ToString());
                                taskListDic.Add("Email", reader["Email"].ToString());
                                taskListDic.Add("Addr", reader["Addr"].ToString());
                                taskListDic.Add("UserID", reader["UserID"].ToString());

                                taskList.Add(taskListDic);
                            }
                        }
                        reader.Close();

                        returnStr = BC_APIResult.GetAPIResult(taskList, (int)BC_APIResultStatus.FAIL, "执行查询Task的API!");

                        break;

                    }
                //管理员删除用户信息
                case "Admindel":
                    {
                        string id = GetParameterByName("ID");

                        sqlB.Length = 0;

                        sqlB.AppendLine("DELETE ");
                        sqlB.AppendLine("   FROM  ");
                        sqlB.AppendLine("  user ");
                        sqlB.AppendLine("  WHERE ");
                        sqlB.AppendLine("  UserID ");
                        sqlB.AppendLine("  = ");
                        sqlB.AppendLine($"    '{int.Parse(id)}' ");
                        sqlB.AppendLine(";");

                        string sql = sqlB.ToString();


                        int affectedRow = BC_MySqlUtils.ExecuteSQL(sqlB.ToString());

                        returnStr = BC_APIResult.GetAPIResult(affectedRow, (int)BC_APIResultStatus.FAIL, "执行删除的API!");


                        break;


                    }

                //修改用户信息
                case "userChange":
                    {
                        string FindName = GetParameterByName("findname");
                        string Findpwd = GetParameterByName("findpwd");
                        string Findemail = GetParameterByName("findemail");
                        string Findtname = GetParameterByName("findtname");
                        string Findsex = GetParameterByName("findsex");
                        string Findaddr = GetParameterByName("findaddr");
                        string Findtel = GetParameterByName("findtel");
                        string FindUserID = GetParameterByName("finduserid");

                        sqlB.Length = 0;

                        sqlB.AppendLine("UPDATE ");
                        sqlB.AppendLine("  user  ");
                        sqlB.AppendLine("  SET ");
                        sqlB.AppendLine(" UserName = ");
                        sqlB.AppendLine($"    '{FindName}' ");
                        sqlB.AppendLine(", UserPwd = ");
                        sqlB.AppendLine($"    '{Findpwd}' ");
                        sqlB.AppendLine(", Email = ");
                        sqlB.AppendLine($"    '{Findemail}' ");
                        sqlB.AppendLine(", Tname = ");
                        sqlB.AppendLine($"    '{Findtname}' ");
                        sqlB.AppendLine(", Sex = ");
                        sqlB.AppendLine($"    '{Findsex}' ");
                        sqlB.AppendLine(", Addr = ");
                        sqlB.AppendLine($"    '{Findaddr}' ");
                        sqlB.AppendLine(", Tel = ");
                        sqlB.AppendLine($"    '{Findtel}' ");
                        sqlB.AppendLine(", SaveTime = ");
                        sqlB.AppendLine($" NOW() ");
                        sqlB.AppendLine("  WHERE ");
                        sqlB.AppendLine("  UserID = ");
                        sqlB.AppendLine($"    '{int.Parse(FindUserID)}' ");

                        sqlB.AppendLine(";");

                        string sql = sqlB.ToString();


                        int affectedRow = BC_MySqlUtils.ExecuteSQL(sqlB.ToString());

                        returnStr = BC_APIResult.GetAPIResult(affectedRow, (int)BC_APIResultStatus.FAIL, "执行修改的API!");

                        break;

                    }
                //查找要修改用户的信息
                case "AdminMeaaage":
                    {
                        // string id = GetParameterByName("ID");

                        sqlB.Length = 0;

                        sqlB.AppendLine("SELECT ");
                        sqlB.AppendLine(" chat.chatid ");
                        sqlB.AppendLine(" ,chat.msg ");
                        sqlB.AppendLine(" ,chat.hfmsg ");
                        sqlB.AppendLine(" ,chat.savetime ");
                        sqlB.AppendLine(" ,chat.chatname ");
                        sqlB.AppendLine(" ,chat.memberid ");
                        sqlB.AppendLine(" FROM chat ");
                        sqlB.AppendLine(" LIMIT 30 ");
                        sqlB.AppendLine(";");

                        MySqlDataReader reader = BC_MySqlUtils.ExecuteSQLGetRS(sqlB.ToString());


                        //把所有的行装进list
                        List<object> taskList = new List<object>();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //吧每一行每一列装成一个字典
                                Dictionary<string, string> taskListDic = new Dictionary<string, string>();
                                taskListDic.Add("chatid", reader["chatid"].ToString());
                                taskListDic.Add("msg", reader["msg"].ToString());
                                taskListDic.Add("hfmsg", reader["hfmsg"].ToString());
                                taskListDic.Add("savetime", reader["savetime"].ToString());
                                taskListDic.Add("memberid", reader["memberid"].ToString());
                                taskListDic.Add("chatname", reader["chatname"].ToString());

                                taskList.Add(taskListDic);
                            }
                        }
                        reader.Close();

                        returnStr = BC_APIResult.GetAPIResult(taskList, (int)BC_APIResultStatus.FAIL, "执行查询Task的API!");

                        break;

                    }
                //管理员删除留言信息
                case "Admindelmessage":
                    {
                        string id = GetParameterByName("ID");

                        sqlB.Length = 0;

                        sqlB.AppendLine("DELETE ");
                        sqlB.AppendLine("   FROM  ");
                        sqlB.AppendLine("  chat ");
                        sqlB.AppendLine("  WHERE ");
                        sqlB.AppendLine("  chatid ");
                        sqlB.AppendLine("  = ");
                        sqlB.AppendLine($"    '{int.Parse(id)}' ");
                        sqlB.AppendLine(";");

                        string sql = sqlB.ToString();

                        int affectedRow = BC_MySqlUtils.ExecuteSQL(sqlB.ToString());

                        returnStr = BC_APIResult.GetAPIResult(affectedRow, (int)BC_APIResultStatus.FAIL, "执行删除的API!");

                        break;

                    }

                //查找商品的信息
                case "Admingoods":
                    {
                        sqlB.Length = 0;

                        sqlB.AppendLine("SELECT ");
                        sqlB.AppendLine(" goods.goodid ");
                        sqlB.AppendLine(" ,goods.goodname ");
                        sqlB.AppendLine(" ,goods.price ");
                        sqlB.AppendLine(" ,goods.note ");
                        sqlB.AppendLine(" ,goods.istj ");
                        sqlB.AppendLine(" ,goods.tprice ");
                        sqlB.AppendLine(" ,goods.filename ");
                        sqlB.AppendLine(" ,goods.delstatus ");
                        sqlB.AppendLine(" ,goods.salestatus ");
                        sqlB.AppendLine(" ,goods.type ");
                        sqlB.AppendLine(" ,goods.num ");
                        sqlB.AppendLine(" FROM goods ");
                        sqlB.AppendLine(" LIMIT 30 ");
                        sqlB.AppendLine(";");

                        MySqlDataReader reader = BC_MySqlUtils.ExecuteSQLGetRS(sqlB.ToString());

                        //把所有的行装进list
                        List<object> taskList = new List<object>();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //吧每一行每一列装成一个字典
                                Dictionary<string, string> taskListDic = new Dictionary<string, string>();
                                taskListDic.Add("goodid", reader["goodid"].ToString());
                                taskListDic.Add("goodname", reader["goodname"].ToString());
                                taskListDic.Add("price", reader["price"].ToString());
                                taskListDic.Add("note", reader["note"].ToString());
                                taskListDic.Add("istj", reader["istj"].ToString());
                                taskListDic.Add("salestatus", reader["salestatus"].ToString());
                                taskListDic.Add("tprice", reader["tprice"].ToString());
                                taskListDic.Add("filename", reader["filename"].ToString());
                                taskListDic.Add("delstatus", reader["delstatus"].ToString());
                                taskListDic.Add("type", reader["type"].ToString());
                                taskListDic.Add("num", reader["num"].ToString());
                                taskList.Add(taskListDic);
                            }
                        }
                        reader.Close();

                        returnStr = BC_APIResult.GetAPIResult(taskList, (int)BC_APIResultStatus.FAIL, "执行查询Task的API!");

                        break;

                    }
                //管理员删除商品信息
                case "Admindelgoods":
                    {
                        string id = GetParameterByName("ID");

                        sqlB.Length = 0;

                        sqlB.AppendLine("DELETE ");
                        sqlB.AppendLine("   FROM  ");
                        sqlB.AppendLine("  goods ");
                        sqlB.AppendLine("  WHERE ");
                        sqlB.AppendLine("  goodid ");
                        sqlB.AppendLine("  = ");
                        sqlB.AppendLine($"    '{int.Parse(id)}' ");
                        sqlB.AppendLine(";");

                        string sql = sqlB.ToString();

                        int affectedRow = BC_MySqlUtils.ExecuteSQL(sqlB.ToString());

                        returnStr = BC_APIResult.GetAPIResult(affectedRow, (int)BC_APIResultStatus.FAIL, "执行删除的API!");

                        break;

                    }
                //查找要修改商品的信息
                case "findmessage":
                    {
                        string id = GetParameterByName("ID");

                        sqlB.Length = 0;

                        sqlB.AppendLine("SELECT ");
                        sqlB.AppendLine(" goods.goodid ");
                        sqlB.AppendLine(" ,goods.goodname ");
                        sqlB.AppendLine(" ,goods.price ");
                        sqlB.AppendLine(" ,goods.note ");
                        sqlB.AppendLine(" ,goods.istj ");
                        sqlB.AppendLine(" ,goods.salestatus ");
                        sqlB.AppendLine(" FROM goods ");
                        sqlB.AppendLine("  WHERE ");
                        sqlB.AppendLine(" goodid = ");
                        sqlB.AppendLine($"    '{int.Parse(id)}' ");
                        sqlB.AppendLine(";");

                        MySqlDataReader reader = BC_MySqlUtils.ExecuteSQLGetRS(sqlB.ToString());


                        //把所有的行装进list
                        List<object> taskList = new List<object>();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //吧每一行每一列装成一个字典
                                Dictionary<string, string> taskListDic = new Dictionary<string, string>();
                                taskListDic.Add("goodid", reader["goodid"].ToString());
                                taskListDic.Add("goodname", reader["goodname"].ToString());
                                taskListDic.Add("price", reader["price"].ToString());
                                taskListDic.Add("note", reader["note"].ToString());
                                taskListDic.Add("istj", reader["istj"].ToString());
                                taskListDic.Add("salestatus", reader["salestatus"].ToString());

                                taskList.Add(taskListDic);
                            }
                        }
                        reader.Close();

                        returnStr = BC_APIResult.GetAPIResult(taskList, (int)BC_APIResultStatus.FAIL, "执行查询Task的API!");

                        break;

                    }
                //修改用户信息
                case "goodsChange":
                    {
                        string Goodsid = GetParameterByName("goodid");
                        string Goodsname = GetParameterByName("goodname");
                        string Goodsprice = GetParameterByName("price");
                        string Goodsnote = GetParameterByName("note");
                        string Goodsistj = GetParameterByName("istj");
                        string Goodssalestatus = GetParameterByName("salestatus");
                        string GoodsUserID = GetParameterByName("goodsUserID");

                        sqlB.Length = 0;

                        sqlB.AppendLine("UPDATE ");
                        sqlB.AppendLine("  goods  ");
                        sqlB.AppendLine("  SET ");
                        sqlB.AppendLine(" goodid = ");
                        sqlB.AppendLine($"    '{Goodsid}' ");
                        sqlB.AppendLine(", goodname = ");
                        sqlB.AppendLine($"    '{Goodsname}' ");
                        sqlB.AppendLine(", price = ");
                        sqlB.AppendLine($"    '{Goodsprice}' ");
                        sqlB.AppendLine(", note = ");
                        sqlB.AppendLine($"    '{Goodsnote}' ");
                        sqlB.AppendLine(", istj = ");
                        sqlB.AppendLine($"    '{Goodsistj}' ");
                        sqlB.AppendLine(", salestatus = ");
                        sqlB.AppendLine($"    '{Goodssalestatus}' ");
                        // sqlB.AppendLine(", Tel = ");
                        // sqlB.AppendLine($"    '{Findtel}' ");
                        // sqlB.AppendLine(", SaveTime = ");
                        // sqlB.AppendLine($" NOW() ");
                        sqlB.AppendLine("  WHERE ");
                        sqlB.AppendLine("  goodid = ");
                        sqlB.AppendLine($"    '{int.Parse(GoodsUserID)}' ");

                        sqlB.AppendLine(";");

                        string sql = sqlB.ToString();


                        int affectedRow = BC_MySqlUtils.ExecuteSQL(sqlB.ToString());

                        returnStr = BC_APIResult.GetAPIResult(affectedRow, (int)BC_APIResultStatus.FAIL, "执行修改的API!");

                        break;

                    }
                //查看评价的信息
                case "findevalu":
                    {
                        string id = GetParameterByName("ID");

                        sqlB.Length = 0;

                        sqlB.AppendLine("SELECT ");
                        sqlB.AppendLine(" evalu.id ");
                        sqlB.AppendLine(", evalu.goodid ");
                        sqlB.AppendLine(", evalu.memberid ");
                        sqlB.AppendLine(", evalu.jb ");
                        sqlB.AppendLine(", evalu.msg ");
                        sqlB.AppendLine(", evalu.savetime ");
                        sqlB.AppendLine(", evalu.hf ");
                        sqlB.AppendLine(", evalu.evaluname ");
                        sqlB.AppendLine(" FROM evalu ");
                        sqlB.AppendLine("  WHERE ");
                        sqlB.AppendLine(" goodid = ");
                        sqlB.AppendLine($"    '{int.Parse(id)}' ");
                        sqlB.AppendLine(";");

                        MySqlDataReader reader = BC_MySqlUtils.ExecuteSQLGetRS(sqlB.ToString());


                        //把所有的行装进list
                        List<object> taskList = new List<object>();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //吧每一行每一列装成一个字典
                                Dictionary<string, string> taskListDic = new Dictionary<string, string>();
                                taskListDic.Add("id", reader["id"].ToString());
                                taskListDic.Add("goodid", reader["goodid"].ToString());
                                taskListDic.Add("memberid", reader["memberid"].ToString());
                                taskListDic.Add("jb", reader["jb"].ToString());
                                taskListDic.Add("msg", reader["msg"].ToString());
                                taskListDic.Add("savetime", reader["savetime"].ToString());
                                taskListDic.Add("hf", reader["hf"].ToString());
                                taskListDic.Add("evaluname", reader["evaluname"].ToString());
                                taskList.Add(taskListDic);
                            }
                        }
                        reader.Close();

                        returnStr = BC_APIResult.GetAPIResult(taskList, (int)BC_APIResultStatus.FAIL, "执行查询Task的API!");

                        break;

                    }

                //管理评价的信息
                case "Admineval":
                    {
                        sqlB.Length = 0;

                        sqlB.AppendLine("SELECT ");
                        sqlB.AppendLine(" evalu.id ");
                        sqlB.AppendLine(", evalu.goodid ");
                        sqlB.AppendLine(", evalu.memberid ");
                        sqlB.AppendLine(", evalu.jb ");
                        sqlB.AppendLine(", evalu.msg ");
                        sqlB.AppendLine(", evalu.savetime ");
                        sqlB.AppendLine(", evalu.hf ");
                        sqlB.AppendLine(", evalu.evaluname ");
                        sqlB.AppendLine(" FROM evalu ");
                        sqlB.AppendLine(" LIMIT 30 ");
                        sqlB.AppendLine(";");

                        MySqlDataReader reader = BC_MySqlUtils.ExecuteSQLGetRS(sqlB.ToString());


                        //把所有的行装进list
                        List<object> taskList = new List<object>();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //吧每一行每一列装成一个字典
                                Dictionary<string, string> taskListDic = new Dictionary<string, string>();
                                taskListDic.Add("id", reader["id"].ToString());
                                taskListDic.Add("goodid", reader["goodid"].ToString());
                                taskListDic.Add("memberid", reader["memberid"].ToString());
                                taskListDic.Add("jb", reader["jb"].ToString());
                                taskListDic.Add("msg", reader["msg"].ToString());
                                taskListDic.Add("savetime", reader["savetime"].ToString());
                                taskListDic.Add("hf", reader["hf"].ToString());
                                taskListDic.Add("evaluname", reader["evaluname"].ToString());
                                taskList.Add(taskListDic);
                            }
                        }
                        reader.Close();

                        returnStr = BC_APIResult.GetAPIResult(taskList, (int)BC_APIResultStatus.FAIL, "执行查询Task的API!");

                        break;

                    }
                //管理员删除评价信息
                case "Admindeleval":
                    {
                        string id = GetParameterByName("ID");

                        sqlB.Length = 0;

                        sqlB.AppendLine("DELETE ");
                        sqlB.AppendLine("   FROM  ");
                        sqlB.AppendLine("  evalu ");
                        sqlB.AppendLine("  WHERE ");
                        sqlB.AppendLine("  id ");
                        sqlB.AppendLine("  = ");
                        sqlB.AppendLine($"    '{int.Parse(id)}' ");
                        sqlB.AppendLine(";");

                        string sql = sqlB.ToString();

                        int affectedRow = BC_MySqlUtils.ExecuteSQL(sqlB.ToString());

                        returnStr = BC_APIResult.GetAPIResult(affectedRow, (int)BC_APIResultStatus.FAIL, "执行删除的API!");

                        break;

                    }
                case "goodsupdata":  //添加商品
                    {
                        string Upgoodname = GetParameterByName("upgoodname");
                        string Upprice = GetParameterByName("upprice");
                        string Upnote = GetParameterByName("upnote");
                        string Upistj = GetParameterByName("upistj");
                        string Upsalestatus = GetParameterByName("upsalestatus");
                        string Uptprice = GetParameterByName("uptprice");
                        string Uptype = GetParameterByName("uptype");

                        sqlB.Length = 0;
                        sqlB.AppendLine("INSERT INTO ");
                        sqlB.AppendLine(" goods(");
                        sqlB.AppendLine(" goods.goodname ");
                        sqlB.AppendLine(", goods.price ");
                        sqlB.AppendLine(", goods.note ");
                        sqlB.AppendLine(", goods.istj ");
                        sqlB.AppendLine(", goods.salestatus ");
                        sqlB.AppendLine(", goods.tprice ");
                        sqlB.AppendLine(", goods.type ");
                        sqlB.AppendLine(")");
                        sqlB.AppendLine(" VALUES(");
                        sqlB.AppendLine($" '{Upgoodname}' ");
                        sqlB.AppendLine($", '{Upprice}' ");
                        sqlB.AppendLine($", '{Upnote}' ");
                        sqlB.AppendLine($", '{Upistj}' ");
                        sqlB.AppendLine($", '{Upsalestatus}' ");
                        sqlB.AppendLine($", '{Uptprice}' ");
                        sqlB.AppendLine($", '{Uptype}' ");
                        sqlB.AppendLine(")");
                        sqlB.AppendLine(";");

                        int affectedRow = BC_MySqlUtils.ExecuteSQL(sqlB.ToString());

                        returnStr = BC_APIResult.GetAPIResult(affectedRow, (int)BC_APIResultStatus.FAIL, "执行我们注册的API!");

                        break;
                    }
                //获取管理信息
                case "Adminstock":
                    {
                        sqlB.Length = 0;

                        sqlB.AppendLine("SELECT ");
                        sqlB.AppendLine(" kcrecord.id ");
                        sqlB.AppendLine(", kcrecord.gid ");
                        sqlB.AppendLine(", kcrecord.happennum ");
                        sqlB.AppendLine(", kcrecord.enternum ");
                        sqlB.AppendLine(", kcrecord.type ");
                        sqlB.AppendLine(", kcrecord.savetime ");
                        sqlB.AppendLine(", kcrecord.goodsname ");
                        sqlB.AppendLine(" FROM kcrecord ");
                        sqlB.AppendLine(" LIMIT 30 ");
                        sqlB.AppendLine(";");

                        MySqlDataReader reader = BC_MySqlUtils.ExecuteSQLGetRS(sqlB.ToString());


                        //把所有的行装进list
                        List<object> taskList = new List<object>();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //吧每一行每一列装成一个字典
                                Dictionary<string, string> taskListDic = new Dictionary<string, string>();
                                taskListDic.Add("id", reader["id"].ToString());
                                taskListDic.Add("gid", reader["gid"].ToString());
                                taskListDic.Add("happennum", reader["happennum"].ToString());
                                taskListDic.Add("enternum", reader["enternum"].ToString());
                                taskListDic.Add("type", reader["type"].ToString());
                                taskListDic.Add("savetime", reader["savetime"].ToString());
                                taskListDic.Add("goodsname", reader["goodsname"].ToString());
                                taskList.Add(taskListDic);
                            }
                        }
                        reader.Close();

                        returnStr = BC_APIResult.GetAPIResult(taskList, (int)BC_APIResultStatus.FAIL, "执行查询Task的API!");

                        break;

                    }
                //查找要修改用户的信息
                case "findstock":
                    {
                        string id = GetParameterByName("ID");

                        sqlB.Length = 0;

                        sqlB.AppendLine("SELECT ");
                        sqlB.AppendLine(" kcrecord.id ");
                        sqlB.AppendLine(", kcrecord.gid ");
                        sqlB.AppendLine(", kcrecord.happennum ");
                        sqlB.AppendLine(", kcrecord.enternum ");
                        sqlB.AppendLine(", kcrecord.type ");
                        sqlB.AppendLine(", kcrecord.savetime ");
                        sqlB.AppendLine(", kcrecord.goodsname ");
                        sqlB.AppendLine(" FROM kcrecord ");
                        sqlB.AppendLine(" WHERE ");
                        sqlB.AppendLine(" id ");
                        sqlB.AppendLine(" = ");
                        sqlB.AppendLine($"    '{int.Parse(id)}' ");
                        sqlB.AppendLine(";");

                        MySqlDataReader reader = BC_MySqlUtils.ExecuteSQLGetRS(sqlB.ToString());

                        //把所有的行装进list
                        List<object> taskList = new List<object>();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //吧每一行每一列装成一个字典
                                Dictionary<string, string> taskListDic = new Dictionary<string, string>();
                                taskListDic.Add("id", reader["id"].ToString());
                                taskListDic.Add("gid", reader["gid"].ToString());
                                taskListDic.Add("happennum", reader["happennum"].ToString());
                                taskListDic.Add("enternum", reader["enternum"].ToString());
                                taskListDic.Add("type", reader["type"].ToString());
                                taskListDic.Add("savetime", reader["savetime"].ToString());
                                taskListDic.Add("goodsname", reader["goodsname"].ToString());

                                taskList.Add(taskListDic);
                            }
                        }
                        reader.Close();

                        returnStr = BC_APIResult.GetAPIResult(taskList, (int)BC_APIResultStatus.FAIL, "执行查询Task的API!");

                        break;

                    }
                //修改用户信息
                case "stockChange":
                    {
                        string Stockgid = GetParameterByName("stockgid");
                        string Stockgoodsname = GetParameterByName("stockgoodsname");
                        string Stocktype = GetParameterByName("stocktype");
                        string Stockenternum = GetParameterByName("stockenternum");
                        string Stockhappennum = GetParameterByName("stockhappennum");
                        string StockUserID = GetParameterByName("stockUserID");

                        sqlB.Length = 0;

                        sqlB.AppendLine("UPDATE ");
                        sqlB.AppendLine("  kcrecord  ");
                        sqlB.AppendLine("  SET ");
                        sqlB.AppendLine(" gid = ");
                        sqlB.AppendLine($"    '{Stockgid}' ");
                        sqlB.AppendLine(", happennum = ");
                        sqlB.AppendLine($"    '{Stockhappennum}' ");
                        sqlB.AppendLine(", enternum = ");
                        sqlB.AppendLine($"    '{Stockenternum}' ");
                        sqlB.AppendLine(", type = ");
                        sqlB.AppendLine($"    '{Stocktype}' ");
                        sqlB.AppendLine(", savetime = ");
                        sqlB.AppendLine($"    NOW() ");
                        sqlB.AppendLine(", goodsname = ");
                        sqlB.AppendLine($"    '{Stockgoodsname}' ");
                        sqlB.AppendLine("  WHERE ");
                        sqlB.AppendLine("  id = ");
                        sqlB.AppendLine($"    '{int.Parse(StockUserID)}' ");
                        sqlB.AppendLine(";");

                        string sql = sqlB.ToString();


                        int affectedRow = BC_MySqlUtils.ExecuteSQL(sqlB.ToString());

                        returnStr = BC_APIResult.GetAPIResult(affectedRow, (int)BC_APIResultStatus.FAIL, "执行修改的API!");

                        break;

                    }

                //查找库存信息
                case "Adminlogistics":
                    {
                        sqlB.Length = 0;

                        sqlB.AppendLine("SELECT ");
                        sqlB.AppendLine(" loginfo.logid ");
                        sqlB.AppendLine(", loginfo.Shiptime ");
                        sqlB.AppendLine(", loginfo.recetime ");
                        sqlB.AppendLine(", loginfo.shraddr ");
                        sqlB.AppendLine(", loginfo.shrtel ");
                        sqlB.AppendLine(", loginfo.logname ");
                        sqlB.AppendLine(", loginfo.tel ");
                        sqlB.AppendLine(", loginfo.wlinfo ");
                        sqlB.AppendLine(", loginfo.email ");
                        sqlB.AppendLine(" FROM loginfo ");
                        sqlB.AppendLine(" LIMIT 30 ");
                        sqlB.AppendLine(";");

                        MySqlDataReader reader = BC_MySqlUtils.ExecuteSQLGetRS(sqlB.ToString());

                        //把所有的行装进list
                        List<object> taskList = new List<object>();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //吧每一行每一列装成一个字典
                                Dictionary<string, string> taskListDic = new Dictionary<string, string>();
                                taskListDic.Add("logid", reader["logid"].ToString());
                                taskListDic.Add("Shiptime", reader["Shiptime"].ToString());
                                taskListDic.Add("recetime", reader["recetime"].ToString());
                                taskListDic.Add("shraddr", reader["shraddr"].ToString());
                                taskListDic.Add("shrtel", reader["shrtel"].ToString());
                                taskListDic.Add("tel", reader["tel"].ToString());
                                taskListDic.Add("wlinfo", reader["wlinfo"].ToString());
                                taskListDic.Add("email", reader["email"].ToString());
                                taskListDic.Add("logname", reader["logname"].ToString());

                                taskList.Add(taskListDic);
                            }
                        }
                        reader.Close();

                        returnStr = BC_APIResult.GetAPIResult(taskList, (int)BC_APIResultStatus.FAIL, "执行查询Task的API!");

                        break;

                    }

                //管理员删除评价信息
                case "Admindellogistics":
                    {
                        string id = GetParameterByName("ID");

                        sqlB.Length = 0;

                        sqlB.AppendLine("DELETE ");
                        sqlB.AppendLine("   FROM  ");
                        sqlB.AppendLine("  loginfo ");
                        sqlB.AppendLine("  WHERE ");
                        sqlB.AppendLine("  logid ");
                        sqlB.AppendLine("  = ");
                        sqlB.AppendLine($"    '{int.Parse(id)}' ");
                        sqlB.AppendLine(";");

                        string sql = sqlB.ToString();

                        int affectedRow = BC_MySqlUtils.ExecuteSQL(sqlB.ToString());

                        returnStr = BC_APIResult.GetAPIResult(affectedRow, (int)BC_APIResultStatus.FAIL, "执行删除的API!");

                        break;

                    }
                //加入购物车
                case "joincar":
                    {
                        string goodid = GetParameterByName("ID");


                        sqlB.Length = 0;

                        sqlB.AppendLine("UPDATE ");
                        sqlB.AppendLine("  goods  ");
                        sqlB.AppendLine("  SET ");
                        sqlB.AppendLine(" delstatus = ");
                        sqlB.AppendLine($"  '1'  ");
                        sqlB.AppendLine("  WHERE ");
                        sqlB.AppendLine("  goodid = ");
                        sqlB.AppendLine($"    '{int.Parse(goodid)}' ");
                        sqlB.AppendLine(";");

                        string sql = sqlB.ToString();


                        int affectedRow = BC_MySqlUtils.ExecuteSQL(sqlB.ToString());

                        returnStr = BC_APIResult.GetAPIResult(affectedRow, (int)BC_APIResultStatus.FAIL, "执行修改的API!");

                        break;

                    }
                //更改商品数量
                case "goodsnum":
                    {
                        string goodid = GetParameterByName("ID");
                        string num = GetParameterByName("Num");


                        sqlB.Length = 0;

                        sqlB.AppendLine("UPDATE ");
                        sqlB.AppendLine("  goods  ");
                        sqlB.AppendLine("  SET ");
                        sqlB.AppendLine(" num = ");
                        sqlB.AppendLine($"    '{num}' ");
                        sqlB.AppendLine("  WHERE ");
                        sqlB.AppendLine("  goodid = ");
                        sqlB.AppendLine($"    '{int.Parse(goodid)}' ");
                        sqlB.AppendLine(";");

                        string sql = sqlB.ToString();


                        int affectedRow = BC_MySqlUtils.ExecuteSQL(sqlB.ToString());

                        returnStr = BC_APIResult.GetAPIResult(affectedRow, (int)BC_APIResultStatus.FAIL, "执行修改的API!");

                        break;

                    }
                //更改商品数量
                case "goodsdel":
                    {
                        string goodid = GetParameterByName("ID");


                        sqlB.Length = 0;

                        sqlB.AppendLine("UPDATE ");
                        sqlB.AppendLine("  goods  ");
                        sqlB.AppendLine("  SET ");
                        sqlB.AppendLine(" delstatus = ");
                        sqlB.AppendLine($"  '0'  ");
                        sqlB.AppendLine("  WHERE ");
                        sqlB.AppendLine("  goodid = ");
                        sqlB.AppendLine($"    '{int.Parse(goodid)}' ");

                        string sql = sqlB.ToString();


                        int affectedRow = BC_MySqlUtils.ExecuteSQL(sqlB.ToString());

                        returnStr = BC_APIResult.GetAPIResult(affectedRow, (int)BC_APIResultStatus.FAIL, "执行修改的API!");

                        break;

                    }
                case "goodsMessage":  //添加留言信息
                    {
                        string messagename = GetParameterByName("Messagename");
                        string contents = GetParameterByName("Contents");

                        sqlB.Length = 0;
                        sqlB.AppendLine("INSERT INTO ");
                        sqlB.AppendLine(" chat(");
                        sqlB.AppendLine(" chat.msg ");
                        sqlB.AppendLine(", chat.savetime ");
                        sqlB.AppendLine(", chat.chatname ");
                        sqlB.AppendLine(")");
                        sqlB.AppendLine(" VALUES(");
                        sqlB.AppendLine($" '{contents}' ");
                        sqlB.AppendLine($", NOW() ");
                        sqlB.AppendLine($", '{messagename}' ");
                        sqlB.AppendLine(")");
                        sqlB.AppendLine(";");

                        int affectedRow = BC_MySqlUtils.ExecuteSQL(sqlB.ToString());

                        returnStr = BC_APIResult.GetAPIResult(affectedRow, (int)BC_APIResultStatus.FAIL, "执行我们注册的API!");


                        break;
                    }
                case "joinmessage":  //回复留言信息
                    {
                        string messageid = GetParameterByName("Messageid");
                        string replyconcent = GetParameterByName("Replyconcent");


                        sqlB.Length = 0;

                        sqlB.AppendLine("UPDATE ");
                        sqlB.AppendLine("  chat  ");
                        sqlB.AppendLine("  SET ");
                        sqlB.AppendLine(" hfmsg = ");
                        sqlB.AppendLine($" '{replyconcent}' ");
                        sqlB.AppendLine("  WHERE ");
                        sqlB.AppendLine("  chatid = ");
                        sqlB.AppendLine($" '{int.Parse(messageid)}' ");
                        sqlB.AppendLine(";");

                        string sql = sqlB.ToString();


                        int affectedRow = BC_MySqlUtils.ExecuteSQL(sqlB.ToString());

                        returnStr = BC_APIResult.GetAPIResult(affectedRow, (int)BC_APIResultStatus.FAIL, "执行修改的API!");

                        break;
                    }
                case "goodspayinfo":  //添加物流信息
                    {
                        string goodspayinfo = GetParameterByName("Goodspayinfo");
                        string tel = GetParameterByName("Tel");
                        string addr = GetParameterByName("Addr");

                        sqlB.Length = 0;
                        sqlB.AppendLine("INSERT INTO ");
                        sqlB.AppendLine(" loginfo(");
                        sqlB.AppendLine(" loginfo.Shiptime ");
                        sqlB.AppendLine(", loginfo.shraddr ");
                        sqlB.AppendLine(", loginfo.shrtel ");
                        sqlB.AppendLine(", loginfo.logname ");
                        sqlB.AppendLine(", loginfo.tel ");
                        sqlB.AppendLine(", loginfo.wlinfo ");
                        sqlB.AppendLine(", loginfo.email ");
                        sqlB.AppendLine(")");
                        sqlB.AppendLine(" VALUES(");
                        sqlB.AppendLine($" NOW() ");
                        sqlB.AppendLine($", '{addr}' ");
                        sqlB.AppendLine($", '14578947523' ");
                        sqlB.AppendLine($", '{goodspayinfo}' ");
                        sqlB.AppendLine($", '{tel}' ");
                        sqlB.AppendLine($", '已支付,待出库' ");
                        sqlB.AppendLine($", '14278451@qq.com' ");
                        sqlB.AppendLine(")");
                        sqlB.AppendLine(";");

                        int affectedRow = BC_MySqlUtils.ExecuteSQL(sqlB.ToString());

                        returnStr = BC_APIResult.GetAPIResult(affectedRow, (int)BC_APIResultStatus.FAIL, "执行我们注册的API!");


                        break;
                    }
                default:
                    {
                        returnStr = BC_APIResult.GetAPIResult("", (int)BC_APIResultStatus.FAIL, "请检查APICommand名称是否正确!");
                        break;
                    }
            }
            await content.Response.WriteAsync(returnStr);
        }
        // 根据名称获取string类型参数
        private static string GetParameterByName(string ParameterName)
        {
            return PostData[ParameterName].ToString();
        }
    }
}


