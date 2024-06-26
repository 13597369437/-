using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 标准函数库
{
    //文件导出类
    internal class Fileexport
    {
        //sunny 3.6.5
        public static void ShowErrorDialog(string msg, bool showMask = false)
        {
            UIMessageDialog.ShowMessageDialog(msg, UILocalize.ErrorTitle, showCancelButton: false, UIStyle.Red, showMask);
        }
        public static bool ShowAskDialog(string msg, bool showMask = false, UIMessageDialogButtons defaultButton = UIMessageDialogButtons.Ok)
        {
            return UIMessageDialog.ShowMessageDialog(msg, UILocalize.AskTitle, showCancelButton: true, UIStyle.Blue, showMask, topMost: true, defaultButton);
        }

        //导出数据库，将数据库用freensql读取到List再进行导出
        public static void SelectDataToExport<T>(List<T> wips)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("序号");
            foreach (System.Reflection.PropertyInfo info in wips[0].GetType().GetProperties())
            {
                if (info.Name != "id")
                    dataTable.Columns.Add(info.Name);
            }
            // 添加行数据
            int j = 1;
            foreach (var w in wips)
            {
                DataRow row = dataTable.Rows.Add();
                int i = 1;

                row[0] = j;
                foreach (System.Reflection.PropertyInfo info in w.GetType().GetProperties())
                {
                    if (info.Name != "id")
                    {
                        string s = info.GetValue(w).ToString();
                        row[i] = s;
                        i++;
                    }
                }
                j++;
            }
            ExportDataToExcel(dataTable, "导出报表");
        }


        //选择要导出的数据
        public static void SelectDataToExport(UIDataGridView Udgv)
        {
            DataTable dataTable = new DataTable();
            // 添加列定义
            foreach (DataGridViewColumn dataGridViewColumn in Udgv.Columns)
            {
                dataTable.Columns.Add(dataGridViewColumn.HeaderText, dataGridViewColumn.ValueType);
            }

            // 添加行数据
            foreach (DataGridViewRow dataGridViewRow in Udgv.Rows)
            {
                if (!dataGridViewRow.IsNewRow)
                {
                    DataRow row = dataTable.Rows.Add();
                    foreach (DataGridViewCell dataGridViewCell in dataGridViewRow.Cells)
                    {
                        row[dataGridViewCell.ColumnIndex] = dataGridViewCell.Value;
                    }
                }
            }

            int rows = Udgv.CurrentRow.Index;
            ExportDataToExcel(dataTable, "导出报表");
        }

        //导出数据到Excel
        public static void ExportDataToExcel(DataTable TableName, string FileName)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //设置文件标题
            saveFileDialog.Title = "导出Excel文件";
            //设置文件类型
            saveFileDialog.Filter = "Excel 工作簿(*.xlsx)|*.xlsx|Excel 97-2003 工作簿(*.xls)|*.xls";
            //设置默认文件类型显示顺序  
            saveFileDialog.FilterIndex = 1;
            //是否自动在文件名中添加扩展名
            saveFileDialog.AddExtension = true;
            //是否记忆上次打开的目录
            saveFileDialog.RestoreDirectory = true;
            //设置默认文件名
            saveFileDialog.FileName = FileName;
            //按下确定选择的按钮  
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //获得文件路径 
                string localFilePath = saveFileDialog.FileName.ToString();

                //数据初始化
                int TotalCount;     //总行数

                TotalCount = TableName.Rows.Count;


                //NPOI
                IWorkbook workbook;
                string FileExt = Path.GetExtension(localFilePath).ToLower();
                if (FileExt == ".xlsx")
                {
                    workbook = new XSSFWorkbook();
                }
                else if (FileExt == ".xls")
                {
                    workbook = new HSSFWorkbook();
                }
                else
                {
                    workbook = null;
                }
                if (workbook == null)
                {
                    return;
                }
                ISheet sheet = string.IsNullOrEmpty(FileName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(FileName);


                try
                {
                    //读取标题  
                    IRow rowHeader = sheet.CreateRow(0);
                    for (int i = 0; i < TableName.Columns.Count; i++)
                    {
                        ICell cell = rowHeader.CreateCell(i);
                        cell.SetCellValue(TableName.Columns[i].ColumnName);
                    }

                    //读取数据  
                    for (int i = 0; i < TableName.Rows.Count; i++)
                    {
                        IRow rowData = sheet.CreateRow(i + 1);
                        for (int j = 0; j < TableName.Columns.Count; j++)
                        {
                            ICell cell = rowData.CreateCell(j);
                            cell.SetCellValue(TableName.Rows[i][j].ToString());
                        }

                    }


                    //转为字节数组  
                    MemoryStream stream = new MemoryStream();
                    workbook.Write(stream);
                    var buf = stream.ToArray();

                    //保存为Excel文件  
                    using (FileStream fs = new FileStream(localFilePath, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(buf, 0, buf.Length);
                        fs.Flush();
                        fs.Close();
                    }


                    //成功提示
                    if (ShowAskDialog("导出成功，是否立即打开？"))
                    {
                        System.Diagnostics.Process.Start(localFilePath);
                    }


                }
                catch (Exception ex)
                {
                    ShowErrorDialog("文件导出异常：" + ex.Message);
                    MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }

        }


        //写入csv
        public static void WriteCsv(string result)
        {
            string path = Directory.GetCurrentDirectory();//获取当前工作目录
            string fileName = path + "\\peizhi.csv";//文件名
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(fileName))
            {
                StreamWriter sw = new StreamWriter(fileName, true, Encoding.UTF8);
                string str1 = "配置名称" + "," + "配置值" + "\t\n";
                sw.Write(str1);
                sw.Close();
            }
            StreamWriter swl = new StreamWriter(fileName, true, Encoding.UTF8);
            string str = result + "\t\n";
            swl.Write(str);
            swl.Close();


        }

        //更新CSV
        public static void WriteCsv(List<string> result)
        {
            string path = Directory.GetCurrentDirectory();//获取当前工作目录
            string fileName = path + "\\peizhi.csv";//文件名
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8);
            string str1 = "配置名称" + "," + "配置值" + "\t\n";
            sw.Write(str1);
            foreach (string s in result)
            {
                sw.Write(s + "\t\n");
            }
            sw.Close();
        }

        //读取csv
        public static void ReadCsv(string path, out List<string> data)
        {
            StreamReader sr;
            data = new List<string>();
            try
            {
                using (sr = new StreamReader(path, Encoding.GetEncoding("GB2312")))
                {
                    string str = "";
                    while ((str = sr.ReadLine()) != null)
                    {
                        data.Add(str);
                    }
                }
            }
            catch (Exception ex)
            {
                foreach (Process process in Process.GetProcesses())
                {
                    if (process.ProcessName.ToUpper().Equals("EXCEL"))
                        process.Kill();
                }
                GC.Collect();
                Thread.Sleep(10);
                Console.WriteLine(ex.StackTrace);
                using (sr = new StreamReader(path, Encoding.GetEncoding("GB2312")))
                {
                    string str = "";
                    while ((str = sr.ReadLine()) != null)
                    {
                        data.Add(str);
                    }
                }
            }

        }

        //导出到csv文件
        public void DataGridViewToExcel(UIDataGridView dgv)
        {
            //程序实例化SaveFileDialog控件，并对该控件相关参数进行设置
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Execl files (*.csv)|*.csv";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.CreatePrompt = true;
            dlg.Title = "保存为csv文件";
            //以上过程也可以通过添加控件，再设置控件属性完成，此处用程序编写出来了，在移植时就可摆脱控件的限制

            if (dlg.ShowDialog() == DialogResult.OK)//打开SaveFileDialog控件，判断返回值结果
            {
                Stream myStream;//流变量
                myStream = dlg.OpenFile();//返回SaveFileDialog控件打开的文件，并将所选择的文件转化成流
                StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));//将选择的文件流生成写入流
                string columnTitle = "";
                try
                {
                    //写入列标题    
                    for (int i = 0; i < dgv.ColumnCount; i++)
                    {
                        if (i > 0)
                        {
                            columnTitle += ",";
                        }
                        columnTitle += dgv.Columns[i].HeaderText;//符号 ， 的添加，在保存为Excel时就以 ， 分成不同的列了
                    }

                    sw.WriteLine(columnTitle);//将内容写入文件流中

                    //写入列内容    
                    for (int j = 0; j < dgv.Rows.Count; j++)
                    {
                        string columnValue = "";
                        for (int k = 0; k < dgv.Columns.Count; k++)
                        {
                            if (k > 0)
                            {
                                columnValue += ",";
                            }
                            if (dgv.Rows[j].Cells[k].Value == null)
                                columnValue += "";
                            else if (dgv.Rows[j].Cells[k].Value.ToString().Contains(","))
                            {
                                columnValue += "\"" + dgv.Rows[j].Cells[k].Value.ToString().Trim() + "\"";//将单元格中的，号转义成文本
                            }
                            else
                            {
                                columnValue += dgv.Rows[j].Cells[k].Value.ToString().Trim() + "\t";//\t 横向跳格
                            }
                        }//获得写入到列中的值
                        sw.WriteLine(columnValue);//将内容写入文件流中
                    }
                    sw.Close();//关闭写入流
                    myStream.Close();//关闭流变量
                    ShowSuccessTip("导出表格成功！");
                }
                catch (Exception e)
                {
                    ShowErrorDialog("导出表格失败！");
                }
                finally
                {
                    sw.Close();
                    myStream.Close();
                }
            }
            else
            {
                ShowInfoTip("取消导出表格操作!");
            }
        }


    }
}
   