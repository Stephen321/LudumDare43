using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class ObstacleSpawner : MonoBehaviour {

    public new Camera camera;
    public Config Config;
    public IceSpawner Ice_Spawner;
    public float Spawn_Offset = 2;
    public GameObject  Spikes;
    public GameObject Collectable;

    public float Min_Spawn_Time = 3.0f;
    public float Collectable_Spawn_Y = 4.0f;
    public float Min_Collectable_Spawn_Time = 1.5f; //need a minimum so they spawn far enough apart
    private float next_spawn_timer;
    private bool spawn_collectable;
    
    private float time_running;

    // Use this for initialization
    void Start ()
    {
        time_running = .0f;
        next_spawn_timer = Min_Spawn_Time;
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
            float first_right = first.transform.position.x + (first.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f);

            if (first_right < cam_left)
            {
                Destroy(first);
            }
        }


        if (Ice_Spawner.GetIceGaps() == 0)
        {
            if (!spawn_collectable && next_spawn_timer > Min_Spawn_Time)
            {
                float cam_right = camera.ViewportToWorldPoint(new Vector3(1, 0.5f, camera.nearClipPlane)).x;
                next_spawn_timer = .0f;
                Spawn(cam_right + Spawn_Offset);
            }
            if (spawn_collectable && next_spawn_timer > Min_Collectable_Spawn_Time)
            {
                float cam_right = camera.ViewportToWorldPoint(new Vector3(1, 0.5f, camera.nearClipPlane)).x;
                SpawnCollectable(cam_right + Spawn_Offset);
                spawn_collectable = false;
                Debug.Log("spawn collectable!");
                next_spawn_timer = .0f;
                time_running = .0f;
            }
        }

        time_running += Time.deltaTime;
        next_spawn_timer += Time.deltaTime;
    }

    void Spawn(float x)
    {

        GameObject new_obstacle = GameObject.Instantiate(Spikes);
        SpriteRenderer new_sprite = new_obstacle.GetComponent<SpriteRenderer>();
        if (Random.value < 0.5f)
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
