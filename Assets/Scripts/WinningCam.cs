using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningCam : MonoBehaviour {

    public Transform player;
    public Vector3 cam_offset;
    public float Camera_Y_Follow;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 new_pos = new Vector3(-3.11f, player.position.y + cam_offset.y, cam_offset.z);
        if (player.position.x > -8.17)
        {
            new_pos.x = player.position.x + cam_offset.x;
        }
        transform.position = new_pos;
    }
}
