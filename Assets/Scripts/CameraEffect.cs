using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class CameraEffect : MonoBehaviour
{
    //Default values
    [Header("Shake")]
    public float shakeDuration = 0.7f;
    public float shakeMagnitude = 0.1f;
    public float dampingSpeed = -5f;

    [Header("Fade")]
    public GameObject Fader;

    [Header("Warning Effect")]
    public GameObject WarningEffect;
    private Material warningShaderMat;

    private void Start()
    {
        warningShaderMat = WarningEffect.GetComponent<Renderer>().material;
    }

    public void CameraWarningEffect(Vector3 position)
    {
        warningShaderMat.SetVector("_CurPosition", position);
    }

    public void CameraFadeTran()
    {
        Fader.GetComponent<Animator>().Play("CameraFadeTran");
    }

    public void CameraFadeBlack()
    {
        Fader.GetComponent<Animator>().Play("CameraFadeBlack");
    }

    public void CameraFadeWhite()
    {
        Fader.GetComponent<Animator>().Play("CameraFadeWhite");
    }

    public void CameraWhiteToTran()
    {
        Fader.GetComponent<Animator>().Play("CameraWhiteToTran");
    }

    public void TriggerShake(float _shakeDuration, float _shakeMagnitude, float _dampingSpeed)
    {
        StartCoroutine(CameraShake(_shakeDuration, _shakeMagnitude, _dampingSpeed));
    }

    private IEnumerator CameraShake(float _shakeDuration, float _shakeMagnitude, float _dampingSpeed)
    {
        float elapsedTime = 0f;
        //Vector3 initPos = transform.localPosition;
        Vector3 initPos = Vector3.zero;

        while(elapsedTime < _shakeDuration)
        {
            float magnitude = _shakeMagnitude * Mathf.Exp(_dampingSpeed * elapsedTime);
            float xOffset = Random.Range(-1f, 1f) * magnitude;
            float yOffset = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(initPos.x + xOffset, initPos.y + yOffset, initPos.z);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = initPos;
    }

    // Update is called once per frame
    void Update()
    {
        //for testing purposes, trigger shake on space key press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key was pressed. Triggering camera shake.");
            TriggerShake(shakeDuration, shakeMagnitude, dampingSpeed);
        }
    }
}
