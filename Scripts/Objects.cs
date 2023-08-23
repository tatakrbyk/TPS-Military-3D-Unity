using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    [SerializeField] private float objectHealth = 100f;
    
    public void ObjectHitDamage(float amountDamage)
    {
        objectHealth -= amountDamage;
        if (objectHealth < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
