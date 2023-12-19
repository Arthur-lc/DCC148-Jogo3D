using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private ObjectPool pool;
	[SerializeField] public GameObject bulletPrefab;
    [SerializeField] public Transform firePoint;
    [SerializeField] public Transform fireTarget;
    private AudioSource shoot;
    private bool isShooting = false;
    private float tempoDisparo = 0f;
    private int balas = 20;
    private int municaoAtual;
    void Start()
    {
        pool = new ObjectPool(bulletPrefab,balas);
        shoot = GetComponent<AudioSource>();
        municaoAtual = balas;
    }

    void Update(){
        if(Input.GetButtonDown("Fire1")){
            isShooting = true;
        }
        if(Input.GetButtonUp("Fire1")){
            isShooting = false;
        }
        tempoDisparo += Time.deltaTime;
        if(isShooting){
            if(tempoDisparo > 0.4f){
                atirar();
                tempoDisparo = 0f;
                municaoAtual--;
            }
        }
        if(municaoAtual == 0 ||Input.GetKeyDown(KeyCode.R)){
            tempoDisparo = -5.0f;
            municaoAtual = balas;
        }
    }

    void atirar(){
        GameObject obj;
        obj = pool.GetFromPool();
        obj.transform.position =  firePoint.position;
        Vector3 direction = (fireTarget.position - firePoint.position).normalized;
        obj.transform.forward = direction;
        shoot.Play();
    }
}