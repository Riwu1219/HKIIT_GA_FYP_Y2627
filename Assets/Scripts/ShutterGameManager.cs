using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class ShutterGameManager : MonoBehaviour
{
    public static ShutterGameManager instance;
    public CameraEffect cameraEffect;

    public GameObject shutter;
    public float damageMultiplier;
    public float spaceScale = 50f;

    [Header("Meteor Mechanic Setting")]
    public MeteorGenerator meteorGenerator;
    public float playTime = 60f;
    public float curTime = 0f;
    public bool isGameStatus = false;
    public bool isPassed = false;

    [Header("FlightCountDown Setting")]
    public Slider progressBar;
    public float startPercent = 0.5f;
    public float endPercent = 1f;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cameraEffect.CameraFadeTran();

    }

    public void StartMeteorDodging()
    {
        isGameStatus = true;
        Invoke("SetMeteorGenerate", 2f);
        progressBar.value = startPercent;
    }

    private void Update()
    {
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

        if (isPassed)
        {
            isGameStatus = false;
            //TODO: Pass condition, Load Moon Scene.
            Invoke("OnPass", 3f);
        }
        
        RefreshUI();
    }

    private void RefreshUI()
    {
        float curProgress = Mathf.Lerp(startPercent, endPercent, curTime / playTime);
        progressBar.value = curProgress;
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
        meteorGenerator.isMeteorGenerate = false;
        // play pass animation or effect here.
        LoadMoonScene();
    }

    public void OnLose()
    {
        cameraEffect.CameraFadeBlack();
        meteorGenerator.isMeteorGenerate = false;
        //TODO: Lose condition, Back to meteor dodge start.
        Invoke("RestartLevel", 3f);
    }

}
