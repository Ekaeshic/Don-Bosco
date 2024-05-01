using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco
{
    public class GameModeChanger : MonoBehaviour
    {
        [SerializeField] private GameMode gameMode = GameMode.Explore;

        public void ChangeGameMode()
        {
            GameManager.SetGameMode(gameMode);
        }
    }
}
