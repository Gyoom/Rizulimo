using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    // power status
    [SerializeField] public bool earthPowerActive;
    [SerializeField] public bool firePowerActive;
    [SerializeField] public bool waterPowerActive;
    // projectiles RB
    [SerializeField] private Rigidbody2D fireBall;
    [SerializeField] private Rigidbody2D earthquake;
    // others Components
    private Animator _playerAnimator;
    [SerializeField] private Transform fireSpawnRight;
    [SerializeField] private Transform fireSpawnLeft;
    private PlayerMovement _playerMovement;
    // speeds powers
    [SerializeField] private float fireSpeed;
    [SerializeField] private float earthSpeed;
    // local variables
    private bool _flipPlayer;
    private bool _isGrounded;
    [HideInInspector] public bool waterPowerCurrentlyActive;
    public static GameObject instance;

    private void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        if (instance == null)
        {
            instance = gameObject;
        }


        _playerMovement = GetComponent<PlayerMovement>();
        _playerAnimator = GetComponent<Animator>();
    }
    

    void Update()
    {

        _isGrounded = _playerMovement.isGrounded;
        if (earthPowerActive && Input.GetKeyDown(KeyCode.E) && _isGrounded)
        {
            Debug.Log("earth");
            StartCoroutine(Jab());
        }

        if (firePowerActive && Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("fire");
            StartCoroutine(Fire());
        }
        
        if (waterPowerActive && Input.GetKeyDown(KeyCode.R) && _isGrounded)
        {
            waterPowerCurrentlyActive = true;
            Debug.Log("water");
            StartCoroutine(Water());
        }
        
    }

    private IEnumerator Fire()
    {
        _playerAnimator.SetTrigger("FirePower");
        yield return new WaitForSeconds(0.1f);
        FireBall();
    }
    
    private IEnumerator Jab()
    {
        _playerAnimator.SetTrigger("EarthPower");
        yield return new WaitForSeconds(0.4f);
        Earthquake();
    }
    
    private IEnumerator Water()
    {
        _playerAnimator.SetTrigger("WaterPower");
        yield return new WaitForSeconds(0.4f);
        waterPowerCurrentlyActive = false;
    }
    
    private void FireBall()
    {
        _flipPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX;
        if (!_flipPlayer)
        {
            var p = Instantiate(fireBall, fireSpawnRight.position, fireSpawnRight.rotation);
            p.AddForce(new Vector2(fireSpeed, 0f));
        }
        else
        {
            var p = Instantiate(fireBall, fireSpawnLeft.position, fireSpawnLeft.rotation);
            p.AddForce(new Vector2(-fireSpeed, 0f));
        }
    }
    
    private void Earthquake()
    {
        _flipPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX;
        if (!_flipPlayer)
        {
            var p = Instantiate(earthquake, fireSpawnRight.position, fireSpawnRight.rotation);
            p.AddForce(new Vector2(earthSpeed, 0f));
        }
        else
        {
            var p = Instantiate(earthquake, fireSpawnLeft.position, fireSpawnLeft.rotation);
            p.AddForce(new Vector2(-earthSpeed, 0f));
        }
    }
}
