using System;
using System.Web;
using System.IO;
using System.Drawing;
using System.Web.UI.HtmlControls;

	public class Dfc
	{
		#region public static bool ThumbnailCallback()
		public static bool ThumbnailCallback()
		{
			return false;
		}
		#endregion

		#region public static void UploadFile(string AllowExtension, string CompleteFilePath, System.Web.UI.HtmlControls.HtmlInputFile myRawFile)
		public static void UploadFile(string AllowExtension, string CompleteFilePath, System.Web.UI.HtmlControls.HtmlInputFile myRawFile)
		{
			if(myRawFile.PostedFile != null)
			{
				HttpPostedFile myFile = myRawFile.PostedFile;
				int FileLen = myFile.ContentLength;

				// Check file size (mustn’t be 0)
				if (FileLen == 0)
					return;

				// Check file extension
				if (Path.GetExtension(myFile.FileName).ToLower() != AllowExtension)
					return;

				// Read file into a data stream
				byte[] myData = new Byte[FileLen];
				myFile.InputStream.Read(myData,0,FileLen);

				// Save the stream to disk
				FileStream newFile
					= new FileStream(CompleteFilePath, 
					FileMode.Create);
				newFile.Write(myData,0, myData.Length);
				newFile.Close();

				File.SetLastWriteTime(CompleteFilePath, System.DateTime.Now);
			}
		}
		#endregion
		#region public static void DeleteFile(string CompletePath)
		public static void DeleteFile(string CompletePath)
		{
			File.Delete(CompletePath);
		}
		#endregion
		#region public static void CopyFile(string CompletePathOld, string CompletePathNew)
		public static void CopyFile(string CompletePathOld, string CompletePathNew)
		{
			File.Copy(CompletePathOld,CompletePathNew,true);
			File.SetLastWriteTime(CompletePathNew, System.DateTime.Now);
		}
		#endregion
		#region public static void RenameFile(string CompletePathOld, string CompletePathNew)
		public static void RenameFile(string CompletePathOld, string CompletePathNew)
		{
			CopyFile(CompletePathOld,CompletePathNew);
			File.Delete(CompletePathOld);
		}
		#endregion
	}