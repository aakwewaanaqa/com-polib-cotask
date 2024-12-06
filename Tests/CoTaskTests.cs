using System.Collections;
using Polib.CoTasks.Convertions;
using Polib.CoTasks.Structs;
using UnityEngine;
using UnityEngine.TestTools;

public class CoTaskTests
{
    [UnityTest]
    public IEnumerator Test()
    {

        yield break;

        async CoTask Run()
        {
            await new WaitForSeconds(2f).Await();
        }
    }
}