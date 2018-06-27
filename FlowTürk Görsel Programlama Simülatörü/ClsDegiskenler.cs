using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlowTürk_Görsel_Programlama_Simülatörü
{
        public struct _Cizgiler
        {  
            public int Baslangic;
            public int Bitis;
            public Point P1;
            public Point P2;           
            public string Yazi;
        }

        public struct _Nesneler
        {
            public int ID;          
            public string NesneAdi;
            public string Turu;    
            public int Top;
            public int Left;
            public string Degeri;
            public string Islem;                   
        }

        public struct _LabelText
        {
            public int ID;
            public string NesneAdi;
            public string Yazi;
            public string Degiskeni;
        }

        public struct _Degiskenler
        {
            public string Degisken_Turu;
            public string Degisken_Adi;
            public string Degeri;
        }

        public struct _GeriYukle
        {
            public int ID;
            public string YapilanIslem;
            public string NesneAdi;
            public string Turu;
            public int Top;
            public int Left;
            public string Degeri;
            public string Islem;
            public int Baslangic;
            public int Bitis;
            public Point P1;
            public Point P2;
            public string Yazi;
    }

}
