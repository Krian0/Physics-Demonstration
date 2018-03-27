using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum eTask { DOING, STOPPING, NOT_DOING }


//-----------------------------------------------------------------------------------------------------------------------------------------------------
//
//  ACTION MANAGER
//
//  Contains:   A Dictionary of ints and Tasks;
//              A List of Tasks, viewable in the Inspector;
//
//  Use:        Require as a Component of GameObject, use the setActionManager function;
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
//
//-----------------------------------------------------------------------------------------------------------------------------------------------------


public class ActionManager : MonoBehaviour
{
    private Dictionary<int, Task> actionList = new Dictionary<int, Task>();
    public List<Task> taskList;


    public void setActionManager(int actionSize, string[] enumNames)
    {
        for (int i = 0; i < actionSize; i++)
            actionList.Add(i, new Task(eTask.NOT_DOING, enumNames[i]));

        taskList = actionList.Values.ToList();
    }


    public bool doing(int index)
    {
        return actionList[index].doing();
    }

    public bool stopping(int index)
    {
        return actionList[index].stopping();
    }

    public bool notDoing(int index)
    {
        return actionList[index].notDoing();
    }


    public void nowDoing(int index)
    {
        taskList[index].changeTask(eTask.DOING);
        actionList[index].changeTask(eTask.DOING);
    }

    public void nowStopping(int index)
    {
        taskList[index].changeTask(eTask.STOPPING);
        actionList[index].changeTask(eTask.STOPPING);
    }

    public void nowNotDoing(int index)
    {
        taskList[index].changeTask(eTask.NOT_DOING);
        actionList[index].changeTask(eTask.NOT_DOING);
    }
}