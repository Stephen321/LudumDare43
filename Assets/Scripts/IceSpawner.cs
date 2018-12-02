using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class IceSpawner : MonoBehaviour {

    public new Camera camera;
    public Config Config; //for children that want to prevent obstacle spawning
    public float Spawn_Offset = 2;
    public GameObject Ice;
    public GameObject Ice_Gap;
    public float Ice_Gap_Chance;

    private int ice_gaps;
    
	// Use this for initialization
	void Start ()
    {
        ice_gaps = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float cam_left = camera.ViewportToWorldPoint(new Vector3(0, 0.5f, camera.nearClipPlane)).x;
        float cam_right = camera.ViewportToWorldPoint(new Vector3(1, 0.5f, camera.nearClipPlane)).x;

        Assert.IsTrue(transform.childCount >= 1, "Always should be at least 1 child object at any time!");

        GameObject first = transform.GetChild(0).gameObject;
        float first_right = first.transform.position.x + (first.GetComponent<BoxCollider2D>().bounds.size.x * 0.5f);

        if (first_right < cam_left)
        {
            if (first.tag == "IceGap")
            {
                ice_gaps--;
            }
            Destroy(first);
        }

        GameObject last = transform.GetChild(transform.childCount - 1).gameObject;
        float last_right = last.transform.position.x + (first.GetComponent<BoxCollider2D>().bounds.size.x * 0.5f);
        float spawn_left = cam_right + Spawn_Offset;
        if (last_right < spawn_left)
        {
            Spawn(last, last_right);
        }
    }

    void Spawn(GameObject last, float x)
    {
        //set up new ice platform position and parent
        GameObject new_ice;
        if (Random.value <= Ice_Gap_Chance)
        {
            new_ice = GameObject.Instantiate(Ice_Gap);
        }
        else
        {
            new_ice = GameObject.Instantiate(Ice);
        }
        BoxCollider2D new_collider = new_ice.GetComponent<BoxCollider2D>();
        float new_width = new_collider.bounds.size.x;
        Vector3 pos = new_ice.transform.position;
        pos.x = x + (new_width * 0.5f);
        new_ice.transform.parent = transform;
        new_ice.transform.position = pos;

        if (new_ice.tag == "IceGap")
        {
            ice_gaps++;
        }

    }

    public int GetIceGaps()
    {
        return ice_gaps;
    }
}
