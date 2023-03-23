using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int health = 10;
    [SerializeField]
    private int max_health = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetHealth(int maxHelath, int health){
        max_health = maxHelath;
        this.health = health;
    }

    public void Damage(int amount){
        if(amount < 0){
            throw new System.ArgumentOutOfRangeException("Cannot have negative damage");
        }
        health -= amount;

        if(health <= 0){
            Defeated();
        }
    }
    public void Defeated(){
        //What happens when defeated?
    }
}
