using System.Collections;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private Quaternion rotationTarget;
    float rotationSpeed = 100f;
    Transform tr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tr = transform;
        rotationTarget = tr.rotation *  Quaternion.Euler(0, -100, 0);
    }

    // Update is called once per frame
    public void Rotate()
    {
        StartCoroutine(RotateDoor());
    }

    private IEnumerator RotateDoor()
    {
        while (Quaternion.Angle(transform.rotation, rotationTarget) > 0.01f)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, 
                rotationTarget, 
                rotationSpeed * Time.deltaTime
            );
            yield return null;
        }
        
        transform.rotation = rotationTarget;
    }
}
