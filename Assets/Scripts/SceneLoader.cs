using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class SceneLoader : MonoBehaviour
{
    public Animator animator;

    IEnumerator LoadSceneWithDelay(string sceneName, float second)
    {
        yield return new WaitForSeconds(second);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnLoadAnimation(string animationName)
    {
        animator.Play(animationName);
    }
}
