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
    public partial class FrmDegiskenNesne : Form
    {
        public FrmDegiskenNesne()
        {
            InitializeComponent();
        }

        int GelenID = -1;          

        #region OZEL FONKSIYONLAR

        public void TumDegiskenler()
        {
            listView2.Items.Clear();

            foreach (_Nesneler Nes in FrmAna._Nesne)
            {
                string[] Degiskenler = Nes.Islem.Split(',');

                if (Nes.Turu == "Degisken")
                {
                    for (int i = 0; i < Degiskenler.Count(); i++)
                    {
                        ListViewItem Liste = new ListViewItem(Degiskenler[i].Split('=')[0]);
                        Liste.SubItems.Add(Degiskenler[i].Split('=')[1]);
                        Liste.SubItems.Add(Degiskenler[i].Split('=')[1]);
                        listView2.Items.Add(Liste);
                    }
                }
            }
        }

        public void GelenIfade()
        {
            string NesneTurleri = "";
            string NesneIslem = "";
            string[] parcala;

            GelenID = FrmAna.NesneID;

            foreach (_Nesneler Nes in FrmAna._Nesne)
            {
                if (Nes.ID == GelenID)
                {
                    NesneTurleri = Nes.NesneAdi;
                    NesneIslem = Nes.Islem;
                }
            }

            parcala = NesneIslem.Split(',');
            txtIslem.Text = NesneIslem;

            for (int i = 0; i < parcala.Count(); i++)
            {
                listBox1.Items.Add(parcala[i].ToString());
            }

        }

        public void GelenIfadeGuncelle()
        {
            string Islem = txtIslem.Text.Trim();

            ClsOzelFonksiyonlar.GelenIfadeGuncelleme(GelenID, Islem,"");

            FrmAna.GuncelleString = Islem+"?";
            FrmAna.PROGRAMGUNCELLE = true;
        }

        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FrmDegiskenNesne_Load(object sender, EventArgs e)
        {
            TumDegiskenler();
            GelenIfade();
        }

        private void FrmDegiskenNesne_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void FrmDegiskenNesne_FormClosing(object sender, FormClosingEventArgs e)
        {
            GelenIfadeGuncelle();
        }
    }
}
