using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foodcon : MonoBehaviour
{
    public BaseFoodStats baseFoodStats;

    [Header("baseFoodStats stats:")]
    [SerializeField]
    public float nutrition;
    [SerializeField]
    public int type;

    void Start()
    {
        //baseFoodStats.PrintInfo();
        LoadBaseFoodStats();
        StartCoroutine(RotTimer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadBaseFoodStats()
    {
        nutrition = baseFoodStats.nutrition;
        type = baseFoodStats.type;
    }
    public void Eat()
    {
        Destroy(gameObject);
    }
    IEnumerator RotTimer(){
        yield return new WaitForSeconds(45);
        Destroy(gameObject);
    }
}
