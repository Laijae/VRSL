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

        
        string sqlQuery = $"SELECT T,I,M,R,P FROM Sign WHERE letter = '{inputQuery}'";
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
                       
            //add to list
            dbPos.Add(thumb);
            dbPos.Add(index);
            dbPos.Add(middle);
            dbPos.Add(ring);
            dbPos.Add(pinky);

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

    // a method to update the any table in the database
    public void updateTable(string table, string column, string value, string userID)
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
