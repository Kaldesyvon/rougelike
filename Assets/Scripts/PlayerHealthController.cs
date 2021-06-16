using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int health;
    public int maxHealth;
    public float damageInvicLength = 1f;
    private float invicCount;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        health = maxHealth;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = health;
        UIController.instance.text.text = string.Format("{0} / {1}", health, maxHealth);
    }
    void Update()
    {
        if (invicCount > 0)
        {
            invicCount -= Time.deltaTime;
        }
    }
    public void DamagePlayer(int damage)
    {
        if (invicCount <= 0)
        {

            health -= damage;

            invicCount = damageInvicLength;

            PlayerController.instance.spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);

            if (health <= 0)
            {
                PlayerController.instance.gameObject.SetActive(false);
                UIController.instance.deathScreen.SetActive(true);
            }

            UIController.instance.healthSlider.value = health;
            UIController.instance.text.text = string.Format("{0} / {1}", health, maxHealth);
        }
    }
}
