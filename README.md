# FlowTurk GPS
Flowtürk Görsel Programlama Simülatörü
FlowTürk Görsel Programlama Simülatörü
Eğitim Kılavuzu

	Günümüzde teknolojinin biz insanlara sunmuş olduğu imkânlar sayamayacağımız kadar fazladır ve her an kullanmaktayız. Bu süreçte sadece kullanan değil bizlerinde teknolojiyi üretebilecek komunda olmamız hem bireysel hem de toplumsal olarak fayda sağlayacaktır ama bunların en başında gelen soru da şu olmaktadır. Ben bu işe nereden başlayacağım? İşte bizlerde tam burada devreye giriyoruz ve bu işin en temel kısmı olan “Algoritma ve Akış diyagramı” kavramlarını sizlerle tanıştırıp bir kapı açmış oluyoruz. Bu iki kavram teknolojinin gelişim sürecini açıklayan en temel yapıdır.


Algoritma ve Akış Diyagramı Nedir?
	Algoritmanın kısa ve öz tanımını yapacak olursak, ”bir problemin çözüm aşamalarının maddeler halinde sıralanması” şekline tanımlayabiliriz. Bunu biraz daha ayrıntılı inceleyecek olursak algoritma oluşturabilmek için ilk önce bir eyleme ihtiyacımız vardır. Bu sadece teknolojiye ait bir terimi değildir, evrendeki tüm düzen ve yapılanma bir algoritma süreci içindedir. Örnek verecek olursak ,”insanın büyüme süreci” ya da “bir insanın bir gününü nasıl geçirdiği” gibi bir süreç içeren tüm eylemlerin algoritması oluşturulabiliriz.


	Algoritmaya kısa bir örnek verecek olursak;
1-Uyandım.
2-Elimi yüzümü yıkadım.
3-Kahvaltı yaptım.
4-Evden çıktım.
5-…                                                     
6-…
7-Eve döndüm.               Tekrar Başa dön
8-TV izledim.
9-Uyudum.




	Akış diyagramını tanımlayacak olursak, belirlemiş olduğumuz algoritmanın kabul görmüş (herkes tarafından aynı sayılan) sembollerle ifade edilme şekline akış diyagramı diyoruz. Kullanmış olduğumuz semboller ise akış diyagramı nesneleri olarak ifade edebiliriz. Akış diyagramı algoritma ile programlama arasındaki bağı kuran en sade biçimdir. Bizler akış diyagramını ne kadar iyi kavrarsak o kadar programlama mantığını çözmüş olur ve gelişmiş programlama dillerine daha rahat geçiş sağlamış oluruz. Buradan sonraki süreç üzerinde akış diyagramları geliştireceğimiz süreçtir.


Akış Diyagramı Nesnelerini tanıyalım.
	Şuanda kullanmış olduğumuz sürümde toplamda 8 adet nesne bulunmaktadır. Bunlar “başlat”,”değişkenler”,”işlem”,”giriş”,”çıkış”,”eğer”,”düğüm” ve “dur”.


1.Başlat
Oluşturacağımız her akış diyagramının bir başlangıç noktası olmaz zorundadır.FlowTürk’ün bu başlangıç noktasını algılayabilmesi için “Başlat” nesnesi koymak durumundayız. Program üzerinde “başlat” ‘ın bir başka görevi ise program üzerindeki tüm değerleri sıfırlamaktır. 


2.Değişkenler

Oluşturacağımız her akış diyagramında değişken tanımlamamız gerekiyor ise tüm değişkenleri “Değişkenler” nesnesinde tanımlayabiliriz.
Ör: ort=0,sayi1=0,sayi2=0 bu şekilde 3 adet değişken tanımlamış olduk.


3.İşlem
Matematiksel işlemlerin yapıldığı nesnedir.4 işlemin dışında “Mod,üst alma” işlemlerini de yapabilirsiniz. İşlemin uzunluğu sonsuz olabilir.

Ör: sonuc=255(35-13*3/4-3)/5  (sayıların yerine değişkenlerde kullanılabilir.)



4.Eğer
Mantıksal kıyaslamaların yapıldığı nesnedir.”>,<,>=,<=,==” operatörlerini kullanabilirsiniz.
Ör: sayi>0,sayi>=0 şeklinde kullanılabilir.

5.Giriş
Kullanıcının dışarıdan değer girmesini sağlayan nesnedir. Aynı zamanda ekrana mesaj verebilirsiniz.


6.Çıkış
Oluşturulan akış diyagramı üzerinden ekrana bir sonuç yazdırmak istediğimizde kullanacağımız nesnedir.

7.Düğüm

Akış diyagramı üzerinde bağlantı noktası oluşturmaya yardımcı olan nesnedir. Başka bir görevi yoktur.

8.Dur

Akış diyagramının sonlandığını belirten nesnedir. Akış diyagramını sonlandırmamız 2 nokta arasında bir doğru oluşturmamızı sağlar.



Kısayol Tuşları Nelerdir?

•	Ctrl + Shift + Q: Programı kapatır.
•	Ctrl + F1: Yeni proje oluşturur.
•	Ctrl + F2: Kayıtlı projeyi açar.
•	Ctrl + S: Projeyi kaydedersiniz.
•	Ctrl + Shift + S: Projeyi farklı kaydedersiniz.
•	Ctrl + Z: Yaptığınız değişikleri geri alırsınız.
•	Ctrl + Y: Yaptığınız değişlikleri iler alırsınız.
•	Ctrl + A: Program üzerindeki tüm nesneleri seçersiniz.
•	Shift + Nesne üzerine Mouse Sağ Tık: 2 Nesneyi birbirine bağlar.
•	Ctrl + Nesne üzerine Mouse Sağ Tık: Nesneleri tek tek seçersiniz.
