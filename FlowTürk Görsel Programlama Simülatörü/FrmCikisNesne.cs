using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlowTürk_Görsel_Programlama_Simülatörü
{
    public partial class FrmCikisNesne : Form
    {
        public FrmCikisNesne()
        {
            InitializeComponent();
        }
      
        int GelenID = -1;

        #region OZEL FONKSIYONLAR
      
        public void TumDegiskenler()
        {
            foreach (_Nesneler Nes in FrmAna._Nesne)
            {
                string[] Degiskenler = Nes.Islem.Split(',');

                if (Nes.Turu == "Degiskenler")
                {
                    for (int i = 0; i < Degiskenler.Count(); i++)
                    {
                      comboBox1.Items.Add(Degiskenler[i].Split('=')[0]);
                    }
                }
            }
        }

        public void GelenIfade()
        {
            string NesneTurleri = "";
            string NesneIslem = "";
            string NesneDeger = "";
            GelenID = FrmAna.NesneID;

            foreach (_Nesneler Nes in FrmAna._Nesne)
            {
                if (Nes.ID == GelenID)
                {
                    NesneTurleri = Nes.NesneAdi;
                    NesneIslem = Nes.Islem;
                    NesneDeger = Nes.Degeri;
                }
            }

            txtIslem.Text = NesneIslem;
            comboBox1.Text = NesneDeger;
        }

        public void GelenIfadeGuncelle()
        {
            string Islem = txtIslem.Text.Trim();
            string Deger = comboBox1.Text.Trim();

            ClsOzelFonksiyonlar.GelenIfadeGuncelleme(GelenID, Islem,Deger);

            FrmAna.GuncelleString = Islem + "?," + Deger;
            FrmAna.PROGRAMGUNCELLE = true;
        }

        #endregion

        private void FrmCikisNesne_Load(object sender, EventArgs e)
        {
            TumDegiskenler();
            GelenIfade();
        }

        private void FrmCikisNesne_FormClosed(object sender, FormClosedEventArgs e)
        {
            GelenIfadeGuncelle();
        }
    }
}
