using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace JMProjectDownloadProtocol
{
   public delegate void TimeOutHandler(object sender, EventArgs e);
    public delegate void IslemHandle(ProjectDownloadProtocol e);
    public class ProjectDownloadProtocol
    {
        SqlConnection Connect ;
        public string FTPServer { get; set; }
        public string ConnectText { get; set; }
        public string Admin { get; set; }
        public string Password { get; set; }
        public int Anlık { get; set; }
        public int Toplam { get; set; }

        public static event IslemHandle IslemdeYenilikOldu;

        //public event TimeOutHandler TimeOutEvent;

        //public void SetTimeout(int milisaniye)
        //{
        //    Thread.Sleep(milisaniye);
        //    if (TimeOutEvent != null)
        //    {
        //        TimeOutEvent(this, EventArgs.Empty);
        //    }
        //}

        #region
        //public delegate string MyDelegate();

        //public event MyDelegate evt; //Event

        //System.IO.FileStream fs;
       

        //public ProjectDownloadProtocol()
        //{

        //    //return "";
        //}
        public void Cikar()
        {
            //int durum = 0;
            //fs = new System.IO.FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" +filename, System.IO.FileMode.OpenOrCreate);
            //byte[] dosya = Properties.Resources.setup;
            ProjectDownloadProtocol sonuc = new ProjectDownloadProtocol();
            sonuc.Toplam = 10;
            //foreach (byte b in dosya)
            //{
            //    sonuc.Anlık = durum;
            //    fs.WriteByte(b);
            IslemdeYenilikOldu(sonuc);
            //    durum++;
            //}
            //fs.Close();

        }
        #endregion

        public string DownloadTransTexts(string GelenGidenParametre)
        {
            string Parametre = "";
            return (Parametre);       
        }

        public string DownloadTransErrors(string GelenGidenParametre)
        {
            string Parametre = GelenGidenParametre;
            return Parametre;
        }

        public string DownloadTransProceses(string gelen)
        {
            //evt += new MyDelegate();

            //int Parametre = GelenGidenParametre;
            return "";
        }

        public string DownloadProtocolPacketStart(string Donen)
        {
            Int64 iRunningByteTotal = 0;

            try
            {
                Connect = new SqlConnection(ConnectText);
                string Path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string[] DosyaListesi;

                StringBuilder result = new StringBuilder();
                FtpWebRequest FTP;

                FTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(FTPServer));               
                FTP.UseBinary = true;              
                FTP.Credentials = new NetworkCredential(Admin, Password);
                FTP.Method = WebRequestMethods.Ftp.ListDirectory;

                WebResponse response = FTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();

                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }

                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                DosyaListesi = result.ToString().Split('\n');

                for (int x = 0; x < DosyaListesi.Count(); x++)
                {
                    int kntrl = 0;

                    for (int i = 0; i < DosyaListesi[x].Length; i++)
                    {
                        if (DosyaListesi[x][i].ToString() == ".")
                        {
                            kntrl = 1;
                        }
                    }

                    if (kntrl == 1)
                    {

                        FileStream SR = new FileStream(Path + "\\" + DosyaListesi[x].ToString(), FileMode.Create);
                        FtpWebRequest FTPi0;
                        FTPi0 = (FtpWebRequest)FtpWebRequest.Create(new Uri(FTPServer + DosyaListesi[x].ToString()));
                        FTPi0.Credentials = new NetworkCredential(Admin, Password);
                        FTPi0.Method = WebRequestMethods.Ftp.DownloadFile;
                        FTPi0.UseBinary = true;
                        FtpWebResponse response2 = (FtpWebResponse)FTPi0.GetResponse();
                        Stream ftpStream = response2.GetResponseStream();
                        long cl = response2.ContentLength;
                        int bufferSize = 1024;
                        int readCount;
                        byte[] buffer = new byte[bufferSize];
                        readCount = ftpStream.Read(buffer, 0, bufferSize);

                        //DownloadTransProceses(readCount.ToString());
                        
                        iRunningByteTotal += bufferSize;

                        double dIndex = (double)(iRunningByteTotal);
                        double dTotal = (double)buffer.Length;
                        double dProgressPercentage = (dIndex / dTotal);
                        int iProgressPercentage = (int)(dProgressPercentage * 100);


                        while (readCount > 0)
                        {
                            SR.Write(buffer, 0, readCount);
                            readCount = ftpStream.Read(buffer, 0, bufferSize);
                            //DownloadTransProceses(readCount.ToString());
                        }
                        ftpStream.Close();
                        SR.Close();
                        response2.Close();

                    //DownloadTransProceses(readCount.ToString());
                        DownloadTransTexts("Dosya İndi" + DosyaListesi[x].ToString());

                    }
                    else
                    {
                        FtpWebRequest FTP2;
                        Directory.CreateDirectory(Path + "\\" + DosyaListesi[x]);
                        string[] DosyaListesi2;
                        FTP2 = (FtpWebRequest)FtpWebRequest.Create(new Uri(FTPServer + DosyaListesi[x]));
                        FTP2.UseBinary = true;
                        FTP2.Credentials = new NetworkCredential(Admin, Password);
                        StringBuilder result2 = new StringBuilder();
                        FTP2.Method = WebRequestMethods.Ftp.ListDirectory;
                        WebResponse response3 = FTP2.GetResponse();
                        StreamReader reader3 = new StreamReader(response3.GetResponseStream());
                        string line3 = reader3.ReadLine();

                        while (line3 != null)
                        {
                            result2.Append(line3);
                            result2.Append("\n");
                            line3 = reader3.ReadLine();
                        }

                        result2.Remove(result2.ToString().LastIndexOf('\n'), 1);
                        reader3.Close();
                        response3.Close();
                        DosyaListesi2 = result2.ToString().Split('\n');

                        for (int y = 0; y < DosyaListesi2.Length; y++)
                        {
                            kntrl = 0;
                            for (int i = 0; i < DosyaListesi2[y].Length; i++)
                            {
                                if (DosyaListesi2[y][i].ToString() == ".")
                                {
                                    kntrl = 1;
                                }
                            }

                            if (kntrl == 1)
                            {
                                try
                                {

                                    FileStream SR = new FileStream(Path + "\\" + DosyaListesi[x].ToString() + "\\" + DosyaListesi2[y].ToString(), FileMode.Create);
                                    FtpWebRequest FTPi0;
                                    FTPi0 = (FtpWebRequest)FtpWebRequest.Create(new Uri(FTPServer + DosyaListesi[x].ToString() + @"/" + DosyaListesi2[y].ToString()));
                                    FTPi0.Credentials = new NetworkCredential(Admin, Password);
                                    FTPi0.Method = WebRequestMethods.Ftp.DownloadFile;
                                    FTPi0.UseBinary = true;
                                    FTPi0.Method = WebRequestMethods.Ftp.DownloadFile;
                                    FTPi0.UseBinary = true;
                                    FtpWebResponse response2 = (FtpWebResponse)FTPi0.GetResponse();
                                    Stream ftpStream2 = response2.GetResponseStream();
                                    long cl = response2.ContentLength;
                                    int bufferSize = 1024;
                                    int readCount;
                                    byte[] buffer = new byte[bufferSize];
                                    readCount = ftpStream2.Read(buffer, 0, bufferSize);

                                    DownloadTransProceses(readCount.ToString());
                                    DownloadTransTexts("Dosya İndiriliyor" + DosyaListesi[x].ToString());

                                    while (readCount > 0)
                                    {
                                        SR.Write(buffer, 0, readCount);
                                        readCount = ftpStream2.Read(buffer, 0, bufferSize);
                                        DownloadTransProceses(readCount.ToString());
                                    }

                                    ftpStream2.Close();
                                    SR.Close();
                                    response2.Close();
                                }
                                catch { DownloadTransErrors("Bir sorun oluştu.Dosya indirme işlemi sonlandırıldı."); }


                                DownloadTransProceses("-1");
                                DownloadTransTexts("Dosya İndi" + DosyaListesi[x].ToString());

                                response3.Close();
                                response.Close();

                            }
                        }
                    }
                }

                DownloadTransTexts("Tüm Dosyalar İndirildi");


                System.Threading.Thread.Sleep(2000);
            }
            catch { DownloadTransErrors("Güncelleme yapılamadı.Lütfen internet bağlantısının ya da sistemin doğru çalıştığından emin olunuz."); ; }

            return DownloadTransTexts("Güncelleme işlemi bitmiştir.");
        }


    }

   



    




}
