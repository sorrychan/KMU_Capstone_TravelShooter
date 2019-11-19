using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimation : StateMachineBehaviour
{
    [Tooltip("랜덤 애니메이션 재생 인덱스")]
    public string parameterName = "IdleIndex";

    [Tooltip("최소값")]
    public int minValue = 0;

    [Tooltip("최대값")]
    public int maxValue = 2;

  
     public void OnStateEnter(Animator animator, int stateMachinePathHash)
    {
        int value = Random.Range(minValue, maxValue + 1);
        animator.SetInteger(parameterName, value);
        //Debug.Log("current value"+value);
    }
}
