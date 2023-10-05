using App.Systems.GameStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Items.Gates
{
    public class Gates : MonoBehaviour
    {
        private BoxCollider2D m_BoxCollider;
        private GameStatesSystem gameStatesSystem;
        [SerializeField]
        private GameObject exitChecker;
        private Animator m_Animator;


        public void Init(GameStatesSystem gameStatesSystem)
        {
            this.gameStatesSystem = gameStatesSystem;
            exitChecker.GetComponent<ExitTheBase>().Init(gameStatesSystem);
            m_BoxCollider = GetComponent<BoxCollider2D>();
            m_Animator = GetComponent<Animator>();
        }
        public void Open()
        {
            exitChecker.SetActive(false);
            m_Animator.SetBool("IsOpen", true);
        }
        public void Close()
        {
            exitChecker.SetActive(false);
            m_Animator.SetBool("IsOpen", false);
        }
    }
}

