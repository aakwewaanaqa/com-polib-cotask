using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Polib.CoTasks.Classes;
using Polib.CoTasks.Compilation;
using UnityEngine;

namespace Polib.CoTasks.Structs
{
    [AsyncMethodBuilder(typeof(CoTaskBuilder<>))]
    public class CoTask<T>
    {
        internal AwaitedSource awaited { get; set; }
        internal T             result  { get; set; }

        public Awaiter GetAwaiter()
        {
            return new Awaiter(this);
        }

        internal void MarkComplete()
        {
            awaited.Dispose();
            awaited = null;
        }

        ~CoTask()
        {
        }

        public class Awaiter : INotifyCompletion
        {
            private CoTask<T> task { get; }

            internal Awaiter(CoTask<T> task)
            {
                this.task = task;
            }

            public bool IsCompleted => task.awaited is null || task.awaited.IsCompleted;

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

            public T GetResult()
            {
                return task.result;
            }

            ~Awaiter()
            {
            }
        }
    }
}