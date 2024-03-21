using UnityEngine;

public class Flammable : MonoBehaviour
{
    [HideInInspector]
    public bool inflamed;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite inflamedSprite;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out Fireball f) && !inflamed)
        { 
            spriteRenderer.sprite = inflamedSprite;
            inflamed = true;
        }
    }
}
