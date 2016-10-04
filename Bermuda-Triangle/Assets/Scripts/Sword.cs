using UnityEngine;
using System.Collections;

/**
 * Created by Daniel Resio
 * handels sword movement
 **/
public class Sword : MonoBehaviour {
    private int rotation = 20;

	// Update is called once per frame
	void Update () {
        if(!(rotation > 0))
            Destroy(gameObject);
        gameObject.transform.Rotate(0, 0, -2);
        rotation--;
  	}
}
