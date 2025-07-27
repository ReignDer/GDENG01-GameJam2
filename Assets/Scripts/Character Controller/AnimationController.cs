using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;
    PlayerController playerController;
    private readonly int speedHash =  Animator.StringToHash("Run"); 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat(speedHash, playerController.GetMovementVelocity().magnitude);
    }
}
