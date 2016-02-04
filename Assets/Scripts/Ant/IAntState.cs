using UnityEngine;
using System.Collections;

public interface IAntState{

    void UpdateState();

    void ToSearchState();

    void ToReturnState();

    void ToGatherState();

    void OnTriggerEnter(Collider other);
}
