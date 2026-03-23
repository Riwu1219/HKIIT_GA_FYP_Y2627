using UnityEngine;
using UnityEngine.SceneManagement;

public class ShutterGameManager : MonoBehaviour
{
    public static ShutterGameManager instance;
    public MeteorGenerator meteorGenerator;

    [SerializeField]
    GameObject shutter;
    Rigidbody shutterRb;
    [SerializeField] private float lerpSpeed = 10f;
    [SerializeField] private float tiltAngle = 15f;
    public float hp = 100f;
    public float damageMultiplier = 1f;
    public CameraEffect cameraEffect;
    

    [SerializeField]
    RectTransform UI_Grid;

    public float spaceScale = 10f;

    bool isDriving = true;

    [SerializeField]
    float speed = 1f;
    float horizontalInput;
    float verticalInput;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        shutterRb = shutter.GetComponent<Rigidbody>();
    }

    private void OnLevelWasLoaded(int level)
    {
        //TODO : Story
    }

    private void Update()
    {
        if (isDriving) 
        {
            
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            if (Mathf.Abs(horizontalInput) > 0.01f)
            {
                float z = (horizontalInput > 0) ? -tiltAngle : tiltAngle; // right = -, left = +
                Quaternion target = Quaternion.Euler(0f, 0f, z);

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

            UI_Grid.anchoredPosition = new Vector2(shutter.transform.position.x * -(100 / spaceScale), UI_Grid.anchoredPosition.y);
            shutterRb.linearVelocity = new Vector3(horizontalInput * speed, verticalInput * speed, 0);
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

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            meteorGenerator.isMeteorGenerate = false;
            Debug.Log("Shutter destroyed!");
            //TODO: Lose condition, Back to meteor dodge start.
            Invoke("RestartLevel", 3f);
        }
    }



}
