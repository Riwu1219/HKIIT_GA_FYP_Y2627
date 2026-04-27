using UnityEngine;

public class CrystalBox : MonoBehaviour
{
    public bool interactionOnHover = false;
    public MeshRenderer[] mrs;
    public GameObject openedModel;
    public GameObject closedModel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetInteractionBtnOnHover(bool state)
    {
        interactionOnHover = state;
        foreach (MeshRenderer mr in mrs) 
        {
            Debug.Log($"{mr.name} -> material count: {mr.materials.Length}");
            mr.materials[1].SetFloat("_Enabled", state ? 1 : 0);

        }
            
    }

    public void OnGrab()
    {
        openedModel.SetActive(false);
        closedModel.SetActive(true);
    }
}
