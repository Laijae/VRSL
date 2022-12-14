using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;


public class Lesson : MonoBehaviour
{
   
    protected List<string> readSign(string query)
    {
        List<string> dbPos = new List<string>();

        string conn = "URI=file:" + Application.dataPath + "/signDB.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();

        // Default working 
        //  string sqlQuery = "SELECT T,I,M,R,P FROM Sign WHERE letter = 'a'";
        // dbcmd.CommandText = sqlQuery;

        dbcmd.CommandText = query;

        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            //get data
            string thumb = reader.GetString(0);
            string index = reader.GetString(1);
            string middle = reader.GetString(2);
            string ring = reader.GetString(3);
            string pinky = reader.GetString(4);
            Debug.Log("thumb = "+thumb);
            
            //add to list
            dbPos.Add(thumb);
            dbPos.Add(index);
            dbPos.Add(middle);
            dbPos.Add(ring);
            dbPos.Add(pinky);


        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

        return dbPos;
    
    }

    private void startAlphabet()
    {
        

        //string array of the alphabet to iterate through the lesson
        List<string> alphabet = new List<string>(26);
        for(int i = 0; i < alphabet.Count; i++)
        {
            alphabet.Add(((char)(i + 65)).ToString());
        }

        // creating a lesson summary
        // check sign for each letter
        // if correct, add to summary
        // if incorrect, don't add to summary
        // display summary at end of lesson
        List<string> summary = new List<string>();
        for(int i = 0; i < alphabet.Count; i++)
        {
            //get sign from database
            string query = $"SELECT T,I,M,R,P FROM Sign WHERE letter = '{alphabet[i]}'";
            List<string> dbPos = readSign(query);

            //get hand data temporarily until link between scripts is set up
            List<string> handPos = new List<string>{"u","d","d","d","d"};
            //List<string> handPos = newController.GetHandData(); 

            //compare
            bool correct = checkSign(dbPos, handPos);

            //display result
            if(correct)
            {
                Debug.Log("Correct!");
                summary.Add(alphabet[i]);
            }
            else
            {
                Debug.Log("Incorrect!");
            }

            Debug.Log("Summary, here are the signs: " + summary);
        }

        
       
    }
    
    bool checkSign(List<string> dbPos, List<string> handPos)
    {
        bool correct = true;
        for(int i = 0; i < dbPos.Count; i++)
        {
            if(dbPos[i] != handPos[i])
            {
                correct = false;
            }
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
        
    }

    

    
}
