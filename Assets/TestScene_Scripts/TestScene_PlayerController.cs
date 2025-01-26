using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestScene_PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField]
    private GameObject finish;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            agent.SetDestination(finish.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Finish"))
            {
                Debug.Log("Finish!!!");
            }
        }
    }

}
