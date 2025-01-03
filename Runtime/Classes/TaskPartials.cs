using System;
using System.Collections;
using UnityEngine;

namespace Polib.CoTasks.Classes
{
    public partial class CoTask
    {
        public WaitUntil coroutine => new(IsCompleted);
        public CoTask    Forget()  => this;

        public CoTask OnDone(Action f)
        {
            CoTaskRunner.Sigleton.StartCoroutine(Run());
            return this;

            IEnumerator Run()
            {
                yield return coroutine;
                f();
            }
        }
    }

    public partial class CoTask<T>
    {
        public WaitUntil Coroutine => new(IsCompleted);

        public CoTask<T> OnDone(Action<T> f)
        {
            CoTaskRunner.Sigleton.StartCoroutine(Run());
            return this;

            IEnumerator Run()
            {
                yield return Coroutine;
                f(result);
            }
        }
    }
}