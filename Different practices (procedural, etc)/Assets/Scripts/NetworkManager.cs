using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public enum RequestType
{
    GET = 0,
    POST = 1,
    PUT=2

}

public class NetworkManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(MakeRequests());
    }


    private IEnumerator MakeRequests()
    {
        //GET
        var getRequest = CreateRequest("https://jsonplaceholder.typicode.com/todos/1");  // access your url
        //if you need to attach header
       // AttachHeader(getRequest, "Authorization", "token");
        yield return getRequest.SendWebRequest();

        var deserializedGetData = JsonUtility.FromJson<Todo>(getRequest.downloadHandler.text);

        //POST
        var dataToPost = new PostData() { Hero = "John Wick", PowerLevel = 9001 };
        var postRequest = CreateRequest("http://regbin.com/echo/post/json", RequestType.POST, dataToPost);

        yield return postRequest.SendWebRequest();
        var deserializedPostData = JsonUtility.FromJson<PostResult>(postRequest.downloadHandler.text);
     

    
    }



    private UnityWebRequest CreateRequest(string path, RequestType type = RequestType.GET, object data = null)
    {
        var request = new UnityWebRequest(path, type.ToString());

        if(data!=null)
        {
            var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        }

        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-type", "application/json");

        return request;
    }

    private void AttachHeader(UnityWebRequest request,string key, string value)
    {
        request.SetRequestHeader(key, value);
    }

}


internal class PostResult
{
    public string success { get; set; }
}

internal class Todo
{
    //you cannot have getter and setter, and you need to match the typecase to the server
    //with jsonutility
    public int userId;
    public int id;
    public string title;
    public bool completed;
}

[Serializable]
internal class PostData
{


    public string Hero;
    public int PowerLevel;
}