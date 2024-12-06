using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Polib.CoTasks.Classes;
using Polib.CoTasks.Compilation;
using UnityEngine;

namespace Polib.CoTasks.Structs
{
    [AsyncMethodBuilder(typeof(CoTaskBuilder))]
    public struct CoTask
    {
        internal CoTaskSource src { get; set; }

        public Awaiter GetAwaiter()
        {
            return new Awaiter(ref this);
        }

        internal void MarkComplete()
        {
            src.Dispose();
            src = null;
        }

        public class Awaiter : INotifyCompletion
        {
            private CoTask task { get; }

            internal Awaiter(ref CoTask task)
            {
                this.task = task;
            }

            public bool IsCompleted => task.src.IsCompleted;

            public void OnCompleted(Action continuation)
            {
                CoTaskRunner.Sigleton.StartCoroutine(Run());
                return;

                IEnumerator Run()
                {
                    yield return new WaitUntil(() => IsCompleted);
                    continuation();
                }
            }

            public void GetResult()
            {
            }
        }
    }

}