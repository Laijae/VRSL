using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Data;

public class ProgressLevel : MonoBehaviour
{

    //scripts
    public DBM dbm;
    public User user;

    public Login login;

    //variables
    private int lessonsTaken = 0;
    private int signsCompleted = 0;
    private int lessonsMastered = 0;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateDatabase(List<string> summary)
    {
        //get current progress levels
        List<int> currentProgress = dbm.readProgress(login.GetID());
        print(String.Join(",", currentProgress.ToString()));

        int cLessonsTaken = Convert.ToInt32((currentProgress[0]));
        int cSignsCompleted = Convert.ToInt32((currentProgress[1]));
        int cLessonsMastered = Convert.ToInt32((currentProgress[2]));     

        //update database with new progress level values 
        //additionally, changing the script variables to the new values
        lessonsTaken = cLessonsTaken + 1;
        dbm.updateTable("progress", "lessonsTaken", lessonsTaken, login.GetID());

        signsCompleted = cSignsCompleted + summary.Count;
        dbm.updateTable("progress", "signsCompleted", signsCompleted, login.GetID());

        if (summary.Count == 26)
        {
            lessonsMastered = cLessonsMastered + 1;
            dbm.updateTable("progress", "LessonsMastered", lessonsMastered, login.GetID());
        }


    }

    public void DisplayStats()
    {
        //display stats
        //printing to console but could appear as text box on screen?





    }

}
