using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Back_UniversoDicta.Infraestructure.Hash
{
    public class Hash
    {
        private static byte[] Key = Convert.FromBase64String("w5KE4f1cbDpIBWCFvANpTpSnbhgIhuscPbsamvA0s8Y=");
        private static byte[] IV = Convert.FromBase64String("B69d83+9+s1fgtPmTxuDzQ==");

        protected string salt = "fenixemtrafesa";
        public string EncodePassword(string originalPassword)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(originalPassword, saltBytes, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hashedBytes = pbkdf2.GetBytes(256 / 8); // 256 bits
                return Convert.ToBase64String(hashedBytes);
            }
        }
        public bool VerifyPassword(string password, string passwordBD)
        {
            string hashedPassword = EncodePassword(password);
            return hashedPassword == passwordBD;
        }
        public string EncryptStringToBytes_Aes(string plainText)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return Convert.ToBase64String(encrypted);
        }

        public string DecryptStringFromBytes_Aes(string cipherText)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }

   

}