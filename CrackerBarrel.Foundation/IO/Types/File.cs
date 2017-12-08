namespace CrackerBarrel.Foundation.IO.Types
{
    public class File : IFile
    {
        public string Extension { get; set; }
        public long Size { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public FileType Type { get; }
        public bool Loaded { get; set; }

        public File(string path)
        {
            Load(path);
            Type = FileType.FILE;
        }

        public bool Exist(string path)
        {
            return System.IO.File.Exists(path);
        }

        public bool Load(string path)
        {
            if (!Exist(path))
                return false;

            FullPath = path;
            Name = System.IO.Path.GetFileNameWithoutExtension(FullPath);
            Extension = System.IO.Path.GetExtension(path);
            Size = new System.IO.FileInfo(path).Length;
            Loaded = true;

            return true;
        }
    }
}