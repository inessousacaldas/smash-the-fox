using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighscoreManager : MonoBehaviour {

    [SerializeField]
    private Text namesText;
    [SerializeField]
    private Text scoresText;
    [SerializeField]
    private InputField inputName;
    private HighScores scores;
    

    public InputField InputName
    {
        get
        {
            return inputName;
        }

        set
        {
            inputName = value;
        }
    }


    // Use this for initialization
    void Start () {

        namesText.text = "";
        scoresText.text = "";
        InputName.interactable = false;
        InputName.gameObject.SetActive(false);

    }
	

    public void showScores()
    {

        namesText.gameObject.SetActive(true);
        scoresText.gameObject.SetActive(true);
        namesText.text = "";
        scoresText.text = "";

        if (!System.IO.File.Exists(Application.persistentDataPath + "/scores"))
        {
            scores = new HighScores();
        }
        else
        {
            scores = FileManager.ReadFromBinaryFile<HighScores>(Application.persistentDataPath + "/scores");
        }

        for (int i = 0; i < scores.getScores().Count; i++)
        {
            namesText.text += scores.getScores()[i].Key + "\r\n";
        }

        for (int i = 0; i < scores.getScores().Count; i++)
        {
            scoresText.text += scores.getScores()[i].Value.ToString() + "\r\n";
        }

        FileManager.WriteToBinaryFile(Application.persistentDataPath + "/scores", scores);
    }

    public void hideScores()
    {
        namesText.gameObject.SetActive(false);
        scoresText.gameObject.SetActive(false);
        namesText.text = "";
        scoresText.text = "";

    }

    public void addScore(int points)
    {
       
        string nameString;

        if (InputName.text != "")
            nameString = InputName.text;
        else
            nameString = "Foxy Lady";

        if (!System.IO.File.Exists(Application.persistentDataPath + "/scores"))
        {
            scores = new HighScores();
        }
        else
        {
            scores = FileManager.ReadFromBinaryFile<HighScores>(Application.persistentDataPath + "/scores");
        }
        if (scores.getScores().Count < 3)
        {
            scores.addScore(nameString, points);
            scores.getScores().Sort(scores.SortByScore);
        }
        else if (points > scores.getScores()[2].Value)
        {
            scores.addScore(nameString, points);
            scores.getScores().Sort(scores.SortByScore);
            scores.getScores().RemoveAt(3);
        }

        InputName.gameObject.SetActive(false);
        InputName.interactable = false;


        FileManager.WriteToBinaryFile(Application.persistentDataPath + "/scores", scores);

        showScores();

    }

}
