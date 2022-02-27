using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCheck : MonoBehaviour
{
    private List<NoteObject> NoteList;
    private NoteObject noteObj;

    public GameObject noteParticle;

    public SkillEasy EZ;
    // For Setting KeyCode on Button
    public KeyCode Up = KeyCode.W;
    public KeyCode Down = KeyCode.S;
    public KeyCode Left = KeyCode.A;
    public KeyCode Right = KeyCode.D;

    //Tracking score and combo in game
    public static int Score = 0;
    public static int Combo = 0;
    //Tracking grade during the game
    public static int PerfectS = 0;
    public static int GreatS = 0;
    public static int GoodS = 0;
    public static int MissS = 0;
    public static float Skill = 0.0f;

    public bool NoteWrong;
   
    public AudioSource AS;

    public Text comboText;
    public Text scoreText;
    //display what grade player press
    public Text Gdisplay;

    void Start()
    {
        // Initialize List
        NoteList = new List<NoteObject>();
        // For Debugging (!!Create circle for distance detection!!)
        //DrawCircle(0.5f, 0.05f);
        //DrawCircle(1.0f, 0.05f);
        //DrawCircle(2.0f, 0.05f);

        //Setting Text Combo & Score to 0
        comboText.text = "Combo : " + Combo.ToString();
        scoreText.text = "Score : " + Score.ToString();
        Combo = 0;
        Score = 0;
        PerfectS = 0;
        GreatS = 0;
        GoodS = 0;
        MissS = 0;
        PlayerPrefs.SetInt("highcombo", Combo);
        SkillBar.AmountSkill = 0f;
    }

    void Update()
    {
        noteObj = currentNote();
        // If currentNote is null then return
        if (currentNote() == null)
        {
            return;
        }

        if (Input.GetKeyDown(Up) || Input.GetKeyDown(Down) ||
            Input.GetKeyDown(Left) || Input.GetKeyDown(Right))
        {
            // If User Hit Note then Destroy Object
            if (Input.GetKeyDown(noteObj.keyToPress))
            {
                DistantCheck dc = noteObj.GetComponent<DistantCheck>();
                AS.Play();
                if (dc.distance <= 0.5)
                {
                    Gdisplay.text = "Perfect!!";
                    Skill += 0.1f;
                    Combo += 1;
                    PerfectS++;
                    Score += 1000;
                    SkillBar.AmountSkill += 5f;
                }
                else if (dc.distance <= 1)
                {
                    Gdisplay.text = "Great";
                    Skill += 0.075f;
                    Combo += 1;
                    GreatS++;
                    Score += 500;
                    SkillBar.AmountSkill += 3f;
                }
                else if (dc.distance <= 2)
                {
                    Gdisplay.text = "Good";
                    Skill += 0.05f;
                    Combo = 0;
                    GoodS++;
                    Score += 300;
                    SkillBar.AmountSkill += 1f;
                }
            }
            else
            {
                Miss();
            }
            deleteNote(noteObj);
            // Delete Note Object
            Instantiate(noteParticle, noteObj.transform.position, Quaternion.identity);
            Destroy(noteObj.gameObject);
        }
        if(Combo > PlayerPrefs.GetInt("highcombo")) 
        {
            PlayerPrefs.SetInt("highcombo", Combo);
        }
        Skill = (Skill >= 1.0f ? 1.0f : Skill);
        comboText.text = "Combo : " + Combo.ToString();
        scoreText.text = "Score : " + Score.ToString();
        
    }
    public void Miss() 
    {
        MissS++;
        NoteWrong = true;
        if (EZ.isskill == false)
        {
            Gdisplay.text = "Miss";
            Combo = 0;
        }
        if (EZ.isskill == true) 
        {
            Gdisplay.text = "Perfect!!";
            Combo += 1;
            Score += 1000;
            EZ.skillamount--;
        }
    }

    // Add note to NoteList
    public void addNote(NoteObject note)
    {
        NoteList.Add(note);
        NoteList.Sort(SortByPosX);
    }

    // Delete note that are on the front
    public void deleteNote(NoteObject noteObj)
    {
        // If NoteList is empty -> return
        if (NoteList.Count == 0)
        {
            return;
        }

        // If Peek of NoteList is noteObj then delete from List
        if (NoteList[0] == noteObj)
        {
            NoteList.RemoveAt(0);
        }
    }

    // For checking what note is on the front
    public NoteObject currentNote()
    {
        // If NoteList is empty -> return null
        if (NoteList.Count == 0)
        {
            return null;
        }

        return NoteList[0];
    }
    static int SortByPosX(NoteObject a, NoteObject b)
    {
        return a.transform.position.x.CompareTo(b.transform.position.x);
    }
    // Function for create circle (!!For distance detection!!)
    private void DrawCircle(float radius, float lineWidth)
    {
        GameObject obj = new GameObject("Circle with radius : " + radius);
        obj.GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, -1.0f);

        int segments = 360;
        LineRenderer line = obj.AddComponent<LineRenderer>();

        line.useWorldSpace = false;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = segments + 1;

        int pointCounts = segments + 1;
        Vector3[] points = new Vector3[pointCounts];

        for (int i = 0; i < pointCounts; i++)
        {
            float rad = Mathf.Deg2Rad * (i * 360.0f / segments);
            points[i] = new Vector3(Mathf.Cos(rad) * radius, Mathf.Sin(rad) * radius, 0.0f);
        }
        line.SetPositions(points);
    }
}