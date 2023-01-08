using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{

    public InputField usernameField;
    public InputField passwordField;

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
        // Check for user input
        if (username != null && password != null)
        {
            // Check if user exists
            // If user exists, load menu
            // If user does not exist, create user

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
    }

    public void ReadPass(string input)
    {
        // Read user input
        //promt user to enter password
        passwordField.gameObject.SetActive(true);
        password = input;
        Debug.Log("password is " + password);
        if (password != null)
        {
            passwordField.gameObject.SetActive(false);
        }
    }
        
    



}
