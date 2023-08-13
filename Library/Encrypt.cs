using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AppSistemaVentas.Library
{
    public class Encrypt
    {

        private static RijndaelManaged rm = new RijndaelManaged();

        public static void encrypt() {

            

            rm.Mode = CipherMode.CBC;

            rm.Padding = PaddingMode.PKCS7;

            rm.KeySize = 0x80;

            rm.BlockSize = 0x80;

        
        }

        public static string EncryptData(string textData, string Encryptionkey) {


            encrypt();
            byte[] passBytes = Encoding.UTF8.GetBytes(Encryptionkey);

            byte[] EncryptionkeyBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
            int len = passBytes.Length;
            if (len> EncryptionkeyBytes.Length) { 
            
            len = EncryptionkeyBytes.Length;
            
            }

            Array.Copy(passBytes, EncryptionkeyBytes, len);
            rm.Key = EncryptionkeyBytes;
            rm.IV = EncryptionkeyBytes;
        
            ICryptoTransform objtransform = rm.CreateEncryptor();
            byte[] textDataByte = Encoding.UTF8.GetBytes(textData);
            return Convert.ToBase64String(objtransform.TransformFinalBlock(textDataByte, 0, textDataByte.Length));
        }

        public static string DecryptData(string EncryptedText, string Encryptionkey) {

            encrypt();
            byte[] encryptedTextByte = Convert.FromBase64String(EncryptedText);
            byte[] passBytes = Encoding.UTF8.GetBytes(Encryptionkey);

            byte[] EncryptionkeyBytes = new byte[0x10];
            int len = passBytes.Length;
            if (len > EncryptionkeyBytes.Length)
            {

                len = EncryptionkeyBytes.Length;

            }
            Array.Copy(passBytes, EncryptionkeyBytes, len);
            rm.Key = EncryptionkeyBytes;
            rm.IV = EncryptionkeyBytes;

            byte[] TextByte = rm.CreateDecryptor().TransformFinalBlock(encryptedTextByte, 0, encryptedTextByte.Length);
            return Encoding.UTF8.GetString(TextByte);
        }
    }
}
