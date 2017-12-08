namespace CrackerBarrel.Foundation.IO.Types
{
    public enum FileType
    {
        FILE = 0,
        FOLDER
    }
    public interface IFileType
    {
        string Name { get; set; }
        string FullPath { get; set; }
        FileType Type { get; }
        bool Load(string path);
        bool Exist(string path);
        bool Loaded { get; set; }
    }
}