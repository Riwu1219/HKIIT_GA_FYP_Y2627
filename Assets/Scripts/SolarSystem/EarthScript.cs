using UnityEngine;

public class EarthScript : MonoBehaviour
{
    private GameObject earthAxis;
    public float earthAxisTilt = 23.5f;
    public float rotationSpeed;
    public float dayPeriodInHours = 24f;


    private void Start()
    {
        earthAxis = new GameObject("EarthAxis");
        earthAxis.transform.position = transform.position;
        transform.parent = earthAxis.transform;
        earthAxis.transform.rotation = Quaternion.Euler(earthAxisTilt, 0, 0);
    }

    void Update()
    {
        transform.rotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
    }
}
