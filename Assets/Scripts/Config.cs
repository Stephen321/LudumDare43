using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour {
    
    public float collectable_spawn_time;

    public float GetCollectableSpawnTime()
    {
        return collectable_spawn_time;
    }
}
