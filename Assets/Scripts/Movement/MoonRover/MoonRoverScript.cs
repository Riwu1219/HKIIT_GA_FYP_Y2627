using Unity.XR.OpenVR;
using UnityEngine;

public class MoonRoverScript : MonoBehaviour
{
    public Rigidbody rb;
    public WheelCollider fl, fr, bl, br;
    public float driveSpeed, steerSpeed;
    private float horizontalInput, verticalInput;
    public GameObject driverPlayer;

    public bool isDriving = false;
    public GameObject interactBtn;

    public OpenVROculusTouchController leftController;
    public GameObject[] ControlObject; 

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
        driverPlayer.transform.SetParent(transform);
        foreach (var obj in ControlObject)
        {
            obj.SetActive(false);
        }

        interactBtn.SetActive(false);
        isDriving = true;
    }

    public void ExitRover()
    {
        driverPlayer.transform.SetParent(null);
        foreach (var obj in ControlObject)
        {
            obj.SetActive(true);
        }

        interactBtn.SetActive(true);
        driverPlayer.transform.position = transform.position + transform.right * 2f;
        isDriving = false;
    }

}
