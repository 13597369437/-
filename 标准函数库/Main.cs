using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;
using 标准函数库;

namespace 标准函数库
{
    public partial class Main : UIForm
    {
        public Main()
        {
            InitializeComponent();
            uiTabControl1.TabVisible = false;
            uiNavMenu1.CreateNode(AddPage(new UserForm()));
        }

        void test()
        {
            MessageBox.Show("holle,word!");
        }

        void test1()
        {
            MessageBox.Show("holle,我是111仓库提供的");
        }

        void test3()
        {
            MessageBox.Show("holle,我是111仓库提供的v.1.1.3");
        }
    }
}
