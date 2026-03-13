using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("Shutter"))
        {
            ShutterGameManager.instance.OnMeteorHit(this.gameObject);
            Debug.Log("Meteor hit the player!, Lose");
        }
    }
}
