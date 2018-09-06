namespace SimpleEssentials.LinqToDb
{
    public interface ICustomCommand
    {
        void Concat(string command);
        string GetCommand();
    }
}
