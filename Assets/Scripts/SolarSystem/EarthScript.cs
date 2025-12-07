using UnityEngine;

public class EarthScript : MonoBehaviour
{
    private GameObject earthAxisTilt;
    public float rotationSpeed;
    public float dayPeriodInHours = 24f;


    private void Start()
    {
        earthAxisTilt = new GameObject("EarthAxis");
        earthAxisTilt.transform.position = transform.position;
        transform.parent = earthAxisTilt.transform;
    }

    void Update()
    {
        //transform.rotation *= Quaternion.Euler(rotationSpeed.x * Time.deltaTime, rotationSpeed.y * Time.deltaTime, 0);
    }
}
