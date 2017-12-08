namespace CrackerBarrel.Foundation.IO.Types
{
    public interface IFile : IFileType
    {
        string Extension { get; set; }
        long Size { get; set; }
    }
}