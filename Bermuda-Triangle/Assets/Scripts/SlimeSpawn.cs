using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawn : MonoBehaviour {

    public GameObject Slime;
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Random.Range(0f, 100f) < 1)
        {
            Instantiate(Slime, gameObject.transform.position, Quaternion.identity);
        }
	}
}
