using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    //private Text interactUI;
    private bool isInRange;

    Animator _animator;
    [SerializeField] private bool firePower;
    [SerializeField] private bool earthPower;
    [SerializeField] private bool windPower;
    [SerializeField] private bool waterPower;
    private PlayerPowers _playerPowers;
    private PlayerMovement _playerMovement;
    private ScoreManager _scoreManager;
    
    

    void Awake()
    {
       // interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        _playerPowers = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPowers>();
        _scoreManager = GameObject.FindWithTag("ScoreManager").GetComponent<ScoreManager>();
        _animator = GetComponent<Animator>();
        int cmp = 0;
        if (firePower) cmp++;
        if (earthPower) cmp++;
        if (windPower) cmp++;
        if (waterPower) cmp++;
        if (cmp > 1)
        {
            firePower = false;
            earthPower = false;
            waterPower = false;
            windPower = false;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && isInRange)
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        _animator.SetTrigger("OpenChest");
        GetComponent<BoxCollider2D>().enabled = false;
        //interactUI.enabled = true;
        if (firePower)
        {
            _playerPowers.firePowerActive = true;
            _scoreManager.firePowerActive = true;
        }

        if (earthPower)
        {
            _playerPowers.earthPowerActive = true;
            _scoreManager.earthPowerActive = true;
        }
    
        if (waterPower) {
            _playerPowers.waterPowerActive = true;
            _scoreManager.waterPowerActive = true;
        }

        if (windPower)
        {
            _playerMovement.doubleJumpPower = true;
            _scoreManager.windPowerActive = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           // interactUI.enabled = false;
            isInRange = false;
        }
    }
}
