using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDAnXi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int curPos;
        public int total;

        private delegate void SetPos(int ipos, int total, string vinfo);//代理
        private void SetTextMesssage(int ipos ,int total, string vinfo)
        {
            if (this.InvokeRequired)
            {
                SetPos setpos = new SetPos(SetTextMesssage);
                this.Invoke(setpos, new object[] { ipos, total, vinfo });
            }
            else
            {
                this.label4.Text = ipos.ToString() + "/"+total;
                this.progressBar1.Value = Convert.ToInt32(ipos/total*100);
                this.listBox1.Items.Add(vinfo);
            }
        }
        private delegate void Trig();//代理
        private void TrigButton()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Trig(TrigButton));
            }
            else
            {
                button1.Enabled = !button1.Enabled;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(textBox1.Text) || numericUpDown1.Value < 0)
            {
                listBox1.Items.Add("输入数据不合法,无法干他娘的");
            }
            else 
            {
                total = (int)numericUpDown1.Value;
                TrigButton();
                Thread fThread = new Thread(new ThreadStart(SleepT));
                fThread.Start();
            }

            



        }
        private void SleepT()
        {
            for (int i = 1; i <= total; i++)
            {
                CreateFile();
                SetTextMesssage(i, total, "正在生成第"+i+"个文件..." + "\r\n");
            }
            TrigButton();
        }


        public void CreateFile() 
        {
            var guid = Guid.NewGuid().ToString();
            List<byte> bytes = new List<byte>();
            
            for (int i = 0; i <50000000; i++)
            {
                bytes.AddRange(Guid.NewGuid().ToByteArray());
            }
            FileBinaryConvertHelper.Bytes2File(bytes.ToArray(), Path.Combine(textBox1.Text, guid + ".mp3") );


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://shang.qq.com/wpa/qunwpa?idkey=ba2343253c2c6ef38d6948aa66914c8af8ebb78f4a2841ba8f729b1f395b8b0a");

        }
    }
}
