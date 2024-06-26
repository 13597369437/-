using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeSql.DataAnnotations;

namespace MySQLTables
{
    //连接数据库
    public class DB
    {
        //static Lazy<IFreeSql> sqliteLazy = new Lazy<IFreeSql>(() => new FreeSql.FreeSqlBuilder()
        //      .UseMonitorCommand(cmd => Trace.WriteLine($"Sql：{cmd.CommandText}"))//监听SQL语句,Trace在输出选项卡中查看
        //      .UseConnectionString(FreeSql.DataType.MySql, $"server=127.0.0.1;Uid=root;password=root;Database=fusemachine;Charset=utf8")
        //      .UseAutoSyncStructure(true) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
        //      .Build());
        //public static IFreeSql MySQL => sqliteLazy.Value;

       
        //public static IFreeSql MySQL = new FreeSql.FreeSqlBuilder()
        //    .UseConnectionString(FreeSql.DataType.MySql, $"server=127.0.0.1;Uid=root;password=root;Database=imsl;Charset=utf8")
        //    .UseAutoSyncStructure(true) //自动同步实体结构到数据库
        //    .Build();

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="name">数据库名称</param>
        /// <returns></returns>
        public static IFreeSql MySQL1(string name)
        {
            return new FreeSql.FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.MySql, $"server=127.0.0.1;Uid=root;password=root;Database={name};Charset=utf8")
            .UseAutoSyncStructure(true) //自动同步实体结构到数据库
            .Build();
        }
    }
     
    #region 回流线智能化系统

    public class 工单记录
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public long id { get; set; }
        public string 工单号 { get; set; }
        public string 料号 { get; set; }
        public int 工单数量 { get; set; }
        public int 叠构号 { get; set; }
        public string 叠构ID { get; set; }
        public string 压机配方 { get; set; }
        public int 层数 { get; set; }
        public int 本数 { get; set; }
        public double 长 { get; set; }
        public double 宽 { get; set; }
        public double 厚 { get; set; }
        public double 重 { get; set; }
        public string 上传人员 { get; set; }
        public string 接收时间 { get; set; }
        public int 等待时间 { get; set; }
        public string 叠板机台 { get; set; }
        public int 已叠本数 { get; set; }
        public int 已拆本数 { get; set; }
        public string 开始时间 { get; set; }
        public string 结束时间 { get; set; }
        public int 执行耗时 { get; set; }
        public string 执行状态 { get; set; } //待生产  生产中 已完成
    }

    //工单参数
    public class 工单参数
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public long id { get; set; }
        public string 一段温度 { get; set; }
        public string 配方段温度 { get; set; }
        public string 三段温度 { get; set; }
        public string 一段升温时间 { get; set; }
        public string 配方升温时间 { get; set; }
        public string 三段升温时间 { get; set; }
        public string 一段恒温时间 { get; set; }
        public string 配方段恒温时间 { get; set; }
        public string 三段恒温时间 { get; set; }
        public string 一段压力 { get; set; }
        public string 二段压力 { get; set; }
        public string 三段压力 { get; set; }
        public string 熔头开关16 { get; set; }
        public string 熔头开关32 { get; set; }
        public string 冷却时间 { get; set; }
        public string 配方编号 { get; set; }
        public string 当前工单号 { get; set; }
        public string 起始层 { get; set; }
        public string 叠板层数 { get; set; }
        public string 套次 { get; set; }
        public string 产品总数 { get; set; }
        public string L1板边距离 { get; set; }
        public string L2板边距离 { get; set; }
        public string W1板边距离 { get; set; }
        public string W2板边距离 { get; set; }
    }

    public class 当前用户
    {
        [Column(IsPrimary = true)]
        public int ID { get; set; }
        public string 机台编号 { get; set; }
        public string 用户名 { get; set; }
        public string 姓名 { get; set; }
        public int 权限 { get; set; }
    }

    public class 配置
    {
        [Column(IsIdentity = false, IsPrimary = true)]
        public int id { get; set; }
        public string 设备ID { get; set; }
        public int 温度段数 { get; set; }
        public int 熔头数量 { get; set; }
        public string 工单来源 { get; set; }
        public string 压力值 { get; set; }
    }

    public class 异常记录
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public long ID { get; set; }
        public string 设备ID { get; set; }
        public string 工单号 { get; set; }
        public string 人员ID { get; set; }
        public string 异常ID { get; set; }
        public string 异常等级 { get; set; }
        public string 异常内容 { get; set; }
        public string 发生时间 { get; set; }
        public string 结束时间 { get; set; }
        public int 异常时长 { get; set; }
    }

    public class 报警清单
    {
        [Column(IsIdentity = false, IsPrimary = true)]
        public int ID { get; set; }
        public string 设备ID { get; set; }
        public int 报警ID { get; set; }
        public string 内容 { get; set; }
        public string 备注 { get; set; }
        public string 英文 { get; set; }
    }

    //需要预加载
    public class PLC地址
    {
        [Column(IsIdentity = false, IsPrimary = true)]
        public int PLCID { get; set; }
        public string 心跳 { get; set; }
        public int 心跳字长 { get; set; }
        public string 机台状态 { get; set; }
        public int  机台状态字长 { get; set; }
        public string 设备手自动 { get; set; }
        public int 设备手自动字长 { get; set; }
        public string EAP在线 { get; set; }
        public int EAP在线字长 { get; set; }
        public string 日期 { get; set; }
        public int 日期字长 { get; set; }
        public string 登录账号 { get; set; }
        public int 登录账号字长 { get; set; }
        public string 登录密码 { get; set; }
        public int 登录密码字长 { get; set; }
        public string 登录请求 { get; set; }
        public int 登录请求字长 { get; set; }
        public string 登录结果 { get; set; }
        public int 登录结果字长 { get; set; }
        public string 权限信息 { get; set; }
        public int 权限信息字长 { get; set; }
        public string 注销信号 { get; set; }
        public int 注销信号字长 { get; set; }
    }

    public class 料号信息暂存
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public long ID { get; set; }
        public string 工单号 { get; set; }
        public int 本数 { get; set; }


    }

    public class 载盘
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public long id { get; set; }
        public string 载盘ID { get; set; }
        public string 工单号 { get; set; }
        public string 型号 { get; set; }
        public int 本数 { get; set; }
        public string 状态 { get; set; }
    }

    public class 载盘记录
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public long id { get; set; }
        public string 载盘ID { get; set; }
        public string 工单号 { get; set; }
        public string 叠板机台 { get; set; }
        public string 叠板人员 { get; set; }
        public string 叠合时间 { get; set; }
        public string 叠构待定 { get; set; }
        public string 压机 { get; set; }
        public string 出压机时间 { get; set; }
        public string 出压机人员 { get; set; }
        public string 拆解机台 { get; set; }
        public string 拆解时间 { get; set; }
        public string 拆解人员 { get; set; }


    }

    //需要预加载
    public class 料架
    {
        [Column(IsPrimary = true)]
        public int id { get; set; }
        public int 料架号 { get; set; }
        public int 层号 { get; set; }
        public string 载盘ID { get; set; }
        public string 当前工单 { get; set; }//
        public string 存料 { get; set; }//记录工单的来向
        public string 存料时间 { get; set; }
        public string 取料 { get; set; }//记录工单的去向
        public int 有载 { get; set; }//1:有载 0无载
        public string 状态 { get; set; } //闲置 存料中 有料 取料中
    }

    //需要预加载id
    public class 载盘ID暂存
    {
        //id=1是G3出压机的载盘ID
        //id=2是G4出压机的载盘ID
        [Column(IsIdentity = true)]
        public long id { get; set; }
        public string 载盘ID { get; set; }
        public int 出料请求 { get; set; }
        public string P工单号 { get; set; }//上一次出料的P型号的P工单号
    }

    //需要预加载id
    public class 拆解载盘ID暂存
    {
        //id=1是B1的载盘ID
        //id=2是B2的载盘ID
        //id=3是B3的载盘ID
        //id=4是B4的载盘ID
        [Column(IsIdentity = true)]
        public int id { get; set; }
        public string 载盘ID { get; set; }
    }

    //需要预加载id
    public class 取料层号暂存
    {
        [Column(IsIdentity = true)]
        public int id { get; set; }
        public int 层号 { get; set; }
    }


    #endregion





    public class 用户管理
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }
        public string 用户名 { get; set; }
        public string 姓名 { get; set; }
        public string 密码 { get; set; }
        public string 权限 { get; set; }
    }


}
