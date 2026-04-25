using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;


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

    public CameraEffect cameraEffect;


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
    } 

    void Update()
    {
        if (hdriSky == null) return;
        hdriSky.rotation.value -= (degreesPerSecond * Time.deltaTime) * universalTimeScale;
        if (hdriSky.rotation.value <= 0f) { hdriSky.rotation.value = 360f; }

    }
}
