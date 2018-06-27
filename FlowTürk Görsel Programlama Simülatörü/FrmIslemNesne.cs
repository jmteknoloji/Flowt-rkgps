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
    public partial class FrmIslemNesne : Form
    {
        public FrmIslemNesne()
        {
            InitializeComponent();
        }

        #region DEGISKENLER

        int GelenID = -1;


        #endregion

        #region OZEL FONKSIYONLAR
      
        public void TumIslemler()
        {

            foreach (_Nesneler Nes in FrmAna._Nesne)
            {
                if (Nes.Turu== "Islem")
                {
                    ListViewItem Liste = new ListViewItem(Nes.NesneAdi);
                    Liste.SubItems.Add(Nes.Degeri);
                    Liste.SubItems.Add(Nes.ID.ToString());
                    listView1.Items.Add(Liste);
                }
            }
        }

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
            string NesneIslem="";
            string[] parcala;
            
            GelenID = FrmAna.NesneID;

            foreach (_Nesneler Nes in FrmAna._Nesne)
            {
                if (Nes.ID == GelenID)
                {
                    NesneTurleri = Nes.NesneAdi;
                    NesneIslem=Nes.Islem;
                }
            }

            parcala = NesneIslem.Split('~');
            txtIslem.Text = parcala[0];

            for (int i = 1; i < parcala.Count(); i++)
            {
                listBox1.Items.Add(parcala[i].ToString());
            }

        }

        public void GelenIfadeGuncelle()
        {
            string Islem = txtIslem.Text.Trim();

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                Islem += "~" + listBox1.Items[i];
            }

            ClsOzelFonksiyonlar.GelenIfadeGuncelleme(GelenID,Islem,"");

            FrmAna.GuncelleString = Islem+"?";
            FrmAna.PROGRAMGUNCELLE = true;
        }

        #endregion

        private void button5_Click(object sender, EventArgs e)
        {

            if (txtIslem.Text.Trim() != "")
            {
                listBox1.Items.Add(txtIslem.Text.Trim());
                txtIslem.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            if (listBox1.SelectedItems.Count > 0)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FrmIslemNesne_Load(object sender, EventArgs e)
        {
            GelenIfade();
            TumIslemler();
            TumDegiskenler();
        }

        private void FrmIslemNesne_FormClosed(object sender, FormClosedEventArgs e)
        {
            GelenIfadeGuncelle();
        }
    }
}
