namespace SimpleEssentials.Log.Writters
{
    public interface IWriter
    {
        bool Write(string message);
    }
}
