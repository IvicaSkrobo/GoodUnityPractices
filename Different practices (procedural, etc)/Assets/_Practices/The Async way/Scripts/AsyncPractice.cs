using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AsyncPractice : MonoBehaviour
{

    //dont use async workflow for webgl games, just use coroutines 
    

    [SerializeField]
    Shape[] shapes;

    //Simple coroutine running all coroutines at once
    [EditorButton]
    public void BeginTest()
    {
        for (var i = 0; i < shapes.Length; i++)
        {
            StartCoroutine(shapes[i].RotateForSeconds(1 + 1 * i));
        }
    }

    //Simple async running all functions at once

    [EditorButton]
    public void BeginTestAsync()
    {
        for (var i = 0; i < shapes.Length; i++)
        {
            shapes[i].RotateForSecondsAsync(1 + 1 * i);
        }
    }


    //Task async running all functions in sequence, one by one 

    [EditorButton]
    public async void BeginTestAsyncTask()
    {
        for (var i = 0; i < shapes.Length; i++)
        {
            //awaits the last task to be completed before going on to the next
           await shapes[i].RotateForSecondsAsyncTask(1 + 1 * i);  
        }
    }


    //Task async running all functions at the same time, but awaiting them all to finish
    //real game example, all enemies attacking at the same time but different attacks animations and projectiles flights
    //wait for all attacks to finish to switch to players turn
    [EditorButton]
    public async void BeginTestAsyncTaskAtTheSameTime()
    {
        var tasks = new List<Task>();
        for (var i = 0; i < shapes.Length; i++)
        {
            //awaits the last task to be completed before going on to the next
            tasks.Add(shapes[i].RotateForSecondsAsyncTask(1 + 1 * i));

        }

        await Task.WhenAll(tasks); //waits for all tasks to complete

        Debug.Log("All are done");
    }

    //Task async running all functions at the same time, but awaiting them all to finish
    //except the first task goes first
    [EditorButton]
    public async void BeginTestAsyncTaskAtTheSameTimeExceptFirst()
    {
        await shapes[0].RotateForSecondsAsyncTask(1);

        var tasks = new List<Task>();

        for (var i = 1; i < shapes.Length; i++)
        {
            //awaits the last task to be completed before going on to the next
            tasks.Add(shapes[i].RotateForSecondsAsyncTask(1 + 1 * i));
        }

        await Task.WhenAll(tasks); //waits for all tasks to complete

        Debug.Log("All are done");
    }

    [EditorButton]
    public async void BeginFindRandomNumber()
    {
     //  var randomNumber =  GetRandomNumb().GetAwaiter().GetResult();
       var randomNumber =  await GetRandomNumb();



     /*   var randomTaskNumber = GetRandomNumb();
        randomTaskNumber.IsCanceled 
     */
        Debug.Log(randomNumber);
    }

    //finds a random number and returns it, it even works after you stoped playing in editor mode
    async Task<int> GetRandomNumb()
    {
        var randomNumber = Random.Range(2000, 5000); //2000 to 5000 miliseconds 
        await Task.Delay(randomNumber);
        return randomNumber;
    }




    //Sequential Coroutines
    IEnumerator SequentialCoroutines()
        {
        yield return StartCoroutine("FirstCoroutine");
        yield return StartCoroutine("SecondCoroutine");
        yield return StartCoroutine("ThirdCoroutine");

        }
}





