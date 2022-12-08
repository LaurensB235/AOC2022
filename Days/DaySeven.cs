using FluentAssertions;
using NUnit.Framework;

namespace AOC2022.Days
{
    public class DaySeven
    {
        private struct File
        {
            public int Size;
            public string Name;
        }

        private class Directory
        {
            public string Name = null!;
            public List<Directory> Directories = new();
            public List<File> Files = new();
            public Directory? Dir;
            public int Size;

            public void Add(File file)
            {
                Files.Add(file);
                AddSize(file.Size);
            }

            private void AddSize(int size)
            {
                Size += size;
                Dir?.AddSize(size);
            }

            public IEnumerable<Directory> GetDirectories()
            {
                foreach (var directory in Directories)
                {
                    yield return directory;
                    foreach (var subDirectory in directory.GetDirectories())
                    {
                        yield return subDirectory;
                    }
                }
            }
        }


        private Directory Root = new();

        [OneTimeSetUp]
        public void Setup()
        {

            var currentDir = Root;

            foreach (var line in System.IO.File.ReadAllLines("Input/DaySeven.txt"))
            {
                var split = line.Split(' ');


                switch (split[0])
                {
                    case "$":

                        if (split[1] == "cd")
                        {
                            currentDir = split[2] switch
                            {
                                "/" => Root,
                                ".." => currentDir?.Dir,
                                _ => currentDir?.Directories?.Find(dir => dir.Name == split[2])
                            };
                        }
                        continue;
                    case "dir":
                        currentDir?.Directories?.Add(new Directory
                        {
                            Name = split[1],
                            Dir = currentDir,
                        });
                        continue;
                    default:
                        currentDir?.Add(new File
                        {
                            Size = int.Parse(split[0]),
                            Name = split[1],
                        });
                        break;
                }
            }
        }

        [Test]
        public void Test1()
        {
            Root.GetDirectories().Where(dir => dir.Size <= 100000).Select(dir => dir.Size).Sum().Should().Be(1642503);
        }

        [Test]
        public void Test3()
        {
            Root.GetDirectories().Where(dir => dir.Size > Root.Size - 40000000).Select(dir => dir.Size).Min().Should().Be(6999588);
        }
    }
}
