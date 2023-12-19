using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private float maxHp = 10f;

    [SerializeField]
    public UnityEvent onDeath;

    private float hp;
    private AudioSource damage;

    private void Start() {
        hp = maxHp;
        damage = GetComponents<AudioSource>()[1];
    }

     public void TakeDamage(float damge)
    {
        StartCoroutine(PlayDamageSoundAndWait(damge));
    }

    private System.Collections.IEnumerator PlayDamageSoundAndWait(float damge)
    {
        damage.Play();

        // Aguarde o tempo do áudio
        yield return new WaitForSeconds(damage.clip.length);

        hp -= damge;
        if (hp <= 0f)
        {
            onDeath.Invoke();
        }
    }

    public void Die() {
        Destroy(gameObject);
    }
}
