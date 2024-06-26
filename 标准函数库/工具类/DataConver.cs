using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 标准函数库
{
    //数据转换
    internal class DataConver
    {

        //32位16进制字符转为两个16位int型数据
        public static int[] HEXtoint32(string H32)
        {
            int[] ints = new int[2];
            int a = Convert.ToInt32(H32, 16);
            ints[0] = a % 65536;
            ints[1] = a / 65536;
            return ints;
        }

        //将32位字符转为两个十六位数据
        public static int[] int32toint16(string value)
        {
            int a = int.Parse(value);
            return new int[] { a % 65536, a / 65536 };
        }
        public static int[] int32toint16(int value)
        {
            return new int[] { value % 65536, value / 65536 };
        }


        //将两个十六位合成一个32位的数
        public static int int_to_int32(int[] values)
        {
            string s = values[1].ToString("X4") + values[0].ToString("X4");
            return Convert.ToInt32(s, 16);
        }

        //将int型数据按要求取几位小数并转为字符串
        public static string int_to_s(int value, int num)
        {
            double db = (double)value;

            return (db / Math.Pow(10, num)).ToString($"f{num}");
        }

        //从int型转为BOOL型
        public static bool[] int_to_bool(int[] data)
        {
            bool[] baojin = new bool[data.Length * 16];
            string ss = "";
            for (int i = 0; i < data.Length; i++)
            {
                string s = Convert.ToString(data[i], 2).PadLeft(16, '0');
                ss = s + ss;
            }
            for (int i = ss.Length - 1; i >= 0; i--)
            {
                baojin[ss.Length - 1 - i] = ss[i] == '1' ? true : false;
            }
            return baojin;
        }


        //把字符串转为对应的plc值(一个字两个字符,要进行高低位转换)
        public static int[] str_ascii(string str, int len)
        {
            int[] ints = new int[len];
            byte[] bytes = Encoding.ASCII.GetBytes(str);

            for (int i = 0; i < bytes.Length / 2; i++)
            {
                if (i >= len)
                    break;

                ints[i] = bytes[2 * i ] + (bytes[2 * i + 1] << 8);
            }

            if (bytes.Length % 2 == 1)
            {
                ints[bytes.Length / 2] = bytes[bytes.Length - 1];
            }

            return ints;
        }

        //把ASCII值转为字符串（一个字两个字符,要进行高低位转换）
        public static string ascii_str(int[] data)
        {
            string str = "";
            foreach (int i in data)
            {
                if (i == 0)
                    break;

                int i1 = i & 0x00ff;
                int i2 = i >> 8;


                str += (char)i1;
                if (i2 != 0)
                    str += (char)i2;
            }
            return str;
        }

        //计算时间差
        public static int timedifference(string stime)
        {
            DateTime dt1 = DateTime.Now;
            DateTime dt2 = Convert.ToDateTime(stime);
            TimeSpan tim = dt1.Subtract(dt2);
            return (int)tim.TotalSeconds;
        }
        public static int timedifference(DateTime dt)
        {
            DateTime dt1 = DateTime.Now;
            DateTime dt2 = Convert.ToDateTime(dt);
            TimeSpan tim = dt1.Subtract(dt2);
            return (int)tim.TotalSeconds;
        }

        //获取类的属性值并将其输出为一个集合(对建立的表进行转换)
        public static List<string> classtolist<T>(T t)
        {
            List<string> list_s=new List<string>();
            foreach (System.Reflection.PropertyInfo info in t.GetType().GetProperties())
            {
                if (info.GetValue(t) != null)
                {
                    string s = info.GetValue(t).ToString();
                    list_s.Add(s);
                }
                else
                    list_s.Add("");
            }
            return list_s;
        }


    }
}
