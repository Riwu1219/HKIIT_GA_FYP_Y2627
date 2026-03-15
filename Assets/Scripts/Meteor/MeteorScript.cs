using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public GameObject UI_Icon;
    private float size = 1f;
    private float speed = 1f;

    void Init(float Size, float Speed)
    {
        size = Size;
        speed = Speed;
        transform.localScale = new Vector3(size, size, size);
        UI_Icon.transform.localScale = new Vector2(size, size);
    }

    void Start()
    {
        UI_Icon = Instantiate(UI_Icon);
        UI_Icon.transform.SetParent(GameObject.Find("Canvas").transform);
        UI_Icon.transform.localPosition = Vector2.zero;

        transform.localScale = new Vector3(size, size, size);
    }

    void Update()
    {
        
        UI_Icon.transform.localPosition = new Vector2(transform.position.x, transform.position.z);
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
