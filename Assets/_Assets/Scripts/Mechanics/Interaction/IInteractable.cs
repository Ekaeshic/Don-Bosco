using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for interactable objects
/// </summary>
public interface IInteractable
{
    bool IsInteractable { get; set; }
    
    /// <summary>
    /// Interact with the object
    /// </summary>
    // Note: Tiap script yang implement IInteractable harus ada method Interact(),
    // Dan tiap script bisa dimasukin mekanisme interaksi yang berbeda-beda.
    void Interact();


    /// <summary>
    /// Interact with the object while holding an item
    /// Used in the ItemSystem Inventory
    /// </summary>
    /// <param name="item">Holded item</param>
    void Interact(DonBosco.ItemSystem.Item item);
}
