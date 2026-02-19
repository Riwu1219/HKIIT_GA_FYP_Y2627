using UnityEngine;

public class ControlLerpping : MonoBehaviour
{
    public Rigidbody lerpObj_rb;

    private void FixedUpdate()
    {
        lerpObj_rb.MovePosition(Vector3.Lerp(transform.position, transform.position, Time.deltaTime * 5f));
    }
}
