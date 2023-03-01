using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{

    //scripts
    public Lesson lesson;
    public Login login;
    
    //buttons
    public Button chooseLesson;
    public Button alphabet;
    

    //as other lessons are added, add more buttons here

    // Start is called before the first frame update
    void Start()
    {
        //set all buttons to inactive
        MainMenu(false);

        login.loginScreen(false);

        login.idCheck(true);

        //start coroutine to wait for login validation
        StartCoroutine(PassLoginScreen());

        
        

    }

    // Update is called once per frame
    void Update()
    {
        
         
    }

    public IEnumerator PassLoginScreen()
    {
        yield return new WaitUntil(() => login.validated == true);
        MainMenu(true);       
    }

    public void MainMenu(bool active)
    {
        chooseLesson.gameObject.SetActive(active);
            
    }

    

    public void LessonsSubmenu()
    {
        // Load lesson submenu

        //set all other buttons to inactive (except for back button)
        chooseLesson.gameObject.SetActive(false);

        //load lesson submenu buttons
        alphabet.gameObject.SetActive(true);

        //as other lessons are added, add more buttons here

    }

    public void lessonSelected()
    {
        switch (EventSystem.current.currentSelectedGameObject.name)
        {
            case "alphabet":
                lesson.lessonToken = "Alphabet";
                break;
            //as other lessons are added, add more cases here
        }
        Debug.Log("Chosen lesson is " + lesson.lessonToken);
    }
}
