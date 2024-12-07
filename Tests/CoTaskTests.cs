using System.Collections;
using NUnit.Framework;
using Polib.CoTasks.Classes;
using UnityEngine;
using UnityEngine.TestTools;

public class CoTaskTests
{
    [UnityTest]
    public IEnumerator TestWaitForSeconds()
    {
        yield return Run().coroutine;
        Assert.That(Time.time, Is.GreaterThanOrEqualTo(2f));
        yield break;

        async CoTask Run()
        {
            const float SECONDS = 2f;
            await new WaitForSeconds(SECONDS);
            Debug.Log($"AFTER {SECONDS} SECONDS");
        }
    }

    [UnityTest]
    public IEnumerator TestWaitUntil()
    {
        const float SECONDS = 0.5f;
        int         c       = 0;
        yield return Run().coroutine;
        yield break;

        async CoTask Add()
        {
            for (; c <= 5; c++) await new WaitForSeconds(SECONDS);
        }

        async CoTask Run()
        {
            Add().Forget();
            await new WaitUntil(() =>
            {
                Debug.Log($"c = {c}");
                return c > 5;
            });
        }
    }
}