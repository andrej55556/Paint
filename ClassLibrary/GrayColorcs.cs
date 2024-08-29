using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Drawing;

namespace ClassLibrary
{
    [Version(4, 20)]
    public class GrayColorcs : IPlugin
    {
        public string Name => "Оттенки серого";

        public string Author => "I";

        public void Transform(Bitmap bitmap)
        {
            for (var i = 0; i < bitmap.Width; i++)
                for (var j = 0; j < bitmap.Height; j++)
                {
                    Color color = bitmap.GetPixel(i, j);
                    int gray = (int)(color.R * 0.299 + color.G * 0.587 + color.B * 0.114);
                    bitmap.SetPixel(i, j, Color.FromArgb(gray,gray,gray));
                }
        }
    }
}
