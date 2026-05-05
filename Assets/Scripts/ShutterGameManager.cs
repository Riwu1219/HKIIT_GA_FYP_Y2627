using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShutterGameManager : MonoBehaviour
{
    public static ShutterGameManager instance;
    public CameraEffect cameraEffect;

    public GameObject shutter;
    public GameObject moon;
    private Transform moonOrigin;
    public float moonDistanceScaler = 1f;
    public float damageMultiplier;
    public float spaceScale = 50f;

    [Header("Meteor Mechanic Setting")]
    public MeteorGenerator meteorGenerator;
    public float playTime = 60f;
    public float curTime = 0f;
    public bool isGameStatus = false;
    public bool isLose = false;
    public bool isPassed = false;

    [Header("FlightCountDown Setting")]
    public Slider progressBar;
    public float startPercent = 0.5f;
    public float endPercent = 1f;

    [Header("SwitchScene")]
    public GameObject animationObject;
    public GameObject player;
    public Transform viewTrans;



    private void Awake()
    {
        instance = this;
        moonOrigin = moon.transform;
    }

    private void Start()
    {
        cameraEffect.CameraFadeTran();

    }

    public void StartMeteorDodging()
    {
        isGameStatus = true;
        ShutterScript.instance.isGameState = true;
        Invoke("SetMeteorGenerate", 2f);
        progressBar.value = startPercent;
    }

    private void Update()
    {
        DebugCheck();
        if (!isGameStatus) { return; }

        cameraEffect.CameraWarningEffect(shutter.transform.position);
        if ( curTime < playTime )
        {
            curTime += Time.deltaTime;
        }
        else
        {
            isPassed = true;
        }

        if (isPassed && !isLose)
        {
            isGameStatus = false;
            ShutterScript.instance.isGameState = false;
            cameraEffect.CameraWarningEffect(new Vector3(0f, 0f, 0f));
            meteorGenerator.isMeteorGenerate = false;
            //TODO: Pass condition, Load Moon Scene.
            //Play Pass Asteroid Area here.
            Invoke("OnPass", 3f);
        }
        
        RefreshUI();
    }

    private void RefreshUI()
    {
        float curProgress = Mathf.Lerp(startPercent, endPercent, curTime / playTime);
        progressBar.value = curProgress;
        Vector3 originPos = moonOrigin.position;
        moon.transform.position = new Vector3(originPos.x, originPos.y, originPos.z + curProgress * moonDistanceScaler);
    }

    public void SetMeteorGenerate()
    {
        meteorGenerator.isMeteorGenerate = true;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMoonScene()
    {
        SceneManager.LoadScene("MoonScene");
    }

    public void OnPass()
    {
        // Load Moon Scene after play pass animation or effect.
        StartCoroutine(SceneLoader.instance.PreloadSceneLoadIn("MoonScene", 3f));
        ShutterScript.instance.cameraEffect.CameraFadeWhite();
        //animationObject.SetActive(true);
        //animationObject.GetComponent<Animator>().Play("ShutterToMoonScene");
        //player.transform.parent = viewTrans;
        //player.transform.position = viewTrans.position;
        //player.transform.rotation = viewTrans.rotation;
        // play pass animation or effect here.
    }

    public void OnLose()
    {
        isLose = true;
        isGameStatus = false;
        ShutterScript.instance.OnRestart();
        cameraEffect.CameraFadeBlack();
        meteorGenerator.isMeteorGenerate = false;
        //TODO: Lose condition, Back to meteor dodge start.
        Invoke("RestartLevel", 3f);
    }

    public void DebugCheck()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ShutterScript.instance.cameraEffect.CameraFadeWhite();
        }
    }
}
