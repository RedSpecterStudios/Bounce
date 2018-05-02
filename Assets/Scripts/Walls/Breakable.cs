using UnityEngine;

namespace Walls {
	public class Breakable : MonoBehaviour {
		private void OnCollisionEnter2D (Collision2D other) {
			if (other.gameObject.CompareTag("Ball")) {
				Destroy(gameObject, 0.1f);
			}
		}
	}
}
