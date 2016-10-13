using UnityEngine;
using System.Collections;

/**
 * Created by Daniel Resio
 * handels sword movement
 **/
public class Sword : MonoBehaviour {

    #region variables
    //default variable settings
    private int rotation = 20;
    #endregion

    // Update is called once per frame
    public void Update () {
        if(!(rotation > 0))
            Destroy(gameObject);
        gameObject.transform.Rotate(0, 0, -2);
        rotation--;
  	}
}
