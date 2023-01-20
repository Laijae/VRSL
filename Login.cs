using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    //scripts
    public InputField usernameField;
    public InputField passwordField;

    //validation
    public bool validated = false;
    public bool unValidated = false;
    public bool passValidated = false;

    //variables
    private string username = null;
    private string password = null;

    
    private string email = null;

    
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        //check for a valid username and password every frame
        Validate();
    }

    public void loginScreen()
    {
        
        // Load login screen
        usernameField.gameObject.SetActive(true);
        passwordField.gameObject.SetActive(true);
    }



    public void Validate()
    {
        
        if (unValidated == true && passValidated == true)
        {
            validated = true;
            Debug.Log("validated");
        }
        
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
        
    



}
