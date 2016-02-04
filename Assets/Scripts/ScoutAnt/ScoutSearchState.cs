using UnityEngine;
using System.Collections;

public class ScoutSearchState : IScoutAntState
{

    //searches for food
    private readonly ScoutAntStatePattern ant;
    private int nextArea;
	private Vector3 antDestination;
    private Vector3 lastPos;
    private float timer;
    public float debugTime = 15f;
    
    public ScoutSearchState(ScoutAntStatePattern antStatePattern)
    {
        ant = antStatePattern;
		antDestination = new Vector3 (Random.Range(ant.minXRange, ant.maxXRange), 0, Random.Range(ant.minZRange, ant.maxZRange));
    }

    public void UpdateState()
    {
        Search();
        //Look(); //not currently using eyes
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
		if (other.CompareTag("Food"))
        {
            ant.target = other.transform;
            ToGatherState();
        }
    }

    private void Search()
    {
		ant.navMeshAgent.destination = antDestination;
        ant.navMeshAgent.Resume();
        //searches for food
        if (ant.navMeshAgent.remainingDistance <= ant.searchStopRange && !ant.navMeshAgent.pathPending)
        {
            //nothing found here
            Debug.Log("No food");
            ant.navMeshAgent.ResetPath();
			antDestination = new Vector3 (Random.Range(ant.minXRange, ant.maxXRange), 0, Random.Range(ant.minZRange, ant.maxZRange));

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
