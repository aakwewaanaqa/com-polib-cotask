using System;
using Polib.CoTasks.Classes;

namespace Polib.CoTasks.Interfaces
{
    public interface PipeReturn
    {
        bool               IsEnd    { get; }
        bool               IsFaulty { get; }
        Exception          Ex       { get; }
        CoTask<PipeReturn> Continue();
    }
}