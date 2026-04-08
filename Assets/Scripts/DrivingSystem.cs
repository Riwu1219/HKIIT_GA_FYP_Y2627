using UnityEngine;
using UnityEngine.InputSystem;

public class DrivingSystem : MonoBehaviour
{
    [Header("DrivingSystem Componenet")]
    public GameObject driverPlayer;
    public CharacterController cc;
    public Transform sit_trans;
    public GameObject[] DisableOnDriveObject;
    public GameObject enterButton;

    [Header("DrivingSystem Input")]
    public InputActionReference controllerInteractionBind;

    [Header("DrivingSystem Status")]
    [SerializeField] protected bool isDriving = false;
    
    protected void Update()
    {
        if (!isDriving) { return; }

        if (controllerInteractionBind.action.WasPressedThisFrame())
        {
            ExitDriveMode();
        }
    }

    public void EnterDriveMode()
    {
        driverPlayer.transform.SetParent(sit_trans);
        foreach (var obj in DisableOnDriveObject)
        {
            obj.SetActive(false);
            cc.enabled = false;
        }

        driverPlayer.transform.position = sit_trans.position;
        driverPlayer.transform.rotation = sit_trans.rotation;
        enterButton.SetActive(false);
        isDriving = true;
    }

    public void ExitDriveMode()
    {
        driverPlayer.transform.SetParent(null);
        foreach (var obj in DisableOnDriveObject)
        {
            obj.SetActive(true);
            cc.enabled = true;
        }

        enterButton.SetActive(true);
        // Place the player next to the rover when exiting (Need adjustment)
        driverPlayer.transform.position = transform.position + transform.right * 2f;
        isDriving = false;
    }
}
