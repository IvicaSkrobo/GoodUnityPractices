using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEditor;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    public static SaveData SaveData;
    private static string filePath;


    BinaryFormatter binaryFormatter = new BinaryFormatter();

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "save.text");
    }

    private void Start()
    {
        Load();
    }

    void Save()
    {
        var json = JsonUtility.ToJson(SaveData);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            binaryFormatter.Serialize(stream, EncryptDecrypt(json));


        }
    }

    void Load()
    {
        if (!File.Exists(filePath))
        {
            SaveData = new SaveData()
            {
                lastUsedChar = CharacterTypes.Archer,
                unlockedCharacters = new List<CharacterTypes>() { CharacterTypes.Archer },
                level = 1,
                playerName = "Anonymous",
                Music = true,
                Vibrate = true

            };
           
            Save();
        }

       

        using (var stream = new FileStream(filePath, FileMode.Open))
        {
            var stringSavedData = (string)binaryFormatter.Deserialize(stream);

            SaveData = JsonUtility.FromJson<SaveData>(EncryptDecrypt(stringSavedData));

        }
    }

    [MenuItem("Developer/Delete Save")]
    public static void DeleteSave()
    {
        if (!File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }


    private static string EncryptDecrypt(string textToEncrypt)
    {
        var inSb = new StringBuilder(textToEncrypt);
        var outSb = new StringBuilder(textToEncrypt.Length);
        for (var i = 0; i < textToEncrypt.Length; i++)
        {
            var c = inSb[i];
            c = (char)(c ^ 129);  //it moves the char by 129 places, there are 128 chars, so this brings it back to normal char after decrypting
            outSb.Append(c);
        }
        return outSb.ToString();
    }


}

public enum CharacterTypes
{
    Archer,
    Warior,
    Mage
}


[Serializable]
public class SaveData
{
    public CharacterTypes lastUsedChar;
    public List<CharacterTypes> unlockedCharacters;
    public int level;
    public string playerName;
    public bool Music;
    public bool Vibrate;
}