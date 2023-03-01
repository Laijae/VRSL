using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    //scripts
    public Login Login;
    public DBM dbm;
    public ProgressLevel progressLevel; 


    //variables

    private string id = null;
    private string fname = null;
    private string surname = null;
    private string email = null;
    private string region = "asl"; //regional language variation, by default will be ASL unless configure to support other languages
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public IEnumerator assignVariablesToDbValues()
    {
        //waits for login validation to be true, so the value in id will be correct and definite 
        yield return new WaitUntil(() => Login.idValidated == true);
        List<string> userdata = dbm.readUser(Login.GetID());
        id = Login.GetID();
        fname = userdata[0];
        surname = userdata[1];
        email = userdata[2];
        region = userdata[3];
    
    }

    public void setName(string input)
    {
        name = input;
    }

    public string getName()
    {
        return name;
    }

    public void setRegion(string input)
    {
        region = input;
    }

    public string getRegion()
    {
        return region;
    }


}
