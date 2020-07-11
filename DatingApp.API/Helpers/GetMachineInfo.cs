using System;
using System.Management;
using Microsoft.Win32;

namespace DatingApp.API.Helpers
{
    public class GetMachineInfo
    {
        public static string GetMacAddress()
        {
            ManagementObjectSearcher objMOS = new ManagementObjectSearcher("Select * FROM Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMOS.Get();
            string macAddress = String.Empty;
            foreach (ManagementObject objMO in objMOC)
            {
                object tempMacAddrObj = objMO["MacAddress"];

                if (tempMacAddrObj == null) //Skip objects without a MACAddress
                {
                    continue;
                }
                if (macAddress == String.Empty) // only return MAC Address from first card that has a MAC Address
                {
                    macAddress = tempMacAddrObj.ToString();
                }
                objMO.Dispose();
            }
            macAddress = macAddress.Replace(":", "");
            return macAddress;
        }
        public static string GetHardSN()
        {
         try
         {
            ManagementObjectSearcher moSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor "); //Win32_DiskDrive
            HardDrive hd = new HardDrive();  // User Defined Class
            foreach (ManagementObject wmi_HD in moSearcher.Get())
            {
                //hd.Model = wmi_HD["Model"].ToString().Trim();  //Model Number
                //hd.Type = wmi_HD["InterfaceType"].ToString().Trim();  //Interface Type
              //  hd.SerialNo = wmi_HD["SerialNumber"].ToString().Trim(); //Serial Number
                hd.SerialNo = wmi_HD["ProcessorId"].ToString().Trim();
            }
            return hd.SerialNo;  // hd.Model + "-" + hd.Type + "-" +// 
         }
         catch (Exception ex)
         {
            return ex.ToString();
         }
        }
        public static string GetString(Object fieldValue)
        {
            if (fieldValue == null) return "";
            return fieldValue.ToString().Trim();
        }
        public static string GenerateAppKey()
        {
            string macAddress = GetMachineInfo.GetMacAddress();
            string hardDiskNo = GetMachineInfo.GetHardSN();
            string winKey = "YNMGQ-8RYV3-4PGQ3-C8XTP-7CFBY";
            int wKLenght = winKey.Length;  //get Length
            int wKRemainder = wKLenght % 2;
            if (wKRemainder > 0)
            {
                winKey = winKey.Remove(winKey.Length - 1);
                wKLenght = wKLenght - 1;
            }
            int startLengh = wKLenght / 2 - 3;
            try
            {
                var AppKey = hardDiskNo + 
                    winKey.Substring(startLengh, 3).Replace("-", "Y") + 
                    macAddress; 
                return AppKey;
            } catch(Exception ex)
            {
                return ex.ToString() ;
            }

            
        }
        private class HardDrive
        {
            private string model = null;
            private string type = null;
            private string serialNo = null;
            public string Model
            {
                get { return model; }
                set { model = value; }
            }
            public string Type
            {
                get { return type; }
                set { type = value; }
            }
            public string SerialNo
            {
                get { return serialNo; }
                set { serialNo = value; }
            }
        }   
        public string GenerateRandomWord(int numletters, Random r)
        {
            //lstWords.Items.Clear();

            // Get the number of words and letters per word.
            int num_letters = numletters;
            int num_words = 5;

            // Make an array of the letters we will use.
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuwxyz123456789=+/".ToCharArray();

            // Make a random number generator.
            Random rand = r;

            // Make the words.

            string bigWord = "";
            for (int i = 1; i <= num_words; i++)
            {
                // Make a word.
                string word = "";
                for (int j = 1; j <= num_letters; j++)
                {
                    // Pick a random number between 0 and 25
                    // to select a letter from the letters array.
                    int letter_num = rand.Next(0, letters.Length - 1);

                    // Append the letter.
                    word += letters[letter_num];
                }

                // Add the word to the list.
                bigWord += word;
            }
            return bigWord;
        }
    
        private void GenerateRandomKeys()
        {
            Random r = new Random();
            RegistryKey oLicense = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\WinHotKeys\");
            #region Fake Random Value
            r = new Random();
            oLicense.SetValue("ShellState", GenerateRandomWord(9, r)); //1
            r = new Random();
            oLicense.SetValue("ShowRecent", GenerateRandomWord(10, r)); //2
            r = new Random();
            oLicense.SetValue("ExplorerStartupTraceRecorded", GenerateRandomWord(11, r)); //3
            r = new Random();
            oLicense.SetValue("UserSignedIn", GenerateRandomWord(12, r)); //4
            oLicense.SetValue("AccentPalette", GenerateRandomWord(10, r)); //5
            oLicense.SetValue("AccentColorMenu", GenerateRandomWord(9, r)); //6
            oLicense.SetValue("AutoCheckSelect", GenerateRandomWord(13, r)); //7
            oLicense.SetValue("ListviewAlphaSelect", GenerateRandomWord(12, r)); //8
            oLicense.SetValue("TaskbarAnimations", GenerateRandomWord(11, r)); //9
            oLicense.SetValue("StartMenuInit", GenerateRandomWord(9, r)); //10
            oLicense.SetValue("TaskbarStateLastRun", GenerateRandomWord(13, r)); //1
            oLicense.SetValue("ShowTaskViewButton", GenerateRandomWord(12, r)); //2;
            oLicense.SetValue("AllowFailFastOnAnyFailure", GenerateRandomWord(11, r)); //3
            oLicense.SetValue("DisableAutoplay", GenerateRandomWord(12, r)); //4
            oLicense.SetValue("InvokeProgID", GenerateRandomWord(12, r)); //5
            oLicense.SetValue("InvokeVerb", GenerateRandomWord(9, r)); //6
            oLicense.SetValue("LaunchTo", GenerateRandomWord(9, r)); //7
            oLicense.SetValue("ListviewShadow", GenerateRandomWord(11, r)); //8
            oLicense.SetValue("TelemetrySalt", GenerateRandomWord(12, r)); //9
            oLicense.SetValue("SeparateProcess", GenerateRandomWord(12, r)); //10
            #endregion
        }
    }
}