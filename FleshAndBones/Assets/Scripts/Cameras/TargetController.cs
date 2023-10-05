using App.World;
using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace App.Cameras
{
    public class TargetController : MonoBehaviour
    {

        [SerializeField]
        private CinemachineTargetGroup targetGroup;
        [SerializeField]
        private ObjectsContainer container;
        private GameObject player;
        void Start()
        {
            player = container.Player;
            SetTargets();
        }

        private void SetTargets()
        {
            CinemachineTargetGroup.Target targetGroupPlayer = new CinemachineTargetGroup.Target { weight = 1, radius = 1, target = player.transform };
            CinemachineTargetGroup.Target[] targets = new CinemachineTargetGroup.Target[] { targetGroupPlayer };
            targetGroup.m_Targets = targets;
        }
    }

}
