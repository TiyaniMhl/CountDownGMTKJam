
    using System;
    using System.Collections;
    using UnityEngine;

    public class Box : Destructible
    {
        public Collider2D largeCollider;
        public Collider2D smallCollider;
        
        protected override void Hit()
        {
            if (AlreadyHit) return;
            AlreadyHit = true;
            largeCollider.enabled = false;
            smallCollider.enabled = true;
            StartCoroutine(BeginDestruction());
        }
        
    }
