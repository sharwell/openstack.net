namespace net.openstack.Core.Compat
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public static class CancellationTokenSourceExtensions
    {
        private static readonly HashSet<Timer> _timers = new HashSet<Timer>();

        public static void CancelAfter(this CancellationTokenSource cts, TimeSpan delay)
        {
            if (cts == null)
                throw new ArgumentNullException("cts");

            TimerState state = new TimerState(cts);
            Timer timer = new Timer(TimeElapsed, state, delay, TimeSpan.FromMilliseconds(-1));
            state.Timer = timer;
            lock (_timers)
            {
                _timers.Add(timer);
            }
        }

        private static void TimeElapsed(object state)
        {
            TimerState timerState = (TimerState)state;
            lock (_timers)
            {
                _timers.Remove(timerState.Timer);
            }

            timerState.Timer.Dispose();
            try
            {
                timerState.CancellationTokenSource.Cancel();
            }
            catch (ObjectDisposedException)
            {
            }
        }

        private sealed class TimerState
        {
            public TimerState(CancellationTokenSource cancellationTokenSource)
            {
                if (cancellationTokenSource == null)
                    throw new ArgumentNullException("cancellationTokenSource");

                CancellationTokenSource = cancellationTokenSource;
            }

            public CancellationTokenSource CancellationTokenSource
            {
                get;
                private set;
            }

            public Timer Timer
            {
                get;
                set;
            }
        }
    }
}
