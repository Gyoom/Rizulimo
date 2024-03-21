using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManager;

public class NotesTimingMaker : MonoBehaviour
{
    public GameObject startButton;

    private float _startTime = 0;
    [SerializeField]
    private CSVWriter _CSVWriter;
    private bool _isPlaying = false;

    [SerializeField, Range(0, 3)]public int stageNum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlaying) {
            DetectKeys ();
        }
    }

    public void StartMusic(){
        startButton.SetActive (false);
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
        _startTime = Time.time;
        _isPlaying = true;
    }

    void DetectKeys(){
        if (Input.GetKeyDown (KeyCode.D)) {
            WriteNotesTiming (0);
        }

        if (Input.GetKeyDown (KeyCode.F)) {
            WriteNotesTiming (1);
        }

        if (Input.GetKeyDown (KeyCode.G)) {
            WriteNotesTiming (2);
        }

        if (Input.GetKeyDown (KeyCode.H)) {
            WriteNotesTiming (3);
        }

        if (Input.GetKeyDown (KeyCode.J)) {
            WriteNotesTiming (4);
        }
    }

    void WriteNotesTiming(int num){
        Debug.Log (GetTiming ());
        _CSVWriter.WriteCSV (GetTiming ().ToString () + "," + num.ToString());
    }

    float GetTiming(){
        return Time.time - _startTime;
    }
}
