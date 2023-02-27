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
   
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //check if lessonToken has been set each frame, if it has, start the lesson with the specific token chosen
        if(lessonToken != null)
        {
            switch (lessonToken)
            {
                case "Alphabet":
                    StartCoroutine(startAlphabet());
                    break;
                //as other lessons are added, add more cases here
                
                //case "Numbers":
                //    startNumbers();
                //    break;

            }
            lessonToken = null;
        }
        
    }

    protected IEnumerator startAlphabet()
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
            
            //prompt user to complete sign
            Debug.Log($"Place your hands in this position for {alphabet[i]}: " + string.Join(",", dbPos));  
            if(dbPos[5] != "S")
            {
                print("Remember to swing your hand!");
            }      

            //Initiate a timer for each sign to be performed
            for(int timer = 1500; timer > 0; timer--)
            {
                //get current handdata from controller
                List<string> handPos = Controller.GetHandData(); 
                //run it through check function
                if(checkSign(handPos, dbPos) && checkSwing(dbPos))
                {
                    print("Correct!");
                    summary.Add(alphabet[i]);
                    break;//break timer loop if correct and go to next sign
                }
                yield return null;//wait a frame, this syncs the for loop to the framerate               
            }


        }

        Debug.Log("Summary, here are the signs you learnt: " + string.Join(",", summary));
        
       
    }

    
    //
    // CHECKING METHODS
    //
    
    protected bool checkSign(List<string> dbPos, List<string> handPos)
    {
        bool correct = true;
        
        for(int i = 0; i < 5; i++)
        {
            if(dbPos[i] != handPos[i])
            {
                correct = false;
                break; // stop checking the lists if we've already found a mismatch
            }
        }
        return correct;
    }

    protected bool checkSwing(List<string> dbPos)
    {
        string dbSwing = dbPos[5].ToLower();
        if(dbSwing == "S")//if the sign requires no swing movement
        {
            return true;
        }
        else if(dbSwing.Length == 1) // if the sign requires only one swing
        {   
            if(ReadControllerSwing(dbSwing) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        else //if the sign requires multiple swing movements
        {
            string[] swingArray = dbSwing.Split(',');//converts the multiple movements to an array of strings
            for(int i = 0; i < swingArray.Length; i++)//iterates through the array
            {
                if(ReadControllerSwing(swingArray[i]) == false)//if any of the swings are incorrect, return false
                {
                    return false;
                }
            }
            return true;//if the entire loop runs without returning false, all swings must be true 
        }
    }

    public bool ReadControllerSwing(string direction)
    {
       
    
        switch (direction)
        {
            case "u":
                if(Controller.up == true)
                {
                    return true;
                }
                break;
            case "d":
                if(Controller.down == true)
                {
                    return true;
                }
                break;
            case "l":
                if(Controller.left == true)
                {
                    return true;
                }
                break;
            case "r":
                if(Controller.right == true)
                {
                    return true;
                }
                break;
            case "f":
                if(Controller.forward == true)
                {
                    return true;
                }
                break;
            case "b":
                if(Controller.backward == true)
                {
                    return true;
                }
                break;
            default:
                return false;
        }
        return false;
    }
    

    
}




    

    

