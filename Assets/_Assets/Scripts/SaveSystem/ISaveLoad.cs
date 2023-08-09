using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DonBosco.SaveSystem;
using System.Threading.Tasks;

namespace DonBosco
{
    public interface ISaveLoad
    {
        Task Save(SaveData saveData);
        Task Load(SaveData saveData);
    }
}
