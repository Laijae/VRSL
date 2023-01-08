using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{

    //scripts
    public Lesson lesson;
    
    //buttons
    public Button chooseLesson;
    public Button alphabet;
    public Text alphabetText;

    //as other lessons are added, add more buttons here

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
