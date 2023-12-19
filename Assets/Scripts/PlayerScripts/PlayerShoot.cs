using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerShoot : MonoBehaviour
{
    private ObjectPool pool;
	[SerializeField] public GameObject bulletPrefab;
    [SerializeField] public Transform firePoint;
    [SerializeField] public Transform fireTarget;
    private AudioSource shoot;
    private AudioSource noAmmo;
    private bool isShooting = false;
    private float tempoDisparo = 0f;
    private int balas = 20;
    private int municaoAtual;
    public Image bulletBar;
    public Image backBulletBar;
    private float fillDuration = 4f;  
    private float fillTimer = 0f;
    void Start()
    {
        pool = new ObjectPool(bulletPrefab,balas);
        //shoot = GetComponents<AudioSource>()[0];
        //noAmmo = GetComponents<AudioSource>()[1];
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
        if(municaoAtual==0 && Input.GetButtonDown("Fire1")){
            //noAmmo.Play();
        }
        if(isShooting && tempoDisparo > 0.4f && municaoAtual>0){
            atirar();
            tempoDisparo = 0f;
            municaoAtual--;
            fillTimer = 0;
        }
        if (municaoAtual == 0 || Input.GetKeyDown(KeyCode.R))
        {
            if (municaoAtual == 0)
            {
                fillTimer += Time.deltaTime;  // Incrementa o temporizador durante a recarga
            }

            if (fillTimer >= fillDuration)
            {
                municaoAtual = balas;
                fillTimer = 0f;  // Reseta o temporizador após recarregar completamente
            }
        }
        AlterarBarra();
    }

    void atirar(){
        GameObject obj;
        obj = pool.GetFromPool();
        obj.transform.position =  firePoint.position;
        Vector3 direction = (fireTarget.position - firePoint.position).normalized;
        obj.transform.forward = direction;
        //shoot.Play();
    }

    public void AlterarBarra(){
        if (municaoAtual > 0)
        {
            float bulletFraction = ((float)municaoAtual / balas);
            bulletBar.fillAmount = bulletFraction;
        }
        else
        {
            float fillPercentage = Mathf.Clamp01(fillTimer / fillDuration);
            bulletBar.fillAmount = fillPercentage;
        }
    }
}