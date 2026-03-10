using UnityEngine;

public class ShutterGameManager : MonoBehaviour
{
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


    void OnMeteorHit()
    {
        meteorGenerator.isMeteorGenerate = false;
                
    }

}
