using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonLiberation
{
    public class Interactables : MonoBehaviour
    {
        public float radius = 0.3f;
        public string interactableText;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, radius);
        }

        public virtual void Interact(PlayerManager playerManager)
        {
            Debug.Log("You interacted");
        }
    }
}
