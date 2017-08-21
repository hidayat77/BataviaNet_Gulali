using System;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Net.Mail;

/// <summary>
/// Email Sender
/// </summary>
public class Mail
{
	public static void Send(Page p, string Sender, string Recipient, string Cc, string Subject, string Body, string Attachment)
	{
		MailMessage mail = new MailMessage();
        

		bool sent = true;
		try
		{
			//Sender
			mail.From = new MailAddress(Sender);

			//Recipient
			string[] t = Recipient.Split(';');
			for (int i = 0; i <= t.GetUpperBound(0); i++)
			{
				if (t[i] != "") mail.To.Add(new MailAddress(t[i]));
			}

			//CC
			string[] c = Cc.Split(';');
			for (int i = 0; i <= c.GetUpperBound(0); i++)
			{
				if (c[i] != "") mail.CC.Add(new MailAddress(c[i]));
			}

			//Subject and Body
			mail.Subject = Subject;
			mail.Body = Body;
            mail.IsBodyHtml = true;
            //mail.Headers.Add("Content-Type", "content=text/html; charset=\"UTF-8\"");

			//Attachments
			string[] a = Attachment.Split(';');
			for (int i = 0; i <= a.GetUpperBound(0); i++)
			{
				if (a[i] != "") mail.Attachments.Add(new Attachment(a[i]));
			}

			//Sending
			//Response.Write()
			SmtpClient smtp = new SmtpClient(Param.SMTPServer);
			smtp.Credentials = new System.Net.NetworkCredential(Param.SMTPUser, Param.SMTPPass);
			smtp.Send(mail);
		}
		catch
		{
			sent = false;
		}

		if (!sent)
			Js.Alert(p, "Mail can not be sent.");
		else
			Js.Alert(p, "Mail sent successfully");
	}
	
	public static bool SendMail(Page p, string Sender, string Recipient, string Cc, string Subject, string Body, string Attachment)
	{
		MailMessage mail = new MailMessage();
        

		bool sent = true;
		try
		{
			//Sender
			mail.From = new MailAddress(Sender);

			//Recipient
			string[] t = Recipient.Split(';');
			for (int i = 0; i <= t.GetUpperBound(0); i++)
			{
				if (t[i] != "") mail.To.Add(new MailAddress(t[i]));
			}

			//CC
			string[] c = Cc.Split(';');
			for (int i = 0; i <= c.GetUpperBound(0); i++)
			{
				if (c[i] != "") mail.CC.Add(new MailAddress(c[i]));
			}

			//Subject and Body
			mail.Subject = Subject;
			mail.Body = Body;
            mail.IsBodyHtml = true;
            //mail.Headers.Add("Content-Type", "content=text/html; charset=\"UTF-8\"");

			//Attachments
			string[] a = Attachment.Split(';');
			for (int i = 0; i <= a.GetUpperBound(0); i++)
			{
				if (a[i] != "") mail.Attachments.Add(new Attachment(a[i]));
			}

			//Sending
			//Response.Write()
			SmtpClient smtp = new SmtpClient(Param.SMTPServer);
			smtp.Credentials = new System.Net.NetworkCredential(Param.SMTPUser, Param.SMTPPass);
			smtp.Send(mail);
		}
		catch
		{
			sent = false;
		}

		if (!sent)
		   //Js.Alert(p, "Mail can not be sent.");
			return false; //"<span style=\"color: red; font-weight:bold;\">Mail can not be sent.</span>";           			
		else
		   //Js.Alert(p, "Mail can be sent.");
			return true; // "<span style=\"color: red; font-weight:bold;\">Mail sent successfully.</span>"; 
	}
}




public class MailEcards
{
	public static void Send(Page p, string Sender, string Recipient, string Cc, string Subject, string Body, string Attachment)
	{
		MailMessage mail = new MailMessage();
        

		bool sent = true;
		try
		{
			//Sender
			mail.From = new MailAddress(Sender);

			//Recipient
			string[] t = Recipient.Split(';');
			for (int i = 0; i <= t.GetUpperBound(0); i++)
			{
				if (t[i] != "") mail.To.Add(new MailAddress(t[i]));
			}

			//CC
			string[] c = Cc.Split(';');
			for (int i = 0; i <= c.GetUpperBound(0); i++)
			{
				if (c[i] != "") mail.CC.Add(new MailAddress(c[i]));
			}

			//Subject and Body
			mail.Subject = Subject;
			mail.Body = Body;
            mail.IsBodyHtml = true;
            //mail.Headers.Add("Content-Type", "content=text/html; charset=\"UTF-8\"");

			//Attachments
			string[] a = Attachment.Split(';');
			for (int i = 0; i <= a.GetUpperBound(0); i++)
			{
				if (a[i] != "") mail.Attachments.Add(new Attachment(a[i]));
			}

			//Sending
			//Response.Write()
			SmtpClient smtp = new SmtpClient(Param.SMTPServer);
			//smtp.Credentials = new System.Net.NetworkCredential(Param.SMTPUser, Param.SMTPPass);
			smtp.Send(mail);
		}
		catch
		{
			sent = false;
		}

		//if (!sent)
		//	Js.Alert(p, "Mail can not be sent.");
		//if(sent)
		//	Js.Alert(p, "Mail sent successfully");
	}
}
