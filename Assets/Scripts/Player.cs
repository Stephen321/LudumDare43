using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float force = 10;
    public float fallGravityMultiplier;
    public float jumpGravityMultiplier;
    public int Coolness_For_Double_Jump;
    public int Coolness;
    public int Speed;
    public float Jump;

    private Rigidbody2D rb;
    private Animator anim;
    private bool jumpdown;
    private bool jump;
    private bool grounded;
    private float distToGround;

    private int jump_hash = Animator.StringToHash("Jump");
    private int grounded_hash = Animator.StringToHash("Grounded");
    public float Time_Before_Double_Jump;
    private float jumping_time;
    private bool already_double_jumped;
    private Vector3 collider_offset;

    private float start_x;
    private float game_timer; 
    private bool start_jump;

    public bool CanDoubleJump()
    {
        return Coolness > Coolness_For_Double_Jump;
    }

    // Use this for initialization
    void Start ()
    {
        Jump = 1.0f;
        Speed = 4;
        start_x = transform.position.x;
        game_timer = .0f;
        Coolness = 0;
        already_double_jumped = false;
        jumping_time = .0f;
        start_jump = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        CircleCollider2D circle_collider = GetComponent<CircleCollider2D>();
        distToGround = circle_collider.bounds.extents.y;
        collider_offset = circle_collider.offset;

        UpdateStats();
        Stats.Died = false;
    }

    public void UpdateStats()
    {
        Stats.ConnectionsSacrificed = Coolness;
        Stats.Time = game_timer;
        Stats.DistanceTravelled = transform.position.x - start_x;
    }
	
	// Update is called once per frame
	void Update ()
    {


        transform.Translate(Vector2.right * Time.deltaTime * Speed);

        //transform.position = new Vector3(transform.position.x, 3, transform.position.z);

        jump = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0);
        grounded = IsGrounded();
        if (jump && grounded)
        {
            start_jump = true; //if there was a point where this was true then start the jump
        }

        if (grounded)
        {
            already_double_jumped = false;
        }
        game_timer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        anim.SetBool(grounded_hash, rb.velocity.y < 0.1f);
        if (start_jump)
        {
            start_jump = false;
            rb.AddForce(Vector2.up * force * Jump);
            jumping_time = .0f;
        }

        if (rb.velocity.y < 0f) //fall faster
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * fallGravityMultiplier * Time.deltaTime;
        }
        else
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * jumpGravityMultiplier * Time.deltaTime;
        }

        //double jumping
        if (Coolness > Coolness_For_Double_Jump && !grounded && !already_double_jumped)
        {
            jumping_time += Time.deltaTime;
            if (jumping_time > Time_Before_Double_Jump)
            {
                if (jump)
                {
                    Vector2 new_vel = rb.velocity;
                    new_vel.y = .0f;
                    rb.velocity = new_vel;
                    anim.SetTrigger(jump_hash);
                    rb.AddForce((Vector2.up * force * 0.7f) + (Vector2.up * force * 0.25f * (Jump * 0.75f)));
                    already_double_jumped = true;
                }
            }
        }
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position + collider_offset, Vector3.down, distToGround + 0.1f, LayerMask.GetMask("Collidable"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            UpdateStats();
            Stats.Died = true;
            SceneManager.LoadScene(2);
        }
    }
}
