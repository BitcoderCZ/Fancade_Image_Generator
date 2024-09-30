using FancadeLoaderLib;
using MathUtils.Vectors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace FancadeImageGenerator
{
    internal static class Program
    {
        private static readonly Rgba32[] Colors_Glitched_PC = [
            new Rgba32(0, 0, 0, 0),
            new Rgba32(29, 29, 40, 255),
            new Rgba32(63, 63, 80, 255),
            new Rgba32(101, 103, 121, 255),
            new Rgba32(144, 147, 164, 255),
            new Rgba32(191, 194, 205, 255),
            new Rgba32(255, 255, 255, 255),
            new Rgba32(148, 83, 94, 255),
            new Rgba32(192, 109, 128, 255),
            new Rgba32(225, 153, 152, 255),
            new Rgba32(253, 181, 168, 255),
            new Rgba32(255, 219, 197, 255),
            new Rgba32(255, 243, 242, 255),
            new Rgba32(192, 54, 80, 255),
            new Rgba32(255, 74, 104, 255),
            new Rgba32(255, 154, 154, 255),
            new Rgba32(201, 80, 64, 255),
            new Rgba32(255, 112, 83, 255),
            new Rgba32(255, 168, 129, 255),
            new Rgba32(232, 168, 0, 255),
            new Rgba32(255, 213, 0, 255),
            new Rgba32(255, 255, 122, 255),
            new Rgba32(0, 136, 76, 255),
            new Rgba32(66, 195, 79, 255),
            new Rgba32(181, 255, 110, 255),
            new Rgba32(0, 108, 218, 255),
            new Rgba32(0, 142, 255, 255),
            new Rgba32(0, 193, 255, 255),
            new Rgba32(104, 83, 162, 255),
            new Rgba32(140, 111, 217, 255),
            new Rgba32(176, 138, 255, 255),
            new Rgba32(235, 112, 171, 255),
            new Rgba32(255, 149, 206, 255),
            new Rgba32(255, 185, 232, 255),
            new Rgba32(0, 0, 0, 255),
            new Rgba32(0, 0, 0, 255),
            new Rgba32(0, 24, 0, 255),
            new Rgba32(0, 35, 0, 255),
            new Rgba32(0, 46, 0, 255),
            new Rgba32(0, 42, 0, 255),
            new Rgba32(0, 52, 0, 255),
            new Rgba32(0, 75, 34, 255),
            new Rgba32(0, 12, 42, 255),
            new Rgba32(0, 3, 53, 255),
            new Rgba32(0, 4, 49, 255),
            new Rgba32(0, 0, 60, 255),
            new Rgba32(0, 0, 75, 255),
            new Rgba32(0, 0, 25, 255),
            new Rgba32(0, 2, 13, 255),
            new Rgba32(0, 0, 55, 255),
            new Rgba32(0, 0, 59, 255),
            new Rgba32(0, 0, 44, 255),
            new Rgba32(0, 0, 121, 255),
            new Rgba32(0, 0, 82, 255),
            new Rgba32(0, 0, 121, 255),
            new Rgba32(0, 0, 121, 255),
            new Rgba32(0, 0, 0, 255),
            new Rgba32(17, 10, 0, 255),
            new Rgba32(10, 0, 0, 255),
            new Rgba32(15, 0, 0, 255),
            new Rgba32(4, 26, 0, 255),
            new Rgba32(10, 33, 0, 255),
            new Rgba32(63, 89, 8, 255),
            new Rgba32(0, 46, 3, 255),
            new Rgba32(0, 38, 0, 255),
            new Rgba32(0, 33, 23, 255),
            new Rgba32(0, 15, 33, 255),
            new Rgba32(0, 12, 55, 255),
            new Rgba32(0, 24, 0, 255),
            new Rgba32(0, 0, 0, 255),
            new Rgba32(0, 33, 33, 255),
            new Rgba32(42, 66, 66, 255),
            new Rgba32(80, 103, 104, 255),
            new Rgba32(119, 137, 139, 255),
            new Rgba32(157, 180, 183, 255),
            new Rgba32(197, 255, 255, 255),
            new Rgba32(255, 91, 63, 255),
            new Rgba32(76, 157, 95, 255),
            new Rgba32(109, 191, 131, 255),
            new Rgba32(146, 232, 165, 255),
            new Rgba32(139, 255, 219, 255),
            new Rgba32(201, 255, 246, 255),
            new Rgba32(237, 160, 48, 255),
            new Rgba32(55, 255, 87, 255),
            new Rgba32(99, 255, 201, 255),
            new Rgba32(198, 219, 95, 255),
            new Rgba32(51, 255, 143, 255),
            new Rgba32(87, 255, 180, 255),
            new Rgba32(130, 255, 228, 255),
            new Rgba32(84, 255, 244, 255),
            new Rgba32(139, 255, 255, 255),
            new Rgba32(228, 20, 119, 255),
            new Rgba32(78, 51, 208, 255),
            new Rgba32(120, 198, 250, 255),
            new Rgba32(130, 25, 86, 255),
            new Rgba32(151, 0, 197, 255),
            new Rgba32(255, 113, 235, 255),
            new Rgba32(255, 85, 63, 255),
            new Rgba32(115, 144, 127, 255),
            new Rgba32(214, 226, 219, 255),
            new Rgba32(255, 217, 85, 255),
            new Rgba32(131, 255, 160, 255),
            new Rgba32(219, 255, 197, 255),
            new Rgba32(234, 0, 0, 255),
            new Rgba32(0, 0, 0, 255),
            new Rgba32(0, 0, 0, 255),
            new Rgba32(22, 11, 19, 255),
            new Rgba32(28, 29, 44, 255),
            new Rgba32(72, 71, 90, 255),
            new Rgba32(83, 75, 116, 255),
            new Rgba32(94, 119, 171, 255),
            new Rgba32(59, 0, 24, 255),
            new Rgba32(64, 0, 16, 255),
            new Rgba32(83, 0, 0, 255),
            new Rgba32(147, 51, 53, 255),
            new Rgba32(151, 64, 66, 255),
            new Rgba32(164, 54, 55, 255),
            new Rgba32(64, 2, 36, 255),
            new Rgba32(129, 0, 38, 255),
            new Rgba32(179, 60, 60, 255),
            new Rgba32(150, 35, 0, 255),
            new Rgba32(175, 50, 6, 255),
            new Rgba32(180, 72, 51, 255),
            new Rgba32(164, 79, 21, 255),
            new Rgba32(198, 112, 38, 255),
            new Rgba32(220, 119, 35, 255),
            new Rgba32(0, 53, 47, 255),
            new Rgba32(0, 44, 44, 255),
        ];

        private static readonly Rgba32[] Colors_Basic = [
            new Rgba32(0, 0, 0, 0),
            new Rgba32(29, 29, 40, 255),
            new Rgba32(63, 63, 80, 255),
            new Rgba32(101, 103, 121, 255),
            new Rgba32(144, 147, 164, 255),
            new Rgba32(191, 194, 205, 255),
            new Rgba32(255, 255, 255, 255),
            new Rgba32(148, 83, 94, 255),
            new Rgba32(192, 109, 128, 255),
            new Rgba32(225, 153, 152, 255),
            new Rgba32(253, 181, 168, 255),
            new Rgba32(255, 219, 197, 255),
            new Rgba32(255, 243, 242, 255),
            new Rgba32(192, 54, 80, 255),
            new Rgba32(255, 74, 104, 255),
            new Rgba32(255, 154, 154, 255),
            new Rgba32(201, 80, 64, 255),
            new Rgba32(255, 112, 83, 255),
            new Rgba32(255, 168, 129, 255),
            new Rgba32(232, 168, 0, 255),
            new Rgba32(255, 213, 0, 255),
            new Rgba32(255, 255, 122, 255),
            new Rgba32(0, 136, 76, 255),
            new Rgba32(66, 195, 79, 255),
            new Rgba32(181, 255, 110, 255),
            new Rgba32(0, 108, 218, 255),
            new Rgba32(0, 142, 255, 255),
            new Rgba32(0, 193, 255, 255),
            new Rgba32(104, 83, 162, 255),
            new Rgba32(140, 111, 217, 255),
            new Rgba32(176, 138, 255, 255),
            new Rgba32(235, 112, 171, 255),
            new Rgba32(255, 149, 206, 255),
            new Rgba32(255, 185, 232, 255),
        ];

        private static readonly Dictionary<Vector2I, (Prefab, ushort)> prefabs = new();

        static unsafe int Main(string[] args)
        {
            try
            {
                Console.Write("Path to the image: ");
                string? ImgPath = Console.ReadLine();

                if (!File.Exists(ImgPath))
                {
                    U.PAKE($"File '{ImgPath}' wasn't found");
                    return 3;
                }

                bool glitchedColors = U.TakeBool("Use glitched colors? (Will look correctly only on PC)", true);
                bool dithering = U.TakeBool("Use dithering?", true);

                Game Game = new Game("Image");

                Console.WriteLine("Loading Image...");
                Image<Rgba32> img = Image.Load<Rgba32>(ImgPath);
                Console.WriteLine("Done");

                // choose image size
                const int maxPrefabs = 512;

                int width;
                int height;

                int maxArea = (int)Math.Sqrt(maxPrefabs) * 8;
                if (img.Width == img.Height)
                {
                    width = maxArea;
                    height = maxArea;
                }
                else
                {
                    maxArea *= maxArea;

                    double scale = Math.Sqrt(maxArea / ((double)img.Width * (double)img.Height));

                    width = (int)(img.Width * scale);
                    height = (int)(img.Height * scale);
                }

                if (width != img.Width || height != img.Height)
                {
                    Console.WriteLine($"Resizing to {width}x{height}...");
                    img.Mutate(x =>
                    {
                        x.Resize(width, height, KnownResamplers.Lanczos2);
                        x.Flip(FlipMode.Vertical);
                    });
                    Console.WriteLine("Done");
                }

                Prefab level = Prefab.CreateLevel("Image");
                Game.Prefabs.Add(level);

                Console.WriteLine("Drawing...");
                // resize the level only once
                level.Blocks.EnsureSize(width - 1, 0, height - 1);

                convert(img, glitchedColors ? Colors_Glitched_PC : Colors_Basic, Game, level, dithering);

                Console.WriteLine("Done");

                Console.WriteLine("Saving game...");

                Game.TrimPrefabs();

                string path = Path.GetFullPath("image.fcg");
                File.WriteAllBytes(path, Array.Empty<byte>());
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                    Game.SaveCompressed(fs);

                Console.WriteLine($"Saved to: {path}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:");
                Console.WriteLine(ex);
                U.PAKE();
                return 1;
            }

            U.PAKE();

            return 0;
        }

        static unsafe void convert(Image<Rgba32> img, Rgba32[] palette, Game game, Prefab prefab, bool dithering)
        {
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    Rgba32 ogCol = img[x, y];

                    byte col = (byte)U.ClosestColor(palette, ogCol);

                    Rgba32 newCol = palette[col];

                    if (dithering)
                    {
                        // Floyd–Steinberg dithering
                        Vector4 error = ogCol.ToVector4() - newCol.ToVector4();

                        diffuseError(img, x + 1, y, error, 7.0f / 16.0f);
                        diffuseError(img, x - 1, y + 1, error, 3.0f / 16.0f);
                        diffuseError(img, x, y + 1, error, 5.0f / 16.0f);
                        diffuseError(img, x + 1, y + 1, error, 1.0f / 16.0f);
                    }

                    var (pixelPrefab, id) = getPrefab(x, y);

                    ref Voxel voxel = ref pixelPrefab.Voxels![(x % 8) + (y % 8) * 64];

                    voxel.Colors[0] = col;
                    voxel.Colors[1] = col;
                    voxel.Colors[2] = col;
                    voxel.Colors[3] = col;
                    voxel.Colors[4] = col;
                    voxel.Colors[5] = col;

                    prefab.Blocks.SetBlock(x / 8, 0, y / 8, id);
                }
            }


            (Prefab, ushort) getPrefab(int x, int y)
            {
                Vector2I pos = new Vector2I(x / 8, y / 8);

                if (!prefabs.TryGetValue(pos, out (Prefab, ushort) item))
                {
                    item = (Prefab.CreateBlock("B" + (pos.X + pos.Y * 8).ToString()), (ushort)(game.Prefabs.Count + RawGame.CurrentNumbStockPrefabs));
                    game.Prefabs.Add(item.Item1);
                    prefabs.Add(pos, item);
                }

                return item;

            }
        }

        static void diffuseError(Image<Rgba32> img, int x, int y, Vector4 error, float factor)
        {
            if (x < 0 || x >= img.Width || y < 0 || y >= img.Height)
                return;

            Rgba32 pixel = img[x, y];
            Vector4 colorVector = pixel.ToVector4();

            Vector4 newColor = colorVector + error * factor;

            newColor = Vector4.Clamp(newColor, Vector4.Zero, Vector4.One);

            img[x, y] = new Rgba32(newColor);
        }
    }
}