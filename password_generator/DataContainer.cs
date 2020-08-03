using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace password_generator {
    public static class DataContainer {
        //public static string[] ps_name = new string[20];
        //public static string[] ps_text = new string[20];
        public static string[] ps_name;
        public static string[] ps_text;

        public static string[] configInit = new string[5]; //0-heslo; 1-configMissing; 2-dataMissing;
        //public static string ps_name;
        //public static string ps_text;
        public static bool changed = false;
        public static int selected = 0;

        public static string appFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\PasswordKeeper";
        public static string dataFilePath = appFolderPath + "\\data1.meh";
        public static string configFilePath = appFolderPath + "\\config.meh";
        public static string passwordFilePath = appFolderPath + "\\ps_data.meh";

        public static string passPhrase = "passPhrase";        // can be any string ///text k zašifrování
        public static string saltValue = "saltValue";        // can be any string ///heslo uživatele
        public static string hashAlgorithm = "SHA1";             // can be "MD5"
        public static int passwordIterations = 7;                  // can be any number
        public static string initVector = "~1B2c3D4e5F6g7H8"; // must be 16 bytes
        public static int keySize = 128;

        public static string Decrypt(string data, string salt) {
            byte[] bytes = Encoding.ASCII.GetBytes(DataContainer.initVector);
            byte[] rgbSalt = Encoding.ASCII.GetBytes(salt);
            byte[] buffer = Convert.FromBase64String(data);
            byte[] rgbKey = new PasswordDeriveBytes(DataContainer.passPhrase, rgbSalt, DataContainer.hashAlgorithm, DataContainer.passwordIterations).GetBytes(DataContainer.keySize / 8);
            RijndaelManaged managed = new RijndaelManaged();
            managed.Mode = CipherMode.CBC;
            ICryptoTransform transform = managed.CreateDecryptor(rgbKey, bytes);
            MemoryStream stream = new MemoryStream(buffer);
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
            byte[] buffer5 = new byte[buffer.Length];
            int count = stream2.Read(buffer5, 0, buffer5.Length);
            stream.Close();
            stream2.Close();
            return Encoding.UTF8.GetString(buffer5, 0, count);
        }
        public static string Decrypt(string data)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(DataContainer.initVector);
            byte[] rgbSalt = Encoding.ASCII.GetBytes(DataContainer.saltValue);
            byte[] buffer = Convert.FromBase64String(data);
            byte[] rgbKey = new PasswordDeriveBytes(DataContainer.passPhrase, rgbSalt, DataContainer.hashAlgorithm, DataContainer.passwordIterations).GetBytes(DataContainer.keySize / 8);
            RijndaelManaged managed = new RijndaelManaged();
            managed.Mode = CipherMode.CBC;
            ICryptoTransform transform = managed.CreateDecryptor(rgbKey, bytes);
            MemoryStream stream = new MemoryStream(buffer);
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
            byte[] buffer5 = new byte[buffer.Length];
            int count = stream2.Read(buffer5, 0, buffer5.Length);
            stream.Close();
            stream2.Close();
            return Encoding.UTF8.GetString(buffer5, 0, count);
        }
        public static string Encrypt(string data, string salt) {
            byte[] bytes = Encoding.ASCII.GetBytes(DataContainer.initVector);
            byte[] rgbSalt = Encoding.ASCII.GetBytes(salt);
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            byte[] rgbKey = new PasswordDeriveBytes(DataContainer.passPhrase, rgbSalt, DataContainer.hashAlgorithm, DataContainer.passwordIterations).GetBytes(DataContainer.keySize / 8);
            RijndaelManaged managed = new RijndaelManaged();
            managed.Mode = CipherMode.CBC;
            ICryptoTransform transform = managed.CreateEncryptor(rgbKey, bytes);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            byte[] inArray = stream.ToArray();
            stream.Close();
            stream2.Close();
            return Convert.ToBase64String(inArray);
        }
        public static string Encrypt(string data)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(DataContainer.initVector);
            byte[] rgbSalt = Encoding.ASCII.GetBytes(DataContainer.saltValue);
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            byte[] rgbKey = new PasswordDeriveBytes(DataContainer.passPhrase, rgbSalt, DataContainer.hashAlgorithm, DataContainer.passwordIterations).GetBytes(DataContainer.keySize / 8);
            RijndaelManaged managed = new RijndaelManaged();
            managed.Mode = CipherMode.CBC;
            ICryptoTransform transform = managed.CreateEncryptor(rgbKey, bytes);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            byte[] inArray = stream.ToArray();
            stream.Close();
            stream2.Close();
            return Convert.ToBase64String(inArray);
        }
    }
}
