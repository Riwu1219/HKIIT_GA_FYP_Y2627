using UnityEngine;
using UnityEngine.InputSystem;
public class VRInputTest : MonoBehaviour
{
    public InputActionReference buttonAction;
    public InputAction buttonAction2;

    //Enable input test
    void OnEnable() { buttonAction.action.Enable(); buttonAction2.Enable(); }
    //Disable input test
    void OnDisable() { buttonAction.action.Disable(); buttonAction2.Disable(); }

    void Update()
    {
        // detect click
        if (buttonAction.action.WasPressedThisFrame())
        {
            Debug.Log("InputActionREFBind button clicked");
        }
        if (buttonAction2.WasPressedThisFrame())
        {
            Debug.Log("InputActionBind button clicked");
        }
    }
}