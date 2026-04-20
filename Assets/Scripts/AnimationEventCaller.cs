using UnityEngine;
using UnityEngine.Events;

public class AnimationEventCaller : MonoBehaviour
{
    public Animator animator;
    public UnityEvent onAnimationPlay;
    public UnityEvent onAnimationPlaying;
    public UnityEvent onAnimationEnd;
    private bool stopFlag = false;

    public void SetStopFlag(bool state) => stopFlag = state;

    public void OnPlay()
    {
        if ( onAnimationPlay == null ) { return; }
        animator.speed = 1;
        onAnimationPlay.Invoke();
    }

    public void OnPlaying()
    {
        if (onAnimationPlaying == null) { return; }
        onAnimationPlaying.Invoke();
    }

    public void Stop()
    {
        if ( animator == null ) { return; }
        if (!stopFlag) { return; }
        animator.speed = 0;
    }

    public void OnEnd()
    {
        if ( onAnimationEnd == null ) { return; }
        onAnimationEnd.Invoke();
    }
}
