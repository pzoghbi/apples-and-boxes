using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    private Vector3 moveToPosition;
    private Vector3 lookAtPosition;

    private bool isMoving = false;
    private bool canMove = false;

    Rigidbody Rigidbody;
    Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Animator.speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        if (isMoving)
        {
            Rigidbody.position = Vector3.MoveTowards(
                Rigidbody.position, 
                moveToPosition, 
                Time.deltaTime * GameConfig.BLOCK_DISTANCE * 2f
            );

            return;
        }

        Vector3 lookAtPosition = Vector3.zero;
        lookAtPosition.x = Input.GetAxisRaw(GameConfig.AXIS_HOR);
        lookAtPosition.z = Input.GetAxisRaw(GameConfig.AXIS_VER);
        lookAtPosition = lookAtPosition * GameConfig.BLOCK_DISTANCE;

        bool wantsToMove = (Mathf.Abs(lookAtPosition.x) > 0f) || (Mathf.Abs(lookAtPosition.z) > 0f);

        if (wantsToMove)
        {
            if (IsPositionPlayable(Rigidbody.position + lookAtPosition))
            {
                moveToPosition = Rigidbody.position + lookAtPosition;
                Animator.SetBool(PlayerAnimations.NAME_JUMPING, true);
                isMoving = true;
            }
        }
    }

    private bool IsPositionPlayable(Vector3 positionToCheck)
    {
        return Physics.Raycast(
            new Ray(positionToCheck, Vector3.down),
            GameConfig.BLOCK_DISTANCE,
            LayerMask.GetMask(GameConfig.LAYER_GROUND)
        );
    }

    // Perform "isPlayable" check on surrounding spaces.
    private bool HasPlayableSpace()
    {
        List<Vector3> vectors = new List<Vector3>();
        vectors.Add(Vector3.left);
        vectors.Add(Vector3.right);
        vectors.Add(Vector3.back);
        vectors.Add(Vector3.forward);

        foreach(Vector3 vector in vectors)
        {
            if (IsPositionPlayable(Rigidbody.position + vector * GameConfig.BLOCK_DISTANCE)) {
                return true;
            }
        }

        return false;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void SetMoving(bool setMoving)
    {
        isMoving = setMoving;
        if (!HasPlayableSpace())
        {
            Die();
        }
    }

    public bool GetMoving()
    {
        return isMoving;
    }
}