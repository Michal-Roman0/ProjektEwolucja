using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDisappear : MonoBehaviour
{
    public void Dispose(){
        Destroy(gameObject);
    }
}
