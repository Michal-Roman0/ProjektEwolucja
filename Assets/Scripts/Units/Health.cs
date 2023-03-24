using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int health = 10;
    [SerializeField]
    private int max_health = 10;

    public HealthBar healthBar;

    public void SetHealth(int maxHealth, int health){
        max_health = maxHealth;
        this.health = health;
        healthBar.SetMaxHealth(max_health);
    }

    public void Damage(int amount){
        if(amount < 0){
            throw new System.ArgumentOutOfRangeException("Cannot have negative damage");
        }
        health -= amount;

        healthBar.SetHealth(health);

        if(health <= 0){
            Defeated();
        }
    }
    public void Defeated(){
        //What happens when defeated?
        Debug.Log("Unit died");
        Destroy(gameObject);
    }
}
