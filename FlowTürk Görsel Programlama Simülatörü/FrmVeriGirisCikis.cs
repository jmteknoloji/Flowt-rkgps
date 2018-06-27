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
    public partial class FrmVeriGirisCikis : Form
    {
        public FrmVeriGirisCikis()
        {
            InitializeComponent();
        }

        public static int DegerGirildi = 0;
        public int ID = 0;
        public string DegiskenTuru = "";      
        public string DegiskenAdi = "";
        public string StandartDeger = "";

        public bool DEGERGIRILDI = false;
        string DosyaYolu = Application.StartupPath + "\\Settings\\CalismaHizi.fgps";

        private void trackBar1_Scroll(object sender, EventArgs e)
        { 
            label1.Text = trackBar1.Value + " Ms";          
        }

        private void FrmVeriGirisCikis_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!((FrmAna)Application.OpenForms["FrmAna"]).toolStripButton12.Enabled)
            {
                ((FrmAna)Application.OpenForms["FrmAna"]).toolStripButton12.Enabled = true;
                ((FrmAna)Application.OpenForms["FrmAna"]).toolStripButton11.Enabled = false;
            }
            
            FrmAna._Degisken_Yedek.Clear();

            foreach (PictureBox pic in FrmAna.picturebox)
            {
                if (pic != null)
                    pic.Cursor = Cursors.SizeAll;
            }

            FrmAna.PROGRAMDURDU = true;
            FrmAna.PROGRAMCALISIYOR = false;

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

        private void FrmVeriGirisCikis_Load(object sender, EventArgs e)
        {
            string[] dizi = System.IO.File.ReadAllLines(DosyaYolu);
            trackBar1.Value = Convert.ToInt16(dizi[0]);
            label1.Text = dizi[0].ToString() + " ms";
            richTextBox1.SelectionStart = DegerGirildi;
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
           string GirilenDeger="";
           StandartDeger="";

           //if (DEGERGIRILDI)
           //    richTextBox1.ReadOnly = false;

            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            try
            {
                int SecilenYaziLength = richTextBox1.GetFirstCharIndexOfCurrentLine();
                int SecilenSatir = richTextBox1.GetLineFromCharIndex(SecilenYaziLength);
                GirilenDeger = richTextBox1.Lines[SecilenSatir];
            }
            catch { }

            if (e.KeyValue != 8)
               if (GirilenDeger.Length > DegerGirildi+12)
                    e.SuppressKeyPress = true;
            
            if (GirilenDeger.Length <= DegerGirildi)
                if (e.KeyValue == 8)
                    e.SuppressKeyPress = true;
            
            if (richTextBox1.SelectionStart < DegerGirildi)
                    e.SuppressKeyPress = true;

           
            if (!DEGERGIRILDI)
            {
                e.SuppressKeyPress = true;
            }
            if (GirilenDeger.Length <= DegerGirildi)
            {
                if (e.KeyData == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                }
            }
            else
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (GirilenDeger != "")
                    {
                        GirilenDeger = GirilenDeger.Substring(DegerGirildi, GirilenDeger.Length - DegerGirildi);
                        ClsOzelFonksiyonlar.GelenDegiskenGuncelleme(DegiskenAdi, GirilenDeger, DegiskenTuru);
                    }
                       
                    richTextBox1.AppendText("\n");
                    richTextBox1.ReadOnly = true;
                    DEGERGIRILDI = false;
                    FrmAna.PROGRAMBEKLET = false;
                   
                }
            }
     
        }
      
        private void richTextBox1_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (DEGERGIRILDI)
                richTextBox1.ReadOnly = false;
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (DEGERGIRILDI)
                richTextBox1.ReadOnly = false;
        }

        private void FrmVeriGirisCikis_Shown(object sender, EventArgs e)
        {
            richTextBox1.Focus();
        }
    }
}
