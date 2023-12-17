using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;

    [Header("Health Bar")]
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    
    [Header("Damage Overlay")]
    public Image overlay;
    public float duration; // Tempo que o overlay fica na tela
    public float fadeSpeed; // Quão rápido o overlay aparece

    private float durationTimer;

    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();

        if(Input.GetKeyDown(KeyCode.LeftAlt)){
            TakeDamage(Random.Range(5, 10));
        }

        if(overlay.color.a > 0){
            if(health < 30)
                return;
                
            durationTimer += Time.deltaTime;
            if(durationTimer > duration){
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }

        }        
    }

    public void UpdateHealthUI()
    {
        float healthFraction = health / maxHealth;

        backHealthBar.fillAmount = Mathf.MoveTowards(backHealthBar.fillAmount, healthFraction, Time.deltaTime * chipSpeed);

        frontHealthBar.fillAmount = healthFraction;

        backHealthBar.color = Color.Lerp(backHealthBar.color, Color.white, Time.deltaTime * chipSpeed);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0.4f);
    }
}