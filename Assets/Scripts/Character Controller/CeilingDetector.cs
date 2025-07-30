using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingDetector : MonoBehaviour
{
    public float ceilingAngleLimit = 10f;
    public bool isInDebugMode;
    private float debugDrawnDuration = 2.0f;
    private bool ceilingWasHit;

    void OnCollisionEnter(Collision collision) => CheckForContact(collision);
    void OnCollisionStay(Collision collision) => CheckForContact(collision);

    private void CheckForContact(Collision collision)
    {
        if (collision.contacts.Length == 0) return;
        
        float angle = Vector3.Angle(-transform.up, collision.contacts[0].normal);

        if (angle < ceilingAngleLimit)
        {
            ceilingWasHit = true;
        }

        if (isInDebugMode)
        {
            Debug.DrawRay(collision.contacts[0].point, collision.contacts[0].normal, Color.red, debugDrawnDuration);
        }
    }
    
    public bool HitCeiling() => ceilingWasHit;
    public void Reset() => ceilingWasHit = false;
}