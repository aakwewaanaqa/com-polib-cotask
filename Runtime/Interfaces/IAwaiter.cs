using System.Runtime.CompilerServices;

namespace Polib.CoTasks.Interfaces
{
    public interface IAwaiter : INotifyCompletion
    {
        bool IsCompleted { get; }
        void GetResult();
    }

    public interface IAwaiter<out T> : INotifyCompletion
    {
        bool IsCompleted { get; }
        T GetResult();
    }
}