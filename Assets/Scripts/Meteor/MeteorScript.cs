using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public GameObject UI_Icon;
    public GameObject UI_Holder;
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
        UI_Icon.transform.localScale = new Vector2(size, size) * 0.1f * (ShutterGameManager.instance.spaceScale / 100);
    }

    void Start()
    {
        mT = transform.GetChild(0).gameObject;
        randRotSpeed = Random.Range(15f, 50f);
        Debug.Log(randRotSpeed);
        randRot = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)); ;

        UI_Icon = Instantiate(UI_Icon);
        UI_Icon.transform.SetParent(GameObject.Find("Tex_Grid").transform);
        UI_Icon.transform.localPosition = Vector2.zero;

        transform.localScale = new Vector3(size, size, size);
        
    }

    void Update()
    {
        float spaceScale = ShutterGameManager.instance.spaceScale;
        mT.transform.Rotate(randRot * Time.deltaTime * randRotSpeed);
        UI_Icon.transform.localPosition = new Vector2(transform.position.x * (spaceScale / 100f), transform.position.z * (spaceScale / 100f));
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (transform.position.z < -10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Destroy(UI_Icon);
    }
}
