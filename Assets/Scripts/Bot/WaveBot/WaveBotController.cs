using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class WaveBotController : MonoBehaviour
{
    public WaveEnemySO enemyData;
    public NavMeshAgent agent;
    public WaveBotStats waveBotStats;
    public Transform target;
    public abstract void Movement();
    public abstract void Attack();
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        waveBotStats = GetComponent<WaveBotStats>();
    }

}
