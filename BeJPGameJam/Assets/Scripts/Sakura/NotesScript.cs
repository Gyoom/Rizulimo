using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesScript : MonoBehaviour
{
    public int lineNum;
    private GameController _gameController;
    private bool isInLine = false;
    private KeyCode _lineKey;
    private int _speedNote;

    public GameObject DelEffect;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
        _lineKey = GameUtil.GetKeyCodeByLineNum(lineNum);
        _speedNote = GameObject.Find ("GameController").GetComponent<GameController>().speedNote;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.down * 5 * Time.deltaTime;
        if (this.transform.position.y < -5.0f) {
            Debug.Log("missed");
            Destroy (this.gameObject);
        }

        if(isInLine){
            CheckInput(_lineKey);
        }
    }

    void OnTriggerEnter (Collider other) {
        isInLine = true;
    }

    void OnTriggerExit (Collider other) {
        isInLine = false;
    }

    void CheckInput (KeyCode key)
    {
        Debug.Log(_lineKey);
        if (Input.GetKeyDown (key)) {
            _gameController.GoodTimingFunc (lineNum);
            Destroy (this.gameObject);
            GenerateEffect();
        }
    }

    void GenerateEffect()
    {
        GameObject effect = Instantiate(DelEffect) as GameObject;
        effect.transform.position = gameObject.transform.position;
    }
}
