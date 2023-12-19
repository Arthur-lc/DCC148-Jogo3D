using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torret : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float cannonForce;
    [SerializeField] private float fireCooldown = 4f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask PlayerMask;


    private float timeSinceLastFire = 0f;
    private Transform target;

    private void Start() {
        target = transform.GetComponentInParent<EnemyMovement>().GetTarget();
        timeSinceLastFire = Random.Range(0f, fireCooldown); // faz com o que os inimigos não atirem a o mesmo tempo
    }

    void Update()
    {
        timeSinceLastFire += Time.deltaTime;
        Vector3 targetPos = target.position;
        if (targetPos.y < transform.position.y)
            targetPos.y = transform.position.y;

        transform.LookAt(targetPos);

        RaycastHit hit;
        Vector3 directionToTarget = (targetPos - transform.position).normalized;
        if (Physics.Raycast(transform.position, directionToTarget, out hit)) {
            Debug.DrawLine(transform.position, hit.point, Color.yellow, 0.0f);
            if (hit.collider.CompareTag("Player"))
            {
                // fire
                if (timeSinceLastFire > fireCooldown) {
                    Fire();
                    timeSinceLastFire = 0f;
                }
            }   
        }
    }

    private void Fire() {
        GameObject newProjectile = Instantiate(projectile);
        newProjectile.transform.position = firePoint.position;
        newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * cannonForce, ForceMode.Impulse);
    }
}

