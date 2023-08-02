using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for interactable objects
/// </summary>
public interface IInteractable
{
    /// <summary>
    /// Interact with the object
    /// </summary>
    // Note: Tiap script yang implement IInteractable harus ada method Interact(),
    // Dan tiap script bisa dimasukin mekanisme interaksi yang berbeda-beda.
    void Interact();
}
