using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // Create a field for the save file.
    string saveFile;

    // Create a GameData field.
    public GameData gameData;

    void Awake()
    {
        // Update the path once the persistent path exists.
        gameData = new GameData();
        saveFile = Application.persistentDataPath + "/gamedata.json";
    }

    [Button]
    public void readFile()
    {
        // Does the file exist?
        if (File.Exists(saveFile))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);

            // Deserialize the JSON data 
            //  into a pattern matching the GameData class.
            gameData = JsonUtility.FromJson<GameData>(fileContents);
            Debug.Log(gameData.didTakeThirdRellic);
        }
    }
    [Button]
    public void writeFile()
    {
        // Serialize the object into JSON and save string.
        string jsonString = JsonUtility.ToJson(gameData);

        // Write JSON to file.
        File.WriteAllText(saveFile, jsonString);
    }
}
