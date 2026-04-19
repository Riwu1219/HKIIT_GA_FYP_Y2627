using UnityEngine;
using UnityEngine.InputSystem;

public class HUDController : MonoBehaviour
{
    public GameObject hudPanel;
    public bool interactable = false;
    public InputAction controllerInteractionBind;

    private void OnEnable() => controllerInteractionBind.Enable();
    private void OnDisable() => controllerInteractionBind.Disable();

    private void Update()
    {
        if (!interactable) { return; }
        if (controllerInteractionBind.IsPressed())
        {
            hudPanel.SetActive(true);
        }
        else
        {
            hudPanel.SetActive(false);
        }
    }

}
