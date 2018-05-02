using UnityEngine;

namespace Walls {
    public class Barriers : MonoBehaviour {
        private void OnTriggerEnter2D (Collider2D other) {
            Destroy(other.gameObject, 1f);
        }
    }
}
