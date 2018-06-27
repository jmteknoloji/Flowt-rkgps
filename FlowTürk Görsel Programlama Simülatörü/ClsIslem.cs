using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlowTürk_Görsel_Programlama_Simülatörü
{
    class ClsIslem
    {
        //string Hata = "";

        _Degiskenler Degisken = new _Degiskenler();

        int EksiKontrol(string Gelen,string islem)
        {          
           int ID = 0;
           int sayac=0;
           int tut = 0;
        
            foreach (char c in Gelen.Trim())
               { if (c == '-') { ID++; } } 
            
            if (Gelen[0].ToString() == "-" && islem == "-")
           {
               foreach (char c in Gelen.Trim())
               {
                   if (c == '-') { sayac++; }
                   if (ID == sayac) { continue; }
                   tut++;
               }

           }
           else
           {
               foreach (char c in Gelen.Trim())
               { if ((c != '-')) { tut++; } else { break; } }
           }
           
            if ((ID == 1) && Gelen[0].ToString() == "-") tut = -1;
            if (ID == 0) tut = -1;
            return tut;
        }
        
        string IlkIsaretKontrol(string GelenTerim)
        {        
            if (GelenTerim[0].ToString() == "+")
            GelenTerim = GelenTerim.Substring(1,GelenTerim.Length-1);

            return GelenTerim;
        }
       
        bool IslemKaldımı(string Islem)
        {
            string Opetorler = "/*+%//";
            bool IslemVar = false;


            for (int i = 0; i <= Islem.Length - 1; i++)
            {
                if ((Opetorler.IndexOf(Convert.ToChar(Islem[i])) >= 0)||(Islem.IndexOf('-')>1))
                {IslemVar = true;}
             }         
            return IslemVar;
        }        
    
        public string Islem(string GelenIfade)
        {
            string GelenVeri = GelenIfade;
            string Parantez_yedek = "";
            string[] cozum_parcala;
            bool Parantez_Yakalama = false;
            int Acilan_parantez = 0;
            string Parantez_Ici = "";
            if (GelenIfade == "") GelenVeri = "0";
            string Cozum = "";
            GelenVeri="("+GelenVeri+")";
            GelenVeri = GelenVeri.Replace(" ", "");
            GelenVeri = GelenVeri.Replace(".", ",");

            //try
            //{
                while (true)
                {
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

                        Parantez_Ici = Parantez_Ici.Replace("+-", "-");
                        Parantez_Ici = Parantez_Ici.Replace("-+", "-");
                        Parantez_Ici = IlkIsaretKontrol(Parantez_Ici);

                        int ID = Parantez_Ici.IndexOf('^');

                        if (ID > 0)
                        {
                            Cozum = TerimAyirici(Parantez_Ici, ID);
                            cozum_parcala = Cozum.Split('?');
                            Parantez_Ici = Parantez_Ici.Replace(cozum_parcala[0], cozum_parcala[1]);

                            continue;
                        }

                        ID = Parantez_Ici.IndexOf('*');

                        if (ID > 0)
                        {
                            Cozum = TerimAyirici(Parantez_Ici, ID);
                            cozum_parcala = Cozum.Split('?');
                            Parantez_Ici = Parantez_Ici.Replace(cozum_parcala[0], cozum_parcala[1]);
                            continue;
                        }



                        ID = Parantez_Ici.IndexOf('%');

                        if (ID > 0)
                        {
                            Cozum = TerimAyirici(Parantez_Ici, ID);
                            cozum_parcala = Cozum.Split('?');
                            Parantez_Ici = Parantez_Ici.Replace(cozum_parcala[0], cozum_parcala[1]);
                            continue;
                        }

                        ID = Parantez_Ici.IndexOf('/');

                        if (ID > 0)
                        {
                            Cozum = TerimAyirici(Parantez_Ici, ID);
                            cozum_parcala = Cozum.Split('?');
                            Parantez_Ici = Parantez_Ici.Replace(cozum_parcala[0], cozum_parcala[1]);
                            continue;
                        }

                        ID = Parantez_Ici.IndexOf('+');

                        if (ID > 0)
                        {
                            Cozum = TerimAyirici(Parantez_Ici, ID);
                            cozum_parcala = Cozum.Split('?');
                            Parantez_Ici = Parantez_Ici.Replace(cozum_parcala[0], cozum_parcala[1]);
                            continue;
                        }


                        ID = EksiKontrol(Parantez_Ici, "-");
                        if (ID >= 0)
                        {
                            Cozum = TerimAyirici(Parantez_Ici, ID);
                            cozum_parcala = Cozum.Split('?');
                            Parantez_Ici = Parantez_Ici.Replace(cozum_parcala[0], cozum_parcala[1]);
                            continue;
                        }
                        break;
                    }

                    GelenVeri = GelenVeri.Replace("(" + Parantez_yedek + ")", Parantez_Ici);

                    if (!IslemKaldımı(GelenVeri)) break;
                }
            //}
            //catch { ClsHatalar fnk = new ClsHatalar(); Hata = fnk.HataMesajlari(2); return Hata; }
            return Parantez_Ici;
        }
        
        double Matematikci(double veri1, double veri2, string islem_turu)
        {
                double sonuc = 0;
          
                if (islem_turu == "*") { sonuc = veri1 * veri2; }
                if (islem_turu == "/") { sonuc = veri1 / veri2; }
                if (islem_turu == "+") { sonuc = veri1 + veri2; }
                if (islem_turu == "-") { sonuc = veri1 - veri2; }
                if (islem_turu == "%") { sonuc = veri1 % veri2; }
                if (islem_turu == "^") { sonuc = Math.Pow(veri1, veri2); }

                return sonuc;
        }
       
        string TerimAyirici(string Terim,int ID)         
         {
            string Opetorler = "*//+-%";         
            string Islem_turu =Terim[ID].ToString(); 
            string veri1 = "";
            string veri2 = "";
            string pozitif= "";
            bool isaret1 =true;
            bool isaret2 = true;   
            double sonuc = 0;
            int i = 1;
            int eksisayi = 0;
            
            //try
            //{

            for (i = ID+1; i <= Terim.Length-1; i++)
            {
                if (eksisayi < 1) { if ((Terim[ID + 1] == '-')) { eksisayi++; isaret2 = false; continue; } }
                if ((Opetorler.IndexOf(Terim[i]) < 0)){ veri2 = veri2 + Terim[i];
                   if (!isaret2) veri2 = "-" + veri2;
            
                }
                else {break;}
            }         
  

             for (int a =ID-1; a >=0 ; a--)
            {
                if (Terim[a] == '-') { veri1 = "-" + veri1; isaret1 = false; }
                if (Opetorler.IndexOf(Terim[a]) < 0) { veri1 = Terim[a] + veri1;   }
                else { break; }
            }


            sonuc = Matematikci(Convert.ToDouble(veri1), Convert.ToDouble(veri2), Islem_turu);
            //}
            //catch { ClsHatalar fnk = new ClsHatalar(); Hata = fnk.HataMesajlari(2); }         
            
            if ((!isaret1 && isaret2 && (Convert.ToDouble(veri1) < Convert.ToDouble(veri2)))  || (!isaret1&&!isaret2&&Islem_turu=="*"))
            { pozitif = "+"; }
       
             return veri1 + Islem_turu + veri2+"?"+pozitif+sonuc;

        }

    }
}
