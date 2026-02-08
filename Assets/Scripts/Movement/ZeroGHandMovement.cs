using UnityEngine;

public class ZeroGHandMovement : MonoBehaviour
{
    public Transform hand;          // Left or Right controller
    public float moveMultiplier = 1.5f;

    private bool isGrabbing = false;
    private Vector3 lastHandPos;

    void Update()
    {
        if (isGrabbing)
        {
            Vector3 handDelta = hand.position - lastHandPos;
            transform.position -= handDelta * moveMultiplier;
            lastHandPos = hand.position;
        }
    }

    public void StartGrab()
    {
        isGrabbing = true;
        lastHandPos = hand.position;
    }

    public void EndGrab()
    {
        isGrabbing = false;
    }
}