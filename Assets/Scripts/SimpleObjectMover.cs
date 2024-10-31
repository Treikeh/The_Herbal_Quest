using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 moveDirection;

    private Vector3 checkpointPosition;


    private void OnEnable()
    {
        CheckpointManager.CheckpointSaved += OnCheckpointSaved;
        CheckpointManager.CheckpointReloaded+= OnCheckpointReloded;
    }

    private void OnDisable()
    {
        CheckpointManager.CheckpointSaved -= OnCheckpointSaved;
        CheckpointManager.CheckpointReloaded -= OnCheckpointReloded;
    }


    private void Start()
    {
        moveDirection = moveDirection.normalized;
        checkpointPosition = transform.position;
    }

    private void FixedUpdate()
    {
        transform.Translate(moveDirection * (moveSpeed * Time.fixedDeltaTime));
    }

    private void OnCheckpointSaved()
    {
        checkpointPosition = transform.position;
    }

    private void OnCheckpointReloded()
    {
        transform.position = checkpointPosition;
    }
}
