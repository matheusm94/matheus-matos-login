using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;

namespace LabMVC.Services
{
    public class LoginService
    {
        public string EncryptPassword(string password)
        {
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.GenerateKey();
                aes.GenerateIV();

                ICryptoTransform encryptor = aes.CreateEncryptor();

                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(passwordBytes, 0, passwordBytes.Length);

                string encryptedPassword = Convert.ToBase64String(encryptedBytes);
                return encryptedPassword;
            }
        }

        public string DecryptPassword(string encryptedPassword)
        {
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.GenerateKey();
                aes.GenerateIV();

                ICryptoTransform decryptor = aes.CreateDecryptor();

                byte[] encryptedBytes = Convert.FromBase64String(encryptedPassword);
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                string decryptedPassword = Encoding.UTF8.GetString(decryptedBytes);
                return decryptedPassword;
            }
        }

    }
}
