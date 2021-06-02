using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_NetCore.Shared
{
    public class MethodsImagenFilter
    {
        public static Bitmap Closing(Bitmap Imagem)
        {
            Closing filter = new Closing();
            Imagem = Imagem.Clone(new Rectangle(0, 0, Imagem.Width, Imagem.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Imagem = filter.Apply(Imagem);
            return Imagem;
        }
    }
}
