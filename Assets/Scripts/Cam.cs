using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {

    public GameObject player;
    public Vector3 cam_offset;
    public Config Config;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!Config.Player_Must_Fly)
        {
            transform.position = new Vector3(player.transform.position.x + cam_offset.x, cam_offset.y, cam_offset.z);
        }
    }
}
