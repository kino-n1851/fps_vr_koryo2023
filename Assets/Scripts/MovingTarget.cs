using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    private bool IsMoving = false;
    // Start is called before the first frame update
    public void SwitchMoveMode()
    {
        if(IsMoving)
        {
            animator.SetBool("Move", false);
            IsMoving = false;
        }else{
            animator.SetBool("Move", true);
            IsMoving = true;
        }
    }
}
