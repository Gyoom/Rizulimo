using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadSpecificScene : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite openDoorSprite;
    [SerializeField] private String sceneName;
    [SerializeField] private bool mustPossessEarthPower;
    [SerializeField] private bool mustPossessFirePower;
    [SerializeField] private bool mustPossessWaterPower;
    [SerializeField] private bool mustPossessWindPower;
    [SerializeField] private GameObject[] torches;
    
    private bool _allTorchesLit;
    private bool _playerPossessEarthPower;
    private bool _playerPossessFirePower;
    private bool _playerPossessWaterPower;
    private bool _playerPossessWindPower;
    private Animator _fadeSystem;
    private PlayerMovement _playerMovement;
    private PlayerPowers _playerPower;
    private SelectMovement _selectMovement;
    private bool _actualyOpen;
    

    private void Awake()
    {
        _fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        _playerPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPowers>();
        _selectMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<SelectMovement>();
        _allTorchesLit = torches.Length == 0;
    }

    private void Update()
    {
        if (!_allTorchesLit)
            CheckTorches();

        if (!_actualyOpen)
        {
            if (mustPossessEarthPower && !_playerPower.earthPowerActive) return;

            if (mustPossessFirePower && !_playerPower.firePowerActive) return;

            if (mustPossessWaterPower && !_playerPower.waterPowerActive) return;

            if (mustPossessWindPower && !_playerMovement.doubleJumpPower) return;

            if (!_allTorchesLit) return;

            spriteRenderer.sprite = openDoorSprite;
            _actualyOpen = true;
        }
    }

    private void CheckTorches()
    {
        foreach (var torch in torches)
        {
            Flammable flammable = torch.GetComponent<Flammable>();
            if (!flammable.inflamed)
            {
                return;
            }
        }
        _allTorchesLit = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        if (mustPossessEarthPower && !_playerPower.earthPowerActive) return;
        if (mustPossessFirePower && !_playerPower.firePowerActive) return;
        if (mustPossessWaterPower && !_playerPower.waterPowerActive) return;
        if (mustPossessWindPower && !_playerMovement.doubleJumpPower) return;
        if (!_allTorchesLit) return;

        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        _fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        _selectMovement.ChangeMovement(sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
