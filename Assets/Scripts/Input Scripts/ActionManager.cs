using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum eTask { DOING, STOPPING, NOT_DOING }


//-----------------------------------------------------------------------------------------------------------------------------------------------------
//
//  ACTION MANAGER
//
//  Contains:   A Dictionary of eActions and Tasks;
//              A List of Tasks, viewable in the Inspector;
//
//  Use:        Require this as a component of the controllable character;
//              Use the doing/stopping/notDoing functions to return a bool based on if the Action Pair's Task is set to the corresponding eTask value;
//              Use the nowDoing/Stopping/NotDoing function to set a specific Action Pair's Task to the corresponding eTask value;
//
//  Purpose:    To contain and keep track of various actions within the game;
//              To avoid additional messes of booleans or needing to check and keep track of multiple other values by offering a third "Stopping" 
//              state, allowing an easy set and check for programming what should happen on "Exiting" a particular state;
//
//-----------------------------------------------------------------------------------------------------------------------------------------------------
//
//  Notes:      The taskList will not appear in the Inspector until the game is running, but once it is you can click the dropdown arrows to see
//              the current eTask state for each Action Pair;
//              ActionList has a mystery green squiggle. It doesn't seem to be causing any issues, though;
//
//-----------------------------------------------------------------------------------------------------------------------------------------------------


public class ActionManager : MonoBehaviour
{
    private Dictionary<eActions, Task> actionList = new Dictionary<eActions, Task>();
    public List<Task> taskList;


    private void Start()
    {
        foreach (eActions action in Enum.GetValues(typeof(eActions)))
            actionList.Add(action, new Task(eTask.NOT_DOING, action.ToString()));

        taskList = actionList.Values.ToList();
        taskList.RemoveAt((int)eActions.ENUM_END);
    }


    public bool doing(eActions keyAction)
    {
        return actionList[keyAction].doing();
    }

    public bool stopping(eActions keyAction)
    {
        return actionList[keyAction].stopping();
    }

    public bool notDoing(eActions keyAction)
    {
        return actionList[keyAction].notDoing();
    }


    public void nowDoing(eActions keyAction)
    {
        taskList[(int)keyAction].changeTask(eTask.DOING);
        actionList[keyAction].changeTask(eTask.DOING);
    }

    public void nowStopping(eActions keyAction)
    {
        taskList[(int)keyAction].changeTask(eTask.STOPPING);
        actionList[keyAction].changeTask(eTask.STOPPING);
    }

    public void nowNotDoing(eActions keyAction)
    {
        taskList[(int)keyAction].changeTask(eTask.NOT_DOING);
        actionList[keyAction].changeTask(eTask.NOT_DOING);
    }
}