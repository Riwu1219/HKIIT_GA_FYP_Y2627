using UnityEngine;

public class MoonBase_Door : MonoBehaviour
{
    public Animator animator;
    public bool opened = false;
    public bool canClose = true;

    public void SetOpened()
    {
        opened = true; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (opened) return;
        if (other.CompareTag("Player"))
        {
            animator.Play("DoorOpen");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseDoor();
        }
    }

    public void CloseDoor()
    {
        if (!canClose) return;
        if (!opened) { Invoke("CloseDoor", 2f); }
        else
        {
            Debug.Log("Closing door");
            animator.Play("DoorClose");
            canClose = false;
        }
    }
}
