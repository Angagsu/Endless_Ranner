using UnityEngine;


public class Chunk : MonoBehaviour
{
    [field: SerializeField] public float ChunkLength { get; private set; }

    public Chunk ShowChunk()
    {
        gameObject.SetActive(true);
        return this;
    }

    public Chunk HideChunk()
    {
        gameObject.SetActive(false);
        return this;
    }
}
