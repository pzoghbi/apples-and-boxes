using System;
using UnityEngine;

public class DisappearingBlock : MonoBehaviour
{
    [SerializeField]
    [Range(0, 3)] float speed = 1f;

    Animator Animator;
    private bool isHazardous = false;

    private void Start()
    {
        Animator = GetComponent<Animator>();
        Animator.speed = speed;
    }

    private void OnTriggerStay(Collider other)
    {
        if (isHazardous)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player) {
                if (!player.GetMoving())
                {
                    player.Die();
                }
            }
        }
    }

    public void SetHazard(int toHazard)
    {
        isHazardous = Convert.ToBoolean(toHazard);
    }
}
