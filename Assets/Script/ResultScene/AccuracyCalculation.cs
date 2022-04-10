using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccuracyCalculation : MonoBehaviour
{
    private int allNote;
    private float accuracy;
    public Text AccuracyText;
    public Text GradeText;
    void Start()
    {
        allNote = ButtonCheck.PerfectS + ButtonCheck.GreatS + ButtonCheck.GoodS + ButtonCheck.MissS;
    }

    // Update is called once per frame
    void Update()
    {
        accuracy = ((ButtonCheck.PerfectS + (ButtonCheck.GreatS / 2.0f) - ButtonCheck.GoodS - ButtonCheck.MissS) / (float)allNote) * 100.0f;       
        if (ButtonCheck.SceneName == "Tun_Scene")
        {
            PlayerPrefs.SetFloat("Accuracy1", accuracy);
        }
        else if (ButtonCheck.SceneName == "SecondSong_Scene")
        {
            PlayerPrefs.SetFloat("Accuracy2", accuracy);
        }
        AccuracyText.text = "Accuracy: " + accuracy.ToString("F2") + "%";
        Grade();
    }

    void Grade()    
    {
        if (accuracy > 95.0f) 
        {
            GradeText.text = "Grade : S";
            if(ButtonCheck.SceneName == "Tun_Scene") 
            {
                PlayerPrefs.SetString("Grade1", "S");
            }
            else if (ButtonCheck.SceneName == "SecondSong_Scene") 
            {
                PlayerPrefs.SetString("Grade2", "S");
            }
        }
        else if (accuracy > 90.0f)
        {
            GradeText.text = "Grade : A";
            if (ButtonCheck.SceneName == "Tun_Scene")
            {
                PlayerPrefs.SetString("Grade1", "A");
            }
            else if (ButtonCheck.SceneName == "SecondSong_Scene")
            {
                PlayerPrefs.SetString("Grade2", "A");
            }
        }
        else if (accuracy > 80.0f)
        {
            GradeText.text = "Grade : B";
            if (ButtonCheck.SceneName == "Tun_Scene")
            {
                PlayerPrefs.SetString("Grade1", "B");
            }
            else if (ButtonCheck.SceneName == "SecondSong_Scene")
            {
                PlayerPrefs.SetString("Grade2", "B");
            }
        }
        else if (accuracy > 70.0f)
        {
            GradeText.text = "Grade : C";
            if (ButtonCheck.SceneName == "Tun_Scene")
            {
                PlayerPrefs.SetString("Grade1", "C");
            }
            else if (ButtonCheck.SceneName == "SecondSong_Scene")
            {
                PlayerPrefs.SetString("Grade2", "C");
            }
        }
        else if (accuracy > 60.0f)
        {
            GradeText.text = "Grade : D";
            if (ButtonCheck.SceneName == "Tun_Scene")
            {
                PlayerPrefs.SetString("Grade1", "D");
            }
            else if (ButtonCheck.SceneName == "SecondSong_Scene")
            {
                PlayerPrefs.SetString("Grade2", "D");
            }
        }
        else if (accuracy < 60.0f)
        {
            GradeText.text = "Grade : F";
            if (ButtonCheck.SceneName == "Tun_Scene")
            {
                PlayerPrefs.SetString("Grade1", "F");
            }
            else if (ButtonCheck.SceneName == "SecondSong_Scene")
            {
                PlayerPrefs.SetString("Grade2", "F");
            }
        }
    }
}
