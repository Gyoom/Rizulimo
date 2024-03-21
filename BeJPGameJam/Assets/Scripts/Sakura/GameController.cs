using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Globalization;
using AudioManager;

public class GameController : MonoBehaviour
{
    public GameObject[] notes;
    private float[] _timing;
    private int[] _lineNum;

    public string filePass;
    private int _notesCount = 0;

    private AudioSource _audioSource;
    private float _startTime = 0;

    public float timeOffset = -1;

    private bool _isPlaying = false;
    public GameObject startButton;
    private Button _buttonComponent;

    public Text scoreText;
    private int _score = 0;
    [SerializeField] public int speedNote;

    [SerializeField, Range(0, 3)]public int stageNum;

    [SerializeField] protected bool isEnd = false;

	public SceneReference nextScene;
    

    // Start is called before the first frame update
    void Start()
    {
        _buttonComponent = startButton.GetComponent<Button>();
        _buttonComponent.onClick.AddListener(StartGame);
        _timing = new float[1024];
        _lineNum = new int[1024];
        LoadCSV ();
    }

    public void StartGame(){
        startButton.SetActive (false);
        _startTime = Time.time;
        if(stageNum == 0){
            BGMManager.Instance.Play(
                audioPath: BGMPath.WIND_MINI,
                volumeRate: 1,
                delay: 0,
                pitch: 1,
                isLoop: false,
                allowsDuplicate: false
            );
        }
        else if(stageNum == 1){
            BGMManager.Instance.Play(
                audioPath: BGMPath.WATER_MINI,
                volumeRate: 1,
                delay: 0,
                pitch: 1,
                isLoop: false,
                allowsDuplicate: false
            );
        }
        else if(stageNum == 2){
            BGMManager.Instance.Play(
                audioPath: BGMPath.EARTH_MINI,
                volumeRate: 1,
                delay: 0,
                pitch: 1,
                isLoop: false,
                allowsDuplicate: false
            );
        }
        else if(stageNum == 3){
            BGMManager.Instance.Play(
                audioPath: BGMPath.FIRE_MINI,
                volumeRate: 1,
                delay: 0,
                pitch: 1,
                isLoop: false,
                allowsDuplicate: false
            );
        }
        _isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlaying) {
            CheckNextNotes ();
            scoreText.text = "Score : " + _score;
            EndGame();
        }
    }
    
    void CheckNextNotes()
    {
        //Debug.Log( _notesCount + ") " + _timing [_notesCount]);
        while (_timing [_notesCount] + timeOffset < GetMusicTime() && _timing [_notesCount] != 0) {
            SpawnNotes (_lineNum[_notesCount]);
            _notesCount++;
        }
        
    }

    void SpawnNotes(int num){
        //Debug.Log("Spawn");
        Instantiate (notes[num], 
            new Vector3 (-4.0f + (2.0f * num), 10.0f, 0),
            Quaternion.identity);
    }

    void LoadCSV(){

        TextAsset csv = Resources.Load<TextAsset>(filePass);
        StringReader reader = new StringReader (csv.text);

        int i = 0;
        while (reader.Peek () > -1) {

            string line = reader.ReadLine ();
            string[] values = line.Split (',');
            _timing [i] = float.Parse( values [0], CultureInfo.InvariantCulture);
            _lineNum [i] = int.Parse( values [1]);
            i++;
        }
    }

    float GetMusicTime(){
        return Time.time - _startTime;
    }

    public void GoodTimingFunc(int num){
        //Debug.Log ("Line:" + num + " good!");
        //Debug.Log (GetMusicTime());

        SEManager.Instance.Play(SEPath.JUMP);
        _score++;
    }

    void EndGame(){
        if (BGMManager.Instance.IsPlaying() == false) {
            Debug.Log("End");
            isEnd = true;
            SceneManager.LoadSceneAsync(nextScene);
        }
    }
}
