using System;
using Mono.Cecil.Cil;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class LevelNotifyComplete : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject UI;

    private void Start()
    {
        UI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UI.SetActive(false);
        }
    }
    public void Interact()
    {
        var rotateObject = door.GetComponent<RotateObject>();
        rotateObject.Rotate();
    }
}
