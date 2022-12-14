using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;


public class inputScr : MonoBehaviour
{
    void readSign()
    {
     string conn = "URI=file:" + Application.dataPath + "/signDB.db"; //Path to database.
     IDbConnection dbconn;
     dbconn = (IDbConnection) new SqliteConnection(conn);
     dbconn.Open(); //Open connection to the database.
     IDbCommand dbcmd = dbconn.CreateCommand();
     string sqlQuery = "SELECT T,I,M,R,P FROM Sign WHERE letter = 'a'";
     dbcmd.CommandText = sqlQuery;
     IDataReader reader = dbcmd.ExecuteReader();
     while (reader.Read())
     {
         string thumb = reader.GetString(0);
         string index = reader.GetString(1);
         string middle = reader.GetString(2);
         string ring = reader.GetString(3);
         string pinky = reader.GetString(4);
         Debug.Log("thumb = "+thumb);
     }
     reader.Close();
     reader = null;
     dbcmd.Dispose();
     dbcmd = null;
     dbconn.Close();
     dbconn = null;
    }
    
    

    // Start is called before the first frame update
    void Start()
    {
        readSign();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
