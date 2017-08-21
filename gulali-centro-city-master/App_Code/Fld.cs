using System;
using System.IO;
using System.Web;

/// <summary>
/// Folder Library
/// </summary>
public class Fld {
	//Cleaner Procedure
	public static void Reset(string dir) {
		if (Directory.Exists(dir)) {
			Delete(dir);
			DeleteFile(dir);
		}
	}
	public static void Delete(string dir) {
		if (Directory.Exists(dir)) {
			string[] subfolders = Directory.GetDirectories(dir);
			foreach (string subfolder in subfolders) {
				if (!HttpContext.Current.Response.IsClientConnected) break;
				try { Directory.Delete(subfolder, true); }
				catch { }
			}
		}
	}
	public static void DeleteFile(string dir) {
		DeleteFile(dir, "*.*");
	}
	public static void DeleteFile(string dir, string searchPattern) {
		if (Directory.Exists(dir)) {
			string[] files = Directory.GetFiles(dir, searchPattern);
			foreach (string file in files) {
				if (!HttpContext.Current.Response.IsClientConnected) break;
				try { File.Delete(file); }
				catch { }
			}
		}
	}
}
