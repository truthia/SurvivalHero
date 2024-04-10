using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvent : MonoBehaviour
{
    public int left;
  public void Attack(int i)
    {
        PlayerController.Instance.animator.SetBool("Attack", false);
        if (i == 0)
        {
            PlayerController.Instance.animator.SetBool("Left", false);
           
        }
        else
        {
            PlayerController.Instance.animator.SetBool("Left", true);
        }
        left = i;
    }
}
