using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject moveLocomotion;

    [Header("Footstep")]
    public AudioSource SFX_Footstep;
    public AudioClip[] moonFootstepClips;
    public AudioClip[] hardFootstepClips;

    [Header("StepAdjust")]
    public float stepInterval = 1.5f;
    public float minMoveThreshold = 0.1f;
    private float stepTimer;

    [Header("Ground Detect")]
    public float rayDistance = 0.2f;
    public LayerMask groundLayer;

    private void Update()
    {
        if (!moveLocomotion.activeSelf) { return; }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float moveAmount = new Vector2(h, v).magnitude;

        if (moveAmount > minMoveThreshold)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f; // reset when not moving
        }
    }

    private void PlayFootstep()
    {
        if (DetectSurface() == 1)
        {
            int index = Random.Range(0, moonFootstepClips.Length);
            SFX_Footstep.PlayOneShot(moonFootstepClips[index]);
        }
        else if (DetectSurface() == 2)
        {
            int index = Random.Range(0, hardFootstepClips.Length);
            SFX_Footstep.PlayOneShot(hardFootstepClips[index]);
        }
        
    }

    private int DetectSurface()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.red, 1f);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, groundLayer))
        {
            // Option 1: check tag
            if (hit.collider.CompareTag("Moon")) { return 1; } 
            else { return 2; }
        }
        return 0;
    }
}