using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Polib.CoTasks.Classes;
using UnityEngine;

namespace Polib.CoTasks.Structs
{
    public struct Frames
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
                CoTaskRunner.Sigleton.StartCoroutine(Run());
                return;

                IEnumerator Run()
                {
                    yield return new WaitForSeconds(10f);
                    continuation();
                }
            }

            public void GetResult()
            {
            }
        }
    }
}