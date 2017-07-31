using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

/// <summary>
/// Image Handler
/// </summary>
public class Imgh {
	//Cropping
	public static void Crop(string Source, string Target, decimal Persen) {
		Image ori = Image.FromFile(Source);

		int h = (int)(ori.Height * Persen);
		int w = (int)(ori.Width * Persen);

		ori.Dispose();

		Crop(Source, Target, h, w);
	}
	public static void Crop(string Source, string Target, int MaxPixel, bool Stretch) {
		Image ori = Image.FromFile(Source);

		int h, w;
		if (ori.Height > ori.Width) {
			h = (!Stretch && ori.Height < MaxPixel) ? ori.Height : MaxPixel;
			w = (int)(ori.Width * ((float)h / (float)ori.Height));
		}
		else {
			w = (!Stretch && ori.Width < MaxPixel) ? ori.Width : MaxPixel;
			h = (int)(ori.Height * ((float)w / (float)ori.Width));
		}

		ori.Dispose();

		Crop(Source, Target, h, w);
	}
	public static void Crop(string Source, string Target, int h, int w) {
		Image ori = Image.FromFile(Source);
		Image img = Image.FromFile(Source);

		//Create blank canvas. Image yang sudah di-crop akan digambar di canvas ini
		Bitmap bm = new Bitmap(w, h, PixelFormat.Format24bppRgb);
		bm.SetResolution(72, 72);

		Graphics gr = Graphics.FromImage(bm);
		gr.SmoothingMode = SmoothingMode.AntiAlias;
		gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
		gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
		gr.DrawImage(img, new Rectangle(0, 0, w, h), 0, 0, ori.Width, ori.Height, GraphicsUnit.Pixel);

		//Save to a file
		bm.Save(Target, ImageFormat.Jpeg);

		//Dispose supaya file tidak di-locked
		ori.Dispose();
		img.Dispose();
		bm.Dispose();
		gr.Dispose();
	}
}
