using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System.Linq;
using System.Data;
using Mono.Data.Sqlite;
using System;

public class IndexInput : MonoBehaviour
{

    public SteamVR_Action_Skeleton LeftSkeleton = null;





    public List<string> signQuery(string query)
    {
       string conn = "URI=file:" + Application.dataPath + "/VRSLdatabase.db"; //Path to database.
       IDbConnection dbconn;
       dbconn = (IDbConnection)new SqliteConnection(conn);
       dbconn.Open(); //Open connection to the database.
       IDbCommand dbcmd = dbconn.CreateCommand();

       dbcmd.CommandText = query;
       IDataReader reader = dbcmd.ExecuteReader();


       List<string> signData = new List<string>();
       while (reader.Read())
       {
           string thumb = reader.GetString(0);
           print("thumb data: " + thumb);
        //    string index = reader.GetString(1);
        //    string middle = reader.GetString(2);
        //    string ring = reader.GetString(3);
        //    string pinky = reader.GetString(4);
        //    signData.Add(thumb);
        //    signData.Add(index);
        //    signData.Add(middle);
        //    signData.Add(ring);
        //    signData.Add(pinky);

       }

       reader.Close();
       reader = null;
       dbcmd.Dispose();
       dbcmd = null;

       dbconn.Close();
       dbconn = null;

       //print(string.Join(",", signData));

       return signData;
    }

    void databaseRead()
    {
            string conn = "URI=file:" + Application.dataPath + "/VRSLdatabase.db"; //Path to database.
            IDbConnection dbconn;
            dbconn = (IDbConnection) new SqliteConnection(conn);
            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT * " + "FROM Sign";
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                string signName = reader.GetString(0);
                string thumb = reader.GetString(1);
                string index = reader.GetString(2);
                string middle = reader.GetString(3);
                string ring = reader.GetString(4);
                string pinky = reader.GetString(5);

            
                Debug.Log( "thumb= "+thumb+"  index ="+index+"  pinky ="+  pinky);
                print( "thumb= "+thumb+"  index ="+index+"  pinky ="+  pinky);
            
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            dbconn = null;
    }
    





    private void Start()
    {
        // string query = "SELECT thumb FROM Sign WHERE letter = 'A'";
        // List<string> signData = signQuery(query);
        // string signString  = string.Join(",",signData);
        // print("signdata: " + signString);

        databaseRead();
    }

    void Update()
    {
        databaseRead();
        //if (SignforA())
        //{
        //    print("A DETECTED");
        //}
        
        //GetHandData(LeftSkeleton.fingerCurls);

        
    }

    

    bool SignforA()
    {      

        // This condition is raw data comparison
        // Once the data for each hand sign is stored, it can be used as a condition instead 
        // checking the LeftSkeleton.fingerCurls array TO the array of data held of x letter

        if(LeftSkeleton.thumbCurl < 0.2 && LeftSkeleton.indexCurl > 0.8 && LeftSkeleton.middleCurl > 0.8 && LeftSkeleton.ringCurl > 0.8 && LeftSkeleton.pinkyCurl > 0.8) 
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }




    


    private List<string> GetHandData(float[] fingerCurls)
    {
        List<string> handData = new List<string>();

        for (int i = 0; i < 5; i++)
        {
            switch (fingerCurls[i])
            {
                case float value when fingerCurls[i] >= 0.7:
                    handData.Add("D");
                    break;

                case float value when fingerCurls[i] > 0.3 && fingerCurls[i] < 0.7:
                    handData.Add("M");
                    break;

                case float value when fingerCurls[i] < 0.3:
                    handData.Add("U");
                    break;
            }
        }

        print(string.Join(",", handData));
        return handData;
    }




}
