using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int health = 10;
    [SerializeField]
    private int maxHealth = 10;

    public UniversalBar healthBar;

    public void SetHealth(int maxHealth, int health){
        this.maxHealth = maxHealth;
        this.health = health;
        healthBar.SetBarMaxFill(maxHealth);
    }

    public void SetHealthFromSave(int health)
    {
        this.health = health;
        healthBar.SetBarFill(health);
    }

    public void Update()
    {
    }

    public void Damage(int amount){
        if(amount < 0){
            throw new System.ArgumentOutOfRangeException("Cannot have negative damage");
        }
        health -= amount;

        StartCoroutine(BlinkTimer());

        healthBar.SetBarFill(health);

        if(health <= 0){
            Defeated();
        }
    }
    public void Defeated(){
        GetComponent<UnitController>().KillSelf();
    }
    private IEnumerator BlinkTimer(){
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Color c = sr.color;
        sr.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sr.color = c;
    }
    public int HP
    {
        get { return health; }
    }
}
