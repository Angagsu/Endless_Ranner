using UnityEngine;

public class Fish : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickupFish();
        }
    }

    public void PickupFish()
    {
        animator.SetTrigger("Pickup");
        GameStats.Instance.OnCollectFish();
    }
}
