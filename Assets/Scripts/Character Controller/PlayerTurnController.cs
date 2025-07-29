using System;
using UnityEngine;

public class PlayerTurnController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(playerController.GetMovementVelocity().x) > 0.1f)
        {
            float targetAngle = playerController.GetMovementVelocity().x > 0 ? 0f : 180f;
            transform.localRotation = Quaternion.Euler(0, targetAngle, 0);
        }
    }
}
