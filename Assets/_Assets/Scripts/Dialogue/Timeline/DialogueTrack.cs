using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace DonBosco.Dialogue
{
    [TrackClipType(typeof(StartDialogueAsset))]
    [TrackBindingType(typeof(DialogueTimeline))]
    public class DialogueTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var playable = ScriptPlayable<DialogueMixerBehaviour>.Create(graph, inputCount);
            return playable;
        }
    }
}
