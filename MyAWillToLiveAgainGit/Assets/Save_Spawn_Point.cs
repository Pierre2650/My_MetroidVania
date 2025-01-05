using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Save_Spawn_Point : MonoBehaviour
{
    public OnGameStart OnGameStart;


    private void OnEnable()
    {
        //load current save point position from json
        string PosFile = OnGameStart.playerSpawnPosFile;
        Player_SpawnPos spawnPos = deserialize(PosFile);

        // Put it on player position
        this.transform.position = new Vector3(spawnPos.PosX, spawnPos.PosY);
    }

    private Player_SpawnPos deserialize(string jsonPath)
    {

        if (Path.GetExtension(jsonPath) == ".json")
        {

            // Read File.
            string info;
            info = File.ReadAllText(jsonPath);

            //Deserialize
            Player_SpawnPos temp = JsonUtility.FromJson<Player_SpawnPos>(info);

            return temp;

        }
        else
        {
            Debug.Log("file is not json");
            return null;
        }


        
        
    }
}



