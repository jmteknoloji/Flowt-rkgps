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
    public partial class FrmCalismaHizi : Form
    {
        public FrmCalismaHizi()
        {
            InitializeComponent();
        }

        string DosyaYolu = Application.StartupPath + "\\Settings\\CalismaHizi.fgps";

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value + " Ms";
        }

        private void FrmCalismaHizi_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void FrmCalismaHizi_Load(object sender, EventArgs e)
        {
            string[] dizi = System.IO.File.ReadAllLines(DosyaYolu);
            trackBar1.Value =Convert.ToInt16(dizi[0]);
            label1.Text = dizi[0].ToString() + " ms";
        }

        private void FrmCalismaHizi_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.Delete(DosyaYolu);

            StreamWriter SonDosyalar = null;

            try
            {
                SonDosyalar = new StreamWriter(DosyaYolu, true);
            }
            catch { }
            SonDosyalar.WriteLine(trackBar1.Value);
            SonDosyalar.Flush();
            SonDosyalar.Close();
        }
    }
}
