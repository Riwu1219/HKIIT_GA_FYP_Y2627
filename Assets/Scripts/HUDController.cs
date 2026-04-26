using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class HUDController : MonoBehaviour
{
    public DrivingSystem drivingSystem;
    public AudioController audioController;
    public InputAction controllerInteractionBind;

    public GameObject panel;
    public GameObject guide;
    public bool interacted = false; // True when first interacted
    public bool interactable = false;
    public bool isOnCooldown = false;
    private bool wasPressedLastFrame = false;

    private void OnEnable() => controllerInteractionBind.Enable();
    private void OnDisable() => controllerInteractionBind.Disable();

    private void Update()
    {
        if (!interactable) { return; }

        bool isPressed = controllerInteractionBind.IsPressed();
        if (isPressed && !wasPressedLastFrame)
        {
            if (isOnCooldown) { return; }
            panel.SetActive(true);
            guide.SetActive(false);
            interacted = true;
            StartCoroutine(TriggerCD(2f));
        }
        if (!isPressed && interacted && wasPressedLastFrame)
        {
            if (audioController.isAudioPlaying) { audioController.FadeOutAudio(0.5f); }
            panel.SetActive(false);
        }
        wasPressedLastFrame = isPressed;
    }

    public void OnIntroAnimationEnd()
    {
        if (interacted) { return; } 
        drivingSystem.SetVeicleCanEnter(true);
        guide.SetActive(true);
        interactable = true;
    }

    IEnumerator TriggerCD(float CD)
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(CD);
        isOnCooldown = false;

    }
}
