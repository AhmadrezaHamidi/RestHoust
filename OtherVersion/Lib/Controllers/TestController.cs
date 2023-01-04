#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lib.Models;
using System.Security.Cryptography;
using System.Text;

namespace Lib.Controllers
{
    [Route("api/Books")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly LibContext _context;
        private readonly IEncDec ec;

        public TestController(LibContext context, IEncDec ec)
        {
            _context = context;
            this.ec = ec;
        }

        // [HttpPost]
        // public string MakeStrung(string original)
        // {

        //     var t = AesCbcEncryptionService.Encrypt(original, "Ahmad");
        //     // using (Aes myAes = Aes.Create())
        //     // {

        //     //     var t = ec.EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);
        //     //     var s1 = Convert.ToBase64String(t);
        //     //     var s2 = Convert.ToBase64String(myAes.Key);
        //     //     var s3 = Convert.ToBase64String(myAes.IV);

        //     var gg = new byteEntity(Convert.ToBase64String(t.Ciphertext),Convert.ToBase64String(t.InitializationVector) ,Convert.ToBase64String(t.Salt));
        //     _context.ByteEntity.Add(gg);
        //     // }
        //      _context.SaveChanges();

        //     return "hugugu";
        // }
        const string passphrase = "This is a passphrase";
        [HttpPost]
        public string MakeStrung(string original)
        {
            var encrypted = Cryptography.AES.Encrypt(
               original,
                passphrase);
            var x = new StringEntity(encrypted);
            _context.StringEntity.Add(x);
            // }
            _context.SaveChanges();

            return "hugugu";
        }

        [HttpGet]
        public string Get(int id)
        {
            var t = _context.StringEntity.FirstOrDefaultAsync(x => x.Id.Equals(id)).Result;
            var decrypted = Cryptography.AES.Decrypt(
                t.Name,
                passphrase);

            //var cipherText = null;
            // var decryptionResult = 
            // var Ciphertext = Encoding.ASCII.GetBytes(t.CipherText);
            // var InitializationVector = Encoding.ASCII.GetBytes(t.Key);
            // var Salt = Encoding.ASCII.GetBytes(t.Iv);
            // var secretKey = "Ahmad";

            // var plaintext = AesCbcEncryptionService.Decrypt(Ciphertext, InitializationVector, Salt, secretKey);
            // var Key = Encoding.UTF8.GetBytes(t.Key);
            // var IV = Encoding.UTF8.GetBytes(t.Iv);


            // if (cipherText == null || cipherText.Length <= 0)
            //     throw new ArgumentNullException("cipherText");
            // if (Key == null || Key.Length <= 0)
            //     throw new ArgumentNullException("Key");
            // if (IV == null || IV.Length <= 0)
            //     throw new ArgumentNullException("IV");

            // // Declare the string used to hold
            // // the decrypted text.
            // string plaintext = null;

            // // Create an Aes object
            // // with the specified key and IV.
            // using (Aes aesAlg = Aes.Create())
            // {
            //     // aesAlg.Key = Key;
            //     // aesAlg.IV = IV;

            //     // Create a decryptor to perform the stream transform.
            //     ICryptoTransform decryptor = aesAlg.CreateDecryptor(Key, IV);

            //     // Create the streams used for decryption.
            //     using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            //     {
            //         using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            //         {
            //             using (StreamReader srDecrypt = new StreamReader(csDecrypt))
            //             {

            //                 // Read the decrypted bytes from the decrypting stream
            //                 // and place them in a string.
            //                 plaintext = srDecrypt.ReadToEnd();
            //             }
            //         }
            //     }
            // }

            return decrypted;
        }

    }
}


//byte[] decBytes1 = Encoding.UTF8.GetBytes(s1);  /
/// decBytes1.Length == 10 !!
// decBytes1 not same as bytes
// Using UTF-8 or other Encoding object will get similar results

// string s2 = BitConverter.ToString(bytes);   // 82-C8-EA-17
// String[] tempAry = s2.Split('-');
// byte[] decBytes2 = new byte[tempAry.Length];
// for (int i = 0; i < tempAry.Length; i++)
//     decBytes2[i] = Convert.ToByte(tempAry[i], 16);
// // decBytes2 same as bytes

// string s3 = Convert.ToBase64String(bytes);  // gsjqFw==
// byte[] decByte3 = Convert.FromBase64String(s3);
// // decByte3 same as bytes

// string s4 = HttpServerUtility.UrlTokenEncode(bytes);    // gsjqFw2
// byte[] decBytes4 = HttpServerUtility.UrlTokenDecode(s4);
// // decBytes4 same as bytes
// Decrypt the bytes to a string.
/// string roundtrip = ec.DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);

//Display the original data and the decrypted data.