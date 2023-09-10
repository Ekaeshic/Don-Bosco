using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.ItemSystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item System/Item")]
    public class ItemSO : ScriptableObject
    {
        [field: SerializeField] public string itemName { get; private set; }
        public Item prefab;
        public Sprite sprite;



        private void OnValidate() {
            #if UNITY_EDITOR
            itemName = this.name;
            UnityEditor.EditorUtility.SetDirty(this);
            #endif
        }
    }
}
