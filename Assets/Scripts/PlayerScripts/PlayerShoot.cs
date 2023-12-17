using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private ObjectPool pool;
	public GameObject bulletPrefab;
    public Transform firePoint;
    private AudioSource shoot;

    void Start()
    {
        pool = new ObjectPool(bulletPrefab,20);
        shoot = GetComponent<AudioSource>();
    }

    void Update(){
        if(Input.GetButtonDown("Fire1")){
            atirar();
        }
    }

    void atirar(){
        GameObject obj;
        obj = pool.GetFromPool();
        obj.transform.position =  firePoint.position;
        shoot.Play();
    }
}