
    using System;
    using System.Collections;
    using UnityEngine;

    public class Box : Destructible
    {
        public Collider2D largeCollider;
        public Collider2D smallCollider;
        
        protected override IEnumerator Hit()
        {
            if (AlreadyHit) yield break;
            AlreadyHit = true;
            Animator.SetTrigger(DestroyThis);
            triggerCollider.enabled = false;
            largeCollider.enabled = false;
            smallCollider.enabled = true;
            yield return new WaitForSeconds(5);
            Destroy(gameObject);
        }
    }
