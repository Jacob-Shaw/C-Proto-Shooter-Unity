using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variable parts:
    //public or private
    //data type (int, float, bool, string)
    //name of variable
    //optional value assigned, otherwise initialized to 0.


    [SerializeField]
    private float _speed = 12f;   //do not forget the "f" to designate as a float
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private float _firingDelay = 0.2f;
    private float _lastBulletFire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager; //This will match what is in Update(); it will be ther piece you
    //are accessing in the other object


    // Start is called before the first frame update
    void Start()
    {
        //take the current position = new position (0,0,0)
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>(); //access a function of an object

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
    }



    // Update is called once per frame, 60 times per second
    void Update()
    {
        PlayerMovement();


         if (Input.GetKeyDown(KeyCode.Space) && Time.time > _lastBulletFire)
        {
            FireBullet();
        }

    }



    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Least effective code
        //float forwardInput = Input.GetAxis("Mouse Y");
        //transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput);
        //transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalInput);

        //More optimal code, place only one line of code
        //transform.Translate(new Vector3(horizontalInput,verticalInput,0) * Time.deltaTime * _speed);

        //Even more optimal code, creating a local variable for the Player's direction and using that in the equation (easier to reimplement and change if needed)
        Vector3 playerDirection = new Vector3(horizontalInput, verticalInput, 0);

        //This is a vector for the player to follow
        transform.Translate(playerDirection * _speed * Time.deltaTime);

        
        //Boundary Variables for Player Area
        float topBoundary = 0;
        float bottomBoundary = -3.8f;
        float leftBoundary = -11;
        float rightBoundary = 11;

        //Variables to control top and bottom boundaries
        Vector3 moveToTop = new Vector3(transform.position.x, topBoundary, 0);
        Vector3 moveToBottom = new Vector3(transform.position.x, bottomBoundary, 0);

        //Variables to control Wrapping left to right and right to left
        Vector3 moveToLeft = new Vector3(leftBoundary, transform.position.y, 0);
        Vector3 moveToRight = new Vector3(rightBoundary, transform.position.y, 0);

        //Variable for Alternative top and bottom range
        //the Mathf.Clamp is giving a range for the y value and eliminating a need for the if/else statement below
        Vector3 topAndBottomRange = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, bottomBoundary, topBoundary), 0);

        //Variables for one way wrap
        //Vector3 moveToTopLeftCorner = new Vector3(leftBoundary, topBoundary, 0);
        //Vector3 moveToBottomLeftCorner = new Vector3(leftBoundary, bottomBoundary, 0);

        /*
        //If a player reaches the top or bottom boundary, do not let them go further in that direction
        if (transform.position.y >= topBoundary)
        {
            transform.position = moveToTop;
        }
        else if(transform.position.y <= bottomBoundary)
        {
            transform.position = moveToBottom;
        }
        */



        //Alternative top and bottom range
        transform.position = topAndBottomRange;



        /* Allow to wrap right to left but block left to right
        if (transform.position.x > rightBoundary || transform.position.x < leftBoundary )
        {
            if (transform.position.y >= topBoundary)
            {
                transform.position = moveToTopLeftCorner;
                
            }
            else if(transform.position.y <= bottomBoundary)
            {
                transform.position = moveToBottomLeftCorner;
            }
            else
            transform.position = moveToLeft;
        }
        */

        //If a player reaches a left or right boundary then wrap to opposite boundary
        
        if (transform.position.x > rightBoundary)
        {
            transform.position = moveToLeft;
        }
        else if (transform.position.x < leftBoundary)
        {
            transform.position = moveToRight;
        }


    }

    void FireBullet()
    {

        Vector3 bulletOrigin = new Vector3(transform.position.x, transform.position.y + 2f, 0);
        

        //If I hit SPACE then spawn game object Bullet
       
            Debug.Log("BANG!");
            Instantiate(_bulletPrefab, bulletOrigin, Quaternion.identity);
            _lastBulletFire = Time.time + _firingDelay;


    }

    public void Damage()
    {
        //Decrement lives
        _lives --;

        //Destroy Player
        if( _lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

}
