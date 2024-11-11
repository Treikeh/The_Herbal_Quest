using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl3WaterLevel : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stopPosition;

    private bool hasStartedMoving;
    private bool isMoving;
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
        checkpointPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (isMoving && transform.position.y < stopPosition)
        {
            transform.Translate(Vector3.up * (moveSpeed * Time.fixedDeltaTime));
        }
    }

    public void StartMoving()
    {
        isMoving = true;
        hasStartedMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    private void OnCheckpointSaved()
    {
        checkpointPosition = transform.position;
    }

    private void OnCheckpointReloded()
    {
        StopMoving();
        transform.position = checkpointPosition;
        if (hasStartedMoving)
        {
            Invoke(nameof(StartMoving), 2.0f);
        }
    }
}
