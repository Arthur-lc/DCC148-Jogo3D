using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float damage = 5f;
    private float time = 0;
    private void OnCollisionEnter(Collision other) {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth) {
            playerHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
