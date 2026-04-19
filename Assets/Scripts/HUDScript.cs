using UnityEngine;

public class HUDScript : MonoBehaviour
{
    public HUDController hudController;

    public void SetInteractable(bool state)
    {
        hudController.interactable = state;
    }
}
