using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubManager : MonoBehaviour
{
    [SerializeField] MenuMovement player;

    //Unlock level
    public bool levelVoid;
    public bool levelWater;
    public bool levelAir;
    public bool levelEarth;
    public bool levelFire;
    //Fleche
    [SerializeField] GameObject fleLvlVoid;
    [SerializeField] GameObject fleLvlWater;
    [SerializeField] GameObject fleLvlEarth;
    [SerializeField] GameObject fleLvlFire;
    [SerializeField] GameObject fleLvlAir;
    //game manager
    ScoreManager scoreManager;

    //SCene level
    [SerializeField] private String levelVoid_Scene, levelWater_Scene, levelAir_Scene, levelEarth_Scene, levelFire_Scene;

    //planet

    [SerializeField] GameObject planet;
    float planetRotation;

    int planetPosition;
    float planeteWantedRotation;
    [SerializeField] float speedRotation;
    bool canChange,canSelect;

    // Start is called before the first frame update
    void Start()
    {
        
         
        planetPosition = 1;
        planetRotation = 0;
        canChange = true;

        fleLvlVoid.SetActive(false);
        fleLvlWater.SetActive(false);
        fleLvlEarth.SetActive(false);
        fleLvlFire.SetActive(false);
        fleLvlAir.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MenuMovement>();
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();

        if (scoreManager != null) levelWater = scoreManager.waterPowerActive;

        levelEarth = scoreManager.earthPowerActive;
        levelFire = scoreManager.firePowerActive;
        if (scoreManager != null) levelAir = scoreManager.windPowerActive;
    }

    // Update is called once per frame
    void Update()
    {
        if(!fleLvlVoid.active && levelVoid) fleLvlVoid.SetActive(true);
        if(!fleLvlWater.active && levelWater) fleLvlWater.SetActive(true);
        if(!fleLvlEarth.active && levelEarth) fleLvlEarth.SetActive(true);
        if(!fleLvlFire.active && levelFire) fleLvlFire.SetActive(true);
        if(!fleLvlAir.active && levelAir) fleLvlAir.SetActive(true);

        if (canChange && Input.GetAxis("Horizontal") !=0)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                changelvl(1);
            }
            else if(Input.GetAxis("Horizontal") < 0)
            {
                changelvl(-1);
            }
        }
        
        player.run = !canChange;

        if (planetRotation > 360) planetRotation = planetRotation - 360;
        if (planetRotation < 0) planetRotation = planetRotation + 360;
        if (planeteWantedRotation > 360) planeteWantedRotation = planeteWantedRotation - 360;
        if (planeteWantedRotation < 0) planeteWantedRotation = planeteWantedRotation + 360;

        if (planetRotation < planeteWantedRotation - 0.25f)
        {
            if(Mathf.Abs(planetRotation - planeteWantedRotation) < 180)
            {
                planetRotation = planetRotation + Time.deltaTime * speedRotation;
            }
            else
            {
                planetRotation = planetRotation - Time.deltaTime * speedRotation;
            }
            
        }
        else if(planetRotation > planeteWantedRotation + 0.25f)
        {
            if (Mathf.Abs(planetRotation - planeteWantedRotation) < 180)
            {
                planetRotation = planetRotation - Time.deltaTime * speedRotation;
            }
            else
            {
                planetRotation = planetRotation + Time.deltaTime * speedRotation;
            }
        }
        else
        {
            canChange = true;
            canSelect = true;
        }
        planet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, planetRotation));

        if(canSelect && Input.GetAxis("Vertical") > 0.1)
        {
            levelSelection();
        }

    }

    void changelvl(int a)
    {
        if(a == 1)
        {
            planetPosition++;
            if (planetPosition > 6)
            {
                planetPosition = 1;
            }
        }
        else
        {
            planetPosition--;
            if (planetPosition < 1)
            {
                planetPosition = 6;
            }
        }
        planeteWantedRotation = 360 / 6 * (planetPosition - 1);
        canChange = false;
        canSelect = false;

    }


    void levelSelection()
    {
        if(levelVoid && planetPosition == 2)
        {
            SceneManager.LoadScene(levelVoid_Scene);
        }
        else if(levelAir && planetPosition == 3)
        {
            SceneManager.LoadScene(levelAir_Scene);
        }
        else if (levelWater && planetPosition == 4)
        {
            SceneManager.LoadScene(levelWater_Scene);
        }
        else if (levelEarth && planetPosition == 6)
        {
            SceneManager.LoadScene(levelEarth_Scene);
        }
        else if (levelFire && planetPosition == 5)
        {
            SceneManager.LoadScene(levelFire_Scene);
        }
    }
}
