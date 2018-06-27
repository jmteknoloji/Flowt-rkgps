using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowTürk_Görsel_Programlama_Simülatörü
{
    class ClsRgtUza
    {
        private bool Class_Root_Anahtar_Varmi(string ClassRoot_Anahtar)
        {
            RegistrySecurity registrySecurity = new RegistrySecurity();
            registrySecurity.AddAccessRule(new RegistryAccessRule("Administrators", RegistryRights.WriteKey, AccessControlType.Allow));
            registrySecurity.SetOwner(new NTAccount("Administrators"));
            try
            {
                using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(ClassRoot_Anahtar, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.WriteKey))
                {
                    if ((key == null) && (Registry.ClassesRoot.CreateSubKey(ClassRoot_Anahtar, RegistryKeyPermissionCheck.ReadWriteSubTree, registrySecurity) != null))
                    {
                        return true;
                    }
                }
            }
            catch (Exception exception)
            {
               
            }
            return false;
        }     

        private bool CurrentUser_Register_Deger_Yaz(string CurrentUserYol, string anahtar, object deger, string tur)
        {
            RegistrySecurity security = new RegistrySecurity();
            security.AddAccessRule(new RegistryAccessRule("Administrators", RegistryRights.FullControl, AccessControlType.Allow));
            security.SetOwner(new NTAccount("Administrators"));
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(CurrentUserYol, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl))
                {
                    if (key != null)
                    {
                        if (key.GetValue(anahtar) != null)
                        {
                            key.DeleteValue(anahtar);
                        }
                        if (tur == "")
                        {
                            key.SetValue(anahtar, deger);
                        }
                        else if (tur == "str")
                        {
                            key.SetValue(anahtar, deger, RegistryValueKind.String);
                        }
                        else if (tur == "bin")
                        {
                            key.SetValue(anahtar, deger, RegistryValueKind.Binary);
                        }
                        else if (tur == "exp_str")
                        {
                            key.SetValue(anahtar, deger, RegistryValueKind.ExpandString);
                        }
                        else if (tur == "none")
                        {
                            key.SetValue(anahtar, deger, RegistryValueKind.None);
                        }
                        else if (tur == "multi_str")
                        {
                            key.SetValue(anahtar, deger, RegistryValueKind.MultiString);
                        }
                        else if (tur == "dword")
                        {
                            key.SetValue(anahtar, deger, RegistryValueKind.DWord);
                        }
                        else if (tur == "qword")
                        {
                            key.SetValue(anahtar, deger, RegistryValueKind.QWord);
                        }
                        else if (tur == "unknow")
                        {
                            key.SetValue(anahtar, deger, RegistryValueKind.Unknown);
                        }
                        return true;
                    }
                }
            }
            catch (Exception exception)
            {
                //MessageBox.Show("CurrentUser: " + exception.Message);
            }
            return false;
        }    

        private bool LocalMachine_Anahtar_Varmi(string LocakMachine_Anahtar)
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(LocakMachine_Anahtar, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl))
            {
                if ((key == null) && (Registry.LocalMachine.CreateSubKey(LocakMachine_Anahtar, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryOptions.Volatile) != null))
                {
                    return true;
                }
            }
            return false;
        }

        private bool anahtar_varmi(string CurrentUserAnahtar)
        { 
            string name = CurrentUserAnahtar;
            RegistrySecurity registrySecurity = new RegistrySecurity();
            registrySecurity.AddAccessRule(new RegistryAccessRule("Administrators", RegistryRights.FullControl, AccessControlType.Allow));
            registrySecurity.SetOwner(new NTAccount("Administrators"));
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(name, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl))
                {
                    if ((key == null) && (Registry.CurrentUser.CreateSubKey(name, RegistryKeyPermissionCheck.ReadWriteSubTree, registrySecurity) != null))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                //MessageBox.Show("CurrentUser anahtar");
            }
            return false;
        }

        
        public void RecommendedPrograms()
        {
            try
            {
                this.Class_Root_Anahtar_Varmi(".fgp");
              
                string currentUserAnahtar = @"Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.fgp";
                this.anahtar_varmi(currentUserAnahtar);
                this.anahtar_varmi(currentUserAnahtar + @"\OpenWithList");
                this.CurrentUser_Register_Deger_Yaz(currentUserAnahtar + @"\OpenWithList", "a", "FlowTürk Görsel Programlama Simülatörü.exe", "str");
                this.CurrentUser_Register_Deger_Yaz(currentUserAnahtar + @"\OpenWithList", "MRUList", "a", "str");
                this.anahtar_varmi(currentUserAnahtar + @"\OpenWithProgids");
                this.CurrentUser_Register_Deger_Yaz(currentUserAnahtar + @"\OpenWithProgids", "FGPS", new byte[0], "none");
            }
            catch (Exception exception)
            {
                //MessageBox.Show(exception.Message + "\n Kayıt defterine girdi yapılaması.");
            }
        }
    }
}
