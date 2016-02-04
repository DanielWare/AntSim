using UnityEngine;
using System.Collections;

public class ScoutGatherState : IScoutAntState
{

    //spend a small amount of time gathering the food

    private readonly ScoutAntStatePattern ant;
    private float gatherTimer;

    public ScoutGatherState(ScoutAntStatePattern antStatePattern)
    {
        ant = antStatePattern;
    }

    public void UpdateState()
    {
        //Debug.Log("GatherState");
        Move();
    }

    public void ToSearchState()
    {
        //not used
        ant.meshRendererFlag.material.color = Color.green;
    }

    public void ToReturnState()
    {
        ant.currentState = ant.returnState;
		gatherTimer = 0f;
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

    private void Move()
    {
        ant.navMeshAgent.destination = ant.target.position;
        //ant.navMeshAgent.Resume();
        if (ant.navMeshAgent.remainingDistance <= ant.gatherRange && !ant.navMeshAgent.pathPending)
        {
            GatherFood();
        }
        
    }

    private void GatherFood()
    {
        //ant.navMeshAgent.Stop();
        gatherTimer += Time.deltaTime;

        if (gatherTimer >= ant.gatherTime)
        {
            //send message to food to reduce in size
            GameObject closest = ClosestFood();//gets closest "food" object
            if(closest)
                closest.GetComponent<FoodReduce>().Reduce();
            //continue with business
            ant.navMeshAgent.ResetPath();
            ToReturnState();
            ant.hasFood = true;
            ant.foodStatusChanged = true;
        }
    }
    
    private GameObject ClosestFood()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Food");
        GameObject closest = null;
        float distance = 64f;
        Vector3 position = ant.transform.position;
        foreach (GameObject go in gos) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

}
