using Bam.Logging;

namespace Bam.Test
{
    public abstract class TestReporter
    {
        readonly Queue<LogMessage> _logMessages;
        private bool? _unloading;
        private readonly List<Task> _tasks;
        public TestReporter()
        {
            EmptyQueueSleepMilliseconds = 10;
            _tasks = new List<Task>();
            _logMessages = new Queue<LogMessage>();
            _unloading = false;
            AppDomain.CurrentDomain.DomainUnload += (sender, args) =>
            {
                _unloading = true;
                Task.WaitAll(_tasks.ToArray());
            };
            
            Task processQueueTask = ProcessQueueAsync();
            _tasks.Add(processQueueTask);
        }

        private Task ProcessQueueAsync()
        {
            return Task.Run(() =>
            {
                while (_unloading != null && !_unloading.Value)
                {
                    while (_logMessages.Count > 0)
                    {
                        LogMessage msg = _logMessages.Dequeue();
                        _tasks.Add(ReportAsync(msg));
                    }
                    Thread.Sleep(EmptyQueueSleepMilliseconds);
                }
            });
        }
        
        protected abstract Task ReportAsync(LogMessage message);
        
        public int EmptyQueueSleepMilliseconds { get; set; }
        
        public virtual LogMessage AddMessage(string format, params string[] args)
        {
            LogMessage message = new LogMessage(format, args);
            _logMessages.Enqueue(message);
            return message;
        }

        public virtual LogMessage AddWarningMessage(string format, params string[] args)
        {
            WarningMessage message = new WarningMessage(format, args);
            _logMessages.Enqueue(message);
            return message;
        }

        public virtual LogMessage AddErrorMessage(string format, Exception ex, params string[] args)
        {
            ErrorMessage message = new ErrorMessage(format, ex, args);
            _logMessages.Enqueue(message);
            return message;
        }
    }
}
