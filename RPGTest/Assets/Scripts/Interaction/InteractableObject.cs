using UnityEngine;
using UnityEngine.AI;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform stopPoint;
    public float StoppingDistance => 2f;
    public Vector3 StoppingPos => stopPoint !=null ? stopPoint.position : transform.position;
 
    public virtual void Interact()
    {
        Debug.Log("interacting");
    }
}

public interface IInteractable
{
    float StoppingDistance { get; }
    Vector3 StoppingPos { get; }
    void Interact();
}
