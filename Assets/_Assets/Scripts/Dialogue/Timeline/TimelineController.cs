using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class TimelineController : MonoBehaviour
{
    [SerializeField] private bool playOnStart = false;
    [SerializeField] private UnityEvent onTimelineEnd;
    private PlayableDirector director;


    private void OnEnable()
    {
        director = GetComponent<PlayableDirector>();
        director.stopped += (x) => onTimelineEnd.Invoke();
    }

    void OnDisable()
    {
        director.stopped -= (x) => onTimelineEnd.Invoke();
    }

    void Start()
    {
        if(playOnStart)
        {
            // Play the timeline if the director is not already playing
            if (!director.playableGraph.IsPlaying())
            {
                director.Play();
            }
        }
    }
}
