using Unity.Physics;
using UnityEngine;
using UnityEngine.UI;

public class ShutterScript : DrivingSystem
{
    public static ShutterScript instance;

    [Header("Shutter Setting")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 1f;
    // may not in use, consider remove
    [SerializeField] private float lerpSpeed = 10f;
    [SerializeField] private float tiltAngle = 15f;
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
        //MoveLogic();

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
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            float y = (horizontalInput > 0.1f) ? tiltAngle : -tiltAngle; // left = + , right = -
            Quaternion target = Quaternion.Euler(0f, y, 0f);

            transform.localRotation = Quaternion.Lerp(
                transform.localRotation,
                target,
                Time.deltaTime * lerpSpeed
            );
        }
        else
        {
            // return to neutral
            Quaternion target = Quaternion.Euler(0f, 0f, 0f);
            transform.localRotation = Quaternion.Lerp(
                transform.localRotation,
                target,
                Time.deltaTime * lerpSpeed
            );
        }
        if (Mathf.Abs(verticalInput) > 0.1f)
        {
            // up = -20, down = +20 (flip signs if you want the opposite)
            float x = (verticalInput > 0.1f) ? -tiltAngle : tiltAngle;

            Quaternion target = Quaternion.Euler(x, 0f, 0f);

            transform.localRotation = Quaternion.Lerp(
                transform.localRotation,
                target,
                Time.deltaTime * lerpSpeed
            );
        }
        else
        {
            // return to neutral
            Quaternion target = Quaternion.Euler(0f, 0f, 0f);
            transform.localRotation = Quaternion.Lerp(
                transform.localRotation,
                target,
                Time.deltaTime * lerpSpeed
            );
        }

        if (transform.position.x > spaceScale && horizontalInput > 0)
        {
            horizontalInput = 0;
            rb.linearVelocity = Vector3.zero;
        }
        if (transform.position.x < -spaceScale && horizontalInput < 0)
        {
            horizontalInput = 0;
            rb.linearVelocity = Vector3.zero;
        }

        rb.linearVelocity = new Vector3(horizontalInput * speed, verticalInput * speed, 0);

        // Meteor Dust Rotation
        Vector3 temp = transform.rotation.eulerAngles;
        meteorDust.transform.rotation = Quaternion.Euler(
            (temp.x + 180),
            temp.y,
            temp.z * -1f
        );
    }
}
