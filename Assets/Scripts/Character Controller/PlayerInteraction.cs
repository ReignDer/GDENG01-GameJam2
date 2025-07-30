using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] InputReader inputReader;
    bool interactKeyIsPressed = false;
    void Start()
    {
        inputReader.Interact += Interact;
    }

    private void Interact(bool isKeyPressed)
    {
        if (isKeyPressed)
        {
            interactKeyIsPressed = isKeyPressed;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (interactKeyIsPressed)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }

        interactKeyIsPressed = false;
    }
    void OnTriggerStay(Collider other)
    {
        if (interactKeyIsPressed)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }

        interactKeyIsPressed = false;
    }
}
