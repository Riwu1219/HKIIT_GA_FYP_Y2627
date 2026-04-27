using UnityEngine;

public class CrystalBox : MonoBehaviour
{
    public bool interactionOnHover = false;
    public MeshRenderer[] mrs;
    public GameObject openedModel;
    public GameObject closedModel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Update()
    {
        if (Mathf.Abs(transform.position.y) > 300f)
        {
            Vector3 playerPos = MoonSceneManager.instance.player.transform.position;
            transform.position = new Vector3(playerPos.x, playerPos.y + 2, playerPos.z);
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
    }

    public void SetInteractionBtnOnHover(bool state)
    {
        interactionOnHover = state;
        foreach (MeshRenderer mr in mrs) 
        {
            mr.materials[1].SetFloat("_Enabled", state ? 1 : 0);

        }
            
    }

    public void OnGrab()
    {
        openedModel.SetActive(false);
        closedModel.SetActive(true);
    }
}
