using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowTürk_Görsel_Programlama_Simülatörü
{
    class ClsKarsilastir
    {
        string Hata = "";

        public string Karsilastir(string Ifade)
        {
            string GelenVeri = Ifade;
            string Parantez_yedek = "";
            bool Parantez_Yakalama = false;
            int Acilan_parantez = 0;
            string Parantez_Ici = "";
            string Cozum = "";
            string[] cozum_parcala;
            GelenVeri = GelenVeri.Replace("OR", "or");
            GelenVeri = GelenVeri.Replace("AND", "and");
            GelenVeri = GelenVeri.Replace("VE", "ve");
            GelenVeri = GelenVeri.Replace("VEYA", "veya");

            //try
            //{
                while (true)
                {
                    GelenVeri = "(" + GelenVeri + ")";

                    for (int i = 0; i < GelenVeri.Length; i++)
                    {

                        if (GelenVeri[i].ToString() == "(")
                        {
                            Parantez_Yakalama = true; Acilan_parantez++;
                            if (Acilan_parantez > 1) { Parantez_Ici = ""; } continue;

                        }
                        if (GelenVeri[i].ToString() == ")") { Parantez_Yakalama = false; break; }

                        if (Parantez_Yakalama) { Parantez_Ici = Parantez_Ici + GelenVeri[i].ToString(); }

                    }

                    Parantez_yedek = Parantez_Ici;

                    while (true)
                    {

                        int ID = Parantez_Ici.IndexOf(">=");

                        if (ID > 0)
                        {
                            Cozum = Parcalayici(Parantez_Ici, ID, ID + 1, ">=");
                            cozum_parcala = Cozum.Split('?');
                            Parantez_Ici = Parantez_Ici.Replace(cozum_parcala[0], cozum_parcala[1]);
                        }

                        ID = Parantez_Ici.IndexOf('>');

                        if (ID > 0)
                        {
                            Cozum = Parcalayici(Parantez_Ici, ID, ID, ">");
                            cozum_parcala = Cozum.Split('?');
                            Parantez_Ici = Parantez_Ici.Replace(cozum_parcala[0], cozum_parcala[1]);

                        }

                        ID = Parantez_Ici.IndexOf("<=");

                        if (ID > 0)
                        {
                            Cozum = Parcalayici(Parantez_Ici, ID, ID + 1, "<=");
                            cozum_parcala = Cozum.Split('?');
                            Parantez_Ici = Parantez_Ici.Replace(cozum_parcala[0], cozum_parcala[1]);
                        }

                        ID = Parantez_Ici.IndexOf('<');

                        if (ID > 0)
                        {
                            Cozum = Parcalayici(Parantez_Ici, ID, ID, "<");
                            cozum_parcala = Cozum.Split('?');
                            Parantez_Ici = Parantez_Ici.Replace(cozum_parcala[0], cozum_parcala[1]);
                        }
 
                        ID = Parantez_Ici.IndexOf("!=");

                        if (ID > 0)
                        {
                            Cozum = Parcalayici(Parantez_Ici, ID, ID + 1, "!=");
                            cozum_parcala = Cozum.Split('?');
                            Parantez_Ici = Parantez_Ici.Replace(cozum_parcala[0], cozum_parcala[1]);
                        }
                      
                        ID = Parantez_Ici.IndexOf('=');

                        if (ID > 0)
                        {
                            Cozum = Parcalayici(Parantez_Ici, ID, ID, "=");
                            cozum_parcala = Cozum.Split('?');
                            Parantez_Ici = Parantez_Ici.Replace(cozum_parcala[0], cozum_parcala[1]);
                        }

                       

                        ID = Parantez_Ici.IndexOf('&');

                        if (ID > 0)
                        {
                            Cozum = Parcalayici(Parantez_Ici, ID, ID, "&");
                            cozum_parcala = Cozum.Split('?');
                            Parantez_Ici = Parantez_Ici.Replace(cozum_parcala[0], cozum_parcala[1]);
                        }

                        ID = Parantez_Ici.IndexOf("||");

                        if (ID > 0)
                        {
                            Cozum = Parcalayici(Parantez_Ici, ID, ID + 1, "||");
                            cozum_parcala = Cozum.Split('?');
                            Parantez_Ici = Parantez_Ici.Replace(cozum_parcala[0], cozum_parcala[1]);
                        }

                        ID = Parantez_Ici.IndexOf("or");

                        if (ID > 0)
                        {
                            Cozum = Parcalayici(Parantez_Ici, ID, ID + 1, "or");
                            cozum_parcala = Cozum.Split('?');
                            Parantez_Ici = Parantez_Ici.Replace(cozum_parcala[0], cozum_parcala[1]);
                        }

                        ID = Parantez_Ici.IndexOf("and");

                        if (ID > 0)
                        {
                            Cozum = Parcalayici(Parantez_Ici, ID, ID + 2, "and");
                            cozum_parcala = Cozum.Split('?');
                            Parantez_Ici = Parantez_Ici.Replace(cozum_parcala[0], cozum_parcala[1]);
                        }

                        ID = Parantez_Ici.IndexOf("ve");

                        if (ID > 0)
                        {
                            Cozum = Parcalayici(Parantez_Ici, ID, ID + 1, "ve");
                            cozum_parcala = Cozum.Split('?');
                            Parantez_Ici = Parantez_Ici.Replace(cozum_parcala[0], cozum_parcala[1]);
                        }

                        ID = Parantez_Ici.IndexOf("veya");

                        if (ID > 0)
                        {
                            Cozum = Parcalayici(Parantez_Ici, ID, ID + 3, "veya");
                            cozum_parcala = Cozum.Split('?');
                            Parantez_Ici = Parantez_Ici.Replace(cozum_parcala[0], cozum_parcala[1]);
                        }


                        break;

                    }

                    GelenVeri = GelenVeri.Replace("(" + Parantez_yedek + ")", Parantez_Ici);

                    if (!IslemKaldımı(GelenVeri)) break;
                }
                GelenVeri = GelenVeri.Replace("(", "");
                GelenVeri = GelenVeri.Replace(")", "");
            //}
            //catch { ClsHatalar fnk = new ClsHatalar(); Hata = fnk.HataMesajlari(1); return Hata; }
            return GelenVeri;
         
        }
      
        bool IslemKaldımı(string Islem)
        {
            string Opetorler = "!=<<=>>=&||andorveya";
            bool IslemVar = false;

            for (int i = 0; i <= Islem.Length - 1; i++)
            {
                if ((Opetorler.IndexOf(Islem[i]) > 0))
                { IslemVar = true; }

            }
            return IslemVar;
        }

        bool MantiksalKiyas(double veri1, double veri2, string Islem)
        {
            bool sonuc = false;
            if (Islem == "!=")   { if (veri1 != veri2) sonuc = true; else sonuc = false; }
            if (Islem == "=")    { if (veri1 == veri2) sonuc = true; else sonuc = false; }                       
            if (Islem == ">")    { if (veri1 >  veri2) sonuc = true; else sonuc = false; }
            if (Islem == ">=")   { if (veri1 >= veri2) sonuc = true; else sonuc = false; }
            if (Islem == "<")    { if (veri1 <  veri2) sonuc = true; else sonuc = false; }
            if (Islem == "<=")   { if (veri1 <= veri2) sonuc = true; else sonuc = false; }
            if (Islem == "&")    { if ((veri1 == 0 && veri2 == 0) || (veri1 == 1 && veri2 == 1)) sonuc = true; else sonuc = false; }
            if (Islem == "||")   { if ((veri1 ==0  || veri2 == 1) || (veri1 == 1 || veri2 == 0)) sonuc = true; else sonuc = false; }
            if (Islem == "and")  { if ((veri1 == 0 && veri2 == 0) || (veri1 == 1 && veri2 == 1)) sonuc = true; else sonuc = false; }
            if (Islem == "or")   { if ((veri1 == 0 || veri2 == 1) || (veri1 == 1 || veri2 == 0)) sonuc = true; else sonuc = false; }
            if (Islem == "ve")   { if ((veri1 == 0 && veri2 == 0) || (veri1 == 1 && veri2 == 1)) sonuc = true; else sonuc = false; }
            if (Islem == "veya") { if ((veri1 == 0 || veri2 == 1) || (veri1 == 1 || veri2 == 0)) sonuc = true; else sonuc = false; }

            return sonuc;
        }

        string Parcalayici(string İfade,int ID,int ID2,string islem)
        {
            string Opetorler = "!=<<=>>=&||andorveya";
            string Karsilastirma_turu =islem;
            string veri1 = "";
            string veri2 = "";
            bool isaret2 = true;
            int i = 1;
            int eksisayi = 0;
            bool sonuc= false;
            int int_sonuc = 0;

            for (i = ID2 + 1; i <= İfade.Length - 1; i++)
            {
                if (eksisayi < 1) { if ((İfade[ID + 1] == '-')) { eksisayi++; isaret2 = false; continue; } }
                if (Opetorler.IndexOf(İfade[i]) < 0 )
                {
                    veri2 = veri2 + İfade[i];
                    if (!isaret2) veri2 = "-" + veri2;

                }
                else { break; }
            }


            for (int a = ID - 1; a >= 0; a--)
            {
                
                if (Opetorler.IndexOf(İfade[a]) < 0) { veri1 = İfade[a] + veri1; }
                else { break; }
            }

            //try
            //{
                sonuc = MantiksalKiyas(Convert.ToDouble(veri1), Convert.ToDouble(veri2), Karsilastirma_turu);
            //}
            //catch { ClsHatalar fnk = new ClsHatalar(); Hata = fnk.HataMesajlari(1); return Hata; }
            if (sonuc) int_sonuc = 1;

            return veri1 + Karsilastirma_turu + veri2+"?"+int_sonuc;
        }   
  
    }
}
