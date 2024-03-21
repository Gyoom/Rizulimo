using System;
using System.Collections;
using System.Collections.Generic;
// using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaveBreaker : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask blockingLayer;
    [SerializeField] private LayerMask groundLayer;
    private bool _isGrounded;
    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.5f, 0.2f), groundLayer);
        if (!_isGrounded)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((1 << col.gameObject.layer | groundLayer) == groundLayer )
        {
            Destroy(gameObject);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, new Vector2(0.5f, 0.2f));
    }
    
}
