using System;
using System.Runtime.CompilerServices;
using Polib.CoTasks.Interfaces;

namespace Polib.CoTasks.Classes
{
    internal class AwaitedSource : IDisposable
    {
        private Func<bool> GetIsCompleted { get; set; }

        internal bool IsCompleted => GetIsCompleted?.Invoke() ?? true;

        internal AwaitedSource(INotifyCompletion awaiter)
        {
            if (awaiter is IAwaiter a)
            {
                GetIsCompleted = () => a.IsCompleted;
                return;
            }

            var prop = awaiter.GetType().GetProperty("IsCompleted");
            if (prop != null)
            {
                GetIsCompleted = () => (bool)prop.GetValue(awaiter);
                return;
            }
        }

        public void Dispose()
        {
            GetIsCompleted = null;
        }
    }
}