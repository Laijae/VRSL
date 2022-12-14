using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        GetHandData();
    }


    //temp return for hand data with no controllers connected
    public List<string> GetHandData()
    {
        
        List<string> handData = new List<string>{"u","d","d","d","d"};
        print(handData);
        return handData;
    }

    //idea - use derivatives of position to estimate rate of "swing" of hand...?
    //if negative y derivative, then hand is moving down
    //if positive y derivative, then hand is moving up
    //if negative x derivative, then hand is moving left
    //if positive x derivative, then hand is moving right

}
