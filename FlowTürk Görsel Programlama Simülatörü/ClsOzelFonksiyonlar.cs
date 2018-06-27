using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlowTürk_Görsel_Programlama_Simülatörü
{
    class ClsOzelFonksiyonlar
    {

        public static void GelenIfadeGuncelleme(int ID2, string GelenIslem, string Deger)
        {
            int GelenID = ID2;
            int ID = 0;
            string NesneAdi = "";
            string Turu = "";
            int Top = 0;
            int Left = 0;
            string Degeri = "";
            string Islem = "";

                //FrmAna frm = new FrmAna();

            //Yedekle
            foreach (_Nesneler Fonk in FrmAna._Nesne)
            {
                if (Fonk.ID == GelenID)
                {
                    ID = Fonk.ID;
                    NesneAdi = Fonk.NesneAdi;
                    Turu = Fonk.Turu;
                    Top = Fonk.Top;
                    Left = Fonk.Left;
                    Degeri = Deger;
                    Islem = GelenIslem;
                }
            }

            //Sil
            for (int l = 0; l < FrmAna._Nesne.Count; l++)
            {
                if (FrmAna._Nesne[l].ID == GelenID)
                {
                    FrmAna._Nesne.RemoveAt(l);
                    break;
                }
            }

            //Kaydet
            _Nesneler Fon = new _Nesneler();
            Fon.ID = ID;
            Fon.NesneAdi = NesneAdi;
            Fon.Turu = Turu;
            Fon.Top = Top;
            Fon.Left = Left;
            Fon.Degeri = Degeri;
            Fon.Islem = Islem;
            FrmAna._Nesne.Add(Fon);

        }

        public static void GelenDegiskenGuncelleme(string DegiskenAdiGelen, string GelenDegeri, string GelenTuru)
        {
            string DegiskenAdi = DegiskenAdiGelen;
            string Degeri = GelenDegeri;
            string Turu = GelenTuru;

            //Yedekle
            foreach (_Degiskenler Fonk in FrmAna._Degisken_Yedek)
            {
                if (Fonk.Degisken_Adi == DegiskenAdi)
                {
                    DegiskenAdi = Fonk.Degisken_Adi;
                    Turu = Fonk.Degisken_Turu;
                }
            }

            //Sil
            for (int l = 0; l < FrmAna._Degisken_Yedek.Count; l++)
            {
                if (FrmAna._Degisken_Yedek[l].Degisken_Adi == DegiskenAdi)
                {
                    FrmAna._Degisken_Yedek.RemoveAt(l);
                    break;
                }
            }

            //Kaydet
            _Degiskenler Fon = new _Degiskenler();
            Fon.Degisken_Adi = DegiskenAdi;
            Fon.Degeri = Degeri;
            Fon.Degisken_Turu = Turu;
            FrmAna._Degisken_Yedek.Add(Fon);
        }
    }
}