using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    private GameObject mT;
    private Rigidbody rb;

    private Vector3 randRot;
    private float randRotSpeed;
    private float size = 1f;
    private float speed = 1f;

    public void Init(float Size, float Speed)
    {
        size = Size;
        speed = Speed;
        transform.localScale = new Vector3(size, size, size);

    }

    void Start()
    {
        mT = transform.GetChild(0).gameObject;
        randRotSpeed = Random.Range(15f, 50f);
        Debug.Log(randRotSpeed);
        randRot = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)); ;


        transform.localScale = new Vector3(size, size, size);
        
    }

    void Update()
    {
        float spaceScale = ShutterGameManager.instance.spaceScale;
        mT.transform.Rotate(randRot * Time.deltaTime * randRotSpeed);
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (transform.position.z < -10f)
        {
            Destroy(gameObject);
        }
    }

}
