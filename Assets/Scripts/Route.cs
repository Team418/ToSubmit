using UnityEngine;

public class Route : MonoBehaviour
{
    public Transform[] waypoints;
    void Awake()
    {
        waypoints = new Transform[transform.childCount];
        for (int i = 0; i <= waypoints.Length - 1; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }	
}
