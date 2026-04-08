using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoonRoverScript : MonoBehaviour
{
    public Rigidbody rb;
    public WheelCollider fl, fr, bl, br;
    public float driveSpeed, steerSpeed;
    private float horizontalInput, verticalInput;
    public GameObject driverPlayer;

    public bool isDriving = false;
    public GameObject interactBtn;
    public InputActionReference controllerInteractionBind;

    public GameObject[] DisableOnDriveObject;
    public CharacterController cc;
    public Transform sit_trans;

    private void Update()
    {
        if (!isDriving) { return; }

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (controllerInteractionBind.action.WasPressedThisFrame())
        {
            ExitRover();
        }
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

    public void EnterRover()
    {
        driverPlayer.transform.SetParent(sit_trans);
        foreach (var obj in DisableOnDriveObject)
        {
            obj.SetActive(false);
            cc.enabled = false;
        }

        driverPlayer.transform.position = sit_trans.position;
        driverPlayer.transform.rotation = sit_trans.rotation;
        interactBtn.SetActive(false);
        isDriving = true;
    }

    public void ExitRover()
    {
        driverPlayer.transform.SetParent(null);
        foreach (var obj in DisableOnDriveObject)
        {
            obj.SetActive(true);
            cc.enabled = true;
        }

        interactBtn.SetActive(true);
        // Place the player next to the rover when exiting (Need adjustment)
        driverPlayer.transform.position = transform.position + transform.right * 2f;
        isDriving = false;
    }
}
