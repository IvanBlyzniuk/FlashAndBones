using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using App.World;
using App.Systems.EnemySpawning;
using App.Systems.Input;
using App.Systems.Wave;
using App.World.Entity.Player.PlayerComponents;
using App.World.Items.Gates;
using App.Systems.GameStates;

namespace App
{
    public class App : MonoBehaviour
    {
        [SerializeField]
        private ObjectsContainer objectsContainer;
        [SerializeField]
        private Camera mainCamera;
        [SerializeField]
        private ObjectPool objectPool;
        [SerializeField]
        private EnemySpawningSystem enemySpawningSystem;
        [SerializeField]
        private InputSystem inputSystem;
        [SerializeField]
        private WaveSystem waveSystem;
        //[SerializeField]
        //private GameStatesSystem gameStatesSystem;
        //[SerializeField]
        //private GameObject enemySpawner;


        private void Start()
        {
            inputSystem.Init(mainCamera,objectsContainer.Player.GetComponent<Player>(), objectsContainer.Pauser);
            enemySpawningSystem.Init(waveSystem,objectPool,objectsContainer.Player.transform);
            waveSystem.Init(enemySpawningSystem);
            //gameStatesSystem.Init(waveSystem, objectsContainer.Gates.GetComponent<Gates>(), objectsContainer.GlobalLight, objectsContainer.Shop);
        }

    }
}
