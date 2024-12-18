using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;

    private Player player;
    private const string IS_WALKING = "IsWalking";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = transform.parent.gameObject.GetComponent<Player>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
