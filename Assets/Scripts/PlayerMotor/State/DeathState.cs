using UnityEngine;


public class DeathState : BaseState
{
    private const string DEATH_ANIMATION = "Death";


    [SerializeField] private Vector3 knockbackForce = new Vector3(0, 6, -4);
    private Vector3 currentKnockback;

    public override void Construct()
    {
        playerMotor.Animator?.SetTrigger(DEATH_ANIMATION);
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
