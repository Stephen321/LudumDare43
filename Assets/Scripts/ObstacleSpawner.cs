using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class ObstacleSpawner : MonoBehaviour {

    public new Camera camera;
    public Config Config;
    public Player Player;
    public IceSpawner Ice_Spawner;
    public float Spawn_Offset = 2;

    public GameObject Spikes;
    public GameObject Spikes2;
    public GameObject SpikesGap;
    public GameObject SpikesDoubleJump;

    public float Spikes_Chance;
    public float Spikes2_Chance;
    //public float SpikesGap_Chance;

    public GameObject Collectable;

    public float Min_Spawn_Time;
    public float Max_Spawn_Time;
    public float Collectable_Spawn_Y;
    public float Min_Collectable_Spawn_Time; //need a minimum so they spawn far enough apart
    private float next_spawn_time;
    private float spawn_timer;
    private bool spawn_collectable;
    
    private float time_running;

    // Use this for initialization
    void Start ()
    {
        time_running = .0f;
        next_spawn_time = Min_Spawn_Time;
        spawn_timer = next_spawn_time;
        spawn_collectable = false; 
    }

    // Update is called once per frame
    void Update()
    {        
        if (time_running > Config.GetCollectableSpawnTime())
        {
            spawn_collectable = true;
        }

            if (transform.childCount > 0)
        {
            float cam_left = camera.ViewportToWorldPoint(new Vector3(0, 0.5f, camera.nearClipPlane)).x;
            GameObject first = transform.GetChild(0).gameObject;
            float first_right = first.transform.position.x + (first.GetComponent<BoxCollider2D>().bounds.size.x * 0.5f);

            if (first_right < cam_left)
            {
                Destroy(first);
            }
        }


        if (Ice_Spawner.GetIceGaps() == 0)
        {
            if (!spawn_collectable && spawn_timer > next_spawn_time)
            {
                float cam_right = camera.ViewportToWorldPoint(new Vector3(1, 0.5f, camera.nearClipPlane)).x;
                spawn_timer = .0f;

                next_spawn_time = Random.Range(Min_Spawn_Time, Max_Spawn_Time);
                Spawn(cam_right + Spawn_Offset);
            }
            if (spawn_collectable && spawn_timer > Min_Collectable_Spawn_Time)
            {
                float cam_right = camera.ViewportToWorldPoint(new Vector3(1, 0.5f, camera.nearClipPlane)).x;
                SpawnCollectable(cam_right + Spawn_Offset);
                spawn_collectable = false;
                Debug.Log("spawn collectable!");
                spawn_timer = .0f;
                time_running = .0f;
            }
        }

        time_running += Time.deltaTime;
        spawn_timer += Time.deltaTime;
    }

    void Spawn(float x)
    {

        GameObject new_obstacle;

        bool flippable = true;
        float r = Random.value;
        if (r < Spikes_Chance)
        {
            new_obstacle = GameObject.Instantiate(Spikes);
        }
        else if (r < Spikes2_Chance)
        {
            new_obstacle = GameObject.Instantiate(Spikes2);
        }
        else// if (r < SpikesGap_Chance)
        {
            if (Player.CanDoubleJump() && Random.value < 0.66f)
            {
                new_obstacle = GameObject.Instantiate(SpikesDoubleJump);
            }
            else
            { 
                new_obstacle = GameObject.Instantiate(SpikesGap);
            }

            flippable = false;
        }

        SpriteRenderer new_sprite = new_obstacle.GetComponent<SpriteRenderer>();
        if (flippable && Random.value < 0.5f)
        {
            new_sprite.flipX = true;
        }

        Vector3 pos = new_obstacle.transform.position;
        pos.x = x;

        new_obstacle.transform.parent = transform;
        new_obstacle.transform.position = pos;
    }

    void SpawnCollectable(float x)
    {

        GameObject new_obstacle = GameObject.Instantiate(Collectable);
        SpriteRenderer new_sprite = new_obstacle.GetComponent<SpriteRenderer>();

        //float new_width = new_sprite.bounds.size.x;
        float new_height = new_sprite.bounds.size.y;

        Vector3 pos = new_obstacle.transform.position;
        pos.x = x;
        pos.y = transform.position.y + Random.Range(new_height * 0.5f, Collectable_Spawn_Y);

        new_obstacle.transform.parent = transform;
        new_obstacle.transform.position = pos;

    }
}
