using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private float maxHp = 10f;

    [SerializeField]
    public UnityEvent onDeath;

    private float hp;

    private void Start() {
        hp = maxHp;
    }

    public void TakeDamage(float damge) {
        hp -= damge;
        if (hp < 0f) {
            onDeath.Invoke();
        }
    }

    public void Die() {
        Destroy(gameObject);
    }
}
