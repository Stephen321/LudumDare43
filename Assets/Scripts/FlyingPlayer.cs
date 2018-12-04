using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPlayer : MonoBehaviour {



    public Vector2 force;

    private Rigidbody2D rb;

    private bool flying;
    private Vector2 flying_vel;

	// Use this for initialization
	void Start () {
        flying = false;

        //testing:
        if (Stats.PlayerSpeed == 0)
        {
            Stats.PlayerSpeed = 8;
        }
        rb = GetComponent<Rigidbody2D>();
        Vector2 new_vel = rb.velocity;
        new_vel.x = Stats.PlayerSpeed;
        rb.velocity = new_vel;
	}
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate()
    {
        if (!flying)
        {
            rb.AddForce(force);
        }
        else
        {
            rb.velocity = flying_vel;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boost"))
        {
            rb.AddForce(force * 5);
        }
        else if (collision.CompareTag("FlyingTrig"))
        {
            rb.gravityScale = 0.0f;
            rb.freezeRotation = false;
            rb.AddTorque(-8.0f);
            flying = true;
            flying_vel = rb.velocity;
            flying_vel.x -= 2.0f;
            flying_vel.y += 1.0f;
        }
    }
}
