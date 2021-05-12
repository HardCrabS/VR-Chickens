using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float health = 1000;
    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject loseText;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthSlider.value = health;
        if(health <= 0)
        {
            Die();
            loseText.SetActive(true);
        }
    }

    void Die()
    {
        Destroy(GetComponent<PlayerMovement>());
    }
}
