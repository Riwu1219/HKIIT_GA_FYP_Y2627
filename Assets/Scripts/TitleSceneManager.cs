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
    public CameraEffect cameraEffect_cam3;

    public bool isStartBtnOnHover = false;

    public void SetStartBtnHover(bool state) => isStartBtnOnHover = state;

    private void Start()
    {
        cameraEffect.CameraFadeTran();
    }

    private void Update()
    {
        if (!controllerInteractionBind.WasPressedThisFrame()) { return; }

        if (isStartBtnOnHover)
        {
            // Load Animation
            cameraEffect.CameraFadeBlack();
            Invoke("DelayTimelineLoad", 1f);
        }
    }
    private void DelayTimelineLoad()
    {
        container.SetActive(false);
        timeline.SetActive(true);
    }

    public void DelayLoadScene()
    {
        Debug.Log("To shutter scene");
        StartCoroutine(SceneLoader.instance.LoadSceneWithDelay("ShutterScene", 1.5f));
    }
}
