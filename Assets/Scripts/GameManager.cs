using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject inputFieldText;
    [SerializeField] GameObject canvasToPersistence;
    public Text bestScoreText;
    public string nameText = "RecordName";

    public int scorePoint = 0;

    /*public Text score1;
    public Text score2;
    public Text score3;
    public int scorePoint1 = 0;
    public int scorePoint2 = 0;
    public int scorePoint3 = 0;*/


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(canvasToPersistence);

        LoadBestScoreDatas();
    }

    void Start()
    {
        BestScoreSetter();
        //TopScoresSetter();

    }

    public void BestScoreSetter()
    {
        bestScoreText.text = "Best Score : " + nameText + " : " + scorePoint;
    }

    /*public void TopScoresSetter()
    { 
        score1.text = "1st Score : " + scorePoint;
        score2.text = "1st Score : " + scorePoint2;
        score2.text = "1st Score : " + scorePoint3;
    }*/

    public void UserNameSetter()
    {
        nameText = inputFieldText.GetComponent<TMP_InputField>().text;
    }

    [System.Serializable]
    class SaveData
    {
        public string nameText;
        public int scorePoint;

        //public int scorePoint2;
        //public int scorePoint3;
    }

    public void SaveUserDatas()
    {
        SaveData datas = new SaveData();
        datas.nameText = nameText;
        datas.scorePoint = scorePoint;

        //datas.scorePoint2 = scorePoint2;
        //datas.scorePoint3= scorePoint3;

        string json = JsonUtility.ToJson(datas);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        Debug.Log("Datas Saved!");
    }

    public void LoadBestScoreDatas()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData datas = JsonUtility.FromJson<SaveData>(json);

            nameText = datas.nameText;
            scorePoint = datas.scorePoint;

            //scorePoint2= datas.scorePoint2;
            //scorePoint3 = datas.scorePoint3;

            Debug.Log("Best Score Datas Restored!");
        }
    }
}