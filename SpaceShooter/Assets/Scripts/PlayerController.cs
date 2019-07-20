using UnityEngine;
using System.Collections;

//this makes the serial of variables visible in inspector
[System.Serializable]
public class Boundary
{
    //declares this class for cleaning up inspector
     public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
     public float speed;
     public float tilt;
     public Boundary boundary;

     private Rigidbody rb;

     public Transform shotSpawn;
     public GameObject shot;
     private float nextFire;
     public float fireRate;
     private AudioSource playerAudio;

     private void Start()
     {
          rb = GetComponent<Rigidbody>();
          playerAudio = GetComponent<AudioSource>();
     }


     private void Update() 
     {
         if ((Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space)) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

            playerAudio.Play();
        }
     }

     void FixedUpdate()
     {
          float moveHorizontal = Input.GetAxis("Horizontal");
          float moveVertical = Input.GetAxis("Vertical");

          Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
          rb.velocity = movement * speed;

          rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 0.0f, Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));

            //banks the player along the x axis when moving left to right
          rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
     }
}