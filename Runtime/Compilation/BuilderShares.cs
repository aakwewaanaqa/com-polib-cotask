using System;

namespace Polib.CoTasks.Compilation
{
    internal static class BuilderShares
    {
        internal static Action Bind(Action a, Action b) => () =>
        {
            a();
            b();
        };
    }
}