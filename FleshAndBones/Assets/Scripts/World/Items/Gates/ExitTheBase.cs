using App.Systems.GameStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Items.Gates
{
    public class ExitTheBase : MonoBehaviour
    {
        private GameStatesSystem gameStatesSystem;
        public void Init(GameStatesSystem gameStatesSystem)
        {
            this.gameStatesSystem = gameStatesSystem;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Exit();
        }
        private void Exit()
        {
            gameObject.SetActive(false);
            gameStatesSystem.FightingState();
        }
    }
}

