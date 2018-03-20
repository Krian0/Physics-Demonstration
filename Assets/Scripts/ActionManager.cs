using UnityEngine;


public abstract class ActionManager : MonoBehaviour
{
    public enum Action { DOING, STOPPING, NOT_DOING }

    public bool doing(Action givenAction)
    {
        return (givenAction == Action.DOING);
    }

    public bool stopping(Action givenAction)
    {
        return (givenAction == Action.STOPPING);
    }

    public bool notDoing(Action givenAction)
    {
        return (givenAction == Action.NOT_DOING);
    }

    public void setDoing(out Action givenAction)
    {
        givenAction = Action.DOING;
    }

    public void setStopping(out Action givenAction)
    {
        givenAction = Action.STOPPING;
    }

    public void setNotDoing(out Action givenAction)
    {
        givenAction = Action.NOT_DOING;
    }
}
