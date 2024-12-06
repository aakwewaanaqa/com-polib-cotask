using System.Collections;
using NUnit.Framework;
using Polib.CoTasks.Classes;
using Polib.CoTasks.Exts;
using Polib.CoTasks.Structs;
using UnityEngine;
using UnityEngine.TestTools;

public class CoTaskTests
{
    [UnityTest]
    public IEnumerator Test()
    {
        yield return Run()
           .OnDone(() => Debug.Log("Done"))
           .Coroutine;
        Assert.That(Time.time, Is.GreaterThanOrEqualTo(2f));
        yield break;

        async CoTask Run()
        {
            await new WaitForSeconds(2f);
        }
    }
}