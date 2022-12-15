
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerForTesting : MonoBehaviour
{
    [SerializeField] ObjectToSpawn objectPrefab;
    [SerializeField] float spawnAmount;
    [SerializeField] int defaultCapacity = 50;
    [SerializeField] int maxCapacity =100;
    [SerializeField] bool usePool;

    private ObjectPool<ObjectToSpawn> pool;

     void Awake()
    {
    }

    private void Start()
    {
        //normal unity pool
        //puts the elements into the array, takes memory better cpu
        pool = new ObjectPool<ObjectToSpawn>(() => { return Instantiate(objectPrefab); },
         gO => gO.gameObject.SetActive(true),
         gO => gO.gameObject.SetActive(false),
          gO => Destroy(gO.gameObject),


          false, //collectionCheck to save cpu to false, but if you try to return the same object to the pool that has already returned error
          defaultCapacity, // capacity, number of objects created at start
          maxCapacity // maxCapacity
         );

        //linked unity pool
        //doesnt take memory, but more effort to grab and release from the pool better memory worse cpu
        //dictionarypool, hashsetpool, listpool



        InvokeRepeating(nameof(SpawnShape), 0.2f, 0.5f);
    }

    private void Update()
    {
        
    }


    private void SpawnShape()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            var objectToSpawn = usePool ? pool.Get() : Instantiate(objectPrefab);
            objectToSpawn.transform.position = transform.position + Random.insideUnitSphere *10;
            objectToSpawn.Init(KillShape);
        }
    }

    private void KillShape(ObjectToSpawn shape)
    {

        if (usePool) {
            pool.Release(shape);
        }
        else {
            Destroy(shape.gameObject);
        }
        
    }
}
