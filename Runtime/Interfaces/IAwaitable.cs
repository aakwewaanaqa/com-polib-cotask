using System;
using System.Runtime.CompilerServices;

namespace Polib.CoTasks.Interfaces
{
    public interface IAwaitable : INotifyCompletion
    {
        bool IsCompleted { get; }
    }
}