using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projection : MonoBehaviour
{
    [SerializeField]
    Transform obstacles;
    [SerializeField]
    LineRenderer line;
    [SerializeField]
    int maxPhysicsFrameIteration = 100;


    Scene simulationScene;
    PhysicsScene physicsScene;


    private Dictionary<Transform, Transform> spawnedObjects = new Dictionary<Transform, Transform>();
    // I'd have a threshold variable that's representing how much of a difference
    // of position and rotation requires a change.
    // Then I'd check if the position of the ghost object is approximatively
    // the same as the position of the real object.
    // If not, then change it's position.
    // Like that, you would avoid having useless calls in the update method,
    // which is a method you should really be careful with.


    private void Start()
    {
        CreatePhysicsScene();
    }
    void CreatePhysicsScene()
    {
        simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        physicsScene = simulationScene.GetPhysicsScene();
        foreach (Transform obstacle in obstacles)
        {
           var ghostObj= Instantiate(obstacle.gameObject, obstacle.position, obstacle.rotation);
            ghostObj.GetComponent<Renderer>().enabled = false;

            SceneManager.MoveGameObjectToScene(ghostObj,simulationScene);

            if (!ghostObj.isStatic) spawnedObjects.Add(obstacle.transform, ghostObj.transform);


        }

    }

    private void Update()
    {
        foreach(var item in spawnedObjects)
        {
            item.Value.position = item.Key.position;
            item.Value.rotation = item.Key.rotation;
        }
    }


    public void SimulateTrajectory(Ball ballPrefab, Vector3 pos, Vector3 velocity)
    {
        var ghostObj = Instantiate(ballPrefab, pos, Quaternion.identity);

        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, simulationScene);

        ghostObj.Init(velocity,true);


        line.positionCount = maxPhysicsFrameIteration;

        //simulate the frames in the future
        for(int i=0;i<maxPhysicsFrameIteration; i++)
        {
            physicsScene.Simulate(Time.fixedDeltaTime);
            line.SetPosition(i, ghostObj.transform.position);
        }

        Destroy(ghostObj.gameObject);
    }



}
