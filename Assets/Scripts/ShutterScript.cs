using Unity.Entities.UniversalDelegates;
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
    public CameraEffect cameraEffect;

    [Header("Controller Setting")]
    public float minDeadzone = 0.1f;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        spaceScale = ShutterGameManager.instance.spaceScale;
    }


    protected override void Update()
    {
        base.Update();
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Energy decrease over time, faster while travel too far from center

        EnengyDecreaseOverTime();
        if (isDriving) { MoveLogic(); }

    }

    private void RefreshEnergyUI()
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
        energy = ShutterGameManager.instance.TakeDamage(energyDecreaseSpeed * multiplier * Time.deltaTime, energy);
        RefreshEnergyUI();
    }

    public void OnMeteorHit(GameObject meteor)
    {
        TakeDamage(meteor.transform.localScale.x * meteorDamageMultiper);
        cameraEffect.TriggerShake(cameraEffect.shakeDuration, cameraEffect.shakeMagnitude * (meteor.transform.localScale.x), cameraEffect.dampingSpeed);
        //Play hit sound effect here
        Destroy(meteor);
        // TODO: Lose condition, Back to meteor dodge start.
    }

    private void TakeDamage(float damage)
    {
        energy = ShutterGameManager.instance.TakeDamage(energyDecreaseSpeed * Time.deltaTime, energy);
        RefreshEnergyUI();
    }

    private void MoveLogic()
    {
        float xI = horizontalInput;
        float yI = verticalInput;
        float x = transform.localPosition.x;
        float y = transform.localPosition.y;

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
