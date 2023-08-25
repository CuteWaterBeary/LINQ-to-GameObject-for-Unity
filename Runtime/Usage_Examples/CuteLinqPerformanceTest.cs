using UnityEngine;
using System.Linq;
using Unity.Linq;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Profiling;

public class CuteLinqPerformanceTest : MonoBehaviour
{
    public Button linq;
    public Button native;

    public GameObject root;

    void Start()
    {
        linq.onClick.AddListener(() =>
        {

            {
                var count = 0;
                var sw = System.Diagnostics.Stopwatch.StartNew();

                Profiler.BeginSample("Performance:CuteLinq");

                var e = root.DescendantsAndSelf().GetEnumerator();
                while (e.MoveNext())
                {
                    count++;
                }

                Profiler.EndSample();
                sw.Stop();

                Debug.Log("CuteLinq:" + count + ":" + sw.Elapsed.TotalMilliseconds + "ms");
            }

            {
                var count = 0;
                var sw = System.Diagnostics.Stopwatch.StartNew();

                Profiler.BeginSample("Performance:CuteLinqForEach");

                root.DescendantsAndSelf().ForEach(_ => { });

                Profiler.EndSample();
                sw.Stop();

                Debug.Log("CuteLinq ForEach:" + count + ":" + sw.Elapsed.TotalMilliseconds + "ms");
            }

            {
                var sw = System.Diagnostics.Stopwatch.StartNew();

                Profiler.BeginSample("Performance:Native");
                var e = root.GetComponentsInChildren<Transform>(true);
                Profiler.EndSample();

                sw.Stop();

                Debug.Log("Native:" + e.Length + ":" + sw.Elapsed.TotalMilliseconds + "ms");
            }
        });

        native.onClick.AddListener(() =>
        {
        });
    }
}
