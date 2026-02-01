using UnityEngine;

public class MoonRoverScript : MonoBehaviour
{
    public Rigidbody rb;
    public WheelCollider fl, fr, bl, br;
    public float driveSpeed, steerSpeed;
    private float horizontalInput, verticalInput;

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        float motor = Input.GetAxis("Vertical") * driveSpeed;
        fl.steerAngle = horizontalInput * steerSpeed;
        fr.steerAngle = horizontalInput * steerSpeed;
        bl.motorTorque = verticalInput * driveSpeed;
        br.motorTorque = verticalInput * driveSpeed;
    }
}
