using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;


namespace wf09
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Process[] processes;
        Thread show;

        private void button1_Click(object sender, EventArgs e)
        {
            if(show == null)
            {
                show = new Thread(ShowProcesses);
                show.Start();
            }
           
        }

        private void Form1_Load(object sender, EventArgs e)

        {
     
            listBox1.Items.Add(
                String.Format("{0, -5} {1, -7} {2}", "No", "id", "name"
                ));
            timer1.Interval = 5000;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
            
            
        }
        private void ShowProcesses()
        {
            processes = Process.GetProcesses();
            int n = processes.Length;
            this.Invoke(new Action(() => {
                for (int i = 0; i < n; i++)
                {
                    listBox1.Items.Add(
                   String.Format("{0, -5} {1, -7} {2}", i + 1, processes[i].Id, processes[i].ProcessName));
                }
            }));
            show = null;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listBox2.Items.Clear();
            Process process = processes[listBox1.SelectedIndex - 1];
            listBox2.Items.Add(process.ProcessName);
            for(int i = 0; i < process.Threads.Count; i++)
            {
                listBox2.Items.Add(process.Threads[i].Id + " " + process.Threads[i].ThreadState);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ShowProcesses();
        }
   
    }
}
