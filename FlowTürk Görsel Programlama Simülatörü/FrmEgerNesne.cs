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
    public partial class FrmEgerNesne : Form
    {
        public FrmEgerNesne()
        {
            InitializeComponent();
        }
        int GelenID = -1;

        #region OZEL FONKSIYONLAR

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
        }

        public void GelenIfadeGuncelle()
        {
            string Islem = txtIslem.Text.Trim();

            ClsOzelFonksiyonlar.GelenIfadeGuncelleme(GelenID, Islem, "");

            FrmAna.GuncelleString = Islem+"?";
            FrmAna.PROGRAMGUNCELLE = true;
        }
       
        #endregion

        private void FrmEgerNesne_Load(object sender, EventArgs e)
        {
            GelenIfade();
        }

        private void FrmEgerNesne_FormClosed(object sender, FormClosedEventArgs e)
        {
            GelenIfadeGuncelle();
        }
    }
}
