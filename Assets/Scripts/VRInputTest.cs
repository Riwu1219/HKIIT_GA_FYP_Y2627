using UnityEngine;
using UnityEngine.InputSystem;
public class VRInputTest : MonoBehaviour
{
    public InputActionReference buttonAction;

    //Enable input test
    void OnEnable() => buttonAction.action.Enable();
    //Disable input test
    void OnDisable() => buttonAction.action.Disable();

    void Update()
    {
        // detect click
        if (buttonAction.action.WasPressedThisFrame())
        {
            Debug.Log("button clicked");
        }
    }
}