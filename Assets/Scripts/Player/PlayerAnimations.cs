using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public const string IDLE = "Idling";
    public const string JUMP = "Jumping";
    public const string NAME_JUMPING = "isJumping";

    public void StopJumping()
    {
        GetComponent<Animator>().SetBool(NAME_JUMPING, false);
        GetComponentInParent<PlayerController>().SetMoving(false);
    }
}
