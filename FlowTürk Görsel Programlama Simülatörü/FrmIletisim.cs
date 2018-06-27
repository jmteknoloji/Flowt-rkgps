using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace FlowTürk_Görsel_Programlama_Simülatörü
{
    public partial class FrmIletisim : Form
    {
        public FrmIletisim()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        SqlConnection baglan = new SqlConnection(ClsDataBase.ServerDataBase);

        int maus_yukselik, maus_sol, fark_sol, fark_yukseklik;
        bool secildi = false;

        [DllImport("JMT.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern static int l();

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,  // Sol üst köşesi x-koordinatı 
            int nTopRect,  // y koordinatı sol üst köşesi 
            int nRightRect,  // x-koordinatı sağ alt köşesi 
            int nBottomRect,  // sağ alt köşe y-koordinatı 
            int nWidthEllipse,  // elips yüksekliği 
            int nHeightEllipse // Elips genişliği 
         );

        private void FrmIletisim_Load(object sender, EventArgs e)
        {
             try
             {
                SqlCommand command = new SqlCommand("select tel from hakkimda", this.baglan);
                string tel = "";
                baglan.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    tel = reader[0].ToString();
                }
                reader.Close();
                label3.Text += tel.ToString();
                baglan.Close();

            }
             catch {  label3.Text += "+90 (545) 854 08 97"; }
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
         
        }

        private void ac_Tick(object sender, EventArgs e)
        {
             if (this.Opacity <= 1.0)   // Eğer formun opacity değer % 100 den az ise;
            {
                this.Opacity += 0.1;   // bu değeri % 10 arttır..
            }
            if (this.Opacity == 1.0)
            {
                ac.Stop(); 
            }
        }

        private void FrmIletisim_MouseMove(object sender, MouseEventArgs e)
        {
            if (secildi)
            {
                fark_yukseklik = maus_yukselik - e.Y;
                fark_sol = maus_sol - e.X;

                this.Top = this.Top - fark_yukseklik;
                this.Left = this.Left - fark_sol;
            }
        }

        private void FrmIletisim_MouseUp(object sender, MouseEventArgs e)
        {
            secildi = false;
        }

        private void kapa_Tick(object sender, EventArgs e)
        {
              if (this.Opacity >= 0.0)   // Eğer formun opacity değer % 100 den az ise;
            {

                this.Opacity -= 0.1;   // bu değeri % 10 arttır..

            }
            if (this.Opacity == 0.0)
            {
                Close();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            kapa.Start();
        }

        private void FrmIletisim_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left))
            {          
                maus_yukselik = e.Y;
                maus_sol = e.X;
                secildi = true;
            }
        }
    }
 }

