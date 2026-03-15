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
        transform.localScale = new Vector3(size, size, size);
        UI_Icon.transform.localScale = new Vector2(size, size);
    }

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}
