using System;
using UnityEngine;

public class orange_fireplace : MonoBehaviour
{
    private GameObject fire;
    private float hits = 0;
    
   

    private float maxHealth = 4f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fire = transform.Find("fire").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
       if (other.CompareTag("Tear"))
       {
           hits += 1;
           updateSize();
       }

      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&hits<maxHealth)
        {
            HandlePlayer.hearts--;
        }
    }


    void updateSize()
    {
        print(hits);
        fire.transform.localScale = new Vector3(1-(1/maxHealth)*hits, 1-(1/maxHealth)*hits, 1);

        print(fire.transform.localScale);
        if (hits >= maxHealth)
        {
            fire.SetActive(false);
        }
    }
}
