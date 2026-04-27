using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;


public class MoonSceneManager : MonoBehaviour 
{
    public static MoonSceneManager instance;

    public GameObject player;

    [Header("SceneLoad")]
    public bool isSkipSceneLoadedAnimation = false;
    public Animator onSceneLoadAnimator;
    public GameObject[] onSceneLoadDisableObjects;
    public GameObject loadSceneElement;
    public Transform spawnpoint;

    [Header("Scene")]
    public Volume volume;
    private HDRISky hdriSky;
    private float siderealDays = 27.322f;
    private float degreesPerSecond;
    public float universalTimeScale = 1f;

    [Header("UI")]
    public GameObject HUD_Canvas;
    public GameObject HologramHUD_Panel;

    [Header("Effect")]
    public AudioSource[] audios;
    public CameraEffect cameraEffect;

    [Header("EndGame")]
    public Transform endGameTrans;
    public Camera mainCamera;
    public Text endGameText; 
    public GameObject[] disableOnEndGame;


    private void Awake()
    {
        if (universalTimeScale < 0) { universalTimeScale *= -1; }
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        cameraEffect.CameraWhiteToTran();
        OnSceneLoaded();
        volume.profile.TryGet(out hdriSky);
        degreesPerSecond = 360f / (siderealDays * 86400f);
    }

    public void OnSceneLoaded()
    {
        foreach (var obj in onSceneLoadDisableObjects)
        {
            obj.SetActive(false);
        }
        if (isSkipSceneLoadedAnimation)
        {
            OnSceneLoadEnd();
            return;
        }
        onSceneLoadAnimator.Play("ShutterToMoonScene");
    }

    public void OnSceneLoadEnd()
    {
        cameraEffect.CameraWhiteToTran();
        foreach (var obj in onSceneLoadDisableObjects)
        {
            obj.SetActive(true);
        }
        player.transform.position = spawnpoint.transform.position;
        player.transform.rotation = spawnpoint.transform.rotation;
        player.transform.parent = null;
        loadSceneElement.SetActive(false);

        // Fade in audio when scene load ended
        foreach (var audio in audios)
        {
            audio.GetComponent<AudioController>().FadeInAudio(1f);
        }

        HUD_Canvas.SetActive(true);
        HologramHUD_Panel.SetActive(true);

    } 

    void Update()
    {
        if (hdriSky == null) return;
        hdriSky.rotation.value -= (degreesPerSecond * Time.deltaTime) * universalTimeScale;
        if (hdriSky.rotation.value <= 0f) { hdriSky.rotation.value = 360f; }

    }

    public void EndGame()
    {
        cameraEffect.CameraFadeWhite();
        loadSceneElement.SetActive(true);
        endGameText.text = "MISSION ACCOMPLISHED";
        endGameText.fontSize = 80;
        Debug.Log("Game Ended");
        Invoke("OnEndGame", 2f);
        foreach (var audio in audios)
        {
            audio.GetComponent<AudioController>().FadeOutAudio(2f);
        }
    }

    private void OnEndGame()
    {
        foreach (var obj in disableOnEndGame)
        {
            obj.SetActive(false);
        }

        player.transform.parent = endGameTrans;
        player.transform.position = endGameTrans.position;
        Transform cam = Camera.main.transform;

        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();
        Vector3 targetForward = endGameTrans.forward;
        targetForward.y = 0;
        float angle = Vector3.SignedAngle(camForward, targetForward, Vector3.up);

        player.transform.Rotate(0, angle, 0);

        cameraEffect.CameraWhiteToTran();
        onSceneLoadAnimator.Play("MoonSceneEndGame");
    }
}
