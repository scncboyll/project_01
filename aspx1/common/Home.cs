using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace TaskSystem
{
    public class BC_Home
    {
        // 页面跳转实现
        public static async Task ProcessRequest(HttpContext context, IApplicationBuilder builder)
        {
            string returnStr = "";
            // 获取DashboardID
            string DashboardID = context.Request.Query["DashboardID"];

            switch (DashboardID)
            {
                case "0":    //用户登录
                    {
                        returnStr = LoadHTML("system/login");
                        break;
                    }
                case "1":
                    {
                        returnStr = LoadHTML("task/TaskList");
                        break;
                    }
                case "2":  //用户注册
                    {
                        returnStr = LoadHTML("system/Register");
                        break;
                    }
                case "3":
                    {
                        returnStr = LoadHTML("task/AddTask");
                        break;
                    }
                case "4":  //管理首页
                    {
                        returnStr = LoadHTML("system/adminIndex");
                        break;
                    }
                case "5":   //管理员登录
                    {
                        returnStr = LoadHTML("system/adminlogin");
                        break;
                    }
                case "6":  //管理员注册
                    {
                        returnStr = LoadHTML("system/adminRegister");
                        break;
                    }
                case "7":  //商城首页
                    {
                        returnStr = LoadHTML("system/index");
                        break;
                    }
                case "8":  //会员管理
                    {
                        returnStr = LoadHTML("system/administrator");
                        break;
                    }
                case "9":  //会员信息修改
                    {
                        returnStr = LoadHTML("system/adminchange");
                        break;
                    }
                case "10":  //留言管理
                    {
                        returnStr = LoadHTML("system/adminmessage");
                        break;
                    }
                case "11":  //商品管理
                    {
                        returnStr = LoadHTML("system/admingoods");
                        break;
                    }
                case "12":  //评价管理
                    {
                        returnStr = LoadHTML("system/adminevaluate");
                        break;
                    }
                case "13":  //库存管理
                    {
                        returnStr = LoadHTML("system/adminstock");
                        break;
                    }
                case "14":  //物流管理
                    {
                        returnStr = LoadHTML("system/adminlogistics");
                        break;
                    }
                case "15":  //购物车
                    {
                        returnStr = LoadHTML("goods/goodscar");
                        break;
                    }
                case "16":  //留言板
                    {
                        returnStr = LoadHTML("goods/goodsmessage");
                        break;
                    }
                case "17":  //商品详情
                    {
                        returnStr = LoadHTML("goods/goodsinfo");
                        break;
                    }
                case "18":  //支付页面
                    {
                        returnStr = LoadHTML("goods/goodspay");
                        break;
                    }
                case "19":  //支付页面
                    {
                        returnStr = LoadHTML("goods/goodscenter");
                        break;
                    }
                default:  //返回用户登录页面
                    {
                        returnStr = LoadHTML("system/login");
                        break;
                    }
            }
            await context.Response.WriteAsync(returnStr);
        }

        // 跳转页面方法
        private static string LoadHTML(string HtmlName)
        {
            StringBuilder HtmlB = new StringBuilder();
            string HtmlPath = Startup.ApplicationBasePath + "/wwwroot/html/" + HtmlName + ".html";

            using (StreamReader sr = new StreamReader(HtmlPath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    HtmlB.AppendLine(line);
                }
            }
            return HtmlB.ToString();
        }
    }
}