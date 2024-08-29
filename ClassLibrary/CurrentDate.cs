using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Drawing;
using ExifLib;
using MetadataExtractor;

namespace ClassLibrary
{
    [Version(4, 20)]
    public class CurrentDate : IPlugin
    {
        public string Name => "Добавление текущей даты и данные геолокации в правом нижнем углу изображения";

        public string Author => "Я";

        public void Transform(Bitmap bitmap)
        {
            // Get current date
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string photo = "IMG_20230726_120512(1).jpg";

            // Initialize variables for geolocation
            double? latitude = null;
            double? longitude = null;

            // Read EXIF data
            //var directories = ImageMetadataReader.ReadMetadata("filtry.jpg");
            var directories = ImageMetadataReader.ReadMetadata(photo);
            foreach (var directory in directories)
            {
                if (directory is MetadataExtractor.Formats.Exif.ExifIfd0Directory ifd0)
                {
                    // Get the date taken
                    if (ifd0.TryGetDateTime(MetadataExtractor.Formats.Exif.ExifDirectoryBase.TagDateTimeOriginal, out DateTime dateTaken))
                    {
                        currentDate = dateTaken.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }

                if (directory is MetadataExtractor.Formats.Exif.GpsDirectory gps)
                {
                    latitude = gps.GetGeoLocation()?.Latitude;
                    longitude = gps.GetGeoLocation()?.Longitude;
                }
            }

            // Create a graphics object
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Set the font and brush
                Font font = new Font("Arial", 12, FontStyle.Bold);
                Brush brush = Brushes.Black;

                // Calculate position for the text
                string geoLocation = latitude.HasValue && longitude.HasValue
                    ? $"Lat: {latitude.Value:F4}, Lon: {longitude.Value:F4}"
                    : "Geolocation not available";

                SizeF dateSize = g.MeasureString(currentDate, font);
                SizeF geoSize = g.MeasureString(geoLocation, font);
                float x = bitmap.Width - Math.Max(dateSize.Width, geoSize.Width) - 10; // 10 pixels from the right
                float y = bitmap.Height - dateSize.Height - geoSize.Height - 10; // 10 pixels from the bottom

                // Draw the date and geolocation
                g.DrawString(currentDate, font, brush, x, y);
                g.DrawString(geoLocation, font, brush, x, y + dateSize.Height);
            }

            // Save the modified image
            bitmap.Save("path_to_save_modified_image.jpg");
        }
    }
}
