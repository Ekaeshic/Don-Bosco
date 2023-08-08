using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace DonBosco.Dialogue
{
    public class StartDialogueAsset : PlayableAsset
    {
        public TextAsset dialogue;
        public string knotPath;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<StartDialogueBehaviour>.Create(graph);
            var behaviour = playable.GetBehaviour();
            behaviour.dialogue = dialogue;
            behaviour.knotPath = knotPath;
            return playable;
        }
    }
}
