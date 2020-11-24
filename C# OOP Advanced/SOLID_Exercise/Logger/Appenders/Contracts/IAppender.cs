namespace Logger.Appenders.Contracts
{
    using Logger.Enumeration;

    public interface IAppender
    {
        void Append(string datetime, ReportLevel reportLevel, string errorMessage);

        ReportLevel ReportLevel { get; set; }

        int AppendedMessages { get; }

        string ToString();
    }
}
