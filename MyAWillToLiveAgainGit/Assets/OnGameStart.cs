using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class OnGameStart : MonoBehaviour
{
     private string documentsPath;
    public string playerSpawnPosFile;

    private void Awake()
    {
        // find Documents of system
        documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    }
    private void OnEnable()
    {
        //ON load game create a folder with json from saving player pos if it doesn't exist
        //else do nothing
        createSavePoint();

    }

    private void Update()
    {
        //End game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            File.Delete(playerSpawnPosFile);
            Application.Quit();
        }
    }


    private void createSavePoint()
    {
  
        // Test if folder exist ( called  AWTLA/Cache)

        string testPath = documentsPath + "\\AWTLA\\Cache";

        if (!Directory.Exists(testPath))
        {
            Directory.CreateDirectory(testPath);
        }
        else
        {
            Debug.Log("Directory AWTLA\\Cache Already exist");
        }


        testPath += "\\SavePoint.json";

        if (!File.Exists(testPath))
        {
            //Create Json
            Player_SpawnPos firstSpawn = new Player_SpawnPos();
            firstSpawn.PosX = -6.05f;
            firstSpawn.PosY = -6.03f;

            //Serializer
            string json = JsonUtility.ToJson(firstSpawn);

            File.WriteAllText(testPath, json);

        }
        else
        {
            Debug.Log("File  SavePoint.json  Already exist");
        }


        playerSpawnPosFile = testPath;

    }
}
