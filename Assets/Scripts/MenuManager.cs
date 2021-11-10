using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public InputField userNameInput;
    public string userName;
    public Text maxScore;
    public int scoreText;
    public string bestPlayerText;

    
    

    private void Awake()
    {
        LoadPlayerData();
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        userNameInput.text = userName;
        maxScore.text = $"Best Score: {bestPlayerText} : {scoreText}";

    }

    

    

    

    
    public void StartGame()
    {
        userName = userNameInput.text;
        SavePLayerData();
        SceneManager.LoadScene(1);
    }  
    

    public void QuitGame()
    {        
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); //original code to quit Unity Player
#endif

    }

    [System.Serializable]
    class SaveData
    {
        public string userName;
        public string bestPlayerText;
        public int scoreText;
    }

    public void SavePLayerData()
    {
        SaveData data = new SaveData();
        data.userName = userName;
        data.bestPlayerText = bestPlayerText;
        data.scoreText = scoreText;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savescore.json", json);
    }

    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/savescore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            userName = data.userName;
            bestPlayerText = data.bestPlayerText;
            scoreText = data.scoreText;
        }
    }
}
