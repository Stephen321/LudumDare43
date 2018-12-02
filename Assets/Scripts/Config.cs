using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour {

    public float Speed;
    public float collectable_spawn_time;

    public float GetSpeed()
    {
        return Speed;
    }

    public float GetCollectableSpawnTime()
    {
        return collectable_spawn_time;
    }
}
