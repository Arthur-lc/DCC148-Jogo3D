using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private ObjectPool pool;
	[SerializeField] public GameObject bulletPrefab;
    [SerializeField] public Transform firePoint;
    private AudioSource shoot;
    private bool isShooting = false;

    void Start()
    {
        pool = new ObjectPool(bulletPrefab,20);
        shoot = GetComponent<AudioSource>();
    }

    void Update(){
        if(Input.GetButtonDown("Fire1")){
            isShooting = true;
        }
        if(Input.GetButtonUp("Fire1")){
            isShooting = false;
        }
        if(isShooting){
            atirar();
        }
    }

    void atirar(){
        GameObject obj;
        obj = pool.GetFromPool();
        obj.transform.position =  firePoint.position;
        obj.transform.forward = firePoint.forward;
        shoot.Play();
    }
}