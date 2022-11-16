using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System.Linq;

public class IndexInput : MonoBehaviour
{

    public SteamVR_Action_Skeleton skeletonAction = null;

    private void Start()
    {
        
    }

    void Update()
    {
        if (SignforA())
        {
            print("A");
        }
        
    }

   

    bool SignforA()
    {      

        // This condition is raw data comparison
        // Once the data for each hand sign is stored, it can be used as a condition instead 
        // checking the skeletonAction.fingerCurls array TO the array of data held of x letter

        if(skeletonAction.thumbCurl < 0.2 && skeletonAction.indexCurl > 0.8 && skeletonAction.middleCurl > 0.8 && skeletonAction.ringCurl > 0.8 && skeletonAction.pinkyCurl > 0.8) 
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }


    // SignChecker function needs to change
    // switch case would be better if it was to have each individual function(letter) case
    // Perhaps find a way to have an input to the function be the letter wished to be checked?
    // system can be made much more efficient 
    bool SignChecker()
    {
        bool sign = false;
        while (sign == false)
        {
            if (SignforA())
            {
                sign = true;
            }
        }
        return true;           
    }




}
