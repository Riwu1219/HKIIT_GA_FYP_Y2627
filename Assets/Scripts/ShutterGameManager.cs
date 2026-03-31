using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShutterGameManager : MonoBehaviour
{
    public static ShutterGameManager instance;
    public MeteorGenerator meteorGenerator;

    [Header("Shutter Setting")]
    [SerializeField] GameObject shutter;
    Rigidbody shutterRb;
    [SerializeField] private float lerpSpeed = 10f;
    [SerializeField] private float tiltAngle = 15f;
    public float energy = 100f;
    public float damageMultiplier = 1f;
    public CameraEffect cameraEffect;

    public float spaceScale = 50f;

    bool isDriving = true;

    [SerializeField] float speed = 1f;
    float horizontalInput;
    float verticalInput;

    [Header("Shutter Energy")]
    [SerializeField] Slider energyBar;
    public float energyDecreaseSpeed = 0.1f;

    [SerializeField] GameObject meteorDust;



    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cameraEffect.CameraFadeTran();
        shutterRb = shutter.GetComponent<Rigidbody>();
    }

    private void OnLevelWasLoaded(int level)
    {
        
        //TODO : Story
    }

    private void Update()
    {
        // Shader Update
        cameraEffect.CameraWarningEffect(shutter.transform.position);

        // Shutter Control
        if (isDriving) 
        {
            // Energy decrease over time, faster while travel too far from center
            float multiplier = 1f;
            if (Mathf.Abs(shutter.transform.position.x) > spaceScale || Mathf.Abs(shutter.transform.position.y) > spaceScale)
            {
                multiplier = Vector2.Distance(shutter.transform.position, Vector2.zero) * 1.5f - spaceScale;
            }
            TakeDamage(energyDecreaseSpeed * multiplier * Time.deltaTime);

            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            if (Mathf.Abs(horizontalInput) > 0.01f)
            {
                float y = (horizontalInput > 0) ? tiltAngle : -tiltAngle; // left = + , right = -
                Quaternion target = Quaternion.Euler(0f, y, 0f);

                shutter.transform.localRotation = Quaternion.Lerp(
                    shutter.transform.localRotation,
                    target,
                    Time.deltaTime * lerpSpeed
                );
            }
            else
            {
                // return to neutral
                Quaternion target = Quaternion.Euler(0f, 0f, 0f);
                shutter.transform.localRotation = Quaternion.Lerp(
                    shutter.transform.localRotation,
                    target,
                    Time.deltaTime * lerpSpeed
                );
            }
            if (Mathf.Abs(verticalInput) > 0.01f)
            {
                // up = -20, down = +20 (flip signs if you want the opposite)
                float x = (verticalInput > 0) ? -tiltAngle : tiltAngle;

                Quaternion target = Quaternion.Euler(x, 0f, 0f);

                shutter.transform.localRotation = Quaternion.Lerp(
                    shutter.transform.localRotation,
                    target,
                    Time.deltaTime * lerpSpeed
                );
            }
            else
            {
                // return to neutral
                Quaternion target = Quaternion.Euler(0f, 0f, 0f);
                shutter.transform.localRotation = Quaternion.Lerp(
                    shutter.transform.localRotation,
                    target,
                    Time.deltaTime * lerpSpeed
                );
            }

            if (shutter.transform.position.x > spaceScale && horizontalInput > 0)
            {
                horizontalInput = 0;
                shutterRb.linearVelocity = Vector3.zero;
            }
            if (shutter.transform.position.x < -spaceScale && horizontalInput < 0)
            {
                horizontalInput = 0;
                shutterRb.linearVelocity = Vector3.zero;
            }

            shutterRb.linearVelocity = new Vector3(horizontalInput * speed, verticalInput * speed, 0);

            // Meteor Dust Rotation
            Vector3 temp = shutter.transform.rotation.eulerAngles;
            meteorDust.transform.rotation = Quaternion.Euler(
                (temp.x + 180),
                temp.y,
                temp.z * -1f
            );
        }  
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMeteorHit(GameObject meteor)
    {
        TakeDamage(meteor.transform.localScale.x * damageMultiplier);
        cameraEffect.TriggerShake(cameraEffect.shakeDuration, cameraEffect.shakeMagnitude * (meteor.transform.localScale.x), cameraEffect.dampingSpeed);
        //Play hit sound effect here
        Destroy(meteor);
        // TODO: Lose condition, Back to meteor dodge start.
    }

    private void RefreshEnergyUI()
    {
        energyBar.value = energy;
    }

    public void TakeDamage(float damage)
    {
        energy -= damage;
        if (energy <= 0)
        {
            energy = 0;
            OnLose();
        }
        RefreshEnergyUI();
    }

    private void OnLose()
    {
        cameraEffect.CameraFadeBlack();
        meteorGenerator.isMeteorGenerate = false;
        //TODO: Lose condition, Back to meteor dodge start.
        Invoke("RestartLevel", 3f);
    }

}
