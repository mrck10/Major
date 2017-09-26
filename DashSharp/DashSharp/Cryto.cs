using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Major
{
    public class Cryto
    {
        // Cryto Realted Functions

        public byte[] bytes = ASCIIEncoding.ASCII.GetBytes("ZeroCool"); // sets the byets to Zero Cool

        public string Encrypt(string originalString) 
        {
            if (String.IsNullOrEmpty(originalString)) // Is String actually ok to encrypt?
            {
                throw new ArgumentNullException
                ("The string which needs to be encrypted can not be null.");
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider(); // Crypto Provider
            MemoryStream memoryStream = new MemoryStream(); // Memory stream
            CryptoStream cryptoStream = new CryptoStream(memoryStream, // Crypto Stream 
                cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write); // Create encrypter
            StreamWriter writer = new StreamWriter(cryptoStream); // Stream Writer 
            writer.Write(originalString); // Writes string and flushes
            writer.Flush(); 
            cryptoStream.FlushFinalBlock(); // again
            writer.Flush(); // again
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length); // Converts it and retunrs
        }

        public string Decrypt(string cryptedString)
        {
            if (String.IsNullOrEmpty(cryptedString)) // test to see if it's a valid string.
            {
                throw new ArgumentNullException
                ("The string which needs to be decrypted can not be null.");
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider(); // crypto Provider
            MemoryStream memoryStream = new MemoryStream // Memory Stream
                (Convert.FromBase64String(cryptedString));
            CryptoStream cryptoStream = new CryptoStream(memoryStream, // the Cryostream 
                cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read); // Decriptor
            StreamReader reader = new StreamReader(cryptoStream); // Stream Set
            return reader.ReadToEnd(); // Stream Reader returner
        }

        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);
    }
}
