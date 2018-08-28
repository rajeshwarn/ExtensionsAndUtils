using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Collections;

namespace Framework
{
    public class Encryption
    {
        // Set your salt here, change it to meet your flavor:
        // The salt bytes must be at least 8 bytes.
        private static byte[] saltBytes = new byte[] { 3, 4, 2, 7, 8, 2, 5, 2, 3, 0 };
        private static string password = "MyEncryptionPassword";
        private static byte[] passwordBytes = new byte[] { 77, 114, 112, 77, 110, 110, 105, 99, 105, 112, 125, 111, 115, 95, 69, 110, 99, 114, 121, 132, 101, 105, 131, 110, 80, 97, 115, 105, 119, 111, 114, 98 };

        public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted)
        {
            byte[] encryptedBytes = null;

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted)
        {
            byte[] decryptedBytes = null;

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }
        

        public static string encodePassMd5(string pass)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5 = new MD5CryptoServiceProvider();
            string encodedString = "";

            originalBytes = ASCIIEncoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(originalBytes);

            foreach (var k in encodedBytes)
                encodedString += k.ToString("x2");

            return encodedString;
        }


        public static string Encrypt(string clearText)
        {
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

            byte[] encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            return Convert.ToBase64String(encryptedData);
        }
        public static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();

            // Algorithm. Rijndael is available on all platforms.

            alg.Key = Key;
            alg.IV = IV;
            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);

            //CryptoStream is for pumping our data.

            cs.Write(clearData, 0, clearData.Length);
            cs.Close();
            byte[] encryptedData = ms.ToArray();
            return encryptedData;
        }

        public static string Decrypt(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password,
            new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            byte[] decryptedData = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }
        public static byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(cipherData, 0, cipherData.Length);
            cs.Close();
            byte[] decryptedData = ms.ToArray();
            return decryptedData;
        }


        
        public static string RSAEncryptString(string inputString, string publicKey, int keyBytesSize = 1024)
        {
            if (String.IsNullOrEmpty(publicKey)) publicKey = password;

            // TODO: Add Proper Exception Handlers
            var rsaCryptoServiceProvider = new RSACryptoServiceProvider(keyBytesSize);
            rsaCryptoServiceProvider.FromXmlString(publicKey);
            int keySize = keyBytesSize / 8;
            byte[] bytes = Encoding.UTF32.GetBytes(inputString);

            // The hash function in use by the .NET RSACryptoServiceProvider here 
            // is SHA1
            // int maxLength = ( keySize ) - 2 - ( 2 * SHA1.Create().ComputeHash( rawBytes ).Length );

            int maxLength = keySize - 42;
            int dataLength = bytes.Length;
            int iterations = dataLength / maxLength;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i <= iterations; i++)
            {
                byte[] tempBytes = new byte[
                        (dataLength - maxLength * i > maxLength) ? maxLength :
                                                      dataLength - maxLength * i];
                Buffer.BlockCopy(bytes, maxLength * i, tempBytes, 0,
                                  tempBytes.Length);
                byte[] encryptedBytes = rsaCryptoServiceProvider.Encrypt(tempBytes,
                                                                          true);
                // Be aware the RSACryptoServiceProvider reverses the order of 
                // encrypted bytes. It does this after encryption and before 
                // decryption. If you do not require compatibility with Microsoft 
                // Cryptographic API (CAPI) and/or other vendors. Comment out the 
                // next line and the corresponding one in the DecryptString function.

                Array.Reverse(encryptedBytes);

                // Why convert to base 64?
                // Because it is the largest power-of-two base printable using only 
                // ASCII characters

                stringBuilder.Append(Convert.ToBase64String(encryptedBytes));
            }
            return stringBuilder.ToString();
        }

        public static string RSADecryptString(string inputString, string privateKey, int keyBytesSize = 1024)
        {
            if (String.IsNullOrEmpty(privateKey)) privateKey = password;

            // TODO: Add Proper Exception Handlers
            var rsaCryptoServiceProvider = new RSACryptoServiceProvider(keyBytesSize);
            rsaCryptoServiceProvider.FromXmlString(privateKey);
            int base64BlockSize = ((keyBytesSize / 8) % 3 != 0) ?
              (((keyBytesSize / 8) / 3) * 4) + 4 : ((keyBytesSize / 8) / 3) * 4;
            int iterations = inputString.Length / base64BlockSize;
            ArrayList arrayList = new ArrayList();
            for (int i = 0; i < iterations; i++)
            {
                byte[] encryptedBytes = Convert.FromBase64String(
                     inputString.Substring(base64BlockSize * i, base64BlockSize));

                // Be aware the RSACryptoServiceProvider reverses the order of 
                // encrypted bytes after encryption and before decryption.
                // If you do not require compatibility with Microsoft Cryptographic 
                // API (CAPI) and/or other vendors.
                // Comment out the next line and the corresponding one in the 
                // EncryptString function.

                Array.Reverse(encryptedBytes);
                arrayList.AddRange(rsaCryptoServiceProvider.Decrypt(encryptedBytes, true));
            }
            return Encoding.UTF32.GetString(arrayList.ToArray(Type.GetType("System.Byte")) as byte[]);
        }
    }
}
