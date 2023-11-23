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
            animator.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            animator.Play("Moving");
            
        }

        private void OnBecameInvisible()
        {
            animator.enabled = false;
        }

        private void OnBecameVisible()
        {
            animator.enabled = true;
        }
    }
}

