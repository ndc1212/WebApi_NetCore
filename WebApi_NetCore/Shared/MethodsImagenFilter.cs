using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_NetCore.Shared
{
    public class MethodsImagenFilter
    {
        public static Bitmap Closing(Bitmap Imagem)
        {
           
            Closing filter = new Closing();
            


            var bmp = new Bitmap(Imagem.Width, Imagem.Height, PixelFormat.Format24bppRgb);
            using (var gr = Graphics.FromImage(bmp))
                gr.DrawImage((Image)Imagem, new Rectangle(0, 0, ((Image)Imagem).Width, ((Image)Imagem).Height));

            
            //bmp = ((Bitmap)bmp).Clone(new Rectangle(0, 0, Imagem.Width, Imagem.Height), PixelFormat.Format24bppRgb);
            bmp = filter.Apply(bmp);

            //Bitmap clone = ((Bitmap)Imagem).Clone(new Rectangle(0, 0, Imagem.Width, Imagem.Height), PixelFormat.Format24bppRgb);
            //Logger.Error(clone.PixelFormat.ToString());
            //clone = new Bitmap(clone);
            //clone = clone.Clone(new Rectangle(0, 0, Imagem.Width, Imagem.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //clone = filter.Apply(clone);

            //Logger.Error(clone.PixelFormat.ToString());
            //Imagem = Imagem.Clone(new Rectangle(0, 0, Imagem.Width, Imagem.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //Logger.Error(Imagem.PixelFormat.ToString());

            //Imagem = filter.Apply(Imagem);               
            return bmp;
        }
    }
}
