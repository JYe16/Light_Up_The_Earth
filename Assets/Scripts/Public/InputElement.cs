using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class InputElement
{
    public bool risingEdge = false;
    public bool longPress = false;
    public bool failingEdge = false;


    public void releaseEdges()
    {
        longPress = false;
        failingEdge = true;
    }

    public void resetEdge()
    {
        risingEdge = false;
        failingEdge = false;
    }
}