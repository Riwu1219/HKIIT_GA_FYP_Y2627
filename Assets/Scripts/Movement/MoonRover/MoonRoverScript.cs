using Unity.XR.OpenVR;
using UnityEngine;

public class MoonRoverScript : MonoBehaviour
{
    public Rigidbody rb;
    public WheelCollider fl, fr, bl, br;
    public float driveSpeed, steerSpeed;
    private float horizontalInput, verticalInput;
    private GameObject tempPlayer;
    public GameObject driverPlayer;

    public bool isDriving = false;
    public GameObject interactBtn;

    public OpenVROculusTouchController leftController;

    private void Update()
    {
        if (!isDriving) { return; }

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

    public void EnterRover(GameObject player)
    {
        interactBtn.SetActive(false);
        this.tempPlayer = player;
        player.SetActive(false);
        driverPlayer.SetActive(true);
        isDriving = true;
    }

    public void ExitRover()
    {
        interactBtn.SetActive(true);
        tempPlayer.transform.position = transform.position + transform.right * 2f;
        tempPlayer.SetActive(true);
        driverPlayer.SetActive(false);
        isDriving = false;
    }

}
