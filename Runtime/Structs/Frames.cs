using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Polib.CoTasks.Classes;
using UnityEngine;

namespace Polib.CoTasks.Structs
{
    public readonly struct Frames
    {
        private Awaiter awaiter { get; }

        public Frames(int count)
        {
            awaiter = new Awaiter(count);
        }

        public Awaiter GetAwaiter() => awaiter;

        public class Awaiter : INotifyCompletion
        {
            private int count { get; }

            public Awaiter(int count)
            {
                this.count = count;
            }

            public bool IsCompleted { get; private set; }

            public void OnCompleted(Action continuation)
            {
                CoTaskRunner.Sigleton.StartCoroutine(Run());
                return;

                IEnumerator Run()
                {
                    for (int i = 0; i < count; i++) yield return new WaitForEndOfFrame();
                    continuation();
                    IsCompleted = true;
                }
            }

            public void GetResult()
            {
            }
        }
    }
}