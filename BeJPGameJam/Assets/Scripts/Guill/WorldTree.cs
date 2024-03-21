using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTree : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private ScoreManager _scoreManager;

    [SerializeField] private Sprite tree1;
    [SerializeField] private Sprite tree2;
    [SerializeField] private Sprite tree3;
    [SerializeField] private Sprite tree4;
    void Start()
    {
        _scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        int cmpt = 0;
        if (_scoreManager.windPowerActive) cmpt++;
        if (_scoreManager.earthPowerActive) cmpt++;
        if (_scoreManager.firePowerActive) cmpt++;
        if (_scoreManager.waterPowerActive) cmpt++;

        if (cmpt == 1)
        {
            _spriteRenderer.sprite = tree1;
            transform.position += new Vector3(-0.058f, -0.198f);
        }
        if (cmpt == 2)
        {
            _spriteRenderer.sprite = tree2;
            transform.position += new Vector3(0.008f, 1.351f);
        }
        if (cmpt == 3)
        {
            _spriteRenderer.sprite = tree3;
            transform.position += new Vector3(0f, 1.972f);
        }
        if (cmpt == 4)
        {
            _spriteRenderer.sprite = tree4;
            transform.position += new Vector3(-0.5f, 1.982f);
        }
    }

}
