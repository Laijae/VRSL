using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class DBM : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    // a method to read the sign table in the database
    public List<string> readSign(string inputQuery)
    {
        // initialising list that will be returned containing all the data for the finger positions.
        List<string> dbPos = new List<string>();

        string conn = "URI=file:" + Application.dataPath + "/signDB.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();

        
        string sqlQuery = $"SELECT T,I,M,R,P,S FROM Sign WHERE letter = '{inputQuery}'";
        dbcmd.CommandText = sqlQuery;

        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            //get data
            string thumb = reader.GetString(0);
            string index = reader.GetString(1);
            string middle = reader.GetString(2);
            string ring = reader.GetString(3);
            string pinky = reader.GetString(4);
            string swing = reader.GetString(5);
                       
            //add to list
            dbPos.Add(thumb);
            dbPos.Add(index);
            dbPos.Add(middle);
            dbPos.Add(ring);
            dbPos.Add(pinky);
            dbPos.Add(swing);

        }
        // closing connection to database
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

        return dbPos;
    
    }

    public List<string> readLogin(string inputQuery)
    {
        // initialising list that will be returned containing all the data for the login credentials.
        List<string> login = new List<string>();

        string conn = "URI=file:" + Application.dataPath + "/signDB.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();

        //query to get the username and password for the user
        string sqlQuery = $"SELECT un,pass FROM Login WHERE UserID = '{inputQuery}'";
        dbcmd.CommandText = sqlQuery;

        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            //get data
            string un = reader.GetString(0);
            string pass = reader.GetString(1);
                                
            //add to list
            login.Add(un);
            login.Add(pass);
            
        }
        // closing connection to database
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

        return login;
    
    }

    public List<string> readUser(string inputQuery)
    {
        // initialising list that will be returned containing all the data for the user credentials.
        List<string> userData = new List<string>();

        string conn = "URI=file:" + Application.dataPath + "/signDB.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();

        //query to get the username and password for the user
        string sqlQuery = $"SELECT fname,surname,email,Region FROM User WHERE UserID = '{inputQuery}'";
        dbcmd.CommandText = sqlQuery;

        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            //get data
            string fname = reader.GetString(0);
            string surname = reader.GetString(1);
            string email = reader.GetString(2);
            string region = reader.GetString(3);
                                
            //add to list
            userData.Add(fname);
            userData.Add(surname);
            userData.Add(email);
            userData.Add(region);

            
        }
        // closing connection to database
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

        return userData;
    
    }

    public List<int> readProgress(string inputQuery)
    {
        // initialising list that will be returned containing all the data for the user credentials.
        List<int> progressData = new List<int>();

        string conn = "URI=file:" + Application.dataPath + "/signDB.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();

        //query to get the username and password for the user
        string sqlQuery = $"SELECT lessonsTaken,signsCompleted,LessonsMastered FROM Progress WHERE UserID = '{inputQuery}'";
        dbcmd.CommandText = sqlQuery;

        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            //get data
            int lessonsTaken = reader.GetInt32(0);
            int signsCompleted = reader.GetInt32(1);
            int lessonsMastered = reader.GetInt32(2);
                                
            //add to list
            progressData.Add(lessonsTaken);
            progressData.Add(signsCompleted);
            progressData.Add(lessonsMastered);

            
        }
        // closing connection to database
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

        return progressData;
    
    }




    // a method to update the any table in the database
    public void updateTable(string table, string column, int value, string userID)
    {
        

        string conn = "URI=file:" + Application.dataPath + "/signDB.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();


        //updating table is restricted to the user's own data
        //therefore the userID is used to identify the user
        
        string sqlQuery = $"UPDATE {table} SET {column} = {value} WHERE UserID = '{userID}'";
        dbcmd.CommandText = sqlQuery;


        
        //execute the query
        IDataReader reader = dbcmd.ExecuteReader();
        
   
        // closing connection to database
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

        
    
    }





}
