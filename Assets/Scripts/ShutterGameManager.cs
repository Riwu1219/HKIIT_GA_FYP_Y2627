using UnityEngine;

public class ShutterGameManager : MonoBehaviour
{
    public static ShutterGameManager instance;

    [SerializeField]
    MeteorGenerator meteorGenerator;
    [SerializeField]
    GameObject shutter;
    Rigidbody shutterRb;

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
