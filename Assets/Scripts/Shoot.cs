using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
   private float speed = 15f;
	public GameObject obj;

    // Update is called once per frame
    void Update()
    {
        movimentar();
        //sumir();
    }
    void movimentar(){
        Rigidbody bulletRb = transform.GetComponent<Rigidbody>();
    	bulletRb.velocity = transform.forward * speed;
    }
    void sumir(){
    	if(transform.position.x > 7){
    		obj.SetActive(false);
    	}
    }
    private void OnTriggerEnter(Collider other){
        obj.SetActive(false);
        // if (other.CompareTag("Enemy1")){
        // other.GetComponent<Enemy1>().DesativarInimigo();
        //     obj.SetActive(false);
        // }
        // if (other.CompareTag("Enemy2")){
        // other.GetComponent<Enemy2>().DesativarInimigo();
        //     obj.SetActive(false);
        // }
        // if (other.CompareTag("Enemy3")){
        // other.GetComponent<Enemy3>().DesativarInimigo();
        //     obj.SetActive(false);
        // }
    }
}
