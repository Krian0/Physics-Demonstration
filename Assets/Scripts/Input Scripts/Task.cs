using UnityEngine;


//-----------------------------------------------------------------------------------------------------------------------------------------------------
//
//  TASK
//
//  Contains:   A string that displays in place of "Element #" in Task Lists/Arrays;
//              An eTask variable;
//
//  Use:        N/A;
//
//  Purpose:    To wrap the eTask in a class with a string, giving it a name in the inspector;
//              To provide functions for checking and setting the eTask state to the Action Manager;
//
//-----------------------------------------------------------------------------------------------------------------------------------------------------
//
//  Notes:      N/A;
//
//-----------------------------------------------------------------------------------------------------------------------------------------------------


[System.Serializable]
public class Task
{
    [HideInInspector]
    public string name;
    public eTask task;


    public Task(eTask givenTask, string actionName)
    {
        task = givenTask;
        name = actionName;
    }


    public bool doing()
    {
        return (task == eTask.DOING);
    }

    public bool stopping()
    {
        return (task == eTask.STOPPING);
    }

    public bool notDoing()
    {
        return (task == eTask.NOT_DOING);
    }


    public void changeTask(eTask givenTask)
    {
        task = givenTask;
    }
}
