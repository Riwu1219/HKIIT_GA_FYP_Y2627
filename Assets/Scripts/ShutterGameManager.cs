using UnityEngine;

public class ShutterGameManager : MonoBehaviour
{
    public static ShutterGameManager instance;
    public MeteorGenerator meteorGenerator;

    [SerializeField]
    GameObject shutter;
    Rigidbody shutterRb;

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

    private void Update()
    {
        if (isDriving) 
        {
            
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

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


    public void OnMeteorHit(GameObject meteor)
    {
        meteorGenerator.isMeteorGenerate = false;
        Destroy(meteor);
        // TODO: Lose condition, Back to meteor dodge start.
    }





}
