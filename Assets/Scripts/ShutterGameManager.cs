using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShutterGameManager : MonoBehaviour
{
    public static ShutterGameManager instance;
    public MeteorGenerator meteorGenerator;
    public CameraEffect cameraEffect;
    
    public GameObject shutter;
    public float damageMultiplier;
    public float spaceScale = 50f;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cameraEffect.CameraFadeTran();
    }

    //private void OnLevelWasLoaded(int level)
    //{
    //    //TODO : Story
    //}

    private void Update()
    {
        cameraEffect.CameraWarningEffect(shutter.transform.position);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public float TakeDamage(float damage, float curEnergy)
    {
        curEnergy -= damage;
        if (curEnergy <= 0)
        {
            curEnergy = 0;
            OnLose();
        }
        return curEnergy;
    }

    private void OnLose()
    {
        cameraEffect.CameraFadeBlack();
        meteorGenerator.isMeteorGenerate = false;
        //TODO: Lose condition, Back to meteor dodge start.
        Invoke("RestartLevel", 3f);
    }

}
