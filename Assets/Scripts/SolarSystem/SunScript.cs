using UnityEngine;

public class SunScript : MonoBehaviour
{
    public GameObject revolutionAxis;

    public float rotationPerDay = 360f;
    public float dayPeriodInHours = 708f;   

    private void Start()
    {
        revolutionAxis = new GameObject("SunRevolutionAxis");
        revolutionAxis.transform.position = Vector3.zero;
        transform.parent = revolutionAxis.transform;
        revolutionAxis.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        float degreesPerSecond = rotationPerDay / (dayPeriodInHours * 3600f);
        revolutionAxis.transform.rotation *= Quaternion.Euler(0, degreesPerSecond * Time.deltaTime * SpaceSceneManager.instance.universalTimeScale, 0);
    }
}
