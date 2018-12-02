using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Config config;
    public float force = 10;
    public float fallGravityMultiplier = 1.5f;
    public float jumpGravityMultiplier = 0.5f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool jumpdown;
    private bool jump;
    private bool grounded;
    private float distToGround;

    private int jump_hash = Animator.StringToHash("Jump");
    private int grounded_hash = Animator.StringToHash("Grounded");

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        distToGround = GetComponent<BoxCollider2D>().bounds.extents.y;
    }
	
	// Update is called once per frame
	void Update ()
    {


        transform.Translate(Vector2.right * Time.deltaTime * config.GetSpeed());

        //transform.position = new Vector3(transform.position.x, 3, transform.position.z);

        jump = Input.GetKeyDown(KeyCode.Space);
        grounded = IsGrounded();
    }

    void FixedUpdate()
    {
        anim.SetBool(grounded_hash, rb.velocity.y < 0.1f);
        if (grounded && jump)
        {
            anim.SetTrigger(jump_hash);
            rb.AddForce(Vector2.up * force);
        }

        if (rb.velocity.y < 0f) //fall faster
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * fallGravityMultiplier * Time.deltaTime;
        }
        else
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * jumpGravityMultiplier * Time.deltaTime;
        }
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector3.down, distToGround + 0.1f, LayerMask.GetMask("Collidable"));
    }

}
