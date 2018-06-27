using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlowTürk_Görsel_Programlama_Simülatörü
{
    public partial class FrmGuncellemeIndirme : Form
    {
        public FrmGuncellemeIndirme()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        StreamWriter server;
        bool Cikis = true;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(progressBar1.Value <450)
            progressBar1.Value = progressBar1.Value + 1;

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            for (int i = 5; i >= 0; i--)
            {

                lblText.Text = i + " Saniye sonra güncelleme başlatılacak.";
                System.Threading.Thread.Sleep(1000);

            }

            lblText.Text = "Program Kapatılacak...";

            System.Threading.Thread.Sleep(2500);

            try
            {
                server = new StreamWriter(Application.StartupPath + "\\Srm.jmt");
                server.WriteLine(Application.ProductVersion);
            }
            catch { return; }
            finally
            {
                server.Flush();
                server.Close();
            }

                //timer1.Stop();
                System.Diagnostics.Process.Start(Application.StartupPath + "\\FlowTürk Update.exe");            
                Application.Exit();      
        }

        private void FrmGuncellemeIndirme_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void button10_Click(object sender, EventArgs e)
        {
         
            //backgroundWorker1.CancelAsync();
            Close();

        }
    }
}
