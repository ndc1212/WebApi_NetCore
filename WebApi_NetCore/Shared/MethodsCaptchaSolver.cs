using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Tesseract;
using System.Diagnostics;
using System.IO;

namespace WebApi_NetCore.Shared
{
    public class MethodsCaptchaSolver
    {
        public static string OCR(Bitmap b)
        {
            try
            {
                b = MethodsImagenFilter.Closing(b);
                string res = string.Empty;
                string path = $@"{Environment.CurrentDirectory}/tessdata/";
                //Debug.Print(path);
                //path = "C:\\Users\\admin\\Desktop\\Captcha-Solver-master\\Captcha-Solver-master\\Captcha-Solver\\Captcha-Solver-Gui\\bin\\Debug\\tessdata\\";
                using (var engine = new TesseractEngine(path, "eng"))
                {
                    string letters = "abcdefghijklmnopqrstuvwxyz";
                    string numbers = "0123456789";
                    engine.SetVariable("tessedit_char_whitelist", $"{numbers}{letters}{letters.ToUpper()}");
                    engine.SetVariable("tessedit_unrej_any_wd", true);
                    engine.SetVariable("tessedit_adapt_to_char_fragments", true);
                    engine.SetVariable("tessedit_redo_xheight", true);
                    engine.SetVariable("chop_enable", true);

                    Bitmap x = b.Clone(new Rectangle(0, 0, b.Width, b.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    //var bytes = ImageToByte2((Image)x);
                    var tess = new BitmapToPixConverter();
                    Pix y = tess.Convert(x);
                    using (var page = engine.Process(y, PageSegMode.SingleLine))
                        res = page.GetText().Replace(" ", "").Trim();

                    
                }

                return res;
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Erro: {ex.Message}");
                return ex.ToString();
            }
        }
        public static byte[] ImageToByte2(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                return stream.ToArray();
            }
        }
    }
}
