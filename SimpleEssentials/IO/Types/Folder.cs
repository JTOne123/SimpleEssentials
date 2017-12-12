namespace SimpleEssentials.IO.Types
{
    public class Folder : IFolder
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public FileType Type { get; }
        public bool Loaded { get; set; }

        public Folder()
        {

        }

        public Folder(string path)
        {
            Load(path);
            Type = FileType.FOLDER;
        }

        public bool Exist(string path)
        {
            return System.IO.Directory.Exists(path);
        }

        public bool Load(string path)
        {
            if (!Exist(path))
                return false;

            FullPath = path;
            Name = System.IO.Path.GetDirectoryName(path);
            Loaded = true;

            return true;
        }
    }
}