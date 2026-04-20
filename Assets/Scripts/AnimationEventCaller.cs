using UnityEngine;
using UnityEngine.Events;

public class AnimationEventCaller : MonoBehaviour
{
    public UnityEvent onAnimationPlay;
    public UnityEvent onAnimationEnd;

    public void OnPlay()
    {
        if ( onAnimationPlay == null ) { return; }
        onAnimationPlay.Invoke();
    }

    public void OnEnd()
    {
        if ( onAnimationEnd == null ) { return; }
        onAnimationEnd.Invoke();
    }
}
