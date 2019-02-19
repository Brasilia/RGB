﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Component for objects that can be damaged.
/// </summary>
public class HealthBHV : MonoBehaviour
{
    public float health = 1;
    public bool invincible = false;

    public delegate void OnKilledDelegate(HealthBHV healthBHV);
    public event OnKilledDelegate OnKilled;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Adds damage to the HealthBHV component.
    /// </summary>
    public bool TakeDamage (float damage)
    {
        if (invincible)
        {
            return false;
        }
        else
        {
            /*if (this.gameObject.tag == "Enemy" && this.gameObject.name.Contains("Straight"))
            {
                Debug.Log("Damaged");
                GetComponent<Animator>().SetTrigger("BlueDamage");
            }*/
            health -= damage;
            if (health <= 0)
            {
                Kill();
            }
            return true;
        }
    }

    // Define o comportamento de morte do objeto
    private void Kill ()
    {
        // animãção, etc
        
        Debug.Log("Killed!");
        OnKilled?.Invoke(this); // triggers event
        
        if (this.gameObject.tag == "Enemy")
        {
            GetComponent<Animator>().SetTrigger("Explode");
            Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            GameObject.Find("Player_ships").GetComponent<Player>().AddScore(100);
            GameObject.Find("Player_ships").GetComponent<Player>().AddMoney(50);
        }
        else
        {
            Destroy(gameObject);//, 2*Time.deltaTime);
        }
    }
}
