using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.XR;

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
        // Check if the player has pressed the exit button ABXY
        if ( XRControllerPressedHandler() ) { ExitRover(); }
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
        driverPlayer.SetActive(false);
        tempPlayer.SetActive(true);
        isDriving = false;
    }

    bool XRControllerPressedHandler()
    {
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevices(inputDevices);

        foreach (var device in inputDevices)
        {
            // Only consider controllers
            if ((device.characteristics & InputDeviceCharacteristics.Controller) == 0) continue;

            bool primaryPressed = false;
            bool secondaryPressed = false;

            // primaryButton is usually A / X / trigger-like "buttonSouth" mapping
            if (device.TryGetFeatureValue(CommonUsages.primaryButton, out primaryPressed) && primaryPressed)
                return true;

            // secondaryButton is usually B / Y / buttonEast mapping
            if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryPressed) && secondaryPressed)
                return true;

            // Optionally check menuButton or grip/trigger as exit triggers:
            bool menuPressed = false;
            if (device.TryGetFeatureValue(CommonUsages.menuButton, out menuPressed) && menuPressed)
                return true;
        }

        return false;
    }

}
