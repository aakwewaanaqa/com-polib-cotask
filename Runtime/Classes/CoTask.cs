using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Polib.CoTasks.Compilation;
using Polib.CoTasks.Interfaces;
using Polib.CoTasks.Structs;
using UnityEngine;

namespace Polib.CoTasks.Classes
{
    [AsyncMethodBuilder(typeof(CoTaskBuilder))]
    public partial class CoTask
    {
        internal AwaitedSource awaited { get; set; }

        public bool    IsCompleted() => awaited is null || awaited.IsCompleted;
        public Awaiter GetAwaiter()  => new(this);

        public void MarkComplete()
        {
            awaited.Dispose();
            awaited = null;
        }

        public class Awaiter : IAwaiter
        {
            private CoTask task { get; }

            public Awaiter(CoTask task) => this.task = task;
            public bool IsCompleted => task.IsCompleted();

            public void OnCompleted(Action continuation)
            {
                CoTaskRunner.Sigleton.StartCoroutine(Run());
                return;

                IEnumerator Run()
                {
                    yield return new WaitUntil(task.IsCompleted);
                    continuation();
                }
            }

            public void GetResult()
            {
            }

            ~Awaiter()
            {
            }
        }
    }
}