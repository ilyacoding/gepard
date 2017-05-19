namespace Gepard.Core.Main
{
    public interface ILogHandler
    {
        string Path { get; set; }
        string Ip { get; set; }
        string UserAgent { get; set; }

        string GetTime();
        void WriteInfo(string message);
    }
}
