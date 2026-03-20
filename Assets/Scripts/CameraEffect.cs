using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class CameraEffect : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;
    public float dampingSpeed = 1.0f;

    
    
    public void TriggerShake(float _shakeDuration, float _shakeMagnitude, float _dampingSpeed)
    {
        StartCoroutine(CameraShake(_shakeDuration, _shakeMagnitude, _dampingSpeed));
    }

    private IEnumerator CameraShake(float _shakeDuration, float _shakeMagnitude, float _dampingSpeed)
    {
        float elapsedTime = 0.0f;
        //Vector3 initPos = transform.localPosition;
        Vector3 initPos = Vector3.zero;

        while(elapsedTime < _shakeDuration)
        {
            float magnitude = _shakeMagnitude * Mathf.Exp(_dampingSpeed * elapsedTime);
            float xOffset = Random.Range(-1f, 1f) * _shakeMagnitude;
            float yOffset = Random.Range(-1f, 1f) * _shakeMagnitude;

            transform.localPosition = new Vector3(initPos.x + xOffset, initPos.y + yOffset, initPos.z);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = initPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key was pressed. Triggering camera shake.");
            TriggerShake(shakeDuration, shakeMagnitude, dampingSpeed);
        }
    }
}
