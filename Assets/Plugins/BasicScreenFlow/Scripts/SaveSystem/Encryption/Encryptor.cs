using System;
using System.Security.Cryptography;
using System.Text;

public class Encryptor
{
    private string _key;

    public Encryptor()
    {
#if UNITY_ANDROID
        _key = Keys.ANDROID_KEY;
#elif UNITY_IOS
        _key = Keys.IOS_KEY;
#else
        _key = Keys.DEFAULT_KEY;
#endif
    }

    public string Encrypt(string dataToEncrypt)
    {
        byte[] data = UTF8Encoding.UTF8.GetBytes(dataToEncrypt);
        using(MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_key));
            using (TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider() { 
                Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 
            })
            {
                ICryptoTransform transform = trip.CreateEncryptor();
                byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
                return Convert.ToBase64String(result, 0, result.Length);
            }
        }
    }

    public string Decrypt(string dataToEncrypt)
    {
        byte[] data = Convert.FromBase64String(dataToEncrypt);
        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_key));
            using (TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider()
            {
                Key = key,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            })
            {
                ICryptoTransform transform = trip.CreateDecryptor();
                byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
                return UTF8Encoding.UTF8.GetString(result);
            }
        }
    }
}
