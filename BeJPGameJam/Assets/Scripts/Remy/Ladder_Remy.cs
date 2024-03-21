using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder_Remy : MonoBehaviour
{
    private bool isInRange;
    private PlayerMovement playerMovement; //a modifier lorsqu'on repasse sur le script PlayerMovement et plus PlayerMovement_Remy
    public BoxCollider2D topCollider;

    void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(); //modifier le nom en fonction du script playerMovement
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange && playerMovement._isClimbing && (Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)))
        {
            playerMovement._isClimbing = false;
            topCollider.isTrigger = false;
            return;
        }

        if (isInRange && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)))
        {
            playerMovement._isClimbing = true;
            topCollider.isTrigger = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            playerMovement._isClimbing = false;
            topCollider.isTrigger = false;
        }
    }
}
