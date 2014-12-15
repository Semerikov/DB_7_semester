using System;
using System.Drawing;
using System.IO;
using System.Web.Razor.Text;

public static class FileHelper
{
    public static byte[] GetFile(string path)
    {
        byte[] fileBytes = null;
        using (var stream = new System.IO.MemoryStream())
        {
            var bitmap = new Bitmap(path);
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            fileBytes = stream.ToArray();
        }

        return fileBytes;
    }

	public static byte[] GetAFile(string path)
	{
		byte[] array;
		using (var stream = File.OpenRead(path))
		{
			array = new byte[stream.Length];
			stream.Read(array, 0, (int) stream.Length);
		}

		return array;
	}
}