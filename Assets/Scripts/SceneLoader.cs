using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    public Animator animator;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator LoadSceneWithDelay(string sceneName, float second)
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

    public IEnumerator PreloadSceneLoadIn(string sceneName, float second)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        while (op.progress < 0.9f)
        {
            Debug.Log("Loading: " + op.progress);
            yield return null;
        }
        Debug.Log("Scene preloaded!");
        // Wait for your condition (button press, fade, etc.)
        yield return new WaitForSeconds(second);

        op.allowSceneActivation = true;
    }
}
