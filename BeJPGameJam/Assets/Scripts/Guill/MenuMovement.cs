using UnityEngine;

public class MenuMovement : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    [SerializeField]
    public bool run;

    [SerializeField] public float speed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        speed = Input.GetAxis("Horizontal");
        if (run && speed > 0.5)
        {
            _spriteRenderer.flipX = false;
            _animator.SetFloat("Speed", 1);
        } else if (run && speed < -0.5)
        {
            _spriteRenderer.flipX = true;
            _animator.SetFloat("Speed", 1);
        }
        else if (!run)
        {
            _animator.SetFloat("Speed", 0);
        }
        
    }
}
