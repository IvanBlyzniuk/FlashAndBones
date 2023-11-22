using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity
{
    public class Rotating : MonoBehaviour
    {
        [SerializeField]
        private float degreesPerSecond;
        
        void Start()
        {

        }

        void Update()
        {
            transform.Rotate(Vector3.back,degreesPerSecond* Time.deltaTime);
        }
    }
}

