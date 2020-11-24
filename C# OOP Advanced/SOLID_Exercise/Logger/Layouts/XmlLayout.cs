namespace Logger.Layouts
{
    using Logger.Layouts.Contracts;
    using System.Text;

    public class XmlLayout : ILayout
    {
        public string Format
        {
            get
            {
                var builder = new StringBuilder();

                builder.AppendLine("<log>");
                builder.AppendLine("    <date>{0}</date>");
                builder.AppendLine("    <level>{1}</level>");
                builder.AppendLine("    <message>{2}</message>");
                builder.AppendLine("</log>");

                return builder.ToString().Trim();
            }
        }

    }
}
