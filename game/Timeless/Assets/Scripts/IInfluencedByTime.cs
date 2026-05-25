using UnityEngine;

public interface IInfluencedByTime
{
    public void tick(int currentTime);
    public void timeResume();
    public void timePause();
    public void onSceneStart()
    {
        GameObject.Find("TimeManager").GetComponent<TimeController>().SubscribeToTimeEvents(this);
    }
}