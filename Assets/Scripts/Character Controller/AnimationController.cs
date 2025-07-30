using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;
    PlayerController playerController;
    private readonly int speedHash =  Animator.StringToHash("Run"); 
    private readonly int jumpHash =  Animator.StringToHash("Jump"); 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        playerController.OnJump += HandleJump;
        playerController.OnLand += HandleLand;
    }

    private void HandleLand(Vector3 momentum) => animator.SetBool(jumpHash, false);

    private void HandleJump(Vector3 momentum) => animator.SetBool(jumpHash, true);

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat(speedHash, playerController.GetMovementVelocity().magnitude);
    }
}
