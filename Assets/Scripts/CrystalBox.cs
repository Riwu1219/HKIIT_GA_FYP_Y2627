using UnityEngine;

public class CrystalBox : MonoBehaviour
{
    public bool interactionOnHover = false;
    public MeshRenderer mr;
    public GameObject openedModel;
    public GameObject closedModel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetInteractionBtnOnHover(bool state)
    {
        interactionOnHover = state;
        mr.materials[1].SetFloat("_Enabled", state ? 1 : 0);
    }

    public void OnGrab()
    {
        openedModel.SetActive(false);
        closedModel.SetActive(true);
    }
}
