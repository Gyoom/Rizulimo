using UnityEngine;

public class Ladder : MonoBehaviour
{

    private bool _isInRange;
    private PlayerMovement _playerMovement;
    [SerializeField]
    private BoxCollider2D collider;
    void Awake()
    {
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (_isInRange 
            && _playerMovement._isClimbing 
            && _playerMovement.rbPlayer.velocity.x < 0.3f 
            && (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.D)))
        {
            _playerMovement._isClimbing = false;
            collider.isTrigger = false;
        }
        
        if (_isInRange && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.S)))
        {
            _playerMovement._isClimbing = true;
            collider.isTrigger = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isInRange = false;
            _playerMovement._isClimbing = false;
            collider.isTrigger = false;
        }
    }
}
