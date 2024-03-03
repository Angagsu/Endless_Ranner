using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    // Gameplay
    private float chunkSpawnZ;
    private Queue<Chunk> activeChunks = new Queue<Chunk>();
    private List<Chunk> chunkPool = new List<Chunk>();

    // Configurable fields
    [SerializeField] private int firstChunkSpawnPosition = -10;
    [SerializeField] private int chunkOnScreen = 5;
    [SerializeField] private float despawnDistanse = 5;

    [SerializeField] private List<GameObject> chunkPrefab;
    [SerializeField] private Transform camerTransform;



    
    private void Awake()
    {
        ResetWorld();
    }
    
    private void Start()
    {
        if (chunkPrefab.Count == 0)
        {
            Debug.LogError("No chunk Prefab found on the world generator script, please assign some prefabs!");
            return;
        }

        if (!camerTransform)
        {
            camerTransform = Camera.main.transform;
            Debug.Log("we have assigned cameraTransform automaticaly to the Camer.main");
        }
    }

    public void ScanPosition()
    {
        float cameraZ = camerTransform.position.z;
        Chunk lastChunk = activeChunks.Peek();

        if (cameraZ >= lastChunk.transform.position.z + lastChunk.ChunkLength + despawnDistanse)
        {
            SpawnNewChunk();
            DeleteLastChunk();
        }
    }

    private void SpawnNewChunk()
    {
        // Get a random index for wich prefab to spawn
        int randomIndex = Random.Range(0, chunkPrefab.Count);

        // Does it already exist within our pool
        Chunk chunk = chunkPool.Find(x => !x.gameObject.activeInHierarchy && x.name == (chunkPrefab[randomIndex].name + "(Clone)"));

        if (!chunk)
        {
            GameObject go = Instantiate(chunkPrefab[randomIndex], transform);
            chunk = go.GetComponent<Chunk>();
        }

        // Place the object and show it

        chunk.transform.position = new Vector3(0, 0, chunkSpawnZ);
        chunkSpawnZ += chunk.ChunkLength;

        activeChunks.Enqueue(chunk);
        chunk.ShowChunk();
    }

    private void DeleteLastChunk()
    {
        Chunk chunk = activeChunks.Dequeue();
        chunk.HideChunk();
        chunkPool.Add(chunk);
    }

    public void ResetWorld()
    {
        // Reset the chunkSpawnZ
        chunkSpawnZ = firstChunkSpawnPosition;

        for (int i = activeChunks.Count; i != 0 ; i--)
        {
            DeleteLastChunk();
        }

        for (int i = 0; i < chunkOnScreen; i++)
        {
            SpawnNewChunk();
        }
    }
}
