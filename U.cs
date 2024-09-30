using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancadeImageGenerator
{
    internal static class U
    {
        /// <summary>
        /// Shows press any key to exit, then waits for key press
        /// </summary>
        /// <param name="message"></param>
        public static void PAKE(string message = "")
        {
            Console.CursorVisible = true;

            if (string.IsNullOrWhiteSpace(message))
                Console.WriteLine("Press any key to exit...");
            else
                Console.WriteLine(message + ", press any key to exit...");

            Console.ReadKey(true);
        }

        public static bool TakeBool(string message, bool defaultOption)
        {
            Console.Write($"{message} ({(defaultOption ? 'Y' : 'y')}/{(defaultOption ? 'n' : 'N')}): ");

            string? typed = Console.ReadLine();

            if (defaultOption)
                return !(typed == "n" || typed == "N");
            else
                return (typed == "y" || typed == "Y");
        }

        public static int ClosestColor(Rgba32[] colors, Rgba32 color)
        {
            int minDiff = int.MaxValue;
            int index = -1;

            if (color.A < 127)
                return 0;

            for (int i = 1; i < colors.Length; i++)
            {
                int diff = ColorDiff(color, colors[i]);

                if (diff < minDiff)
                {
                    minDiff = diff;
                    index = i;
                }
            }

            return index;
        }

        public static int ColorDiff(Rgba32 c1, Rgba32 c2)
            => (int)Math.Sqrt((c1.R - c2.R) * (c1.R - c2.R)
                                 + (c1.G - c2.G) * (c1.G - c2.G)
                                 + (c1.B - c2.B) * (c1.B - c2.B));
    }
}
