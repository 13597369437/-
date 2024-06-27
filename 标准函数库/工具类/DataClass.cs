using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySQLTables;

namespace 标准函数库 
{
    //全局变量
    internal class DataClass
    {
        public static int wddindex = 0;//配置温度段索引
        public static int rtnumindex = 0;//配置熔头数索引
        public static bool datasourceindex = false;//是否在线获取参数
        public static string User = "";//当前登录的ID
        public static string Name = "";//当前登录的用户名
        public static int Power = 0;//当前登录的用户权限
        public static int value_D7702 = 0;//设备待料状态


        public static int[] data_D7700 = new int[10];//设备状态信息
        public static int[] data_D7700_jl = new int[10];//记录设备状态信息

        public static int rtnum = 12;//熔头数量
        public static bool yali = false;//是否设置压力
        public static bool language = true;//中英文切换
        public static string sbID = "";//设备ID
        public static string[] wdd = new[] { "1", "2", "3" };//温度段数
        public static string[] rtnums = new[] {"8", "12", "16", "20", "24"};//熔头数量

        public static List<string> peizhivalues = new List<string>();


        //读取配置信息
        public static void readpeizhi(IFreeSql sql)
        {
            var peizhi = sql.Select<配置>().ToOne();

            sbID = peizhi.设备ID;
        
        }
    }
}
