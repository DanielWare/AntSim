using UnityEngine;
using System.Collections;

public class AntStatePattern : MonoBehaviour {

    public float sightRange = 10f;
    public float gatherTime = 2f;
    public float gatherRange = 1.5f;
    public float colonyRange = 4f;
    public float searchStopRange = 1f;
    public float minXRange = -50f;
	public float maxXRange = 50f;
    public float minZRange = -50f;
	public float maxZRange = 50f;
    public float foodLife = 15f;
    public MeshRenderer meshRendererFlag;
    public Transform eyes;
    public Transform home;
    public GameObject foodChunk;
    private Vector3 pos;
    private GameObject t;

    [HideInInspector]
    public bool hasFood;
    [HideInInspector]
    public bool foodStatusChanged;
    [HideInInspector]
    public IAntState currentState;
    [HideInInspector]
    public SearchState searchState;
    [HideInInspector]
    public ReturnState returnState;
    [HideInInspector]
    public GatherState gatherState;
    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    [HideInInspector]
    public Transform target;


    public void Awake()
    {
        searchState = new SearchState(this);
        returnState = new ReturnState(this);
        gatherState = new GatherState(this);

        navMeshAgent = GetComponent<NavMeshAgent>();

        hasFood = false;
        foodStatusChanged = false;
    }

    void Start ()
    {
        currentState = searchState;
        meshRendererFlag.material.color = Color.green;
	}
	
	void Update ()
    {
        if(hasFood && foodStatusChanged)
        {
            foodStatusChanged = false;
            CreateFood();
        }
        else if (!hasFood && foodStatusChanged)
        {
            foodStatusChanged = false;
            t.transform.parent = null;         
        }
        currentState.UpdateState();
	}

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    private void CreateFood()
    {
        t = Instantiate(foodChunk, transform.position, transform.rotation) as GameObject;
        t.transform.parent = transform;
        t.transform.localPosition = new Vector3(0f, 0.5f, 0.5f);

    }

}
