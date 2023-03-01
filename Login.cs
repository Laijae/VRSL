using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    //scripts
    public DBM dbm;

    //inputs
    public InputField usernameField;
    public InputField passwordField;
    public InputField idField;

    //validation
    public bool validated = false;
    public bool unValidated = false;
    public bool passValidated = false;

    public bool idValidated = false;

    //variables
    private string username = null;
    private string password = null;
    private string id = null;

    
    private string email = null;

    
    // Start is called before the first frame update
    void Start()
    {
        //start coroutine to wait for login validation
        StartCoroutine(Validate());
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public string GetID()
    {
        return id;
    }

    public void idCheck(bool active)
    {
        // Load id screen
        idField.gameObject.SetActive(active);      
    }

    public void loginScreen(bool active)
    {      
       
        
        // Load login screen
        usernameField.gameObject.SetActive(active);
        passwordField.gameObject.SetActive(active);
    }



    public IEnumerator Validate()
    {
        while(!idValidated)
        {
            //wait until the id field is filled
            yield return new WaitUntil(() => id != null);

            if (ValidateID())
            {
                idField.gameObject.SetActive(false);
                idValidated = true;
                print("going to login screen");
                loginScreen(true);//if id passes, go to the login screen
            }
            else
            {
                //if id fails, go back to the id screen
                print("ID not found");
                id = null;
            }
            
        }
    
        
        while(validated == false)
        {
            //wait until the username and password fields are filled
            yield return new WaitUntil(() => username != null && password != null);

            if (ValidateLogin())
            {
                usernameField.gameObject.SetActive(false);
                passwordField.gameObject.SetActive(false);
                Debug.Log("validated");  
                validated = true;
            }
            else
            {
                print("login invalid - try again");
                username = null;
                password = null;
            }
        }

 
    }

    public bool ValidateID()
    {
        //call dbm to read the username and password for a specific ID
        List<string> credentials = dbm.readLogin(id);
        print(string.Join(", ", credentials));
        //if a record is found with the username and password, validated = true
        if(credentials.Count == 2)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool ValidateLogin()
    {
        //call dbm to read the username and password for a specific ID
        //if the username and password match, validated = true
        List<string> credentials = dbm.readLogin(id);
        if(username != null && password != null)
        {
            if (credentials[0] == username && credentials[1] == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void ReadUN(string input)
    {
        // Read user input
        //promt user to enter username
        username = input;
        Debug.Log("username is " + username);
    }

    public void ReadPass(string input)
    {
        // Read user input
        //promt user to enter password
        password = input;
        Debug.Log("password is " + password);
    }


    public void ReadID(string input)
    {
        //Read user input
        //promt user to enter id        
        id = input;
        Debug.Log("id is " + id);        
    }
    
        
    



}
