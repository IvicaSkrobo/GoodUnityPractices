using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Shape : MonoBehaviour
{
    
    public IEnumerator RotateForSeconds(float duration)
    {
        var end = Time.time + duration;
        while(Time.time <end)
        {
            transform.Rotate(new Vector3(1, 1) * Time.deltaTime * 150);
            yield return null;
        }
    }

    public async void RotateForSecondsAsync(float duration)
    {
        var end = Time.time + duration;
        while (Time.time < end)
        {
            transform.Rotate(new Vector3(1, 1) * Time.deltaTime * 150);
            await Task.Yield(); //coroutines null hits the frame, yield misses a bit
        }
    }

    public async Task RotateForSecondsAsyncTask(float duration)
    {
        var end = Time.time + duration;
        while (Time.time < end)
        {
            transform.Rotate(new Vector3(1, 1) * Time.deltaTime * 150);
            await Task.Yield(); //coroutines null hits the frame, yield misses a bit
        }
    }

}
