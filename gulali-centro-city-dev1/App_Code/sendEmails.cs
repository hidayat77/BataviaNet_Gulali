using System;
using System.Collections.Generic;
using System.Web;
using System.Net.Mail;

/// <summary>
/// Summary description for sendEmails
/// </summary>
public class sendEmails
{
	public sendEmails()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void sendEmail(string mailFrom, string Subject, string Message, string mailTo ,string username,string password,string server,int port)
    {
        System.Net.Mail.MailMessage objMM = new System.Net.Mail.MailMessage();
        SmtpClient MailObj = new SmtpClient(server, port);
        //System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("testing@viveramedia.co.uk", "t3sting");
        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(username, password);
        MailObj.Credentials = credentials;
        objMM.To.Add(new MailAddress(mailTo));
        objMM.From = new MailAddress(mailFrom);
        objMM.IsBodyHtml = true;
        objMM.Priority = System.Net.Mail.MailPriority.Normal;

        //Set the subject
        objMM.Subject = Subject;


        objMM.Body = Message;

        SmtpClient SmtpMail = new SmtpClient();

        //Now, to send the message, use the Send method of the SmtpMail class
        MailObj.Send(objMM);

    }

    public static void newStaffAdded_sendEmail(string mailFrom, string Subject, string Message, string mailTo)
    {
        try
        {
            System.Net.Mail.MailMessage objMM = new System.Net.Mail.MailMessage();
            SmtpClient MailObj = new SmtpClient("mail.viveramedia.co.uk", 25);
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("testing@viveramedia.co.uk", "t3sting");
            MailObj.Credentials = credentials;
            objMM.To.Add(new MailAddress(mailTo));
            objMM.From = new MailAddress(mailFrom);
            objMM.IsBodyHtml = true;
            objMM.Priority = System.Net.Mail.MailPriority.Normal;

            //Set the subject
            objMM.Subject = Subject;


            objMM.Body = Message;

            SmtpClient SmtpMail = new SmtpClient();

            //Now, to send the message, use the Send method of the SmtpMail class
            MailObj.Send(objMM);

        }
        catch (Exception exp)
        { 
        
            }
    }
}