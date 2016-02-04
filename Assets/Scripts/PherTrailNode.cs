using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PherTrailNode : MonoBehaviour {

    public GameObject t;
    private Vector3 lastPos;
    private List<GameObject> nodeList;
    private float lifeTime;
    public Transform prevNode;

    public void Awake()
    {
        lifeTime = GetComponent<TrailRenderer>().time;
        nodeList = new List<GameObject>();
        t = Instantiate(t, transform.position, transform.rotation) as GameObject;
        nodeList.Add(t);
        lastPos = transform.position;
        Destroy(t, lifeTime);
        prevNode = t.transform;
    }

    public void Update()
    {
        if(Vector3.Distance(transform.position, lastPos) > 5f)
        {
            t = Instantiate(t, transform.position, transform.rotation) as GameObject;
            nodeList.Add(t);
            lastPos = transform.position;
            Destroy(t, lifeTime);
            t.GetComponent<PherNodeLL>().prevNode = prevNode;
            prevNode = t.transform;
        }
    }

}
