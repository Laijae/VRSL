using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class Controller : MonoBehaviour
{
    // Transforms
    public float x = 0.0f;
    public float y = 0.0f;
    public float z = 0.0f;

    // Swings  
    private Queue directionQueue = new Queue();
    public bool trackSwing = true;
    public bool up = false;
    public bool down = false;
    public bool left = false;
    public bool right = false;
    public bool forward = false;
    public bool backward = false;

    

    // Frame data 
    private int framesetCount = 1;
    private float[] framesetOne = new float[3]; //A frameset is essentially a save of the transform at a specific frame
    private float[] framesetTwo = new float[3];
    private int lastSavedFrame = 0;
    



    // Start is called before the first frame update
    void Start()
    {
        //saves a frameset, 
        StartCoroutine(SaveFrameset());
        StartCoroutine(QueueCleaner());
        

        
    }

    void Update()
    {
        //All the active properties of the controller that contribute to the overall tracking of the controller
        GetHandData(); //get hand/finger data from controller
        ReadTransform(); //read transform data from controller
        ReadQueue(); //read swing queue to determine swing direction
        //debug and cleaners
        directionChecker();
    }

    public void directionChecker()
    {
        //checks the direction of the swing
        if (up == true)
        {
            Debug.Log("up has been set to true");
        }
        else if (down == true)
        {
            Debug.Log("down has been set to true");
        }
        else if (left == true)
        {
            Debug.Log("left has been set to true");
        }
        else if (right == true)
        {
            Debug.Log("right has been set to true");
        }
        else if (forward == true)
        {
            Debug.Log("forward has been set to true");
        }
        else if (backward == true)
        {
            Debug.Log("backward has been set to true");
        }
    }

    public void QueueCounter()
    {
        //counts the number of items in the queue
        int count = directionQueue.Count;
        Debug.Log("The number of items in the queue is: " + count);
    }


    //
    //TRANSFORMS
    //

    public void ReadTransform()
    {
        //read transform data from controller
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
        z = gameObject.transform.position.z;
    }
   

    //
    //HAND DATA
    //

    //temp return for hand data with no controllers connected
    //will be replaced with actual hand data from controllers when controllers are connected
    public List<string> GetHandData()
    {
        //temporary "thumbs up" hand data
        List<string> handData = new List<string>{"u","d","d","d","d"};
        //print(string.Join(",", handData));
        return handData;
    }


    //    
    //SWING DETECTION
    //

    public void ReadQueue()
    {       
        //waits for the queue to have three calculated framesets before taking the average as the swing
        //opted for counting the elements in queue rather than syncing the time, as the time can change with testing 
        //and the queue will always have three elements when the swing is calculated

        //including the '>' is a failsafe to make sure the queue never overloads.
        //if the queue bugs and does not detect at 3, and continues to add to the queue, it will never == 3 and will never calculate the swing
        if(directionQueue.Count >= 3) 
        {   
            print("reading queue");        
            string direction = Majority();
            print(direction + " majority found");
            StartCoroutine(EnableSwing(direction)); 
            directionQueue.Clear();
        }       
    } 
        
    public string Majority()
    {       
        //read the three directions that are in the queue
        //use majority voting to determine the swing direction
        //if the majority is x, then the swing is x
        //This is visualised by the mealy machine diagram in the documentation 

        print("finding majority");

        string x = directionQueue.Dequeue().ToString(); //first input = x 
        if(directionQueue.Peek() == x) //represents [x] -> maj
        {
            return x;
            print("first majority found");
        }
        else 
        {
            string y = directionQueue.Dequeue().ToString(); //second input = y
            if(directionQueue.Peek() == x)//represents taking the x path to maj 
            {
                //[x,y,x]
                return x;
            }
            else if(directionQueue.Peek() == y)//represents taking the y/z path to maj
            {
                return y;
                print("second majority found");
            }
            else //represents taking the s path to N/A
            {
                return "s";
            }
        }
    }
        
    //async function that allows for swing to stay true after a specific amount of time 
    // programmed 'swing leniance', giving an amount of time that the swing stays true after the swing is detected
    public IEnumerator EnableSwing(string direction)
    {
        print("enabling swing");

        //enable the swing
        //wait for 1 second
        //disable the swing
        switch (direction.ToLower())
        {
            case "u":
                up = true;
                yield return new WaitForSeconds(1);
                up = false;
                break;
            case "d":
                down = true;
                yield return new WaitForSeconds(1);
                down = false;
                break;
            case "l":
                left = true;
                yield return new WaitForSeconds(1);
                left = false;
                break;
            case "r":
                right = true;
                yield return new WaitForSeconds(1);
                right = false;
                break;
            case "f":
                forward = true;
                yield return new WaitForSeconds(1);
                forward = false;
                break;
            case "b":
                backward = true;
                yield return new WaitForSeconds(1);
                backward = false;
                break;
            case "s":
                break;
        }

        // OLD CODE
        //waits one second and then resets all swings to nothing 
        // // yield return new WaitForSeconds(2);
        // // print("setting everything to FALSE");
        // // up = false;
        // // down = false;
        // // left = false;
        // // right = false;
        // // forward = false;
        // // backward = false;

        // By yielding and resetting each swing individually, it represents the second FSM diagram in the documentation
        // This means that from one swing state, you can transfer to another swing state
        // without having to reset to 'static' first

    }

    
    public IEnumerator QueueCleaner()
    {
        while(true)
        {
            if(directionQueue.Count < 3)
            {   

                yield return new WaitForSeconds(0.3f);

                if(directionQueue.Count < 3)
                {
                    directionQueue.Clear();
                }
            }
        }
        
    }
        
    

    // Async function to save frameset
    public IEnumerator SaveFrameset()
    {
        //saves a frameset
        //if x frames have passed, then initialise frameset two to the current transform
        //it alternates betweening saving the 'past' (framesetOne) and 'present' (framesetTwo)
   
         
        while(trackSwing == true)
        {
            //delay for x frames    
            for(int i = 0; i < 3; i++)
            {
                yield return null;
            }
            
            //condition to see if a swing needs to be calculated
            bool detect = false;    

            //save frameset
            switch (framesetCount)
            {
                case 1:
                    framesetOne[0] = x;
                    framesetOne[1] = y;
                    framesetOne[2] = z;
                    framesetCount++;
                    break;
                case 2:
                    detect = true;
                    framesetTwo[0] = x;
                    framesetTwo[1] = y;
                    framesetTwo[2] = z;
                    framesetCount = 1;
                    break;            
            }
            //print("frameset 1 = " + string.Join(",", framesetOne));
            //print("frameset 2 = " + string.Join(",", framesetTwo));

            //run a check to see if a swing has been detected
            if(detect == true)
            {
                DetectSwing();
                detect = false;
            }
            

        }    
    }

    public float[] GetDifferences()
    {
        //get difference between frameset1 and frameset2
        float xDiff = framesetTwo[0] - framesetOne[0];
        float yDiff = framesetTwo[1] - framesetOne[1];
        float zDiff = framesetTwo[2] - framesetOne[2];
        float[] diff = new float[3] {xDiff, yDiff, zDiff};
        //useful print for testing
        //print("xDiff = " + xDiff + " yDiff = " + yDiff + " zDiff = " + zDiff);

        return diff;
    }    

    public void DetectSwing()
    {
        //detect swing motion
        //if swing motion is detected, then add to directionQueue
        
        //frame = x axis , y value = difference in frameset1 val vs frameset2
        //if gradient val higher than {test} swing detected 
        
        float[] diff = GetDifferences();
        
        for(int difference = 0; difference < diff.Length; difference++)
        {
            if(Math.Abs(diff[difference]) > 5) //using modulus to get absolute value, to determine if swing is true 
            {

                //switch statement to determine direction of swing
                switch (difference) 
                {
                    case 0:
                        //case 0 = x axis
                        if(diff[difference] > 0) 
                        {
                            directionQueue.Enqueue("R");
                            print("R Detected");
                            
                        }
                        else if(diff[difference] < 0)
                        {
                            directionQueue.Enqueue("L");
                            print("L Detected");
                        }
                        break;

                    case 1:
                        //case 1 = y axis
                        if(diff[difference] > 0)
                        {
                            directionQueue.Enqueue("U");
                            print("U Detected");

                        }
                        else if(diff[difference] < 0)
                        {
                            directionQueue.Enqueue("D");
                            print("D Detected");
                        }
                        break;

                    case 2:
                        //case 2 = z axis
                        if(diff[difference] > 0)
                        {
                            directionQueue.Enqueue("F");
                            print("F Detected");
                        }
                        else if(diff[difference] < 0)
                        {
                            directionQueue.Enqueue("B");
                            print("B Detected");
                        }
                        break;    

                }
            }
            else
            {
                //swing not detected, add 'static' to queue
                //directionQueue.Enqueue("S");
                //print("S Detected");

                //OLD 
                // Would add static to the queue if no swing was detected
                // since the frame updates were so fast, this would cause the queue to fill up with statics
                // This meant that the queue would be full of statics, and the swing would never be detected

                //NEW
                // The queue is now only filled with swings
                // If a swing is incomplete e.g. if the queue < 3 
                // there will be no swing detected
                // If the swing stays incomplete for x time, the queue will be cleared to avoid affecting the next swing

            }

            
                         
        }
       
    }

    

    

    
   
}

     















