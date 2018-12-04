using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GotCollectable : MonoBehaviour
{
    private Sacrifice Sacrifice;
    private ObstacleSpawner ObstacleSpawner;

    void Start()
    {
        Sacrifice = GameObject.FindGameObjectWithTag("Sacrifice").GetComponent<Sacrifice>();
        ObstacleSpawner = GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ObstacleSpawner.collectables_alive--;
            Destroy(gameObject);
            Sacrifice.SetupSacrifice();
        }
    }
}
