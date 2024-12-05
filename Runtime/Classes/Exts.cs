using System.Runtime.CompilerServices;
using Polib.CoTasks.Structs;
using UnityEngine;

namespace Polib.CoTasks.Classes
{
    public static class Exts
    {
        public static INotifyCompletion GetAwaiter(this WaitForEndOfFrame task)
        {
            return new CoTask().GetAwaiter();
        }
    }
}