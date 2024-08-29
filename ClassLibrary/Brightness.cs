using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Drawing;

namespace ClassLibrary
{
    [Version(3, 404)]
    public class Brightness : IPlugin
    {
        public string Name => "Повышение яркости";

        public string Author => "No name";

        public void Transform(Bitmap bitmap)
        {
            int bright = 10;
            for (var i = 0; i < bitmap.Width; i ++)
                for (var j = 0; j < bitmap.Height; j ++)
                {
                    int r = Math.Min(255, /*Math.Max(0, */bitmap.GetPixel(i, j).R + bright)/*)*/;
                    int g = Math.Min(255,/* Math.Max(0, */bitmap.GetPixel(i, j).G + bright)/*)*/;
                    int b = Math.Min(255, /*Math.Max(0, */bitmap.GetPixel(i, j).B + bright)/*)*/;
                    
                    bitmap.SetPixel(i, j, Color.FromArgb(r,g,b));
                }
        }

    }
}
