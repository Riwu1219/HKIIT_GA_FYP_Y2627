using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;


public class MoonSceneManager : MonoBehaviour
{
    public Volume volume;        // Assign your Sky and Fog Global Volume
    private HDRISky hdriSky;
    private float siderealDays = 27.322f;
    private float degreesPerSecond;
    public float universalTimeScale = 1f;


    void Start()
    {
        volume.profile.TryGet(out hdriSky);
        degreesPerSecond = 360f / (siderealDays * 86400f);
    }

    void Update()
    {
        if (hdriSky == null) return;
        hdriSky.rotation.value += (degreesPerSecond * Time.deltaTime) * universalTimeScale;
        if (hdriSky.rotation.value >= 360f) { hdriSky.rotation.value = 0f; }

    }
}
