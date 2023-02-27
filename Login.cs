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
    public inputField idField;

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
        //set all buttons to inactive
        loginScreen(false);


    }

    // Update is called once per frame
    void Update()
    {
        //check for a valid username and password every frame
        if(validated == false)
        {
            Validate();

        }
        
    }

    public void loginScreen(bool active)
    {      
        // Load login screen
        usernameField.gameObject.SetActive(active);
        passwordField.gameObject.SetActive(active);
    }



    public void Validate()
    {
        
        if (unValidated == true && passValidated == true)
        {
            validated = true;
            Debug.Log("validated");
            
        }
        
    }

    public void ValidateLogin()
    {
        //call dbm to read the username and password for a specific ID
        //if the username and password match, validated = true
    }

    public void ReadUN(string input)
    {
        // Read user input
        //promt user to enter username
        username = input;
        Debug.Log("username is " + username);
        if(username != null)
        {
            usernameField.gameObject.SetActive(false);
        }
        // include database check
        unValidated = true;

    }

    public void ReadPass(string input)
    {
        // Read user input
        //promt user to enter password
        
        password = input;
        Debug.Log("password is " + password);
        if (password != null)
        {
            passwordField.gameObject.SetActive(false);
        }
        // include database check 
        passValidated = true;
        
    }


    public void ReadID(string input)
    {
        //Read user input
        //promt user to enter password
        
        id = input;
        Debug.Log("id is " + id);
        if (id != null)
        {
            idField.gameObject.SetActive(false);
        }
        // include database check 
        idValidated = true;
        
        
    }
    
        
    



}
