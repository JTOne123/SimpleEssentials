namespace SimpleEssentials.ToQuery
{
    public interface ICustomCommand
    {
        void Concat(string command);
        string GetCommand();
    }
}
