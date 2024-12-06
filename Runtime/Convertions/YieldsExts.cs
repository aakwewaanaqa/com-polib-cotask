using System;
using System.Collections;
using System.Reflection;
using Polib.CoTasks.Interfaces;
using UnityEngine;

namespace Polib.CoTasks.Convertions
{
    public class WaitForSecondsAwaiter : IAwaitable
    {
        private float targetTime  { get; }
        public  bool  IsCompleted { get; private set; }

        public WaitForSecondsAwaiter(WaitForSeconds wfs)
        {
            const BindingFlags FLAGS = BindingFlags.NonPublic | BindingFlags.Instance;
            var seconds = typeof(WaitForSeconds)
                                 .GetField("m_Seconds", FLAGS)?
                                 .GetValue(wfs)
                              as float? ?? 0f;
            targetTime = Time.time + seconds;
        }

        public void OnCompleted(Action continuation)
        {
            IEnumerator Run()
            {
                yield return new WaitUntil(() => Time.time > targetTime);
                continuation();
                IsCompleted = true;
            }
        }
    }

    public static class YieldsExts
    {
        public static WaitForSecondsAwaiter Await(this WaitForSeconds wfs) => new WaitForSecondsAwaiter(wfs);
    }
}