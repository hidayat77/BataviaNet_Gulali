using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.Xml;
using System.Security;
using System.Security.Cryptography;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using System.Text;

public class ConnectionStringManager
{
    public ConnectionStringManager()
    {
    }
    public static string GetConnectionString(string connName)
    {
       /* try
        {*/

            string encryptionPassword = "supersecret";
            string connString = string.Empty;
            string configPath = string.Empty;
            string p = Process.GetCurrentProcess().MainModule.FileName;
            configPath = "D:\\Project\\commweb\\webroot\\web.config";

            XmlDocument doc = new XmlDocument();
            doc.Load(configPath);
            XmlNode node = null;

            node = doc.SelectSingleNode("configuration/connectionStrings/add[@name = \"CnnStr\"]");
            if (node != null)
            {
                XmlAttribute attr = node.Attributes["connectionString"];
                if (attr != null)
                {
                    string conString = node.Attributes["connectionString"].Value;
                    SqlConnectionStringBuilder conStringBuilder = new SqlConnectionStringBuilder(conString);
                    node.Attributes["connectionString"].Value = conStringBuilder.ConnectionString;

                    SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(attr.Value);
                    if (IsIntegratedSecurity(csb.ToString()))
                    {
                        string clearPass = Decrypt(csb.Password, encryptionPassword);
                        if (string.IsNullOrEmpty(clearPass)){
                            csb.Password = Encrypt(csb.Password,encryptionPassword);
                            connString = csb.ToString();
                            csb.InitialCatalog = connString;
                            attr.Value = csb.ToString();
                            doc.Save(configPath);
                        }
                        else
                        {
                            csb.Password = clearPass;
                            connString = csb.ToString();
                            attr.Value = csb.ToString();
                        }
                    }
                }
            }

            return connString;
          
        /*}
        catch (Exception)
        {
            return null;
        }*/
    }

    private static bool IsIntegratedSecurity(string attr)
    {
        return attr.ToUpper().Contains("Password");
    }

    private static readonly byte[] salt = Encoding.ASCII.GetBytes("Ent3r your oWn S@lt v@lu# h#r3");

    public static string Encrypt(string textToEncrypt, string encryptionPassword)
    {
        var algorithm = GetAlgorithm(encryptionPassword);

        byte[] encryptedBytes;
        using (ICryptoTransform encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV))
        {
            byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(textToEncrypt);
            encryptedBytes = InMemoryCrypt(bytesToEncrypt, encryptor);
        }
        return Convert.ToBase64String(encryptedBytes);
    }

    public static string Decrypt(string encryptedText, string encryptionPassword)
    {
        var algorithm = GetAlgorithm(encryptionPassword);

        byte[] descryptedBytes;
        using (ICryptoTransform decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV))
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            descryptedBytes = InMemoryCrypt(encryptedBytes, decryptor);
        }
        return Encoding.UTF8.GetString(descryptedBytes);
    }

    // Performs an in-memory encrypt/decrypt transformation on a byte array.
    private static byte[] InMemoryCrypt(byte[] data, ICryptoTransform transform)
    {
        MemoryStream memory = new MemoryStream();
        using (Stream stream = new CryptoStream(memory, transform, CryptoStreamMode.Write))
        {
            stream.Write(data, 0, data.Length);
        }
        return memory.ToArray();
    }

    // Defines a RijndaelManaged algorithm and sets its key and Initialization Vector (IV) 
    // values based on the encryptionPassword received.
    private static RijndaelManaged GetAlgorithm(string encryptionPassword)
    {
        // Create an encryption key from the encryptionPassword and salt.
        var key = new Rfc2898DeriveBytes(encryptionPassword, salt);

        // Declare that we are going to use the Rijndael algorithm with the key that we've just got.
        var algorithm = new RijndaelManaged();
        int bytesForKey = algorithm.KeySize / 8;
        int bytesForIV = algorithm.BlockSize / 8;
        algorithm.Key = key.GetBytes(bytesForKey);
        algorithm.IV = key.GetBytes(bytesForIV);
        return algorithm;
    }


}
