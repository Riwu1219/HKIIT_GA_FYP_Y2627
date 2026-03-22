using UnityEngine;

public class ShutterScript : MonoBehaviour
{
    int hp = 100;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            ShutterGameManager.instance.meteorGenerator.isMeteorGenerate = false;
            Debug.Log("Shutter destroyed!");
        }
    }


}
