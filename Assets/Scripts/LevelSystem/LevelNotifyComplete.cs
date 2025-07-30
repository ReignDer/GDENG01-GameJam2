using Mono.Cecil.Cil;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class LevelNotifyComplete : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject door;

    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         var rotateObject = door.GetComponent<RotateObject>();
    //         rotateObject.Rotate();
    //     }
    // }

    public void Interact()
    {
        var rotateObject = door.GetComponent<RotateObject>();
        rotateObject.Rotate();
    }
}
