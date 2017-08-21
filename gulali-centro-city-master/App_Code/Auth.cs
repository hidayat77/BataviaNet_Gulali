using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;

/// <summary>
/// Security Business Logics
/// </summary>
public class Auth
{
	//Password Business Logic
	public static string Hash(string pInput)
	{
		MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

		byte[] data = System.Text.Encoding.ASCII.GetBytes(pInput);
		data = md5.ComputeHash(data);

		string x = "";
		for (int i = 0; i < data.Length; i++)
			x += data[i].ToString("x2");

		return x;
	}
}
