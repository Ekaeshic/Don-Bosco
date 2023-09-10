using System;
using System.Collections;
using System.Collections.Generic;
using DonBosco.ItemSystem;
using UnityEngine;

namespace DonBosco
{
    public class PlayerEvents
    {
        public event Action<string> onChangeScene;
        public void ChangeScene(string sceneName)
        {
            if (onChangeScene != null)
            {
                onChangeScene(sceneName);
            }
        }

        public event Action<GameObject> onInteract;
        public void Interact(GameObject interactable)
        {
            if (onInteract != null)
            {
                onInteract(interactable);
            }
        }

        public event Action<Item> onItemDrop;
        public void ItemDrop(Item item)
        {
            if (onItemDrop != null)
            {
                onItemDrop(item);
            }
        }
    }
}
