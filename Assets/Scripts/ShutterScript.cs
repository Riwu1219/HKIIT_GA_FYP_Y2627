using Unity.Entities.UniversalDelegates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class ShutterScript : DrivingSystem
{
    public static ShutterScript instance;

    [Header("Shutter Setting")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 1f;
    // may not in use, consider remove
    [SerializeField] private float maxRotAngle = 10f;
    [SerializeField] private float lerpSpeed  = 15f;
    private float spaceScale = 50f;

    [Header("Shutter Energy")]
    [SerializeField] Slider energyBar;
    public float energy = 100f;
    public float energyDecreaseSpeed = 0.1f;
    public float damageMultiplier = 1f;
    public float meteorDamageMultiper = 1f;

    [Header("Effect")]
    [SerializeField] GameObject meteorDust;
    public GameObject meteorSFXHolder;
    private AudioSource meteorHitSFX;
    public AudioSource shutterMoveSFX;
    public AudioSource AmbienceOnMeteorSFX;
    public GameObject ambienceSound;
    public GameObject warningSFX;
    public CameraEffect cameraEffect;

    [Header("Controller Setting")]
    public float minDeadzone = 0.1f;

    [Header("Monitor UI")]
    public GameObject monitorCanvas;

    [Header("Lighting")]
    public bool lightDown = false;
    public GameObject mainLight;
    public Animator mainLight_Ani;
    public GameObject warnningLight;
    public Animator warningLight_Ani;

    public bool isGameState = false;

    void Awake()
    {
        instance = this;
    }

    protected override void Start()
    {
        base.Start();
        spaceScale = ShutterGameManager.instance.spaceScale;
        meteorHitSFX = meteorSFXHolder.GetComponent<AudioSource>();
        canExit = false;
        SetVeicleCanEnter(false);
    }


    protected override void Update()
    {
        base.Update();
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Energy decrease over time, faster while travel too far from center

        
        if (isDriving) 
        {
            EnengyDecreaseOverTime();
            MoveLogic(); 
        }
        

        RefreshUI();
    }

    protected override void OnDriveModeEnter()
    {
        //May add ui animation
        shutterMoveSFX.GetComponent<AudioController>().FadeInAudio(1f);
        AmbienceOnMeteorSFX.GetComponent<AudioController>().FadeInAudio(1f);
        monitorCanvas.SetActive(true);
        monitorCanvas.GetComponent<Animator>().Play("Show");
        ShutterGameManager.instance.StartMeteorDodging();
    }

    private void RefreshUI()
    {
        energyBar.value = energy;
    }

    private void EnengyDecreaseOverTime()
    {
        float multiplier = 1f;
        if (Mathf.Abs(transform.position.x) > spaceScale || Mathf.Abs(transform.position.y) > spaceScale)
        {
            multiplier = Vector2.Distance(transform.position, Vector2.zero) * 1.5f - spaceScale;
        }
        TakeDamage(energyDecreaseSpeed * multiplier * Time.deltaTime);
        
    }

    public void OnMeteorHit(GameObject meteor, Vector3 pos)
    {
        if (!isGameState) { return; }
        float _damage = meteor.transform.localScale.x * meteorDamageMultiper;
        TakeDamage(_damage);
        cameraEffect.TriggerShake(cameraEffect.shakeDuration, cameraEffect.shakeMagnitude * (meteor.transform.localScale.x), cameraEffect.dampingSpeed);
        //Play hit sound effect
        meteorSFXHolder.transform.position = pos;
        MeteorHitSFX();

        if (warnningLight.activeSelf == false)
        {
            warningSFX.GetComponent<AudioSource>().Play();
            warnningLight.SetActive(true);
        }
        if (energy > 70f)
        {
            mainLight_Ani.Play("Flash1");
        }
        else if (lightDown == false)
        {
            lightDown = true;
            mainLight.SetActive(false);
            Debug.Log("Main light down");
        }
        Debug.Log("Hit by meteor, size, damage: " + meteor.transform.localScale.x + ", " + _damage);
        Destroy(meteor);
        // TODO: Lose condition, Back to meteor dodge start.
    }

    public void MeteorHitSFX()
    {
        meteorHitSFX.pitch = Random.Range(0.8f, 1.2f);
        meteorHitSFX.Play();
    }

    public void TakeDamage(float damage)
    {
        energy -= damage;
        if (energy <= 0)
        {
            energy = 0;
            ShutterGameManager.instance.OnLose();
        }
    }

    public void OnRestart()
    {
        shutterMoveSFX.GetComponent<AudioController>().FadeOutAudio(0.5f);
        warningSFX.GetComponent<AudioController>().FadeOutAudio(1f);
        ambienceSound.GetComponent<AudioController>().FadeOutAudio(1f);
    }

    private void MoveLogic()
    {
        float xI = horizontalInput;
        float yI = verticalInput;
        float x = transform.localPosition.x;
        float y = transform.localPosition.y;

        shutterMoveSFX.panStereo = -xI;

        if (Mathf.Abs(x) > spaceScale)
        {
            transform.localPosition -= new Vector3( x / spaceScale * Time.deltaTime, 0f, 0f);
        }
        if (Mathf.Abs(y) > spaceScale)
        {
            transform.localPosition -= new Vector3(0f, y / spaceScale * Time.deltaTime, 0f);
        }
        
        Vector3 targetEuler = Vector3.zero;

        if (Mathf.Abs(xI) > minDeadzone || Mathf.Abs(yI) > minDeadzone)
        {
            targetEuler = new Vector3(-yI * maxRotAngle, xI * maxRotAngle, 0f);
            transform.localPosition += new Vector3(xI * speed * Time.deltaTime, yI * speed * Time.deltaTime, 0);
        }

        Quaternion targetRot = Quaternion.Euler(targetEuler);

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, Time.deltaTime * lerpSpeed);

        // Meteor Dust Rotation
        Vector3 temp = transform.localRotation.eulerAngles;
        meteorDust.transform.localRotation = Quaternion.Euler(
            (temp.x + 180),
            temp.y,
            temp.z * -1f
        );
    }
}
