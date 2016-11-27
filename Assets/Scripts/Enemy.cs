using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public Transform[] thisRoute;
    public int routeNumber;
    public float speed;
    public int WayPointIndex = 0;
    public int SyncAttempts = 0;

    void Start()
    {
        routeNumber = GameObject.Find("GameMaster").GetComponent<WaveSpawner>().waveIndex;
        routeNumber %= 4;
        switch (routeNumber)
        {
            case 0:
                GameObject route1 = GameObject.Find("Route_1");
                thisRoute = new Transform[route1.transform.childCount];
                for (int i = 0; i <= thisRoute.Length - 1; i++)
                    thisRoute[i] = route1.transform.GetChild(i);
                break;
            case 1:
                GameObject route2 = GameObject.Find("Route_2");
                thisRoute = new Transform[route2.transform.childCount];
                for (int i = 0; i <= thisRoute.Length - 1; i++)
                    thisRoute[i] = route2.transform.GetChild(i);
                break;
            case 2:
                GameObject route3 = GameObject.Find("Route_3");
                thisRoute = new Transform[route3.transform.childCount];
                for (int i = 0; i <= thisRoute.Length - 1; i++)
                    thisRoute[i] = route3.transform.GetChild(i);
                break;
            case 3:
                GameObject route4 = GameObject.Find("Route_4");
                thisRoute = new Transform[route4.transform.childCount];
                for (int i = 0; i <= thisRoute.Length - 1; i++)
                    thisRoute[i] = route4.transform.GetChild(i);
                break;
        }
        target = thisRoute[0];
    }

    void Update()
    {
        Vector3 dir = target.transform.position - this.transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);

        if (Vector3.Distance(this.transform.position, target.transform.position) <= 0.2f)
            GetNextWayPoint();
    }

    void GetNextWayPoint()
    {
        if (WayPointIndex >= thisRoute.Length - 1)
        {
            Destroy(this.gameObject);
            return;
        }
        WayPointIndex++;
        target = thisRoute[WayPointIndex];
    }
}
