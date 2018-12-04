using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour {

    public Camera camera;
    public float collectable_spawn_time;
    public IceSpawner iceSpawner;
    public bool Game_Won;
    public bool Player_Must_Fly;
    public int Min_Dist_To_Fly;
    public float Player_Must_Fly_X;
    public bool NoObstacles { get; set; }

    void Start()
    {
        Player_Must_Fly = false;
        Player_Must_Fly_X = 0;
    }

    public float GetCollectableSpawnTime()
    {
        return collectable_spawn_time;
    }

    void Update()
    {
        if (Game_Won && NoObstacles && iceSpawner.GetIceGaps() == 0)
        {
            Player_Must_Fly_X = camera.ViewportToWorldPoint(new Vector3(1, 0.5f, camera.nearClipPlane)).x + Min_Dist_To_Fly;
            Player_Must_Fly = true;
        }
    }
}
