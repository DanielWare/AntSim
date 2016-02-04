using UnityEngine;
using System.Collections;

public class FoodReduce : MonoBehaviour {

    public float sizeChange = 0.1f;

    public void Reduce()
    {
        transform.localScale -= new Vector3(sizeChange, 0f, sizeChange);
        if(transform.lossyScale.x <= 0.3f)
        {
            gameObject.SetActive(false);
        }
    }
}
