using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //take the current position = new position (0,5,0) at start of game
        transform.position = new Vector3(0,5,0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward);
    }
}
