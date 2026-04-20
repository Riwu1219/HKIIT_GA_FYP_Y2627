using UnityEngine;
using UnityEngine.InputSystem;

public class HUDController : MonoBehaviour
{
    public DrivingSystem drivingSystem;
    public InputAction controllerInteractionBind;
    
    public GameObject panel;
    public GameObject guide;
    public bool interactable = false;

    private void OnEnable() => controllerInteractionBind.Enable();
    private void OnDisable() => controllerInteractionBind.Disable();

    private void Update()
    {
        if (!interactable) { return; }
        if (controllerInteractionBind.IsPressed())
        {
            panel.SetActive(true);
            guide.SetActive(false);
        }
        else
        {
            panel.SetActive(false);
        }
    }

    public void OnIntroAnimationEnd()
    {
        drivingSystem.SetVeicleCanEnter(true);
        guide.SetActive(true);
        interactable = true;
    }

}
