using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
  private bool enemiesSpawned;
  public Vector3 spawnPoint;
  public float moveSpeed;

  private Rigidbody2D myRigidBody;

  private bool isMoving;

  public float timeBetweenMove;
  private float timeBetweenMoveCounter;
  public float timeToMove;
  private float timeToMoveCounter;
  private Vector3 moveDirection;

  public float waitToReload;
  private bool reloading;
  private GameObject thePlayer;

  // Start is called before the first frame update
  void Start()
  {
    //timeBetweenMoveCounter = timeBetweenMove;
    //timeToMoveCounter = timeToMove;

    myRigidBody = GetComponent<Rigidbody2D>();

    timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
    timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeBetweenMove * 1.25f);

    myRigidBody.isKinematic = true;
  }

  // Update is called once per frame
  void Update()
  {
    if (isMoving)
    {
      timeToMoveCounter -= Time.deltaTime;
      myRigidBody.velocity = moveDirection;

      if (timeToMoveCounter < 0)
      {
        isMoving = false;
        //timeBetweenMoveCounter = timeBetweenMove;
        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);

      }
    }
    else
    {
      timeBetweenMoveCounter -= Time.deltaTime;
      myRigidBody.velocity = Vector2.zero;

      if (timeBetweenMoveCounter < 0f)
      {
        if (Random.Range(0f, 1f) > 0.97f)
        {
          isMoving = true;
          //timeToMoveCounter = timeToMove;
          timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeBetweenMove * 1.25f);

          moveDirection = new Vector3(Random.Range(-0.2f, 0.2f) * moveSpeed, Random.Range(-0.3f, 0.3f) * moveSpeed, 0f);
        }
      }
    }
  }
}
