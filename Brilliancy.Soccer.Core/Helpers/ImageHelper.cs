using Brilliancy.Soccer.Common.Dtos.Configuration;
using Brilliancy.Soccer.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
namespace Brilliancy.Soccer.Core.Helpers
{

    public static class ImageHelper
    {
        public static Stream Convert(Stream imageStream, int maxWidth, int maxHeight, bool forceSize = true)
        {
            var image = System.Drawing.Image.FromStream(imageStream);
            return ConvertToSize(image, (float)maxWidth, (float)maxHeight, forceSize);
        }

        private static Stream ConvertToSize(System.Drawing.Image image, float width,
            float height,bool forceSize)
        {
            float scaleW = width / image.Width;
            float scaleH = height / image.Height;
            float scale = Math.Min(scaleW, scaleH);
            if (scale > 1 && !forceSize)
            {
                scale = 1;
            }

            int newWidth = forceSize ? (int)width : (int)(image.Width * scale);
            int newHeight = forceSize ? (int)height : (int)(image.Height * scale);

            using (var bitmap = new System.Drawing.Bitmap(image, new System.Drawing.Size(newWidth, newHeight)))
            {
                var brush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
                var graph = System.Drawing.Graphics.FromImage(bitmap);
                graph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                graph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                if (forceSize)
                {
                    graph.FillRectangle(brush, new System.Drawing.RectangleF(0, 0, width, height));
                    graph.DrawImage(image, new System.Drawing.Rectangle((int)((width - newWidth) / 2),
                        (int)(height - newHeight) / 2, newWidth, newHeight));
                }

                var memoryStream = new MemoryStream();
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return memoryStream;
            }
        }
    }
}
