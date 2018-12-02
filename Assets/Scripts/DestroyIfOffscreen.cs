using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfOffscreen : MonoBehaviour {

    public float X;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (transform.position.x < X)
        {
            Destroy(gameObject);
        }	
	}
}
