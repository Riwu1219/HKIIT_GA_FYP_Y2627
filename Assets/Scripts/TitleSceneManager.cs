using UnityEngine;
using UnityEngine.InputSystem;

public class TitleSceneManager : MonoBehaviour
{
    public static TitleSceneManager instance;
    private void Awake() => instance = this;

    public InputAction controllerInteractionBind;
    private void OnEnable() => controllerInteractionBind.Enable();
    private void OnDisable() => controllerInteractionBind.Disable();

    public GameObject timeline;
    public GameObject container;

    public CameraEffect cameraEffect;   

    public bool isStartBtnOnHover = false;

    public void SetStartBtnHover(bool state) => isStartBtnOnHover = state;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (!controllerInteractionBind.WasPressedThisFrame()) { return; }

        if (isStartBtnOnHover)
        {
            container.SetActive(false);
            // Load Animation
            timeline.SetActive(true);
        }
    }

}
