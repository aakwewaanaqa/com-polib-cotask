using System;
using System.Collections;
using System.Reflection;
using Polib.CoTasks.Classes;
using Polib.CoTasks.Interfaces;
using UnityEngine;

namespace Polib.CoTasks.Classes
{
    public static class YieldsExts
    {
        public static IAwaiter GetAwaiter(this YieldInstruction yi)
        {
            return new YiAwaiter(yi);
        }

        public static IAwaiter GetAwaiter(this IEnumerator ie)
        {
            return new IeAwaiter(ie);
        }
    }

    public class YiAwaiter : IAwaiter
    {
        private YieldInstruction yi { get; }

        public YiAwaiter(YieldInstruction yi)
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

    public class IeAwaiter : IAwaiter
    {
        private IEnumerator ie { get; }

        public IeAwaiter(IEnumerator ie)
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