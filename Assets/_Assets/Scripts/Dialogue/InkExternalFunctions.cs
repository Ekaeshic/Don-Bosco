using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

namespace DonBosco.Dialogue
{
    public class InkExternalFunctions
    {
        public void Bind(Story story, Animator emoteAnimator)
        {
            story.BindExternalFunction("playEmote", (string emoteName) => PlayEmote(emoteName, emoteAnimator));
        }

        public void Unbind(Story story) 
        {
            if(story.TryGetExternalFunction("playEmote", out var ext)) 
            {
                story.UnbindExternalFunction("playEmote");
            }
        }

        public void PlayEmote(string emoteName, Animator emoteAnimator)
        {
            if (emoteAnimator != null) 
            {
                emoteAnimator.Play(emoteName);
            }
            else 
            {
                Debug.LogWarning("Tried to play emote, but emote animator was "
                    + "not initialized when entering dialogue mode.");
            }
        }
        
    }

}