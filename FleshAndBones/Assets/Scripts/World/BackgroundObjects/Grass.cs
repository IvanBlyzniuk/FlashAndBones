using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.BackgroundObjects
{
    public class Grass : MonoBehaviour
    {
        private Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            animator.Play("Moving");
        }
    }
}

