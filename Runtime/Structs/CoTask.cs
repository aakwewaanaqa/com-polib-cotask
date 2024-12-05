using System;
using System.Runtime.CompilerServices;
using Polib.CoTasks.Compilation;

namespace Polib.CoTasks.Structs
{
    [AsyncMethodBuilder(typeof(CoTaskBuilder))]
    public struct CoTask
    {
        public Awaiter GetAwaiter()
        {
            return new Awaiter();
        }

        public class Awaiter : INotifyCompletion
        {
            public bool IsCompleted { get; private set; }

            public void OnCompleted(Action continuation)
            {
                continuation();
            }

            public void GetResult()
            {
            }
        }
    }

}