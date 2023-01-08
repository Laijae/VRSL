using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;


public class Lesson : MonoBehaviour
{

    //assign a public variable to the Controller script
    public Controller Controller;
    public string lessonToken = null;
    public DBM dbm;
   



   //readSign will probably be moved to DBM.cs once dbm functionality is completed 
   //readsign will ask the database for the sign for the given letter

    

    protected void startAlphabet()
    {
        

        string[] alphabet = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
        List<string> summary = new List<string>();
        

        // creating a lesson summary
        // check sign for each letter
        // if correct, add to summary
        // if incorrect, don't add to summary
        // display summary at end of lesson
        for(int i = 0; i < alphabet.Length; i++)
        {
            //get sign from database
            string query = alphabet[i].ToLower();
            List<string> dbPos = dbm.readSign(query);
            Debug.Log($"Place your hands in this position for {alphabet[i]}: " + string.Join(",", dbPos));

            //get handdata from controller
            List<string> handPos = Controller.GetHandData(); 

            //compare
            bool correct = checkSign(dbPos, handPos);

            //
            if(correct)
            {
                summary.Add(alphabet[i]);
            }
            

        }

        Debug.Log("Summary, here are the signs you learnt: " + string.Join(",", summary));
        
       
    }

    

    
    protected bool checkSign(List<string> dbPos, List<string> handPos)
    {
        bool correct = true;
        for(int i = 0; i < dbPos.Count; i++)
        {
            if(dbPos[i] != handPos[i])
            {
                correct = false;
                break; // stop checking the lists if we've already found a mismatch
            }
        }
        
        // print the appropriate message depending on whether the two lists are equal
        if (correct)
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Incorrect!");
        }
        
        return correct;

    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //check if lessonToken has been set eah frame, if it has, start the lesson with the specific token chosen
        if(lessonToken != null)
        {
            switch (lessonToken)
            {
                case "Alphabet":
                    startAlphabet();
                    break;
                //as other lessons are added, add more cases here
                
                //case "Numbers":
                //    startNumbers();
                //    break;

            }
            lessonToken = null;
        }
        
    }

    

    
}
