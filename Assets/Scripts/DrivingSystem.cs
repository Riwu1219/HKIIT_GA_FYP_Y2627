using UnityEngine;
using UnityEngine.InputSystem;

public class DrivingSystem : MonoBehaviour
{
    [Header("DrivingSystem Componenet")]
    public GameObject driverPlayer;
    public CharacterController cc;
    public Transform sit_trans;
    public Vector3 exitOffset = new Vector3(2f, 0f, 0f);
    public GameObject[] DisableOnDriveObject;
    public GameObject interactionObject;
    public MeshRenderer mr;
    
    [Header("DrivingSystem Input")]
    public InputAction controllerInteractionBind;
    [SerializeField] protected float horizontalInput, verticalInput;

    [Header("DrivingSystem Status")]
    public bool canEnter = true;
    public bool canExit = true;
    private bool interactionBtnOnHover = false;
    [SerializeField] protected bool isDriving = false;

    private void OnEnable() => controllerInteractionBind.Enable();
    private void OnDisable() => controllerInteractionBind.Disable();

    virtual protected void Start()
    {
        if (!canEnter)
        {
            SetInteractionBtnOnHover(false);
        }
    }

    virtual protected void Update()
    {
        if (!isDriving) 
        { 
            if (interactionBtnOnHover && controllerInteractionBind.WasPressedThisFrame())
            {
                Debug.Log("DrivingSystem: Enter Drive Mode");
                EnterDriveMode();
            }
            return; 
        }

        if (controllerInteractionBind.WasPressedThisFrame())
        {
            ExitDriveMode();
        }
    }

    public void SetInteractionBtnOnHover(bool state)
    {
        interactionBtnOnHover = state;
        mr.materials[1].SetFloat("_Enabled", state ? 1 : 0);
    }

    public void EnterDriveMode()
    {
        driverPlayer.transform.SetParent(sit_trans);
        foreach (var obj in DisableOnDriveObject)
        {
            obj.SetActive(false);
        }
        cc.enabled = false;
        driverPlayer.transform.position = sit_trans.position;
        driverPlayer.transform.rotation = sit_trans.rotation;
        interactionObject.SetActive(false);
        OnDriveModeEnter();
        isDriving = true;
    }

    public void SetVeicleCanEnter(bool state)
    {
        canEnter = state;
        interactionObject.SetActive(state);
    }

    virtual protected void OnDriveModeEnter()
    {
        
    }

    public void ExitDriveMode()
    {
        if (!canExit) { return; }
        driverPlayer.transform.SetParent(null);
        foreach (var obj in DisableOnDriveObject)
        {
            obj.SetActive(true);
            cc.enabled = true;
        }

        interactionObject.SetActive(true);
        // Place the player next to the rover when exiting (Need adjustment)
        driverPlayer.transform.position = sit_trans.position + exitOffset;
        driverPlayer.transform.rotation = Quaternion.Euler(0f, sit_trans.transform.rotation.eulerAngles.y, 0f);
        OnDriveModeExit();
        isDriving = false;
    }

    public void SetIsDriving(bool state)
    {
        isDriving  = state;
    }

    virtual protected void OnDriveModeExit()
    {

    }
}
