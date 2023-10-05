using App.Systems.Wave;
using App.World.Items.Gates;
using App.World.UI.Events;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace App.Systems.GameStates
{
    public class GameStatesSystem : MonoBehaviour
    {
        private WaveSystem waveSystem;
        private StateMachine gameStateMachine;
        private FightingState fightingState;
        private RestingState restingState;
        private Gates gates;
        [SerializeField]
        private AudioClip fightingMusic;
        [SerializeField]
        private AudioClip restingMusic;
        private AudioSource audioSource;
        [SerializeField]
        private CountUpdatedEvent countUpdatedEvent;
        private int daysCount;

        public int DaysCount 
        { 
            get => daysCount; 
            set 
            {
                if(daysCount < 0 )
                {
                    throw new System.InvalidOperationException("Days count can't be less than 0");
                }
                daysCount = value;
                countUpdatedEvent?.CallCountUpdatedEvent(value);
            } 
        }

        public void Init(WaveSystem waveSystem, Gates gates, Light2D light, GameObject shop)
        {
            DaysCount = 0;
            AudioListener.volume = PlayerPrefs.GetFloat("Volume", 0.1f);
            audioSource = GetComponent<AudioSource>();
            this.waveSystem = waveSystem;
            this.gates = gates;
            gates.Init(this);
            gameStateMachine = new StateMachine();
            fightingState = new FightingState(this, waveSystem, light,fightingMusic,audioSource,shop);
            restingState = new RestingState(this, gates, light,restingMusic, audioSource);
            gameStateMachine.Initialize(restingState);
        }

        public void FightingState()
        {
            gameStateMachine.ChangeState(fightingState);
        }
        public void RestingState()
        {
            gameStateMachine.ChangeState(restingState);
        }
        private void Update()
        {
            gameStateMachine.CurrentState.Update();
        }
    }

}
