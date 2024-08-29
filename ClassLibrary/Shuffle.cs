using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace ClassLibrary
{
    [Version(6, 0)]
    public class Shuffle : IPlugin
    {
        public string Name => "Разбить изображение на девять равных частей и поменять их местами в произвольном порядке";

        public string Author => "3";

        /*public void Transform(Bitmap bitmap)
        {
            int width = bitmap.Width / 3;
            int height = bitmap.Height / 3;

            Bitmap shuffledImage = new Bitmap(bitmap.Width, bitmap.Height);

            int[] indices = Enumerable.Range(0, 9).OrderBy(x => Guid.NewGuid()).ToArray();

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    int index = y * 3 + x;
                    int newX = indices[index] % 3;
                    int newY = indices[index] / 3;

                    Rectangle srcRect = new Rectangle(x * width, y * height, width, height);
                    Rectangle destRect = new Rectangle(newX * width, newY * height, width, height);

                    using (Graphics g = Graphics.FromImage(shuffledImage))
                    {
                        g.DrawImage(bitmap, destRect, srcRect, GraphicsUnit.Pixel);
                    }
                }
            }

            bitmap = shuffledImage;
            bitmap.Save("result.jpg");
        }*/

        public void Transform(Bitmap bitmap)
        {
            // Проверка на минимальные размеры
            if (bitmap.Width < 3 || bitmap.Height < 3)
            {
                throw new ArgumentException("Изображение должно быть не меньше 3x3 пикселей.");
            }

            // Вычисляем размеры сегментов
            int segmentWidth = bitmap.Width / 3; // Ширина сегмента
            int segmentHeight = bitmap.Height / 3; // Высота сегмента

            // Создаем новый Bitmap для перемешанных сегментов
            Bitmap shuffledImage = new Bitmap(bitmap.Width, bitmap.Height);
            int[] indices = Enumerable.Range(0, 9).OrderBy(x => Guid.NewGuid()).ToArray(); // Перемешиваем индексы

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    int index = y * 3 + x; // Текущий индекс
                    int newX = indices[index] % 3; // Новый X для перемешивания
                    int newY = indices[index] / 3; // Новый Y для перемешивания

                    // Определяем области для копирования
                    Rectangle srcRect = new Rectangle(x * segmentWidth, y * segmentHeight, segmentWidth, segmentHeight);
                    Rectangle destRect = new Rectangle(newX * segmentWidth, newY * segmentHeight, segmentWidth, segmentHeight);

                    // Копируем сегмент из оригинального изображения в перемешанное
                    using (Graphics g = Graphics.FromImage(shuffledImage))
                    {
                        g.DrawImage(bitmap, destRect, srcRect, GraphicsUnit.Pixel);
                    }
                }
            }

            // Сохраняем перемешанное изображение
            shuffledImage.Save("result.jpg");
            // Обновляем оригинальное изображение
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(shuffledImage, 0, 0, bitmap.Width, bitmap.Height);
            }
        }
    }
}
