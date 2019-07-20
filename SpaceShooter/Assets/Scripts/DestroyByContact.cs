using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{

    public GameObject explosion;

    public GameObject playerExplode;
    private GameController gameController;


    private void Start() 
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();

            gameController.AddScore(0);
        }

        else
        {
            Debug.Log("Cannot find gameController");
        }

    }


    private void OnTriggerEnter(Collider other) 
    {
        
        if(other.tag == "Boundary")
        {
            return;
        }

        if(other.tag == "Player")
        {
            Instantiate(playerExplode, transform.position, transform.rotation);
            gameController.GameOver();
        }

        Instantiate(explosion, transform.position, transform.rotation);

        gameController.AddScore(1);


        //destroys whatever other thing is touched
        Destroy(other.gameObject);
        //destroys THIS thing touched
        Destroy(gameObject);
        
    }
}
