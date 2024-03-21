using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVWriter : MonoBehaviour
{
    public string fileName;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WriteCSV(string txt){
        StreamWriter streamWriter;
        FileInfo fileInfo;
        fileInfo = new FileInfo (Application.dataPath +"/"+ fileName + ".csv");
        streamWriter = fileInfo.AppendText ();
        streamWriter.WriteLine (txt);
        streamWriter.Flush();
        streamWriter.Close ();
    }
}
