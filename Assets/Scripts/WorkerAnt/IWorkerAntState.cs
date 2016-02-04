using UnityEngine;
using System.Collections;

public interface IWorkerAntState{

    void UpdateState();

    void ToSearchState();

    void ToReturnState();

    void ToGatherState();

    void OnTriggerEnter(Collider other);
}
