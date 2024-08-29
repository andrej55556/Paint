using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Drawing;

namespace ClassLibrary
{
    [Version(2, 0)]
    public class HatchingTransform : IPlugin
    {
        public string Name => "Штриховка";

        public string Author => "No name";

        public void Transform(Bitmap bitmap)
        {
            for (var i = 0; i < bitmap.Width; i += 8)
                for (var j = 0; j < bitmap.Height; j += 8)
                {
                    bitmap.SetPixel(i, j, Color.Black);
                    if(i < bitmap.Height && j < bitmap.Width)
                        bitmap.SetPixel(j, i, Color.Black);
                }
        }

    }
}
