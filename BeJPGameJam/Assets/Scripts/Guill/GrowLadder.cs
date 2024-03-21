using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowLadder : MonoBehaviour
{
    // others Components
    [SerializeField] private SpriteRenderer spriteRenderer;
    private BoxCollider2D _thisCollider;
    [SerializeField] private BoxCollider2D childrenCollider;
    private PlayerMovement _playerMovement;
    private PlayerPowers _playerPowers;
    // locale variables :
    private bool _isInRange;
    [SerializeField] private bool grewUp;
    private float _offsetXGrow = -0.8621142f;
    [SerializeField] private float offsetYGrow = 0.7791089f;
    private float _sizeXGrow = 0.9863048f;
    [SerializeField] private float sizeYGrow = 6.015755f;
    
    [SerializeField] private Sprite Sprite1;
    [SerializeField] private Sprite Sprite2;
    [SerializeField] private Sprite Sprite3;
    void Awake()
    {
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        _playerPowers = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPowers>();
        _thisCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (grewUp)
        {
            if (_isInRange 
                && _playerMovement._isClimbing 
                && _playerMovement.rbPlayer.velocity.x < 0.3f 
                && (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.D)))
            {
                _playerMovement._isClimbing = false;
                childrenCollider.isTrigger = false;
            }
        
            if (_isInRange && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.S)))
            {
                _playerMovement._isClimbing = true;
                childrenCollider.isTrigger = true;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (grewUp)
        {
            if (col.CompareTag("Player"))
            {
                _isInRange = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log("stay");
        if (col.gameObject.CompareTag("Player") && _playerPowers.waterPowerCurrentlyActive && !grewUp)
        {
            
            _thisCollider.offset = new Vector2(_offsetXGrow, offsetYGrow);
            _thisCollider.size = new Vector2(_sizeXGrow, sizeYGrow);
            StartCoroutine(GrowTree());
            grewUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (grewUp)
        {
            if (other.CompareTag("Player"))
            {
                _isInRange = false;
                _playerMovement._isClimbing = false;
                childrenCollider.isTrigger = false;
            }
        }
    }
    
    private IEnumerator GrowTree()
    {
        yield return new WaitForSeconds(1f);
        spriteRenderer.sprite = Sprite1;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = Sprite2;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = Sprite3;
    }
}
