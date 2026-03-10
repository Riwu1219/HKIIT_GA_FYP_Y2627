using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("Shutter"))
        {
            
            Debug.Log("Meteor hit the player!, Lose");
        }
    }
}
