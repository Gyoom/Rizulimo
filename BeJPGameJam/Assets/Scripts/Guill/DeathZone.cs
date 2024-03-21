using System;
using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    private Transform _playerSpawn;
    private Animator _fadeSystem;
    private void Awake()
    {
        _playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        _fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(ReplacePlayer(col));
        }
    }
    
    public IEnumerator ReplacePlayer(Collider2D col)
    {
        _fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        col.transform.position = _playerSpawn.position;
    }
}
