using UnityEngine;
using System.Collections;

public interface IScoutAntState{

    void UpdateState();

    void ToSearchState();

    void ToReturnState();

    void ToGatherState();

    void OnTriggerEnter(Collider other);
}
