using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float damage = 3f;

    // Update is called once per frame
    void Update()
    {
        movimentar();
    }
    
    void movimentar(){
        Rigidbody bulletRb = transform.GetComponent<Rigidbody>();
        bulletRb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Enemy")){
            Debug.Log("Pegou no inimigo");
            other.GetComponent<HealthComponent>().TakeDamage(damage);
        }

        gameObject.SetActive(false);
    }
}
