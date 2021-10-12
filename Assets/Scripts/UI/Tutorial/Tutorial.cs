using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    /*
     *
     *  step order:
     *  1. move
     *  2. props
     *  3. look
     *  4. shoot
     *  5. timer
     *
     */
    public Button nextStep;

    public Canvas moveHandle;
    // Start is called before the first frame update
    void Start()
    {
        //by default, show skip button and next step button
        int currentStep = 0;
        nextStep.onClick.AddListener(delegate{showNextStep(currentStep);});
    }

    void showNextStep(int currentStep)
    {
        switch (currentStep)
        {
            case 0:
                moveHandle.overrideSorting = true;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
