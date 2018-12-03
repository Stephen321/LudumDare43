using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GotCollectable : MonoBehaviour
{
    private Sacrifice Sacrifice;

    void Start()
    {
        Sacrifice = GameObject.FindGameObjectWithTag("Sacrifice").GetComponent<Sacrifice>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            Sacrifice.SetupSacrifice();
        }
    }
}
