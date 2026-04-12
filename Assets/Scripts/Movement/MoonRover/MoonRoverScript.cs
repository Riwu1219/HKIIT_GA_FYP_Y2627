using UnityEngine;

public class MoonRoverScript : DrivingSystem
{
    public Rigidbody rb;
    public WheelCollider fl, fr, bl, br;
    public float driveSpeed, steerSpeed;

    public GameObject interactBtn;

    protected override void Update()
    {
        base.Update();

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (!isDriving) { return; }
        
        float motor = Input.GetAxis("Vertical") * driveSpeed;
        fl.steerAngle = horizontalInput * steerSpeed;
        fr.steerAngle = horizontalInput * steerSpeed;
        bl.motorTorque = verticalInput * driveSpeed;
        br.motorTorque = verticalInput * driveSpeed;
    }

    protected override void OnDriveModeExit()
    {
        rb.linearVelocity = Vector3.zero;
        fl.steerAngle = 0;
        fr.steerAngle = 0;
        bl.motorTorque = 0;
        br.motorTorque = 0;
    }
}
