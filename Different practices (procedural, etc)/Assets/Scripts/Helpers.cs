using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Helpers
{

    private static Camera _camera;

    public static Camera Camera
    {
        get
        {
            if (_camera == null) _camera = Camera.main;   // because Camera.main is expensive Unity is searching it with tag 
            return _camera;
        }
    }

    private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetWait(float time)
    {
        if (WaitDictionary.TryGetValue(time, out var wait)) return wait;

        WaitDictionary[time] = new WaitForSeconds(time);
        return WaitDictionary[time];

    }

    // example for bad ienumerator 
    /* IEnumerator Bad()
     * {
     * for(int i =0; i<100;i++){
     * 
     * yield return new WaitForSeconds(0.1f); // this is creating new garbage each 0.1f seconds
     *                          }
     * }


    // example for the dictionary helper method
    /* IEnumerator Bad()
     * {
     * for(int i =0; i<100;i++){
     * 
     * yield return new WaitForSeconds(0.1f); // this is creating new garbage each 0.1f seconds
     *                          }
     * }
    */

    /*  IEnumerator Right ()
      {
          for (int i = 0; i < 100; i++)
          {
              yield return new Helper.GetWait(0.1f); // this will create only one new waitforsecond and use that one, each time it needs to
          }
         */


    //is the pointer over ui object, so in mobile games you can stop touch from interacting with the game
    // if you are hitting ui 

    private static PointerEventData _eventDataCurrentPosition;
    private static List<RaycastResult> _results;

    public static bool IsOverUI ()
    {
        _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        _results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(_eventDataCurrentPosition, _results);
        return _results.Count > 0;

    }


    //find world point of canvas element
    //how to put a 3d object or particle on the canvas
    // so this is extremely usefull with ui, lets say you want to spawn fireworks or something on ui, so cool
    public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, Camera, out var result);
        return result; // vector3 in world coordinates
    }

    //cleanup businness, could be used in ui lists of objects or in game 
    public static void DeleteChildren(this Transform t)
    {
        foreach (Transform child in t)
            Object.Destroy(child.gameObject);
    }
}
