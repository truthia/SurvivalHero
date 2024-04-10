using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleebotController : WaveBotController
{
    public override void Attack()
    {
        target.GetComponent<PlayerStats>().TakeDamage(waveBotStats.CurrentDamage,transform);
    }

    public override void Movement()
    {
        if(agent.enabled)
        agent.SetDestination(PlayerController.Instance.transform.position);
    }

  
}
