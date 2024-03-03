using UnityEngine;

public class DeathState : BaseState
{
    [SerializeField] private Vector3 knockbackForce = new Vector3(0, 4, -3);
    private Vector3 currentKnockback;

    public override void Construct()
    {
        playerMotor.animator?.SetTrigger("Death");
        currentKnockback = knockbackForce;
    }
    public override Vector3 ProcessMotion()
    {
        Vector3 moveVector = currentKnockback;

        currentKnockback = new Vector3(
            0,
            currentKnockback.y -= playerMotor.Gravity * Time.deltaTime,
            currentKnockback.z += 2f * Time.deltaTime
            );

        if (currentKnockback.z > 0)
        {
            currentKnockback.z = 0;

            GameManager.Instance.ChangeState(GameManager.Instance.GetComponent<GameStateDeath>());
        }
        

        return currentKnockback;
    }
}
