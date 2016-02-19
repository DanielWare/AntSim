using UnityEngine;
using System.Collections;

public class SearchState : IAntState {

    //searches for food
    private readonly AntStatePattern ant;
    private int nextArea;
	private Vector3 antDestination;
    private Vector3 lastPos;
    private float timer;
    public float debugTime = 15f;
    
    public SearchState(AntStatePattern antStatePattern)
    {
        ant = antStatePattern;
		antDestination = new Vector3 (Random.Range(ant.minXRange, ant.maxXRange), 0, Random.Range(ant.minZRange, ant.maxZRange));
    }

    public void UpdateState()
    {
        Search();
        timer += Time.deltaTime;
        if(timer > debugTime)
        {
            ReNav();
            timer = 0;   
        }
    }

    public void ToSearchState()
    {
        //not used
        ant.meshRendererFlag.material.color = Color.green;
    }

    public void ToReturnState()
    {
        //not used
        ant.meshRendererFlag.material.color = Color.red;
    }

    public void ToGatherState()
    {
        ant.currentState = ant.gatherState;
        ant.meshRendererFlag.material.color = Color.yellow;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PherNode"))
        {
            if (other.GetComponent<PherNodeLL>().prevNode)
            {
                antDestination = other.GetComponent<PherNodeLL>().prevNode.position;
            }
        }
		if (other.CompareTag("Food"))
        {
            ant.target = other.transform;
            ToGatherState();
        }
    }

    private void Search()
    {
		ant.navMeshAgent.destination = antDestination;
        //searches for food
        if(ant.navMeshAgent.remainingDistance <= ant.searchStopRange && !ant.navMeshAgent.pathPending)
        {
            //nothing found here
            ant.navMeshAgent.ResetPath();
			antDestination = new Vector3 (Random.Range(ant.minXRange, ant.maxXRange), 0, Random.Range(ant.minZRange, ant.maxZRange));
        }
        if (!ant.navMeshAgent.hasPath)
        {
            Debug.Log("No Path");
        }
    }

    
    private void ReNav()
    {
        //if ant gets bugged and stuck in search state
        if(lastPos == ant.transform.position)
        {
            ant.navMeshAgent.ResetPath();
            Debug.Log("ReNav");
            antDestination = new Vector3 (Random.Range(ant.minXRange, ant.maxXRange), 0, Random.Range(ant.minZRange, ant.maxZRange));
        }
        lastPos = ant.transform.position;
    } 

}
