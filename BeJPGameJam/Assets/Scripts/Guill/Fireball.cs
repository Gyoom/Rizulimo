using System;
using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((1 << col.gameObject.layer | groundLayer) == groundLayer )
        {
            StartCoroutine(DestroyFireBall());
        }
        
    }
    
    private IEnumerator DestroyFireBall()
    { ;
        yield return new WaitForSeconds(0.01f);
        Destroy(gameObject);
    }
}
