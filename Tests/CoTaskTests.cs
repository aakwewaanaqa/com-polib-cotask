using System;
using System.Collections;
using NUnit.Framework;
using Polib.CoTasks.Classes;
using Polib.CoTasks.Exts;
using Polib.CoTasks.Exts.Web;
using Polib.CoTasks.Structs;
using UnityEngine;
using UnityEngine.Networking;
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

    [UnityTest]
    public IEnumerator TestWebRequest()
    {
        yield return Run().coroutine;
        yield break;

        async CoTask Run()
        {
            using var download = new DownloadHandlerBuffer();
            await "https://www.bing.com/".Get(download);
            Debug.Log(download.text);
        }
    }

    [Test]
    public void TestPipeResult1()
    {
        PipeResult r = default;
        Assert.That(r.IsFaulty, Is.False);
    }

    [Test]
    public void TestPipeResult2()
    {
        PipeResult r = new PipeResult(new Exception());
        Assert.That(r.IsFaulty, Is.True);
    }

    [UnityTest]
    public IEnumerator TestRetryThen()
    {
        int count = 0;

        CoPipe Loop
            = async () =>
            {
                Debug.Log($"count = {count}");
                if (count++ > 5) return default;
                return new PipeResult(new Exception());
            };

        CoPipe Finally
            = async () =>
            {
                Debug.Log("Finally");
                return default;
            };

        yield return Run().coroutine;
        yield break;

        async CoTask Run()
        {
            var r = await Loop.RetryThen(Finally)();
            while (!r.IsEnd)
            {
                r = await r.Continue();
            }
        }
    }
}