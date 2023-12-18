using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
   private float speed = 20f;
	public GameObject obj;
    [SerializeField] public Transform firePoint;

    // Update is called once per frame
    void Update()
    {
        movimentar();
    }
    void movimentar(){
        Rigidbody bulletRb = transform.GetComponent<Rigidbody>();
    	Vector3 direction = (firePoint.transform.position - transform.position).normalized;
        bulletRb.velocity = direction * speed;
    }
    void FixedUpdate(){
        sumir();
    }
    void sumir(){
        if (Vector3.Distance(transform.position, firePoint.transform.position) < 0.1f)
        {
            obj.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other){
        obj.SetActive(false);
        if (other.CompareTag("Enemy")){
            other.GetComponent<HealthComponent>().TakeDamage(1);
            obj.SetActive(false);
        }
    }
}
