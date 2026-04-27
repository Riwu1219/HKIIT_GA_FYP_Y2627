using UnityEngine;

public class MoonRoverScript : DrivingSystem
{
    public Rigidbody rb;
    public WheelCollider fl, fr, bl, br;
    public float driveSpeed, steerSpeed;
    public AudioSource driveSFX;
    public float soundHorizontalShiftRate;
    public GameObject interactBtn;

    public float speed;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        speed = Mathf.Abs(rb.linearVelocity.magnitude) / 7f;
        driveSFX.volume = speed;
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (!isDriving) { return; }
        
        float motor = Input.GetAxis("Vertical") * driveSpeed;
        driveSFX.panStereo = horizontalInput * soundHorizontalShiftRate;
        fl.steerAngle = horizontalInput * steerSpeed;
        fr.steerAngle = horizontalInput * steerSpeed;
        bl.motorTorque = verticalInput * driveSpeed;
        br.motorTorque = verticalInput * driveSpeed;
    }

    protected override void OnDriveModeEnter()
    {

        driveSFX.Play();
        rb.isKinematic = false;
    }

    protected override void OnDriveModeExit()
    {
        driveSFX.Stop();
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;
        fl.steerAngle = 0;
        fr.steerAngle = 0;
        bl.motorTorque = 0;
        br.motorTorque = 0;
    }
}
