using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace DonBosco.Dialogue
{
    public class DialogueMixerBehaviour : PlayableBehaviour
    {
        int m_FirstFrameHappened = -1;
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var dialogueTimeline = playerData as DialogueTimeline;

            if (dialogueTimeline == null)
            {
                return;
            }

            int inputCount = playable.GetInputCount();
            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                ScriptPlayable<StartDialogueBehaviour> inputPlayable = (ScriptPlayable<StartDialogueBehaviour>)playable.GetInput(i);
                StartDialogueBehaviour input = inputPlayable.GetBehaviour();

                if (inputWeight > 0.5f)
                {
                    if(m_FirstFrameHappened == i)
                    {
                        return;
                    }
                    dialogueTimeline.StartDialogue(input.dialogue, input.knotPath);
                    m_FirstFrameHappened = i;
                }
            }
        }


        public override void OnPlayableDestroy(Playable playable)
        {
            m_FirstFrameHappened = -1;
        }
    }
}