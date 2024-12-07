using System;
using System.Collections;
using Polib.CoTasks.Interfaces;
using UnityEngine;

namespace Polib.CoTasks.Classes
{
    public static class YieldsExts
    {
        public static IAwaiter GetAwaiter(this YieldInstruction yi)
        {
            return new YieldInstructionAwaiter(yi);
        }

        public static IAwaiter GetAwaiter(this IEnumerator ie)
        {
            return new IEnumeratorAwaiter(ie);
        }
    }

    public class YieldInstructionAwaiter : IAwaiter
    {
        private YieldInstruction yi { get; }

        public YieldInstructionAwaiter(YieldInstruction yi)
        {
            this.yi = yi;
        }

        public void OnCompleted(Action continuation)
        {
            CoTaskRunner.Sigleton.StartCoroutine(Run());
            return;

            IEnumerator Run()
            {
                yield return yi;
                continuation();
                IsCompleted = true;
            }
        }

        public bool IsCompleted { get; private set; }

        public void GetResult()
        {
        }
    }

    public class IEnumeratorAwaiter : IAwaiter
    {
        private IEnumerator ie { get; }

        public IEnumeratorAwaiter(IEnumerator ie)
        {
            this.ie = ie;
        }

        public void OnCompleted(Action continuation)
        {
            CoTaskRunner.Sigleton.StartCoroutine(Run());
            return;

            IEnumerator Run()
            {
                yield return ie;
                continuation();
                IsCompleted = true;
            }
        }

        public bool IsCompleted { get; private set; }

        public void GetResult()
        {
        }
    }
}