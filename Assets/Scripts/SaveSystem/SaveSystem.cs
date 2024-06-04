using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    string filePath;

    public string saveFileName;

    public static SaveSystem instance;

    private void Awake()
    {
        // Construct the file path using Application.persistentDataPath
        filePath = Path.Combine(Application.persistentDataPath, saveFileName + ".saveData");
        Debug.Log("File Path: " + filePath); // Add this line to debug log the file path

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame(GameData saveData)
    {
        // Create the directory if it doesn't exist
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        // Create or overwrite the file
        using (FileStream dataStream = new FileStream(filePath, FileMode.Create))
        {
            BinaryFormatter converter = new BinaryFormatter();
            converter.Serialize(dataStream, saveData);
        }
    }

    public GameData LoadGame()
    {
        // Check if the file exists before attempting to load
        if (File.Exists(filePath))
        {
            // Open the file and deserialize the data
            using (FileStream dataStream = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter converter = new BinaryFormatter();
                GameData saveData = converter.Deserialize(dataStream) as GameData;
                return saveData;
            }
        }
        else
        {
            Debug.LogError("Save file not found in " + filePath);
            return null;
        }
    }
}
