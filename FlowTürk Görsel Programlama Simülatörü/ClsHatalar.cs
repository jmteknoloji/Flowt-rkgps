using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowTürk_Görsel_Programlama_Simülatörü
{
    class ClsHatalar
    {

        public string HataMesajlari(int HataNo)
        {
            switch (HataNo)
            {
                case 1:  return "Lütfen Karşılaştırma Sorgusunu Kontrol Ediniz.";
                case 2:  return "Lütfen İşlem Sorgusunu Kontrol Ediniz.";
                case 3:  return "Programınız 'Başlat' nesnesiz başlayamaz.Lütfen 'Başlat' nesnesi ekleyiniz.";
                case 4:  return "Programınız şuan çalıştığı için seçim yapamazsınız !";
           
                default: return ""; 
            }
        }
    
    
    }
}
