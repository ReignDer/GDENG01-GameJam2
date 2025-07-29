using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform tr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        tr.rotation = Quaternion.identity;
    }
}
