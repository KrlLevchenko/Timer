using System;
using System.Threading;

namespace TimerClient
{
    public class ProgramExecutor
    {
        private readonly CancellationTokenSource _cancellationTokenSource;

        public ProgramExecutor(CancellationTokenSource cancellationTokenSource)
        {
            _cancellationTokenSource = cancellationTokenSource;
        }

        public void SuccessfullyExit() => _cancellationTokenSource.Cancel();
        public void FaultExit() => Environment.Exit(1);
    }
}