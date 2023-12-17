using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torret : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float cannonForce;
    [SerializeField] private float fireCooldown = 4f;
    [SerializeField] private Transform firePoint;

    private float timeSinceLastFire = 0f;
    private Transform target;

    private void Start() {
        target = transform.GetComponentInParent<EnemyMovement>().GetTarget();
        timeSinceLastFire = Random.Range(0f, fireCooldown); // faz com o que os inimigos não atirem a o mesmo tempo
    }

    void Update()
    {
        Vector3 targetPos = target.position;
        targetPos.y = transform.position.y;

        transform.LookAt(targetPos);

        // fire
        timeSinceLastFire += Time.deltaTime;
        if (timeSinceLastFire > fireCooldown) {
            Fire();
            timeSinceLastFire = 0f;
        }
    }

    private void Fire() {
        GameObject newProjectile = Instantiate(projectile);
        newProjectile.transform.position = firePoint.position;
        newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * cannonForce, ForceMode.Impulse);
    }
}
