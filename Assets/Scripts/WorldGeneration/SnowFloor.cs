using UnityEngine;

public class SnowFloor : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Material material;
    [SerializeField] private float offsetSpeed;

    private void Update()
    {
        transform.position = (Vector3.forward * player.position.z);
        material.SetVector("_offset", new Vector4(0, -transform.position.z * offsetSpeed, 0, 0));
    }
}
