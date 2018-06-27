using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace FlowTürk_Görsel_Programlama_Simülatörü
{
    public partial class FrmAna : Form
    {
        public FrmAna()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
         public FrmAna(string file)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            
            ProjeAc(file);
        
         }  

        #region DEGİSKENLER

        string PROGRAM_ADI = "FlowTürk Görsel Programlama Simülatörü "+Application.ProductVersion+" (Beta Sürüm)";
        FrmVeriGirisCikis _FrmVeriGirisCikis;
        FrmCalismaHizi _FrmCalisma;
        FrmAyarlar _FrmAyarlar;
        FrmGuncellemePaneli _FrmGuncellemePaneli;

        SqlConnection ServerDateBase = new SqlConnection(ClsDataBase.ServerDataBase);

        public static PictureBox[] picturebox = new PictureBox[1000];
        public static Label[] TextLabel = new Label[1000];
        Label[] TextLabelEger = new Label[1000];

        public static List<_Cizgiler> _Cizgi = new List<_Cizgiler>();
        public static List<_Degiskenler> _Degisken_Yedek = new List<_Degiskenler>();
        public static List<_Degiskenler> _Degisken = new List<_Degiskenler>();
        public static List<_Nesneler> _Nesne = new List<_Nesneler>();

        List<_Cizgiler> _Cizgi_Yedek = new List<_Cizgiler>();
        List<_GeriYukle> _GeriIleriYukle = new List<_GeriYukle>();
        List<_Nesneler> _SecilenNesneler = new List<_Nesneler>();

        _Nesneler SecilenN = new _Nesneler();

        Graphics CizgiSil;
        Graphics Cizgim;
        Color CizgiRenk=Color.DarkOrchid;
        Pen firca = new Pen(Color.DarkRed, 2); 
        Random rasgele = new Random();
        ArrayList SONACILAN_PROGRAMLAR = new ArrayList();
        Point P1 = new Point(0, 0);
        Point P2 = new Point(0, 0);

        public static string YeniVersiyon = "";
        public static string GuncelleString = "";
        public static int NesneID = -1;
        public static int Cizgi_Baslangic = -1, Cizgi_Baslangic_ID = -1, CizgiKiyas = -1;
        string Bir_Once_Secilen = "", En_Son_Secilen = "";
        string Secilen = "";
        string EvetHayir = "";
        string SONACILANPROGRAM = "";
        int NesneInt = 0;
        int maus_yukselik, maus_sol, fark_sol, fark_yukseklik;
        int x1, x2, y1, y2, sn, sn_o;
        int PicID = 0, NesID = 0, Cizgi_ID = 0;
        int NesneEkle = -1;
        int NameToID = 0;
        int secilen = 0, onceki_secilen = 0;
        int SayacCizgi = 0, CizgiYaziSay = 0;
        int SonPicTop, SonPicLeft;
        int Geri = 0; int ileri = 0;
        int IlerGeriBulunanSıra;
        bool secildi = false;
        bool CizgiYenile = false;
        bool AktifSeciliDurum = false;
        bool UpdateKontrolTrueFalse = false;
        bool NesAraBul = false;
        bool PROGRAMDEGISTI = false; bool NEWOBJECT = false; bool GERIYUKLEMEON = false; bool teksefergir = true;
        
        public static bool PROGRAMDURDU = false, PROGRAMGUNCELLE = false, PROGRAMBEKLET = false, SATIRAKTIFLIK = false, PROGRAMCALISIYOR = false;

        #endregion

        #region OZEL FONKSIYONLAR

        [DllImport("User32.dll")]
        private static extern IntPtr LoadCursorFromFile(String str);

        ClsHatalar Hata = new ClsHatalar();
      
        public static Cursor Create(string filename)
        {
            IntPtr hCursor = LoadCursorFromFile(filename);

            if (!IntPtr.Zero.Equals(hCursor))
            {
                return new Cursor(hCursor);
            }
            else
            {
                throw new ApplicationException("Could not create cursor from file " + filename);
            }

        }

        public int NameIleIDGetir(string NesName)
        {
            try
            {

                for (int ID = 0; ID < picturebox.Count(); ID++)
                {
                    if (NesName == picturebox[ID].Name)
                    {
                        NameToID = ID; break;
                    }
                }
            }
            catch { }

            return NameToID;
        }

        void SonIkiNesneGetir()
        {
            try
            {
                for (sn = 0; sn < 1000; sn++)
                {
                    if (En_Son_Secilen == picturebox[sn].Name)
                    {
                        secilen = sn; break;
                    }
                }

                for (sn_o = 0; sn_o < 1000; sn_o++)
                {
                    if (Bir_Once_Secilen == picturebox[sn_o].Name)
                    { onceki_secilen = sn_o; break; }
                }
            }
            catch { }
        }

        public int IDIleNesneGetir(int GelenID)
        {
            try
            {
                for (int Pic_ID = 0; Pic_ID < _Nesne.Count; Pic_ID++)
                {
                    if (_Nesne[Pic_ID].ID == GelenID)
                    {
                        for (int ID = 0; ID < picturebox.Count(); ID++)
                        {
                            if (_Nesne[Pic_ID].NesneAdi == picturebox[ID].Name)
                            {
                                PicID = ID; break;
                            }
                        }
                    }
                }
            }
            catch { }
            return PicID;
        }

        int NesneIleIDGetir(int GelenID)
        {
            try
            {
                for (int Pic_ID = 0; Pic_ID < _Nesne.Count; Pic_ID++)
                {
                    if (_Nesne[Pic_ID].NesneAdi == picturebox[GelenID].Name)
                    {
                        for (int ID = 0; ID < picturebox.Count(); ID++)
                        {
                            if (_Nesne[Pic_ID].ID == ID)
                            {
                                NesID = ID; break;
                            }
                        }
                    }
                }
            }
            catch { }
            return NesID;
        }

        string TarihDuzenle(string Gelen)
        {
            string[] Gelen_dizi = Gelen.Split(':');
            string Giden = "";
            foreach (string tut in Gelen_dizi)
            {
                Giden += tut;
            }
            return Giden;
        }

        void BtnBaglaSil()
        {
            btnBagla.Visible = true;
            btnSil.Visible = true;
            btnBagla.Top = picturebox[secilen].Top - btnBagla.Height;
            btnBagla.Left = picturebox[secilen].Left + (picturebox[secilen].Width - btnSil.Width) / 2;
            btnSil.Top = picturebox[secilen].Top + picturebox[secilen].Height;
            btnSil.Left = picturebox[secilen].Left + (picturebox[secilen].Width - btnSil.Width) / 2;
            btnBagla.Top = btnBagla.Top - fark_yukseklik;
            btnBagla.Left = btnBagla.Left - fark_sol;
            btnSil.Top = btnSil.Top - fark_yukseklik;
            btnSil.Left = btnSil.Left - fark_sol;
            btnBagla.BringToFront();
            btnSil.BringToFront();
        }

        void PROGRAM_DURDUR()
        {
            if (!toolStripButton12.Enabled)
            {
                toolStripButton12.Enabled = true;
                toolStripButton11.Enabled = false;

            }

            picturebox[IDIleNesneGetir(Cizgi_Baslangic)].BackColor = Color.Transparent;
            TextLabel[IDIleNesneGetir(Cizgi_Baslangic)].ForeColor = Color.Black;

            foreach (PictureBox pic in picturebox)
            {
                if (pic != null)
                    pic.Cursor = Cursors.SizeAll;
            }

            _Degisken_Yedek.Clear();
            PROGRAMCALISIYOR = false;
            PROGRAMDURDU = false;
        }

        void PROGRAM_BEKLET()
        {
            #region // SONSUZ DONGU //

            while ((PROGRAMBEKLET))
            {
                System.Threading.Thread.Sleep(500);
                if (PROGRAMDURDU)
                {
                    foreach (PictureBox pic in picturebox)
                    {
                        if (pic != null)
                            pic.Cursor = Cursors.SizeAll;
                    }

                    _Degisken_Yedek.Clear();
                    PROGRAMBEKLET = false;
                    PROGRAMCALISIYOR = false;
                    break;
                }
            }

            #endregion
        }

        string LabelTextKisitlama(string Turu, string Gelen)
        {
            string Giden = Gelen;

            if (Turu != "Eger")
            {
                if (Gelen.Length > 23)
                {
                    Giden = Gelen.Substring(0, 23);
                    Giden = Giden + "...";
                }
            }
            else
            {
                if (Gelen.Length > 15)
                {
                    Giden = Gelen.Substring(0, 15);
                    Giden = Giden + "...";
                }
            }

            return Giden;
        }

        void LabelTextGuncelle()
        {
            SonIkiNesneGetir();
            NesneID = secilen;
            string[] GunculleParca = GuncelleString.Split('?');

            if (PROGRAMGUNCELLE)
            {
                TextLabel[NesneID].Text = LabelTextKisitlama(TextLabel[NesneID].Name.Split('_')[0], GunculleParca[0]) + GunculleParca[1];
                TextLabel[NesneID].Left = (picturebox[NesneID].Width - TextLabel[NesneID].Width) / 2;

                PROGRAMGUNCELLE = false;
            }
        }

        void IslemleriGerceklestir(string Turu, string Islem, string Degeri)
        {
            string CikisDegeri = "";
            string SonucDegisken = "";
            string SonIslem = "";
            string DegerSonuc = "";

            if (Turu == "Baslat")
            {
                _Degisken_Yedek.Clear();
            }
          
            try
            {
                if (Turu == "Islem")
                {
                    SonucDegisken = Islem.Split('=')[0];

                    foreach (_Degiskenler nesne in _Degisken_Yedek)
                    {
                        Islem = Islem.Replace(nesne.Degisken_Adi, nesne.Degeri);
                    }

                    SonIslem = Islem.Split('=')[1];
                    ClsIslem Clsislem = new ClsIslem();
                    DegerSonuc = Clsislem.Islem(SonIslem);
                    ClsOzelFonksiyonlar.GelenDegiskenGuncelleme(SonucDegisken, DegerSonuc, "");
                }
            }
            catch
            {
                MessageBox.Show("İşlem nesnesinde bir hata tespit edildi.Lütfen kontrol ediniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (Application.OpenForms["FrmVeriGirisCikis"] != null)
                {
                    _FrmVeriGirisCikis.Close();
                    PROGRAMDURDU = true;
                    PROGRAMCALISIYOR = false;
                }
                return;
            }

            if (Turu == "Degiskenler")
            {
                string[] Degiskenler = Islem.Split(',');
                _Degiskenler deg = new _Degiskenler();

                for (int i = 0; i < Degiskenler.Count(); i++)
                {
                    deg.Degisken_Adi = Degiskenler[i].Split('=')[0];
                    deg.Degeri = Degiskenler[i].Split('=')[1];
                    _Degisken_Yedek.Add(deg);
                }
            }

            if (Turu == "Giris")
            {

                ((FrmVeriGirisCikis)Application.OpenForms["FrmVeriGirisCikis"]).DEGERGIRILDI = true;
                ((FrmVeriGirisCikis)Application.OpenForms["FrmVeriGirisCikis"]).richTextBox1.AppendText(Islem + " > ");
                ((FrmVeriGirisCikis)Application.OpenForms["FrmVeriGirisCikis"]).richTextBox1.SelectionStart = ((FrmVeriGirisCikis)Application.OpenForms["FrmVeriGirisCikis"]).richTextBox1.Text.Length;
                FrmVeriGirisCikis.DegerGirildi = Islem.Length + 3;
                ((FrmVeriGirisCikis)Application.OpenForms["FrmVeriGirisCikis"]).DegiskenAdi = Degeri;
                PROGRAMBEKLET = true;
            }

            if (Turu == "Cikis")
            {
                ((FrmVeriGirisCikis)Application.OpenForms["FrmVeriGirisCikis"]).DEGERGIRILDI = true;

                for (int i = 0; i < _Degisken_Yedek.Count; i++)
                {
                    if (_Degisken_Yedek[i].Degisken_Adi == Degeri)
                    {
                        CikisDegeri = _Degisken_Yedek[i].Degeri;
                    }
                }

                ((FrmVeriGirisCikis)Application.OpenForms["FrmVeriGirisCikis"]).richTextBox1.AppendText(Islem + " " + CikisDegeri + "\n");
                ((FrmVeriGirisCikis)Application.OpenForms["FrmVeriGirisCikis"]).DEGERGIRILDI = false;

            }
            try
            {
                if (Turu == "Eger")
                {
                    int KiyasSonuc = 0;
                    string KiyasSonucstr = "";
                    SonucDegisken = Islem.Split('=')[0];


                    foreach (_Degiskenler nesne in _Degisken_Yedek)
                    {
                        Islem = Islem.Replace(nesne.Degisken_Adi, nesne.Degeri);
                    }

                    ClsKarsilastir Kıyas = new ClsKarsilastir();
                    KiyasSonuc = Convert.ToInt16(Kıyas.Karsilastir(Islem));

                    if (KiyasSonuc == 0)
                        KiyasSonucstr = "HAYIR";
                    else
                        KiyasSonucstr = "EVET";

                    foreach (_Cizgiler cizgi in _Cizgi)
                    {
                        if (cizgi.Yazi != "")
                        {
                            if (cizgi.Yazi.Split('_')[1] == (Cizgi_Baslangic).ToString())
                            {

                                if (cizgi.Yazi.Split('_')[0] == KiyasSonucstr)
                                {
                                    CizgiKiyas = Convert.ToInt16(cizgi.Yazi.Split('_')[2]);
                                }

                            }
                        }

                    }
                }
            }
            catch { MessageBox.Show("Eğer nesnesinde bir hata tespit edildi.Lütfen kontrol ediniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (Application.OpenForms["FrmVeriGirisCikis"] != null)
            {
                _FrmVeriGirisCikis.Close();
                PROGRAMDURDU = true;
                PROGRAMCALISIYOR = false;
            }
                return; }


            if (Turu == "Dur")
            {
                ((FrmVeriGirisCikis)Application.OpenForms["FrmVeriGirisCikis"]).DEGERGIRILDI = false;
            }
        }

        void Calistir()
        {
            int Bitis = -1;
            Cizgi_Baslangic = -1;
            Cizgi_Baslangic_ID = -1;
            bool DikkatEger = false;
            int CalismaHizi = 1000;

            btnBagla.Hide();
            btnSil.Hide();
            CizgiYenile = true;
            CizgiSil = this.CreateGraphics();
            CizgiSil.Clear(this.BackColor);

            //try
            //{
                listBox1.Items.Clear();
                picturebox[onceki_secilen].BackColor = Color.Transparent;
                picturebox[secilen].BackColor = Color.Transparent;
                TextLabel[secilen].ForeColor = Color.Black;
                TextLabel[onceki_secilen].ForeColor = Color.Black;

                for (int i = 0; i < _Nesne.Count(); i++)
                {
                    if (_Nesne[i].Turu == "Baslat")
                    {
                        Cizgi_Baslangic = _Nesne[i].ID; break;
                    }
                }

                for (int i = 0; i < _Nesne.Count(); i++)
                {
                    if (_Nesne[i].Turu == "Dur")
                    { Bitis = _Nesne[i].ID; break; }
                }

                picturebox[IDIleNesneGetir(Cizgi_Baslangic)].BackColor = Color.Red;
                TextLabel[IDIleNesneGetir(Cizgi_Baslangic)].ForeColor = Color.White;
                System.Threading.Thread.Sleep(CalismaHizi);
                TextLabel[IDIleNesneGetir(Cizgi_Baslangic)].ForeColor = Color.Black;
                picturebox[IDIleNesneGetir(Cizgi_Baslangic)].BackColor = Color.Transparent;

                listBox1.Items.Add(IDIleNesneGetir(Cizgi_Baslangic));

                for (int bul = 0; bul < listBox2.Items.Count; bul++)
                {
                    if (picturebox[IDIleNesneGetir(Cizgi_Baslangic)].Name == listBox2.Items[bul].ToString())
                    {
                        Cizgi_Baslangic_ID = bul;
                    }
                }

                IslemleriGerceklestir(_Nesne[Cizgi_Baslangic_ID].Turu, _Nesne[Cizgi_Baslangic_ID].Islem, _Nesne[Cizgi_Baslangic_ID].Degeri);

                while (true)
                {

                    for (int i = 0; i < _Cizgi.Count(); i++)
                    {
                        CalismaHizi = ((FrmVeriGirisCikis)Application.OpenForms["FrmVeriGirisCikis"]).trackBar1.Value;

                        if (Cizgi_Baslangic == _Cizgi[i].Baslangic)
                        {
                            if (!DikkatEger)
                                Cizgi_Baslangic = _Cizgi[i].Bitis;
                            else
                                Cizgi_Baslangic = CizgiKiyas;

                            DikkatEger = false;


                            listBox1.Items.Add(IDIleNesneGetir(Cizgi_Baslangic));

                            picturebox[IDIleNesneGetir(Cizgi_Baslangic)].BackColor = Color.Red;
                            TextLabel[IDIleNesneGetir(Cizgi_Baslangic)].ForeColor = Color.White;

                            for (int bul = 0; bul < listBox2.Items.Count; bul++)
                            {
                                if (picturebox[IDIleNesneGetir(Cizgi_Baslangic)].Name == listBox2.Items[bul].ToString())
                                {
                                    Cizgi_Baslangic_ID = bul;
                                }
                            }

                            IslemleriGerceklestir(_Nesne[Cizgi_Baslangic_ID].Turu, _Nesne[Cizgi_Baslangic_ID].Islem, _Nesne[Cizgi_Baslangic_ID].Degeri);
                            if (_Nesne[Cizgi_Baslangic_ID].Turu == "Eger")
                                DikkatEger = true;


                            PROGRAM_BEKLET();
                            if (PROGRAMDURDU)
                            {
                                PROGRAM_DURDUR();
                                picturebox[IDIleNesneGetir(Cizgi_Baslangic)].BackColor = Color.Transparent;
                                TextLabel[IDIleNesneGetir(Cizgi_Baslangic)].ForeColor = Color.Black;
                                break;
                            }

                            System.Threading.Thread.Sleep(CalismaHizi);
                            TextLabel[IDIleNesneGetir(Cizgi_Baslangic)].ForeColor = Color.Black;
                            picturebox[IDIleNesneGetir(Cizgi_Baslangic)].BackColor = Color.Transparent;

                        }
                    }

                    if (Cizgi_Baslangic == Bitis)
                    {
                        picturebox[IDIleNesneGetir(Cizgi_Baslangic)].BackColor = Color.Transparent;
                        TextLabel[IDIleNesneGetir(Cizgi_Baslangic)].BackColor = Color.Black;

                        PROGRAMDURDU = true; break;
                    }
                }

            //}
            //catch { }
        }

        void SayfaTemizle()
        {
            if (_Cizgi.Count > 0)
            {
                bas:
                for (int l = 0; l < _Cizgi.Count;)
                {
                    _Cizgi.RemoveAt(l); goto bas;
                }
            }

            bas2:

            for (int l = 0; l < _Nesne.Count;)
            {
                _Nesne.RemoveAt(l);
                goto bas2;
            }

            bas3:

            for (int l = 0; l < _Degisken.Count;)
            {
                _Degisken.RemoveAt(l);
                goto bas3;
            }

            for (int a = 0; a < picturebox.Count(); a++)
            {
                Controls.Remove(picturebox[a]);
            }

            for (int m = 0; m < TextLabelEger.Count(); m++)
            {
                Controls.Remove(TextLabelEger[m]);
            }



            Text = PROGRAM_ADI;
            CizgiSil = this.CreateGraphics();
            CizgiSil.Clear(this.BackColor);
            btnSil.Visible = false;
            btnBagla.Visible = false;
            NesneInt = 0;
            SayacCizgi = 0;
        }

        void YeniProje()
        {
            if ((PROGRAMDEGISTI) && (_Nesne.Count > 0))
            {
                DialogResult Mesaj = MessageBox.Show("Projenizi kaydetmek istiyor musunuz ?", "Dikkat", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (Mesaj == DialogResult.Yes)
                {
                    PROGRAMDEGISTI = false;
                 
                    ProjeKaydet();
                    SayfaTemizle();
                }
                if (Mesaj == DialogResult.No)
                {
                    SayfaTemizle();
                }
            }
            else
            { SayfaTemizle(); }
            
        }

        void ProjeKaydet()
        {
            SaveFileDialog Kaydet = new SaveFileDialog();

            if (_Nesne.Count > 0)
            {
                Kaydet.Filter = "FlowTurk Görsel Programlama (.fgp)  |*.fgp";
                Kaydet.ShowDialog();
                KaydetPaket(Kaydet.FileName);
            }
            else
            {
                MessageBox.Show("Boş sayfa kaydı yapılamıyor !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void KaydetPaket(string dosyaYolu)
        {
            #region DEGISKENLER

            int ID = 0;
            string NesneAdi = "";
            string Turu = "";
            int Top = 0;
            int Left = 0;
            string Deger = "";
            string Islem = "";
            int Baslangic = 0;
            int Bitis = 0;
            Point P1 = new Point(0, 0);
            Point P2 = new Point(0, 0);
            string Yazi = "";
            string Degisken_Turu = "";
            string Degisken_Adi = "";
            string Degeri = "";

            #endregion

            if (dosyaYolu != "")
            {
                StreamWriter KaydetDosya = new StreamWriter(dosyaYolu);
                for (int i = 0; i < _Nesne.Count(); i++)
                {
                    ID = _Nesne[i].ID;
                    NesneAdi = _Nesne[i].NesneAdi;
                    Turu = _Nesne[i].Turu;
                    Top = _Nesne[i].Top;
                    Left = _Nesne[i].Left;
                    Deger = _Nesne[i].Degeri;
                    Islem = _Nesne[i].Islem;

                    for (int bul = 0; bul < picturebox.Count(); bul++)
                    {
                        if (NesneAdi == picturebox[IDIleNesneGetir(bul)].Name)
                        {
                            Top = picturebox[IDIleNesneGetir(bul)].Top;
                            Left = picturebox[IDIleNesneGetir(bul)].Left;
                        }
                    }
                    KaydetDosya.WriteLine(ID + "#" + NesneAdi + "#" + Turu + "#" + Top + "#" + Left + "#" + Islem + "#" + Deger);
                }

                KaydetDosya.WriteLine("?");

                for (int i = 0; i < _Cizgi.Count(); i++)
                {
                    Baslangic = _Cizgi[i].Baslangic;
                    Bitis = _Cizgi[i].Bitis;
                    P1 = _Cizgi[i].P1;
                    P2 = _Cizgi[i].P2;
                    Yazi = _Cizgi[i].Yazi;

                    KaydetDosya.WriteLine(Baslangic + "#" + Bitis + "#" + P1.X + "#" + P1.Y + "#" + P2.X + "#" + P2.Y + "#" + Yazi);
                }

                KaydetDosya.WriteLine("?");

                for (int i = 0; i < _Nesne.Count(); i++)
                {
                    if (_Nesne[i].Turu == "Degisken")
                    {
                        string[] Parcala = _Nesne[i].Islem.Split(',');

                        for (int say = 0; say < Parcala.Count(); say++)
                        {
                            Degisken_Adi = Parcala[say].Split('=')[0];
                            Degeri = Parcala[say].Split('=')[1];
                            Degisken_Turu = Parcala[say].Split('=')[1];

                            KaydetDosya.WriteLine(Degisken_Turu + "#" + Degisken_Adi + "#" + Degeri);
                        }

                    }
                }

                KaydetDosya.Flush();
                KaydetDosya.Close();
                PROGRAMDEGISTI = false;
                MessageBox.Show("Başarıyla kayıt işleminiz gerçekleştirildi.");
            }
        }

        void DegiskenKayit(string Ifade)
        {
            if (Ifade != "")
            {
                _Degiskenler Fonk = new _Degiskenler();
                string[] Degiskenler = Ifade.Split(',');

                for (int i = 0; i < Degiskenler.Count(); i++)
                {
                    Fonk.Degisken_Adi = Degiskenler[i].Split('=')[0];
                    Fonk.Degeri = Degiskenler[i].Split('=')[1];
                    Fonk.Degisken_Turu = Degiskenler[i].Split('=')[1];
                    _Degisken.Add(Fonk);
                }
            }
        }

        void ProjeAc(string GelenDosyaYolu)
        {
                                 
            
            string DosyaYolu = "";
            int Devam = 0;
            int i = 0;

            if ((PROGRAMDEGISTI) && (_Nesne.Count > 0))
            {
                DialogResult Mesaj = MessageBox.Show("Değişiklikleri kaydetmek istiyor musunuz ?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (Mesaj == DialogResult.Yes)
                {
                    KaydetPaket(SONACILANPROGRAM);
                }
            }

            StreamWriter SonDosyalar = null;

            if (GelenDosyaYolu == "")
            {
                OpenFileDialog file = new OpenFileDialog();
                file.Title = "FlowTurk Akış Diyagramı Dosyasını Seçtiniz";
                file.Filter = "FlowTurk Görsel Progralama |*.fgp";
                file.InitialDirectory = Application.StartupPath + "\\Örnekler";
                file.RestoreDirectory = true;
                file.CheckFileExists = false;

                if (file.ShowDialog() == DialogResult.OK)
                {
                    DosyaYolu = file.FileName;
                }
            }
            else
            {
                DosyaYolu = GelenDosyaYolu;
                string PROGRAM = PROGRAM_ADI + " - " + Path.GetFileNameWithoutExtension(DosyaYolu);
                this.Text = PROGRAM;
            }

            if (DosyaYolu != "")
            {
                try
                {
                    SonDosyalar = new StreamWriter(Application.StartupPath + "\\Settings\\Son Acilan.fgps", true);

                    SONACILANPROGRAM = DosyaYolu;
                    SONACILAN_PROGRAMLAR.Add(SONACILANPROGRAM);

                    SonDosyalar.WriteLine(SONACILANPROGRAM);
                    SonDosyalar.Flush();
                    SonDosyalar.Close();

                    SayfaTemizle();
                    string[] dizi = System.IO.File.ReadAllLines(DosyaYolu);

                    _Nesneler Nesne = new _Nesneler();
                    _Cizgiler Cizgi = new _Cizgiler();
                    _Degiskenler Degisken = new _Degiskenler();

                    for (i = 0; i < dizi.Count(); i++)
                    {
                        if (dizi[i] == "?") { Devam = i; break; }

                        Nesne.ID = Convert.ToInt16(dizi[i].Split('#')[0]);
                        Nesne.NesneAdi = dizi[i].Split('#')[1];
                        Nesne.Turu = dizi[i].Split('#')[2];
                        Nesne.Top = Convert.ToInt16(dizi[i].Split('#')[3]);
                        Nesne.Left = Convert.ToInt16(dizi[i].Split('#')[4]);
                        Nesne.Islem = dizi[i].Split('#')[5];
                        Nesne.Degeri = dizi[i].Split('#')[6];

                        NesneOlustur(Nesne.ID, Nesne.Turu, Nesne.Top, Nesne.Left, Nesne.Islem, Nesne.Degeri, false);
                    }

                    for (i = Devam + 1; i < dizi.Count(); i++)
                    {
                        if (dizi[i] == "?") { Devam = i; break; }

                        Cizgi.Baslangic = Convert.ToInt16(dizi[i].Split('#')[0]);
                        Cizgi.Bitis = Convert.ToInt16(dizi[i].Split('#')[1]);
                        Cizgi.P1 = new Point(Convert.ToInt16(dizi[i].Split('#')[2]), Convert.ToInt16(dizi[i].Split('#')[3]));
                        Cizgi.P2 = new Point(Convert.ToInt16(dizi[i].Split('#')[4]), Convert.ToInt16(dizi[i].Split('#')[5]));
                        Cizgi.Yazi = dizi[i].Split('#')[6];
                        _Cizgi.Add(Cizgi);

                        #region LABEL OLUSTURMA

                        if (Cizgi.Yazi != "")
                        {
                            TextLabelEger[SayacCizgi] = new Label();
                            TextLabelEger[SayacCizgi].Name = Cizgi.Yazi;
                            TextLabelEger[SayacCizgi].Text = Cizgi.Yazi.Split('_')[0];
                            TextLabelEger[SayacCizgi].Font = new Font("Tahoma", 12, FontStyle.Bold);
                            TextLabelEger[SayacCizgi].SetBounds((P1.X + P2.X) / 2 - 30, P2.Y - (P2.Y - P1.Y) / 2 - 10, P2.X, P2.Y);
                            TextLabelEger[SayacCizgi].AutoSize = true;
                            TextLabelEger[SayacCizgi].BackColor = Color.White;
                            if (Cizgi.Yazi.Split('_')[0] == "EVET") TextLabelEger[SayacCizgi].ForeColor = Color.DeepSkyBlue;
                            else TextLabelEger[SayacCizgi].ForeColor = Color.Crimson;
                            TextLabelEger[SayacCizgi].BorderStyle = BorderStyle.FixedSingle;
                            TextLabelEger[SayacCizgi].SendToBack();
                            Controls.Add(TextLabelEger[SayacCizgi]);
                            SayacCizgi++;
                        }

                        #endregion
                      
                        
                        Point Nokta1 = new Point(x1, y1);
                        Point Nokta2 = new Point(x2, y2);
                        GraphicsPath end_path = new GraphicsPath();
                        end_path.AddLine(0, 0, -2, -2);
                        end_path.AddLine(0, 0, 2, -2);
                        CustomLineCap end_cap = new CustomLineCap(null, end_path);
                        firca.CustomEndCap = end_cap;

                        Cizgim = this.CreateGraphics();
                        Cizgim.SmoothingMode = SmoothingMode.AntiAlias;

                        Cizgim.DrawLine(firca, P1.X, P1.Y, P2.X, P2.Y);
                        Cizgim.Dispose();

                    }

                    for (i = Devam + 1; i < dizi.Count(); i++)
                    {
                        if (dizi[i] == "?") { Devam = i; break; }

                        Degisken.Degisken_Turu = dizi[i].Split('#')[0];
                        Degisken.Degisken_Adi = dizi[i].Split('#')[1];
                        Degisken.Degeri = dizi[i].Split('#')[2];
                        _Degisken.Add(Degisken);
                    }
                }
                catch { MessageBox.Show("Böyle bir dosya bulunmamaktadır !", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

                string PROGRAM = PROGRAM_ADI + " - [ " + Path.GetFileNameWithoutExtension(SONACILANPROGRAM)+" ]";
                this.Text = PROGRAM;
                PROGRAMDEGISTI = false;

            }
        }

        void PencereAc()
        {
            string NesneTurleri = "";

            foreach (_Nesneler Nes in _Nesne)
            {
                if (IDIleNesneGetir(Nes.ID) == secilen)
                {
                    NesneTurleri = Nes.Turu;
                    NesneID = Nes.ID;
                }
            }

            if (NesneTurleri == "Islem")
            {
                FrmIslemNesne Pencere = new FrmIslemNesne();
                Pencere.ShowDialog();
            }

            if (NesneTurleri == "Degiskenler")
            {
                FrmDegiskenNesne Pencere2 = new FrmDegiskenNesne();
                Pencere2.ShowDialog();
            }

            if (NesneTurleri == "Giris")
            {
                FrmGirisNesne Pencere3 = new FrmGirisNesne();
                Pencere3.ShowDialog();
            }

            if (NesneTurleri == "Cikis")
            {
                FrmCikisNesne Pencere4 = new FrmCikisNesne();
                Pencere4.ShowDialog();
            }

            if (NesneTurleri == "Eger")
            {
                FrmEgerNesne Pencere5 = new FrmEgerNesne();
                Pencere5.ShowDialog();
            }
        }

        void IleriSarPlay()
        {
            ileri = Geri;

             if (ileri >= _GeriIleriYukle.Count-1) { 
                toolStripButton15.Enabled = false;
                }
            toolStripButton16.Enabled = true;
            if (ileri >= _GeriIleriYukle.Count) { ileri = ileri - 2; return; }

           

            Geri = ileri;
            Geri++;

            foreach (PictureBox PicNes in picturebox)
            {
                if (PicNes == null) return;

                int NewNesID = _GeriIleriYukle[ileri].ID;

                if (_GeriIleriYukle[ileri].YapilanIslem == "NesneKayit")
                {
                    PROGRAMDEGISTI = true;
                    picturebox[NewNesID].Visible = true;
                    Geri++;       
                }

                if (_GeriIleriYukle[ileri].NesneAdi == PicNes.Name)
                {
                    PicNes.Top = _GeriIleriYukle[ileri].Top;
                    PicNes.Left = _GeriIleriYukle[ileri].Left;
                }
            }
        }
       
        void GeriSarPlay()
        {
            if (teksefergir) { ileri = _GeriIleriYukle.Count - 1; teksefergir = false; }
            if (NEWOBJECT) { ileri = _GeriIleriYukle.Count - 1; NEWOBJECT = false; }

            Geri = ileri;

            if (Geri <=0) {
                toolStripButton16.Enabled = false;
               
                 }
            toolStripButton15.Enabled = true;

            if (Geri < 0) { Geri = Geri + 2; return; }

          

            GERIYUKLEMEON = true;
            ileri = Geri;
            ileri--;

            if ((_GeriIleriYukle[Geri].YapilanIslem == "Yerdegistirme") || (_GeriIleriYukle[Geri].YapilanIslem == "NesneKayit"))
            {
                foreach (PictureBox PicNes in picturebox)
                {
                    if (PicNes == null) return;

                    if (_GeriIleriYukle[Geri].YapilanIslem == "NesneKayit")
                    {
                        if (_GeriIleriYukle[Geri].NesneAdi == PicNes.Name)
                        {
                            PicNes.Visible = false;
                            ileri--;
                        }
                    }
                   
                    if (_GeriIleriYukle[Geri].NesneAdi == PicNes.Name)
                    {
                        PicNes.Top = _GeriIleriYukle[Geri].Top;
                        PicNes.Left = _GeriIleriYukle[Geri].Left;
                        toolStripButton15.Enabled = true;
                    }  
                }
            }

            if(_GeriIleriYukle[Geri].YapilanIslem == "CizgiBaglaSil")
            {
               
                    bas:
                    for (int l = 0; l < _Cizgi.Count; l++)
                    {
                        if ((IDIleNesneGetir(_Cizgi[l].Baslangic) == _GeriIleriYukle[Geri].Baslangic) || (IDIleNesneGetir(_Cizgi[l].Bitis) == _GeriIleriYukle[Geri].Bitis))
                        {
                            _Cizgi.RemoveAt(l); goto bas;
                        }

                    }


                CizgiYenile = true;

            }
        }

        void GeriYukleKayit(string YapilanIslem, string Name,string Turu,int BasC,int BitC,Point P1,Point P2,string Yazi,bool ozel)
        {
            _GeriYukle GeriyeYukle = new _GeriYukle();

            toolStripButton16.Enabled = true;

            if (Turu == "Nesne")
            {
                if ((_GeriIleriYukle.Count >=2))
                {
                    SonPicTop = _GeriIleriYukle[_GeriIleriYukle.Count -1].Top;
                    SonPicLeft = _GeriIleriYukle[_GeriIleriYukle.Count -1].Left;
                }
                if (ozel) { SonPicTop = 0;SonPicLeft = 0; }
               
                foreach (PictureBox Nesne in picturebox)
                {
                    if (Nesne == null) return;
                    if (((SonPicTop == picturebox[secilen].Top) & (SonPicLeft == picturebox[secilen].Left))&!ozel) return;

                    SonIkiNesneGetir();
                    if (_GeriIleriYukle.Count > 1)
                    {  //try
                       //{
                        for (int ara = 1; ara < _GeriIleriYukle.Count; ara++)
                        {
                            if ((_GeriIleriYukle[ara].NesneAdi == picturebox[secilen].Name))
                                if (_GeriIleriYukle[ara].Left == picturebox[secilen].Left & _GeriIleriYukle[ara].Top == picturebox[secilen].Top & _GeriIleriYukle[ara - 1].YapilanIslem != "NesneKayit") return;
                        }
                        //}
                        //catch { }\
                    }

                    if (Name == Nesne.Name)
                    {
                        foreach (_Nesneler GerNes in _Nesne)
                        {
                            if (GerNes.NesneAdi == Name)
                            {
                                GeriyeYukle.ID = GerNes.ID;
                                GeriyeYukle.NesneAdi = Nesne.Name;
                                GeriyeYukle.Degeri = GerNes.Degeri;
                                GeriyeYukle.Islem = GerNes.Islem;
                                GeriyeYukle.Left = Nesne.Left;
                                GeriyeYukle.Top = Nesne.Top;
                                GeriyeYukle.Turu = GerNes.Turu;
                                GeriyeYukle.YapilanIslem = YapilanIslem;
                                _GeriIleriYukle.Add(GeriyeYukle);
                            }
                        }
                    }
                }
            }

            if(Turu=="Cizgi")
            {
                GeriyeYukle.Turu = Turu;
                GeriyeYukle.Baslangic = BasC;
                GeriyeYukle.Bitis = BitC;
                GeriyeYukle.P1 = P1;
                GeriyeYukle.P2 = P2;
                GeriyeYukle.YapilanIslem = YapilanIslem;
                GeriyeYukle.Yazi = Yazi;
                _GeriIleriYukle.Add(GeriyeYukle);
            }
        }

        #endregion

        #region CIZGISEL FONKSIYONLAR

        void CizgiKayit(int Baslangic, int Bitis, string Yazi, Point P1, Point P2)
        {
            if (Baslangic != Bitis)
            {
                _Cizgiler Fonk = new _Cizgiler();
                Fonk.Baslangic = Baslangic;
                Fonk.Bitis = Bitis;
                Fonk.P1 = P1;
                Fonk.P2 = P2;
                Fonk.Yazi = Yazi;
                _Cizgi.Add(Fonk);
                Cizgi_ID++;

                //NEWOBJECT = true;
                GeriYukleKayit("CizgiBaglaSil", "", "Cizgi", Baslangic, Bitis, P1, P2, Yazi,false);
            }
        }

        void CizgiOlustur()
        {
                 
           
            string CizgiYazi = "";
            PROGRAMDEGISTI = true;
            SonIkiNesneGetir();
            CizgiKonumBelirleme();
            EvetHayir = "EVET";

            #region LABEL OLUSTURMA

            foreach (_Nesneler Nes in _Nesne)
            {
                if (Nes.ID == NesneIleIDGetir(secilen))
                {
                    if (picturebox[(onceki_secilen)].Name.Split('_')[0].Trim() == "Eger")
                    {
                        for (int i = 0; i < _Cizgi.Count; i++)
                        {
                            if (_Cizgi[i].Baslangic == NesneIleIDGetir(onceki_secilen))
                            {
                                if (_Cizgi[i].Yazi == "")
                                { EvetHayir = "EVET"; break; }
                                if (_Cizgi[i].Yazi.Split('_')[0] == "EVET")
                                { EvetHayir = "HAYIR"; break; }
                                if (_Cizgi[i].Yazi.Split('_')[0] == "HAYIR")
                                { EvetHayir = "EVET"; break; }
                            }
                        }
                     
                        CizgiKonumDegistirmeKayit();
                        SonIkiNesneGetir();

                        TextLabelEger[SayacCizgi] = new Label();
                        TextLabelEger[SayacCizgi].Name = EvetHayir + "_" + NesneIleIDGetir(onceki_secilen) + "_" + NesneIleIDGetir(secilen) + "_" + SayacCizgi;
                        TextLabelEger[SayacCizgi].Text = EvetHayir;
                        TextLabelEger[SayacCizgi].Font = new Font("Tahoma", 11, FontStyle.Bold);
                        TextLabelEger[SayacCizgi].SetBounds((P1.X + P2.X) / 2 - 30, P2.Y - (P2.Y - P1.Y) / 2 - 10, P2.X, P2.Y);
                        TextLabelEger[SayacCizgi].AutoSize = true;
                        TextLabelEger[SayacCizgi].BackColor = Color.White;
                        if (EvetHayir == "EVET") TextLabelEger[SayacCizgi].ForeColor = Color.DeepSkyBlue;
                        else TextLabelEger[SayacCizgi].ForeColor = Color.Crimson;
                        TextLabelEger[SayacCizgi].BorderStyle = BorderStyle.FixedSingle;
                        TextLabelEger[SayacCizgi].SendToBack();
                        Controls.Add(TextLabelEger[SayacCizgi]);
                        CizgiYazi = TextLabelEger[SayacCizgi].Name;
                        SayacCizgi++;
                    }
                }
            }

            #endregion

            for (int m = 0; m < _Nesne.Count; m++)
            {
                if (Cizgi_Baslangic == _Nesne[m].ID)
                {
                    listBox1.Items.Add(m);
                    Cizgi_Baslangic_ID = m;
                }
            }

            CizgiKayit(NesneIleIDGetir(onceki_secilen), NesneIleIDGetir(secilen), CizgiYazi, P1, P2);          
            

            
            Point Nokta1 = new Point(x1, y1);
            Point Nokta2 = new Point(x2, y2);
            GraphicsPath end_path = new GraphicsPath();
            end_path.AddLine(0, 0, -2, -2);
            end_path.AddLine(0, 0, 2, -2);
            CustomLineCap end_cap = new CustomLineCap(null, end_path);
            firca.CustomEndCap = end_cap;

            Cizgim = this.CreateGraphics();
            Cizgim.SmoothingMode = SmoothingMode.AntiAlias;

            Cizgim.DrawLine(firca, P1.X, P1.Y, P2.X, P2.Y);
            Cizgim.Dispose();
        }

        void CizgiKonumDegistirmeKayit()
        {
            if (CizgiYenile)
            {
                
              
                for (int i = 0; i < _Cizgi.Count; i++)
                {
                    sn_o = IDIleNesneGetir(_Cizgi[i].Baslangic);
                    sn = IDIleNesneGetir(_Cizgi[i].Bitis);
                    onceki_secilen = sn_o;
                    secilen = sn;

                    int BasX = picturebox[sn_o].Location.X;
                    int BasY = picturebox[sn_o].Location.Y;
                    int BasHeight = picturebox[sn_o].Size.Height;
                    int BasWidth = picturebox[sn_o].Size.Width;
                    int SonX = picturebox[sn].Location.X;
                    int SonY = picturebox[sn].Location.Y;
                    int SonWidth = picturebox[sn].Size.Width;
                    int SonHeight = picturebox[sn].Size.Height;

                    //1. Görünüm
                    if ((BasY <= SonY) && (SonY - SonHeight >= BasY))
                    {
                        x1 = BasX + BasWidth / 2;
                        y1 = BasY + BasHeight + 5;
                        x2 = SonX + SonWidth / 2;
                        y2 = SonY - 5;
                    }
                    //2. Görünüm
                    else if ((SonX <= BasX) && ((SonY + SonWidth >= BasY) || (SonY - SonWidth <= BasY)))
                    {
                        x1 = BasX - 5;
                        y1 = BasY + BasWidth / 2;
                        x2 = SonX + SonWidth / 2;
                        y2 = SonY + SonHeight + 5;

                        if (SonY + picturebox[sn].Size.Width >= BasY)
                        {
                            x1 = BasX - 5;
                            y1 = BasY + BasHeight / 2;
                            x2 = SonX + SonWidth + 5;
                            y2 = SonY + SonHeight / 2;
                        }
                    }
                    ////3. Görünüm
                    if ((SonY <= BasY) && (BasY - BasHeight >= SonY))
                    {
                        x1 = BasX + BasWidth / 2;
                        y1 = BasY - 5;
                        x2 = SonX + SonWidth / 2;
                        y2 = SonY + SonHeight + 5;
                    }
                    //4. Görünüm
                    else if ((BasX <= SonX) && ((BasY >= SonY) || (SonY - SonHeight <= BasY)))
                    {
                        x1 = BasX + BasWidth + 5;
                        y1 = BasY + BasHeight / 2;
                        x2 = SonX - 5;
                        y2 = SonY + SonHeight / 2;
                    }

                    P1 = new Point(x1, y1);
                    P2 = new Point(x2, y2);
                    
                    
                    Cizgim = this.CreateGraphics();
                    Cizgim.SmoothingMode = SmoothingMode.AntiAlias;
                    Cizgim.DrawLine(firca, P1.X, P1.Y, P2.X, P2.Y);
                    Cizgim.Dispose();

                    #region CIZGI YAZI KONUMLANDIRMA

                    if (_Cizgi[i].Yazi != "")
                    {
                        for (int m = 0; m < TextLabelEger.Count(); m++)
                        {
                            if (TextLabelEger[m] != null)
                            {
                                if ((NesneIleIDGetir(secilen)).ToString() == TextLabelEger[m].Name.Split('_')[2])
                                {
                                    TextLabelEger[(m)].SetBounds((P1.X + P2.X) / 2 - 30, P2.Y - (P2.Y - P1.Y) / 2 - 10, P2.X, P2.Y); 
                                }
                            }
                        }
                    }

                    #endregion

                }
                SonIkiNesneGetir();
                CizgiYenile = false;
            }
        }   

        void CizgiKonumBelirleme()
        {
            try
            {
                SonIkiNesneGetir();
                onceki_secilen = sn_o;
                secilen = sn;

                int BasX = picturebox[sn_o].Location.X;
                int BasY = picturebox[sn_o].Location.Y;
                int BasHeight = picturebox[sn_o].Size.Height;
                int BasWidth = picturebox[sn_o].Size.Width;
                int SonX = picturebox[sn].Location.X;
                int SonY = picturebox[sn].Location.Y;
                int SonWidth = picturebox[sn].Size.Width;
                int SonHeight = picturebox[sn].Size.Height;

                //1. Görünüm
                if ((BasY <= SonY) && (SonY - SonHeight >= BasY))
                {
                    x1 = BasX + BasWidth / 2;
                    y1 = BasY + BasHeight + 5;
                    x2 = SonX + SonWidth / 2;
                    y2 = SonY - 5;
                }
                //2. Görünüm
                else if ((SonX <= BasX) && ((SonY + SonWidth >= BasY) || (SonY - SonWidth <= BasY)))
                {
                    x1 = BasX - 5;
                    y1 = BasY + BasWidth / 2;
                    x2 = SonX + SonWidth / 2;
                    y2 = SonY + SonHeight + 5;

                    if (SonY + picturebox[sn].Size.Width >= BasY)
                    {
                        x1 = BasX - 5;
                        y1 = BasY + BasHeight / 2;
                        x2 = SonX + SonWidth + 5;
                        y2 = SonY + SonHeight / 2;
                    }
                }
                ////3. Görünüm
                if ((SonY <= BasY) && (BasY - BasHeight >= SonY))
                {
                    x1 = BasX + BasWidth / 2;
                    y1 = BasY - 5;
                    x2 = SonX + SonWidth / 2;
                    y2 = SonY + SonHeight + 5;
                }
                //4. Görünüm
                else if ((BasX <= SonX) && ((BasY >= SonY) || (SonY - SonHeight <= BasY)))
                {
                    x1 = BasX + BasWidth + 5;
                    y1 = BasY + BasHeight / 2;
                    x2 = SonX - 5;
                    y2 = SonY + SonHeight / 2;
                }

                P1.X = x1;
                P1.Y = y1;
                P2.X = x2;
                P2.Y = y2;

            }
            catch { }
        }

        #endregion

        #region NESNESEL FONKSIYONLAR

        void NesneKayit(int ID, string NesneAdi, string Turu, int Top, int Left, string Islem, string Degeri)
        {
            _Nesneler Fonk = new _Nesneler();
            Fonk.ID = ID;
            Fonk.NesneAdi = NesneAdi;
            Fonk.Turu = Turu;
            Fonk.Top = Top;
            Fonk.Left = Left;
            Fonk.Degeri = Degeri;
            Fonk.Islem = Islem;
            _Nesne.Add(Fonk);



            for (int i = _GeriIleriYukle.Count-1; i >Geri; i--)
            {
                if (!GERIYUKLEMEON) break;
                _GeriIleriYukle.RemoveAt(i);
            }
            GERIYUKLEMEON = false;
            NEWOBJECT = true;

            GeriYukleKayit("NesneKayit",NesneAdi, "Nesne", 0, 0, new Point(0, 0), new Point(0, 0), "",true);
            GeriYukleKayit("Yerdegistirme", NesneAdi, "Nesne", 0, 0, new Point(0, 0), new Point(0, 0), "",false);

          
        }

        public void FormaNesneEkle()
        {
            if (!PROGRAMCALISIYOR)
            {
                Point point = this.PointToClient(Cursor.Position);
                string NesneSekil = "";

                switch (NesneEkle)
                {

                    case 0:
                        {
                            NesneSekil = HangiNesneOlusturulacak("Baslat");
                            NesneOlustur(0, "Baslat", point.Y - Image.FromFile(NesneSekil).Size.Height / 2, point.X - Image.FromFile(NesneSekil).Size.Width / 2, "", "", true);
                            PROGRAMDEGISTI = true;
                            NesneEkle = -1;
                            this.Cursor = Cursors.Default; break;
                        }

                    case 1:
                        {
                            NesneSekil = HangiNesneOlusturulacak("Islem");
                            NesneOlustur(0, "Islem", point.Y - Image.FromFile(NesneSekil).Size.Height / 2, point.X - Image.FromFile(NesneSekil).Size.Width / 2, "top=sayi1+sayi2", "", true);
                            PROGRAMDEGISTI = true;
                            NesneEkle = -1;
                            this.Cursor = Cursors.Default; break;
                        }

                    case 2:
                        {
                            NesneSekil = HangiNesneOlusturulacak("Degiskenler");
                            NesneOlustur(0, "Degiskenler", point.Y - Image.FromFile(NesneSekil).Size.Height / 2, point.X - Image.FromFile(NesneSekil).Size.Width / 2, "top=0,sayi1=0,sayi2=0", "", true);
                            PROGRAMDEGISTI = true;
                            NesneEkle = -1;
                            this.Cursor = Cursors.Default; break;
                        }

                    case 3:
                        {
                            NesneSekil = HangiNesneOlusturulacak("Giris");
                            NesneOlustur(0, "Giris", point.Y - Image.FromFile(NesneSekil).Size.Height / 2, point.X - Image.FromFile(NesneSekil).Size.Width / 2, "Bir sayı giriniz.", "", true);
                            PROGRAMDEGISTI = true;
                            NesneEkle = -1;
                            this.Cursor = Cursors.Default; break;
                        }

                    case 4:
                        {
                            NesneSekil = HangiNesneOlusturulacak("Cikis");
                            NesneOlustur(0, "Cikis", point.Y - Image.FromFile(NesneSekil).Size.Height / 2, point.X - Image.FromFile(NesneSekil).Size.Width / 2, "Sonuç :", "", true);
                            PROGRAMDEGISTI = true;
                            NesneEkle = -1;
                            this.Cursor = Cursors.Default; break;
                        }

                    case 5:
                        {
                            NesneSekil = HangiNesneOlusturulacak("Eger");
                            NesneOlustur(0, "Eger", point.Y - Image.FromFile(NesneSekil).Size.Height / 2, point.X - Image.FromFile(NesneSekil).Size.Width / 2, "sayi1 > sayi2", "", true);
                            PROGRAMDEGISTI = true;
                            NesneEkle = -1;
                            this.Cursor = Cursors.Default; break;
                        }

                    case 6:
                        {
                            NesneSekil = HangiNesneOlusturulacak("Dugum");
                            NesneOlustur(0, "Dugum", point.Y - Image.FromFile(NesneSekil).Size.Height / 2, point.X - Image.FromFile(NesneSekil).Size.Width / 2, "", "", true);
                            PROGRAMDEGISTI = true;
                            NesneEkle = -1;
                            this.Cursor = Cursors.Default; break;
                        }

                    case 7:
                        {
                            NesneSekil = HangiNesneOlusturulacak("Dur");
                            NesneOlustur(0, "Dur", point.Y - Image.FromFile(NesneSekil).Size.Height / 2, point.X - Image.FromFile(NesneSekil).Size.Width / 2, "", "", true);
                            PROGRAMDEGISTI = true;
                            NesneEkle = -1;
                            this.Cursor = Cursors.Default; break;
                        }
                }
            }
        }

        public string HangiNesneOlusturulacak(string NesneTuru)
        {
            string Sekil = "";

            if (NesneTuru == "Baslat") Sekil = Application.StartupPath + "\\Nesneler\\Baslat.png";
            if (NesneTuru == "Islem") Sekil = Application.StartupPath + "\\Nesneler\\Islem.png";
            if (NesneTuru == "Degiskenler") Sekil = Application.StartupPath + "\\Nesneler\\Degiskenler.png";
            if (NesneTuru == "Eger") Sekil = Application.StartupPath + "\\Nesneler\\Eger.png";
            if (NesneTuru == "Giris") Sekil = Application.StartupPath + "\\Nesneler\\Giris.png";
            if (NesneTuru == "Cikis") Sekil = Application.StartupPath + "\\Nesneler\\Cikis.png";
            if (NesneTuru == "Dugum") Sekil = Application.StartupPath + "\\Nesneler\\Dugum.png";
            if (NesneTuru == "Dur") Sekil = Application.StartupPath + "\\Nesneler\\Dur.png";

            return Sekil;
        }

        public void NesneOlustur(int ID, string Nesneturu, int top, int left, string islem, string degeri, bool kayitislem)
        {
            #region METOT ICI DEGISKENLER

            string[] Harfler = { "a", "b", "c", "d", "e", "f", "g", "h", "k", "l", "m", "n", "o", "p", "r", "s", "t", "q", "y", "z" };
            int rasgele_sayi = rasgele.Next(0, 1000);
            int rasgele_harf = rasgele.Next(0, 19);
            string rsg_secilen_harf = Harfler[rasgele_harf];
            string Nesne_turu = Nesneturu;
            string Degeri = degeri;
            string Islem = islem;
            string NesneSekil = "";
            string virgul = "";
            int Baslat_nesne_sayisi = 0;
            int Genislik = 0;
            int Yukseklik = 0;
            int Top = top;
            int Left = left;
            int IDNesne = 0;
            bool devam = true;
            bool ID_Var = false;

            #endregion

            if (Degeri != "")
            {
                virgul = ",";
            }

            foreach (_Nesneler nesne in _Nesne)
            {
                if (nesne.Turu == Nesneturu)
                { Baslat_nesne_sayisi++; }
            }

            if ((Baslat_nesne_sayisi > 0) && Nesne_turu == "Baslat")
            { MessageBox.Show("İkinci Bir Başlat Nesnesi Ekleyemezsiniz !"); devam = false; }
            else { devam = true; }

            NesneSekil = HangiNesneOlusturulacak(Nesne_turu);

            if (devam)
            {
                #region PICTUREBOX OLUSTURMA

                picturebox[NesneInt] = new PictureBox();
                picturebox[NesneInt].Name = Nesne_turu + "_" + TarihDuzenle(DateTime.Now.ToLongTimeString().ToString()) + "_Pic_" + rsg_secilen_harf + rasgele_sayi;
                picturebox[NesneInt].SetBounds(Left, Top, 0, 0);
                picturebox[NesneInt].BackgroundImageLayout = ImageLayout.Center;
                picturebox[NesneInt].BackgroundImage = Image.FromFile(NesneSekil);
                picturebox[NesneInt].Size = new Size(Image.FromFile(NesneSekil).Size.Width, Image.FromFile(NesneSekil).Size.Height);
                picturebox[NesneInt].BackColor = Color.Transparent;
                picturebox[NesneInt].BorderStyle = BorderStyle.None;
                picturebox[NesneInt].MouseUp += new System.Windows.Forms.MouseEventHandler(picture_up);
                picturebox[NesneInt].MouseDown += new System.Windows.Forms.MouseEventHandler(picture_down);
                picturebox[NesneInt].MouseMove += new System.Windows.Forms.MouseEventHandler(picture_move);
                picturebox[NesneInt].Click += new System.EventHandler(picture_click);
                picturebox[NesneInt].DoubleClick += new System.EventHandler(picture_doubleclick);
                picturebox[NesneInt].Cursor = Cursors.SizeAll;
                Genislik = picturebox[NesneInt].Width;
                Yukseklik = picturebox[NesneInt].Height;
                Controls.Add(picturebox[NesneInt]);

                #endregion

                #region LABEL OLUSTURMA

                TextLabel[NesneInt] = new Label();
                TextLabel[NesneInt].Name = picturebox[NesneInt].Name + "_Lab";
                TextLabel[NesneInt].Text = LabelTextKisitlama(Nesne_turu, Islem) + virgul + Degeri;
                TextLabel[NesneInt].Font = new Font("Tahoma", 10, FontStyle.Bold);
                TextLabel[NesneInt].Top = 20;
                TextLabel[NesneInt].AutoSize = true;
                TextLabel[NesneInt].BackColor = Color.Transparent;
                TextLabel[NesneInt].ForeColor = Color.Black;
                TextLabel[NesneInt].MouseUp += new System.Windows.Forms.MouseEventHandler(picture_up);
                TextLabel[NesneInt].MouseDown += new System.Windows.Forms.MouseEventHandler(LabelText_down);
                TextLabel[NesneInt].MouseMove += new System.Windows.Forms.MouseEventHandler(picture_move);
                TextLabel[NesneInt].Click += new System.EventHandler(label_click);
                TextLabel[NesneInt].DoubleClick += new System.EventHandler(LabelText_doubleclick);
                TextLabel[NesneInt].Cursor = Cursors.SizeAll;
                TextLabel[NesneInt].SendToBack();

                picturebox[NesneInt].Controls.Add(TextLabel[NesneInt]);
                TextLabel[NesneInt].Left = (picturebox[NesneInt].Width - TextLabel[NesneInt].Width) / 2;
                #endregion



                if (kayitislem)
                {
                    #region KAYIT ID BELIRLEME

                    bas:
                    for (int bul = 0; bul < _Nesne.Count; bul++)
                    {
                        if (_Nesne[bul].ID == IDNesne)
                        { ID_Var = true; }
                    }
                    if (ID_Var)
                    {
                        IDNesne++;
                        ID_Var = false;
                        goto bas;
                    }
                    #endregion

                    NesneKayit(IDNesne, picturebox[NesneInt].Name, Nesne_turu, Top, Left, Islem, Degeri);
                }
                else
                { NesneKayit(ID, picturebox[NesneInt].Name, Nesne_turu, Top, Left, Islem, Degeri); }

                if (NesneInt == 0)
                {
                    En_Son_Secilen = picturebox[NesneInt].Name;
                    Bir_Once_Secilen = En_Son_Secilen;
                    secilen = 0;
                }


                NesneInt++;
                CizgiYenile = true;

            }
        }

        #endregion
    
        #region NESNE EVENTLERI

        public void label_click(object sender, EventArgs e)
        {
            if (!PROGRAMCALISIYOR)
            {
                if (((ModifierKeys & Keys.Shift) == Keys.Shift)||chkOtoBagla.Checked)
                {
                    btnBagla_Click_1(btnBagla, new EventArgs());
                }

                if ((ModifierKeys & Keys.Control) != Keys.Control)
                {
                    string Gecici_secilen = (sender as Label).Name;
                    AktifSeciliDurum = true;

                    Secilen = Gecici_secilen.Split('_')[0] + "_" + Gecici_secilen.Split('_')[1] + "_" + Gecici_secilen.Split('_')[2] + "_" + Gecici_secilen.Split('_')[3];
                    if (Secilen != En_Son_Secilen)
                    {
                        Bir_Once_Secilen = En_Son_Secilen;
                        En_Son_Secilen = Secilen;
                    }
                }
            }
        }

        public void picture_click(object sender, EventArgs e)
        {
            if (!PROGRAMCALISIYOR)
            {
                if (((ModifierKeys & Keys.Shift) == Keys.Shift) || chkOtoBagla.Checked)
                {
                    btnBagla_Click_1(btnBagla, new EventArgs());
                }

                if ((ModifierKeys & Keys.Control) != Keys.Control)
                {
                    Secilen = (sender as PictureBox).Name;
                    if (Secilen != En_Son_Secilen)
                    {
                        Bir_Once_Secilen = En_Son_Secilen;
                        En_Son_Secilen = Secilen;

                    }
                    AktifSeciliDurum = true;
                }
            }
        }

        public void picture_move(object sender, MouseEventArgs e)
        {
            if (!PROGRAMCALISIYOR)
            {
                if (En_Son_Secilen == picturebox[secilen].Name)
                {
                    if (secildi)
                    {
                        CizgiSil.Clear(BackColor);
                        CizgiYenile = true;

                        fark_yukseklik = maus_yukselik - e.Y;
                        fark_sol = maus_sol - e.X;

                        if ((MousePosition.Y > 115) && (MousePosition.X > 100))
                        {       
                            foreach (_Nesneler TasNes in _SecilenNesneler)
                            {
                                picturebox[NameIleIDGetir(TasNes.NesneAdi)].Top = picturebox[NameIleIDGetir(TasNes.NesneAdi)].Top - fark_yukseklik;
                                picturebox[NameIleIDGetir(TasNes.NesneAdi)].Left = picturebox[NameIleIDGetir(TasNes.NesneAdi)].Left - fark_sol;
                                btnBagla.Visible = false;
                                btnSil.Visible = false;
                            }
                        }
                        PROGRAMDEGISTI = true;
                    }
                }
            }

        }

        public void picture_up(object sender, MouseEventArgs e)
        {
            if (!PROGRAMCALISIYOR)
            {
                GeriYukleKayit("Yerdegistirme",Secilen, "Nesne", 0, 0, new Point(0, 0), new Point(0, 0), "",false);
                secildi = false;
                BtnBaglaSil();
            }
        }

        public void picture_doubleclick(object sender, EventArgs e)
        {
            if (!PROGRAMCALISIYOR)
            {
                PencereAc();
            }
        }

        public void LabelText_doubleclick(object sender, EventArgs e)
        {
            if (!PROGRAMCALISIYOR)
            {
                string Gecici_secilen = (sender as Label).Name;

                Secilen = Gecici_secilen.Split('_')[0] + "_" + Gecici_secilen.Split('_')[1] + "_" + Gecici_secilen.Split('_')[2] + "_" + Gecici_secilen.Split('_')[3];
                if (Secilen != En_Son_Secilen)
                {
                    Bir_Once_Secilen = En_Son_Secilen;
                    En_Son_Secilen = Secilen;
                }

                PencereAc();
            }
        }

        public void DownEntegre()
        {
            if (Secilen != En_Son_Secilen)
            {
                Bir_Once_Secilen = En_Son_Secilen;
                En_Son_Secilen = Secilen;
            }

            for (int i = 0; i < 1000; i++)
            {
                if (En_Son_Secilen == picturebox[i].Name)
                { secilen = i; break; }
            }

            for (int i = 0; i < 1000; i++)
            {
                if (Bir_Once_Secilen == picturebox[i].Name)
                { onceki_secilen = i; break; }
            }

            SonIkiNesneGetir();

            if ((ModifierKeys & Keys.Control) != Keys.Control)
            {

                if (Bir_Once_Secilen != En_Son_Secilen)
                {
                    picturebox[onceki_secilen].BackColor = Color.Transparent;
                    TextLabel[onceki_secilen].ForeColor = Color.Black;
                }

                foreach (Label lab in TextLabel)
                {
                    if (lab != null)
                    {
                        if (lab.ForeColor == Color.White)
                        {
                            lab.ForeColor = Color.Black;
                        }
                    }
                }

                foreach (PictureBox pic in picturebox)
                {
                    if (pic != null)
                    {
                        if (pic.BackColor == Color.Red)
                        {
                            pic.BackColor = Color.Transparent;
                        }
                    }
                }
            }

            for (int i = 0; i < 1000; i++)
            {
                if (Secilen == picturebox[i].Name)
                {
                    picturebox[i].BackColor = Color.Red;
                    TextLabel[i].ForeColor = Color.White;
                    picturebox[i].Cursor = Cursors.SizeAll;
                    secilen = i; break;
                }
            }
            BtnBaglaSil();
        }

        public void NesneSecmeAndCokluSecme()
        {
            if ((ModifierKeys & Keys.Control) != Keys.Control)
            {
                NesAraBul = false;

                foreach (_Nesneler NesAra in _SecilenNesneler)
                {
                    if (NesAra.NesneAdi == Secilen.ToString())
                    {
                        NesAraBul = true;
                        break;
                    }
                }

                if ((_SecilenNesneler.Count <= 1))
                {
                    _SecilenNesneler.Clear();
                    DownEntegre();
                    foreach (_Nesneler sec in _Nesne)
                    {
                        if (sec.NesneAdi == Secilen.ToString())
                        {
                            SecilenN.ID = sec.ID;
                            SecilenN.NesneAdi = Secilen;
                            SecilenN.Turu = sec.Turu;
                            SecilenN.Top = sec.Top;
                            SecilenN.Left = sec.Left;
                            SecilenN.Degeri = sec.Degeri;
                            SecilenN.Islem = sec.Islem;
                            _SecilenNesneler.Add(SecilenN);
                        }
                    }
                }

                if (!NesAraBul)
                {
                    _SecilenNesneler.Clear();
                    foreach (_Nesneler sec in _Nesne)
                    {
                        if (sec.NesneAdi == Secilen.ToString())
                        {
                            SecilenN.ID = sec.ID;
                            SecilenN.NesneAdi = Secilen;
                            SecilenN.Turu = sec.Turu;
                            SecilenN.Top = sec.Top;
                            SecilenN.Left = sec.Left;
                            SecilenN.Degeri = sec.Degeri;
                            SecilenN.Islem = sec.Islem;
                            _SecilenNesneler.Add(SecilenN);
                        }
                    }
                    DownEntegre();

                }
                NesAraBul = false;
            }
            else
            {
                foreach (_Nesneler NesAra in _SecilenNesneler)
                {
                    if (NesAra.NesneAdi == Secilen.ToString())
                    {
                        NesAraBul = true;
                        break;
                    }
                }
                DownEntegre();

                if (!NesAraBul)
                {
                    foreach (_Nesneler sec in _Nesne)
                    {
                        if (sec.NesneAdi == Secilen.ToString())
                        {
                            SecilenN.ID = sec.ID;
                            SecilenN.NesneAdi = Secilen;
                            SecilenN.Turu = sec.Turu;
                            SecilenN.Top = sec.Top;
                            SecilenN.Left = sec.Left;
                            SecilenN.Degeri = sec.Degeri;
                            SecilenN.Islem = sec.Islem;
                            _SecilenNesneler.Add(SecilenN);
                        }
                    }
                }
                else
                {
                    for (int l = 0; l < _SecilenNesneler.Count; l++)
                    {
                        if (_SecilenNesneler[l].NesneAdi == Secilen)
                        {
                            _SecilenNesneler.RemoveAt(l);
                            break;
                        }
                    }
                    #region Rengi Değiştir
                    SonIkiNesneGetir();

                    picturebox[sn].BackColor = Color.Transparent;
                    TextLabel[(sn)].ForeColor = Color.Black;
                    #endregion
                }
                NesAraBul = false;
            }
        }

        public void LabelText_down(object sender, MouseEventArgs e)
        {
            if (!PROGRAMCALISIYOR)
            {
                string Gecici_secilen = (sender as Label).Name;
                Secilen = Gecici_secilen.Split('_')[0] + "_" + Gecici_secilen.Split('_')[1] + "_" + Gecici_secilen.Split('_')[2] + "_" + Gecici_secilen.Split('_')[3];

                if (e.Button == MouseButtons.Right)
                {

                    //DownEntegre();

                    Point ptLowerLeft = new Point(picturebox[secilen].Width, 0);
                    ptLowerLeft = picturebox[secilen].PointToScreen(ptLowerLeft);
                    MenuNesneSecenekleri.Show(ptLowerLeft);
                }

                if ((e.Button == MouseButtons.Left))
                {
                    secildi = true;
                    maus_yukselik = e.Y;
                    maus_sol = e.X;
                    NesneSecmeAndCokluSecme();
                }
            }
        }

        public void picture_down(object sender, MouseEventArgs e)
        {
            if (!PROGRAMCALISIYOR)
            {
                Secilen = (sender as PictureBox).Name;

                if (e.Button == MouseButtons.Right)
                {
                    //DownEntegre();

                    Point ptLowerLeft = new Point(picturebox[secilen].Width, 0);
                    ptLowerLeft = picturebox[secilen].PointToScreen(ptLowerLeft);
                    MenuNesneSecenekleri.Show(ptLowerLeft);
                }

                if ((e.Button == MouseButtons.Left))
                {
                    secildi = true;
                    maus_yukselik = e.Y;
                    maus_sol = e.X;
                    NesneSecmeAndCokluSecme();
                }
            }
        }

        #endregion

        #region PROGRAM ICI NESNESEL FONKSIYONLAR

        private void button6_Click(object sender, EventArgs e)
        {
            Cursor = Create(Path.Combine(Application.StartupPath, "Nesne Ekle Icon.cur"));
            NesneEkle = 6;
        }

        private void FrmAna_Click(object sender, EventArgs e)
        {
            FormaNesneEkle();

            foreach (Label lab in TextLabel)
            {
                if (lab != null)
                {
                    if (lab.ForeColor == Color.White)
                    {
                        lab.ForeColor = Color.Black;
                    }
                }
            }

            foreach (PictureBox pic in picturebox)
            {
                if (pic != null)
                {
                    if (pic.BackColor == Color.Red)
                    {
                        pic.BackColor = Color.Transparent;
                    }
                }
            }

            _SecilenNesneler.Clear();
            btnBagla.Hide();
            btnSil.Hide();
            CizgiYenile = true;
            Refresh();
           
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FrmIletisim iletisim = new FrmIletisim(); iletisim.ShowDialog();
        }

        private void btnSil_Click_1(object sender, EventArgs e)
        {
            SonIkiNesneGetir();

            for (int i = 0; i < _SecilenNesneler.Count; i++)
            {
                for (int m = 0; m < listBox8.Items.Count; m++)
                {
                    if (((listBox8.Items[m].ToString().Split('_')[2]) == NesneIleIDGetir(NameIleIDGetir(_SecilenNesneler[i].NesneAdi)).ToString()) || ((listBox8.Items[m].ToString().Split('_')[1]) == NesneIleIDGetir(NameIleIDGetir(_SecilenNesneler[i].NesneAdi)).ToString()))
                    {
                        Controls.Remove(TextLabelEger[m]);
                    }
                }

                if (_Cizgi.Count > 0)
                {
                    bas:
                    for (int l = 0; l < _Cizgi.Count; l++)
                    {
                        if ((IDIleNesneGetir(_Cizgi[l].Baslangic) ==NameIleIDGetir(_SecilenNesneler[i].NesneAdi)) || (IDIleNesneGetir(_Cizgi[l].Bitis) == NameIleIDGetir(_SecilenNesneler[i].NesneAdi)))
                        {
                            _Cizgi.RemoveAt(l); goto bas;
                        }

                    }
                }

                for (int l = 0; l < _Nesne.Count; l++)
                {
                    if (_Nesne[l].NesneAdi == _SecilenNesneler[i].NesneAdi)
                    {
                        _Nesne.RemoveAt(l);
                        break;
                    }
                }
            Controls.Remove(picturebox[NameIleIDGetir(_SecilenNesneler[i].NesneAdi)]);

            }
            CizgiYenile = true;
            CizgiSil = this.CreateGraphics();
            CizgiSil.Clear(this.BackColor);
            btnSil.Visible = false;
            btnBagla.Visible = false;
            En_Son_Secilen = Bir_Once_Secilen;
            PROGRAMDEGISTI = true;

        }

        private void btnBagla_Click_1(object sender, EventArgs e)
        {
            bool AnacizgiBaglantiDurumu = false;
            bool cizgiBaglantiDurumu = true;

            CizgiYaziSay = 0;

            for (int i = 0; i < _Cizgi.Count(); i++)
            {
                SonIkiNesneGetir();

                if ((IDIleNesneGetir(_Cizgi[i].Baslangic) == sn_o))
                {
                    AnacizgiBaglantiDurumu = true; break;
                }

                if ((IDIleNesneGetir(_Cizgi[i].Bitis) == sn_o) && (IDIleNesneGetir(_Cizgi[i].Baslangic) == sn))
                {
                    AnacizgiBaglantiDurumu = true; break;
                }
            }

            if (picturebox[secilen].Name == picturebox[onceki_secilen].Name)
            {
                AnacizgiBaglantiDurumu = true;
            }

            if (picturebox[(onceki_secilen)].Name.Split('_')[0] == "Eger")
            {
                foreach (_Cizgiler Nes in _Cizgi)
                {
                    if (Nes.Baslangic == NesneIleIDGetir(onceki_secilen))
                    {
                        CizgiYaziSay++;
                        if (Nes.Bitis == NesneIleIDGetir(secilen))
                        {
                            cizgiBaglantiDurumu = false;
                        }
                    }
                }

                if (cizgiBaglantiDurumu)
                {
                    if (CizgiYaziSay <= 1)
                    { AnacizgiBaglantiDurumu = false; }
                    else
                    { AnacizgiBaglantiDurumu = true; }
                }
            }


            if ((!AnacizgiBaglantiDurumu)) { CizgiOlustur(); }
            else { MessageBox.Show("Bu nesne ile birden fazla bağlayamazsınız."); }
        }

        private void FrmAna_Load(object sender, EventArgs e)
        {
            ClsRgtUza uzantı = new ClsRgtUza();
            uzantı.RecommendedPrograms();
           
            Text = PROGRAM_ADI;
        }

        private void FrmAna_Paint(object sender, PaintEventArgs e)
        {
         
            CizgiKonumDegistirmeKayit();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Calistir();
        }

        private void button10_MouseMove(object sender, MouseEventArgs e)
        {
            btn1.Location = new Point(-12, 85);
        }
        
        private void FrmAna_MouseMove(object sender, MouseEventArgs e)
        {
            btn1.Location = new Point(-91, 85);
            btn2.Location = new Point(-91, 125);
            btn3.Location = new Point(-91, 165);
            btn4.Location = new Point(-91, 205);
            btn5.Location = new Point(-91, 245);
            btn6.Location = new Point(-91, 285);
            btn7.Location = new Point(-91, 325);
            btn8.Location = new Point(-91, 365);
            panel1.Location = new Point(-90, 405);

            if (AktifSeciliDurum)
            {
                btnBagla.Hide();
                btnSil.Hide();
                CizgiYenile = true;

                CizgiSil = this.CreateGraphics();
                CizgiSil.Clear(this.BackColor);

                AktifSeciliDurum = false;
            }
           
        }  

        private void btn2_MouseMove(object sender, MouseEventArgs e)
        {
            btn2.Location = new Point(-12, 125);
        }

        private void btn3_MouseMove(object sender, MouseEventArgs e)
        {
            btn3.Location = new Point(-17, 165);
        }

        private void btn4_MouseMove(object sender, MouseEventArgs e)
        {
            btn4.Location = new Point(-17, 205);
        }

        private void btn5_MouseMove(object sender, MouseEventArgs e)
        {
            btn5.Location = new Point(-17, 245);
        }

        private void btn6_MouseMove(object sender, MouseEventArgs e)
        {
            btn6.Location = new Point(-17, 285);
        }

        private void btn7_MouseMove(object sender, MouseEventArgs e)
        {
            btn7.Location = new Point(-17, 325);
        }

        private void btn8_MouseMove(object sender, MouseEventArgs e)
        {
            btn8.Location = new Point(-17, 365);
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            Cursor = Create(Path.Combine(Application.StartupPath, "Nesne Ekle Icon.cur"));
            NesneEkle = 0;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            Cursor = Create(Path.Combine(Application.StartupPath, "Nesne Ekle Icon.cur"));
            NesneEkle = 2;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            Cursor = Create(Path.Combine(Application.StartupPath, "Nesne Ekle Icon.cur"));
            NesneEkle = 1;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            Cursor = Create(Path.Combine(Application.StartupPath, "Nesne Ekle Icon.cur"));
            NesneEkle = 5;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            Cursor = Create(Path.Combine(Application.StartupPath, "Nesne Ekle Icon.cur"));
            NesneEkle = 3;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            Cursor = Create(Path.Combine(Application.StartupPath, "Nesne Ekle Icon.cur"));
            NesneEkle = 4;
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            Cursor = Create(Path.Combine(Application.StartupPath, "Nesne Ekle Icon.cur"));
            NesneEkle = 6;
        }

        private void FrmAna_SizeChanged(object sender, EventArgs e)
        {
            AktifSeciliDurum = true;
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
           if (_FrmCalisma ==null || _FrmCalisma.IsDisposed)
            { 
                _FrmCalisma = new FrmCalismaHizi();
                _FrmCalisma.Left = (this.Width) - _FrmCalisma.Width - 20;
                _FrmCalisma.Top = 110;
                _FrmCalisma.Show();
            }
        }

        private void toolStripButton3_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ProjeAc(e.ClickedItem.ToolTipText);
        }

        private void toolStripButton3_DropDownOpening(object sender, EventArgs e)
        {
            string AcilacakDosyaYolu = Application.StartupPath + "\\Settings\\Son Acilan.fgps";

            if (!File.Exists(AcilacakDosyaYolu)) return;
            string[] GelenList = File.ReadAllLines(AcilacakDosyaYolu);

            toolStripButton3.DropDownItems.Clear();
            int id = 0;

            for (int i = GelenList.Length-1; i >0 ; i--)
            {
                if (GelenList[i].Trim() != "")
                {
                    toolStripButton3.DropDownItems.Add((id + 1) + " - " + Path.GetFileNameWithoutExtension(GelenList[i]) + ".fgp");
                    FileInfo info = new FileInfo(GelenList[0]);
                    toolStripButton3.DropDownItems[id].ToolTipText = GelenList[i];
                    id++;
                }
                if (id >= 10) break;
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (_FrmAyarlar == null || _FrmAyarlar.IsDisposed)
            {
                _FrmAyarlar = new FrmAyarlar();
                _FrmAyarlar.Show();
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
        }

        private void CalismaAlanıSagTık_MouseMove(object sender, MouseEventArgs e)
        {
            CalismaAlanıSagTık.Text = PROGRAM_ADI;
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void projeyiÇalıştırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton12_Click(toolStripButton12, new EventArgs());
        }

        private void projeyiBekletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton11_Click(toolStripButton11, new EventArgs());
        }

        private void projeyiDurdurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(toolStripButton1, new EventArgs());
        }

        private void yardımToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton7_Click(toolStripButton7, new EventArgs());
        }

        private void ayarlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton10_Click(toolStripButton10, new EventArgs());
        }

        private void CalismaAlanıSagTık_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (_FrmCalisma == null || _FrmCalisma.IsDisposed)
            {
                //Cizgim.Clear(BackColor);

                Show();

                this.WindowState = FormWindowState.Maximized;

            }
            else { this.WindowState = FormWindowState.Minimized; }
            CizgiYenile = true;

        }

        private void FrmAna_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Hide();
                CalismaAlanıSagTık.Visible = true;
            }
        }

        private void Tik_mouse(object sender, MouseEventArgs e)
        {
            LogoSagTık.Show(Control.MousePosition);
        }

        private void CalismaAlanıSagTık_BalloonTipShown(object sender, EventArgs e)
        {
            CalismaAlanıSagTık.ShowBalloonTip(30000);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (_FrmGuncellemePaneli == null || _FrmGuncellemePaneli.IsDisposed)
            {
                _FrmGuncellemePaneli = new FrmGuncellemePaneli();
                _FrmGuncellemePaneli.Show();
            }
        }

        private void backgroundWorker1_DoWork_1(object sender, DoWorkEventArgs e)
        {
           

            try
            {
                ServerDateBase.Open();

                SqlDataReader reader = new SqlCommand("select surum from Program  where adi='FlowTurk G.P.S.'", ServerDateBase).ExecuteReader();
                while (reader.Read())
                {
                    YeniVersiyon = reader[0].ToString();
                }
                reader.Close();

                ServerDateBase.Close();

                if (YeniVersiyon.Trim() != "")
                {
                    if (YeniVersiyon.Trim() != Application.ProductVersion)
                    {
                        UpdateKontrolTrueFalse = true;
                        BtnUpdate.Visible = true;
                       
                        new FrmGuncellemePaneli().ShowDialog();
                    }
                }
            }
            catch { return; }
        }

        private void FlowTurkUpdateKontrolTick_Tick(object sender, EventArgs e)
        {
           if(!UpdateKontrolTrueFalse)
            {
                try { FlowTurkUpdateKontrol.RunWorkerAsync(); } catch { return;}
               
            }
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem6_DropDownOpening(object sender, EventArgs e)
        {
            string AcilacakDosyaYolu = Application.StartupPath + "\\Settings\\Son Acilan.fgps";

            if (!File.Exists(AcilacakDosyaYolu)) return;
            string[] GelenList = File.ReadAllLines(AcilacakDosyaYolu);

            toolStripMenuItem6.DropDownItems.Clear();
            int id = 0;

            for (int i = GelenList.Length - 1; i > 0; i--)
            {
                if (GelenList[i].Trim() != "")
                {
                    toolStripMenuItem6.DropDownItems.Add((id + 1) + " - " + Path.GetFileNameWithoutExtension(GelenList[i]) + ".fgp");
                    FileInfo info = new FileInfo(GelenList[0]);
                    toolStripMenuItem6.DropDownItems[id].ToolTipText = GelenList[i];
                    id++;
                }
                if (id >= 10) break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Drawing.Graphics Cizim;
            Cizim = this.CreateGraphics();
            Cizim.Clear(this.BackColor);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            panel1.Location = new Point(-2, 405);
        }

        private void toolStripMenuItem28_Click(object sender, EventArgs e)
        {
            btnSil_Click_1(btnBagla, new EventArgs());
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            GeriSarPlay();
        }

        private void toolStripMenuItem27_Click(object sender, EventArgs e)
        {
            SonIkiNesneGetir();

            for (int i = 0; i < _SecilenNesneler.Count; i++)
            {
                for (int m = 0; m < listBox8.Items.Count; m++)
                {
                    if (((listBox8.Items[m].ToString().Split('_')[2]) == NesneIleIDGetir(NameIleIDGetir(_SecilenNesneler[i].NesneAdi)).ToString()) || ((listBox8.Items[m].ToString().Split('_')[1]) == NesneIleIDGetir(NameIleIDGetir(_SecilenNesneler[i].NesneAdi)).ToString()))
                    {
                        Controls.Remove(TextLabelEger[m]);
                    }
                }

                if (_Cizgi.Count > 0)
                {
                    bas:
                    for (int l = 0; l < _Cizgi.Count; l++)
                    {
                        if ((IDIleNesneGetir(_Cizgi[l].Baslangic) == NameIleIDGetir(_SecilenNesneler[i].NesneAdi)) || (IDIleNesneGetir(_Cizgi[l].Bitis) == NameIleIDGetir(_SecilenNesneler[i].NesneAdi)))
                        {
                            _Cizgi.RemoveAt(l); goto bas;
                        }

                    }
                }
            }

            CizgiYenile = true;
            Refresh();

            PROGRAMDEGISTI = true;
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            IleriSarPlay();
        }

        private void FrmAna_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }
       

        private void FrmAna_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = false;
        }

        private void FrmAna_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.Z))
            {
               toolStripButton16.PerformClick();
            }

            if (e.Control && (e.KeyCode == Keys.Y))
            {
                toolStripButton15.PerformClick();
            }
        }

        private void toolStripButton16_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {
         

        }

        private void btn8_Click(object sender, EventArgs e)
        {
            Cursor = Create(Path.Combine(Application.StartupPath, "Nesne Ekle Icon.cur"));
            NesneEkle = 7;
        }

        private void toolStripButton3_ButtonClick(object sender, EventArgs e)
        {
            ProjeAc("");
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            YeniProje();
        }

        private void toolStripButton8_ButtonClick(object sender, EventArgs e)
        {
            if (this.Text.Trim().Length >PROGRAM_ADI.Length)
            {
                if ((PROGRAMDEGISTI) && (_Nesne.Count > 0) && SONACILANPROGRAM != "")
                {
                    KaydetPaket(SONACILANPROGRAM);
                }

            }
            else
            {
                ProjeKaydet();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["FrmVeriGirisCikis"]!=null ) 
            {
                _FrmVeriGirisCikis.Close();
                PROGRAMDURDU = true;
                PROGRAMCALISIYOR = false;
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            CizgiSil = this.CreateGraphics();
            CizgiSil.Clear(this.BackColor);
            CizgiYenile = true;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ProjeKaydet();
        }
    
        private void timer1_Tick(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear();
            listBox7.Items.Clear();
            listBox8.Items.Clear();
            listBox9.Items.Clear();
            listBox11.Items.Clear();

            label1.Text = SayacCizgi.ToString();
            //GuncelGeriSarListCount =
            IlerGeriBulunanSıra = _GeriIleriYukle.Count-1;
            foreach (_Nesneler nesne in _Nesne)
            {
                listBox2.Items.Add(nesne.NesneAdi);
            }

            foreach (_Nesneler nesne in _SecilenNesneler)
            {
                listBox9.Items.Add(nesne.NesneAdi);
            }

            foreach (_GeriYukle nesne in _GeriIleriYukle)
            {
                if (nesne.NesneAdi != null)
                {
                    listBox11.Items.Add(nesne.YapilanIslem+" "+ nesne.NesneAdi + " " + nesne.Left + "-" + nesne.Top + "-" + nesne.Islem);
                }
                if ((nesne.Baslangic != 0) || (nesne.Bitis != 0))
                {
                    listBox11.Items.Add( nesne.Baslangic + " " + nesne.Bitis);
                }

            }


            foreach (_Degiskenler nesne in _Degisken_Yedek)
            {
                listBox7.Items.Add(nesne.Degisken_Adi + " : " + nesne.Degeri);
            }

            foreach (_Cizgiler cizgi in _Cizgi)
            {
                listBox3.Items.Add(cizgi.Baslangic + " " + cizgi.Bitis + " " + cizgi.Yazi);
            }

            for (int i = 0; i <picturebox.Count(); i++)
            {
                if (picturebox[i] != null)
                    listBox5.Items.Add(picturebox[i].Name);
            }

            for (int i = 0; i <TextLabelEger.Count(); i++)
            {
                if (TextLabelEger[i] != null)
                    listBox8.Items.Add(TextLabelEger[i].Name);
            }
        }

        private void bilgileriGösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnBagla_Click_1(sender, new EventArgs());
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            SonIkiNesneGetir();

            for (int m = 0; m < listBox8.Items.Count; m++)
            {
                if (((listBox8.Items[m].ToString().Split('_')[2]) == NesneIleIDGetir(secilen).ToString()) || ((listBox8.Items[m].ToString().Split('_')[1]) == NesneIleIDGetir(secilen).ToString()))
                {
                    Controls.Remove(TextLabelEger[m]);
                }
            }

            if (_Cizgi.Count > 0)
            {
            bas:
                for (int l = 0; l < _Cizgi.Count; l++)
                {
                    if ((IDIleNesneGetir(_Cizgi[l].Baslangic) == secilen) || (IDIleNesneGetir(_Cizgi[l].Bitis) == secilen))
                    {
                        _Cizgi.RemoveAt(l); goto bas;
                    }

                }
            }

            MenuNesneSecenekleri.Close();

            FrmAna_Click(sender, new EventArgs());
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            btnSil_Click_1(sender, new EventArgs());
        }

        private void müşteriyiSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton12_Click(sender, new EventArgs());
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            if (toolStripButton12.Enabled)
            {
                toolStripButton12.Enabled = false;
                toolStripButton11.Enabled = true;

            }
            else
            {
                toolStripButton12.Enabled = true;
                toolStripButton11.Enabled = false;
            }
            PROGRAMBEKLET = true;
        }

        private void yolcuTransferOnaylaSeçToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PencereAc();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            Cizgi_Baslangic = -1;

            for (int i = 0; i < _Nesne.Count(); i++)
            {
                if (_Nesne[i].Turu == "Baslat")
                {
                    Cizgi_Baslangic = _Nesne[i].ID; break;
                }
            }

            if (Cizgi_Baslangic == -1)
            {
                MessageBox.Show(Hata.HataMesajlari(3));
                PROGRAMDURDU = false;
            }
            else
            {
                if (((_FrmVeriGirisCikis == null) || (_FrmVeriGirisCikis.IsDisposed)) && _Nesne.Count > 0)
                {
                    foreach (PictureBox pic in picturebox)
                    {
                     if(pic!=null)
                        pic.Cursor = Cursors.No;
                    }

                    _FrmVeriGirisCikis = new FrmVeriGirisCikis();
                    _FrmVeriGirisCikis.Left = (this.Width) - _FrmVeriGirisCikis.Width - 20;
                    _FrmVeriGirisCikis.Top = 110;
                    ProgramCalisiyor_Arkaplan.RunWorkerAsync();
                    _FrmVeriGirisCikis.Show();

                    CizgiYenile = true;

                    CizgiSil = this.CreateGraphics();
                    CizgiSil.Clear(this.BackColor);

                    PROGRAMCALISIYOR = true;
                    PROGRAMDURDU = false;
                }
                else
                {
                    PROGRAMBEKLET = false;
                }

                if (!PROGRAMDURDU)
                {
                    if (toolStripButton11.Enabled)
                    {
                        toolStripButton12.Enabled = true;
                        toolStripButton11.Enabled = false;

                    }
                    else
                    {
                        toolStripButton12.Enabled = false;
                        toolStripButton11.Enabled = true;
                    }
                }

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            LabelTextGuncelle();
        }

        private void FrmAna_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                NesneEkle = -1;
                this.Cursor = Cursors.Default;
            }
        }

        private void FrmAna_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                c.Dispose();
            }
        
        }

        private void FrmAna_Activated(object sender, EventArgs e)
        {
            CizgiYenile = true;
        }
    
       

        private void güncellemeleriKontrolEtToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

        private void FrmAna_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PROGRAMDEGISTI)
            {
                DialogResult Mesaj = MessageBox.Show("Proje kaydedilmemiş ! \n\nDeğişiklikler kaydedilsin mi ?", "Proje Kaydetme", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                if (DialogResult.Yes == Mesaj)
                    ProjeKaydet();

                if (DialogResult.Cancel == Mesaj)
                    e.Cancel = true;
                this.CalismaAlanıSagTık.Dispose();
            }
        }

        private void LogoSagTık_Opening(object sender, CancelEventArgs e)
        {

        }

        private void backgroundWorker1_DoWork_2(object sender, DoWorkEventArgs e)
        {

        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
                   
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            ColorDialog renk = new ColorDialog();
            renk.ShowDialog();
            CizgiRenk = renk.Color;  
        }
    }
}
