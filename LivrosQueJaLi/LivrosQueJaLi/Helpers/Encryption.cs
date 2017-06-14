using PCLCrypto;
using System;
using System.Text;
using static PCLCrypto.WinRTCrypto;

namespace EnviaEmailDLL.Modelo.Logica
{
    public static class Encryption
    {
        static byte[] saltValueBytes = Encoding.UTF8.GetBytes("123$%45678912345");
        static readonly string password = "AIspJl16W+JOJkeYFAmD2S0AsR3ijJZ9JqpH2lMm57CS+ptz/Sw=";
        static byte[] key = CreateDerivedKey(password);
        static ISymmetricKeyAlgorithmProvider aes = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
        static ICryptographicKey symetricKey = aes.CreateSymmetricKey(key);

        private static byte[] CreateDerivedKey(string encryptionKey, int keyLengthInBytes = 16, int iterations = 1000)
        {
            byte[] key = NetFxCrypto.DeriveBytes.GetBytes(encryptionKey, saltValueBytes, iterations, keyLengthInBytes);
            return key;
        }

        public static string EncryptAes(string data)
        {
            var bytes = CryptographicEngine.Encrypt(symetricKey, Encoding.UTF8.GetBytes(data));
            return Convert.ToBase64String(bytes);
        }

        public static string DecryptAes(string data)
        {
            var str = Convert.FromBase64String(data);
            var bytes = CryptographicEngine.Decrypt(symetricKey, str);
            string returnValue = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            return returnValue;
        }
    }
}