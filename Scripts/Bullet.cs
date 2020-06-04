using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _bulletSpeed = 8f;


    // Update is called once per frame
    void Update()
    {
        //Translate lazer up
        transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);

        if (transform.position.y > 8f )
        {
            Object.Destroy(this.gameObject);   // or use  Destroy(this.gameObject)
        }
    }
}
