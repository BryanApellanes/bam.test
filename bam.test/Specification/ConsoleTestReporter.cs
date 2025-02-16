using Bam.Logging;
using Bam.Console;

namespace Bam.Test.Specification
{
    public class ConsoleTestReporter : TestReporter
    {
        protected override Task ReportAsync(LogMessage message)
        {
            return Task.Run(() =>
            {
                string logMessageType = message.GetType().Name;
                switch (logMessageType)
                {
                    case nameof(WarningMessage):
                        Message.PrintLine(message.ToString(), ConsoleColor.Yellow);
                        break;
                    case nameof(ErrorMessage):
                        Message.PrintLine(message.ToString(), ConsoleColor.Magenta);
                        break;
                    default:
                        Message.PrintLine(message.ToString());
                        break;
                }
            });
        }
    }
}
