using FancadeLoaderLib;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Reflection;

namespace FancadeImageGenerator
{
    internal static class Program
    {
        // actual color index is [index] + 1 (0 is empty)
        public static readonly Argb32[] Colors = new[]
        {
            new Argb32(30, 30, 40, 255), // 1
            new Argb32(66, 66, 82, 255),
            new Argb32(105, 107, 124, 255),
            new Argb32(150, 153, 169, 255), // 4
            new Argb32(198, 201, 211, 255),
            new Argb32(255, 255, 255, 255),
            new Argb32(152, 86, 96, 255), // 7
            new Argb32(197, 113, 132, 255),
            new Argb32(231, 159, 157, 255),
            new Argb32(255, 188, 174, 255), // 10
            new Argb32(255, 223, 200, 255),
            new Argb32(255, 249, 247, 255),
            new Argb32(199, 56, 83, 255), // 13
            new Argb32(255, 78, 111, 255),
            new Argb32(255, 164, 164, 255),
            new Argb32(209, 85, 68, 255), // 16
            new Argb32(255, 119, 88, 255),
            new Argb32(255, 179, 137, 255),
            new Argb32(240, 179, 0, 255), // 19
            new Argb32(255, 226, 0, 255),
            new Argb32(255, 255, 129, 255),
            new Argb32(0, 143, 80, 255), // 22
            new Argb32(69, 201, 82, 255),
            new Argb32(187, 255, 114, 255),
            new Argb32(0, 113, 229, 255), // 25
            new Argb32(0, 150, 255, 255),
            new Argb32(0, 201, 255, 255),
            new Argb32(110, 88, 169, 255), // 28
            new Argb32(148, 118, 227, 255),
            new Argb32(186, 146, 255, 255),
            new Argb32(247, 119, 180, 255), // 31
            new Argb32(255, 158, 217, 255),
            new Argb32(255, 196, 244, 255),
        };

        public static string BasePath;

        public static string GamePath;
        public static string ImgPath;
        public static Game Game;

        static int Main(string[] args)
        {
            BasePath = Assembly.GetExecutingAssembly().Location;

            Console.Write("Path to the game or empty to create a new game: ");
            GamePath = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(GamePath))
                Console.WriteLine("Created empty game");
            else if (!File.Exists(GamePath))
            {
                U.PAKE($"File '{GamePath}' wasn't found");
                return 2;
            }
            Console.Write("Path to the image: ");
            ImgPath = Console.ReadLine()!;

            if (!File.Exists(ImgPath))
            {
                U.PAKE($"File '{ImgPath}' wasn't found");
                return 3;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(GamePath))
                    Game = new Game("Image");
                else
                {
                    Console.WriteLine("Loading game...");
                    using (SaveReader reader = new SaveReader(GamePath))
                        Game = Game.Load(reader);

                    Game.FixIds();
                    Console.WriteLine("Done");
                }

                Console.WriteLine("Loading Image...");
                Image<Argb32> img = Image.Load<Argb32>(ImgPath);
                Console.WriteLine("Done");

                int width = takeNmber("Target image width", 4, 400);
                int height = takeNmber("Target image height", 4, 400);

                Console.WriteLine("Resizing...");
                img.Mutate(x =>
                {
                    x.Resize(width, height, KnownResamplers.Lanczos2);
                    x.Flip(FlipMode.Vertical);
                });
                Console.WriteLine("Done");

                Level level = AddLevel("Image");

                Game.FixIds();
                addColBlocks(out Dictionary<byte, ushort> colToId);

                Console.WriteLine("Drawing...");
                // resize the level only once
                level.BlockIds.SetSegment(width - 1, 0, height - 1, 1);

                // draw image
                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                    {
                        Argb32 col = img[x, y];
                        ushort block = colToId[(byte)U.ClosestColor(Colors, col)];
                        level.BlockIds.SetSegment(x, 0, y, (ushort)(block));
                    }

                // add color blocks
                int j = 0;
                foreach (var item in colToId)
                {
                    level.BlockIds.SetSegment(j * 2, 0, height + 2, item.Value);
                    j++;
                }

                Console.WriteLine("Done");

                Game.FixIds();

                Console.WriteLine("Saving game...");

                string path = Path.Combine(Environment.CurrentDirectory, U.RandomGameId() + " + image");
                File.WriteAllBytes(path, Array.Empty<byte>());
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
                    Game.Save(fs);

                Console.WriteLine("Done");
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

        static int takeNmber(string name, int min, int max)
        {
            while (true)
            {
                Console.Write($"{name}, between {min}-{max}: ");
                string inp = Console.ReadLine()!;
                if (!int.TryParse(inp, out int num))
                {
                    Console.WriteLine("Invalid number");
                    continue;
                }
                else if (num < min)
                {
                    Console.WriteLine($"{name} can't be smaller than {min}");
                    continue;
                }
                else if (num > max)
                {
                    Console.WriteLine($"{name} can't be larger than {max}");
                    continue;
                }
                else
                    return num;
            }
        }

        static unsafe void addColBlocks(out Dictionary<byte, ushort> colToId)
        {
            colToId = new Dictionary<byte, ushort>();

            for (byte col = 1; col < Colors.Length + 1; col++)
            {
                Block block = GetBlock("COL" + (col - 1));

                colToId.Add((byte)(col - 1), block.MainId);

                List<Vector3I> toRemove = new List<Vector3I>();
                foreach (var item in block.Sections)
                    if (item.Key != Vector3I.Zero)
                        toRemove.Add(item.Key);

                foreach (var pos in toRemove)
                    block.Sections.Remove(pos);

                BlockSection section;
                if (!block.Sections.TryGetValue(Vector3I.Zero, out section))
                    section = new BlockSection(new SubBlock[8 * 8 * 8], BlockAttribs.Default, block.MainId);

                for (int j = 0; j < section.Blocks.Length; j++)
                {
                    section.Blocks[j].Colors[0] = col;
                    section.Blocks[j].Colors[1] = col;
                    section.Blocks[j].Colors[2] = col;
                    section.Blocks[j].Colors[3] = col;
                    section.Blocks[j].Colors[4] = col;
                    section.Blocks[j].Colors[5] = col;
                }
            }
        }

        static Block GetBlock(string name)
        {
            foreach (Block block in Game.CustomBlocks.GetBlocks())
                if (block.Name == name)
                    return block;

            ushort id = Math.Max((ushort)(Block.GetCustomBlockOffset(Game.PaletteVersion) + Game.Levels.Count), (ushort)(Game.CustomBlocks.HightestSegmentID() + 1));
            Block b = new Block(id, name);
            Game.CustomBlocks.AddBlock(b);

            return b;
        }

        static Level AddLevel(string name)
        {
            Level level = new Level(name);
            Game.Levels.Add(level);

            return level;
        }
    }
}