using System;
using System.Drawing;

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
}