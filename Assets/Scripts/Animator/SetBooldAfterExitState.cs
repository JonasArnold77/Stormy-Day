using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBooldAfterExitState : StateMachineBehaviour
{

    [SerializeField]
    private string booleanVariableName;
    [SerializeField]
    private string booleanVariableName2;

    [SerializeField]
    private string SetTrueValue;

    // OnStateExit is called when a transition ends and the state 
    //machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(booleanVariableName, false);
        animator.SetBool(booleanVariableName2, false);

        animator.SetBool(SetTrueValue, true);
    }


}
