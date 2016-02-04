using UnityEngine;
using System.Collections;

public class ReturnState : IAntState {

    private readonly AntStatePattern ant;

    public ReturnState(AntStatePattern antStatePattern)
    {
        ant = antStatePattern;
    }

    public void UpdateState()
    {
        //Debug.Log("ReturnState");
        ReturnHome(); 
    }

    public void ToSearchState()
    {
        ant.meshRendererFlag.material.color = Color.green;
        ant.currentState = ant.searchState;
    }

    public void ToReturnState()
    {
        //not used
        ant.meshRendererFlag.material.color = Color.red;
    }

    public void ToGatherState()
    {
        //not used
        ant.meshRendererFlag.material.color = Color.yellow;
    }

    public void OnTriggerEnter(Collider other)
    {
        //not used
    }

    private void ReturnHome()
    {
        ant.navMeshAgent.destination = ant.home.position;
        ant.navMeshAgent.Resume();

        if(ant.navMeshAgent.remainingDistance <= ant.colonyRange && !ant.navMeshAgent.pathPending)
        {
            ant.navMeshAgent.ResetPath();
            ToSearchState();
            ant.hasFood = false;
            ant.foodStatusChanged = true;
        }
        
    }
}
