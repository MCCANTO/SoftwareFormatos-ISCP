using System.Security.Cryptography;
using System.Text;

namespace IK.SCP.Common.Helpers
{
    public static class Crypto
    {

        private static byte[] EncryptTextToMemory(string Data)
        {
            byte[] Key = new byte[24]
            {
                (byte) 1,
                (byte) 2,
                (byte) 3,
                (byte) 4,
                (byte) 5,
                (byte) 6,
                (byte) 7,
                (byte) 8,
                (byte) 9,
                (byte) 10,
                (byte) 11,
                (byte) 12,
                (byte) 13,
                (byte) 14,
                (byte) 15,
                (byte) 16,
                (byte) 17,
                (byte) 18,
                (byte) 19,
                (byte) 20,
                (byte) 21,
                (byte) 22,
                (byte) 23,
                (byte) 24
            };

            byte[] IV = new byte[8]
            {
                (byte) 8,
                (byte) 7,
                (byte) 6,
                (byte) 5,
                (byte) 4,
                (byte) 3,
                (byte) 2,
                (byte) 1
            };

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, new TripleDESCryptoServiceProvider().CreateEncryptor(Key, IV), CryptoStreamMode.Write);
            byte[] bytes = new ASCIIEncoding().GetBytes(Data);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] array = memoryStream.ToArray();
            cryptoStream.Close();
            memoryStream.Close();
            return array;
        }

        private static string DecryptTextFromMemory(byte[] Data)
        {
            byte[] Key = new byte[24]
            {
                (byte) 1,
                (byte) 2,
                (byte) 3,
                (byte) 4,
                (byte) 5,
                (byte) 6,
                (byte) 7,
                (byte) 8,
                (byte) 9,
                (byte) 10,
                (byte) 11,
                (byte) 12,
                (byte) 13,
                (byte) 14,
                (byte) 15,
                (byte) 16,
                (byte) 17,
                (byte) 18,
                (byte) 19,
                (byte) 20,
                (byte) 21,
                (byte) 22,
                (byte) 23,
                (byte) 24
            };

            byte[] IV = new byte[8]
            {
                (byte) 8,
                (byte) 7,
                (byte) 6,
                (byte) 5,
                (byte) 4,
                (byte) 3,
                (byte) 2,
                (byte) 1
            };

            CryptoStream cryptoStream = new CryptoStream((Stream)new MemoryStream(Data), new TripleDESCryptoServiceProvider().CreateDecryptor(Key, IV), CryptoStreamMode.Read);
            byte[] numArray = new byte[Data.Length];
            cryptoStream.Read(numArray, 0, numArray.Length);
            return new ASCIIEncoding().GetString(numArray);
        }

        public static string Encriptar(string _cadenaAencriptar) => Convert.ToBase64String(EncryptTextToMemory(_cadenaAencriptar));

        public static string Desencriptar(string _cadenaAdesencriptar) => DecryptTextFromMemory(Convert.FromBase64String(_cadenaAdesencriptar));

    }
}
