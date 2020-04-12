using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillagerMovement : MonoBehaviour
{

  public bool canMove;
  public float moveSpeed;
  private Rigidbody2D myRigidBody;

  public bool isWalking;

  public float walkTime;
  private float walkCounter;

  public float waitToWalkTime;
  private float waitCounter;

  private int walkDirection;

  public Collider2D walkZone;
  private Vector2 minWalkPoint;
  private Vector2 maxWalkPoint;
  private bool hasWalkZone;
  private Text npcname;
  private DialogueManager dialogueManager;
  private QuestsManager questsManager;

  // Start is called before the first frame update
  void Start()
  {
    myRigidBody = GetComponent<Rigidbody2D>();

    dialogueManager = FindObjectOfType<DialogueManager>();
    questsManager = FindObjectOfType<QuestsManager>();

    waitCounter = waitToWalkTime;
    walkCounter = walkTime;

    chooseDirection();

    if (walkZone != null)
    {
      minWalkPoint = walkZone.bounds.min;
      maxWalkPoint = walkZone.bounds.max;
      hasWalkZone = true;
    }

    npcname = gameObject.GetComponentInChildren<Text>();
    //npcname.text = gameObject.name;
    canMove = true;
  }

  // Update is called once per frame
  void Update()
  {

    if (dialogueManager.dialoguePanel.activeSelf /*|| !questsManager.isDialogueActive()*/)
    {
      canMove = false;
    }
    if (!canMove)
    {
      myRigidBody.velocity = Vector2.zero;
      return;
    }

    if (isWalking)
    {
      walkCounter -= Time.deltaTime;
      if (walkCounter < 0)
      {
        isWalking = false;
        waitCounter = waitToWalkTime;
      }
      switch (walkDirection)
      {
        case 0:
          myRigidBody.velocity = new Vector2(0, moveSpeed);
          if (hasWalkZone && transform.position.y > maxWalkPoint.y)
          {
            isWalking = false;
            walkCounter = walkTime;
          }
          break;
        case 1:
          myRigidBody.velocity = new Vector2(moveSpeed, 0);
          if (hasWalkZone && transform.position.x > maxWalkPoint.x)
          {
            isWalking = false;
            walkCounter = walkTime;
          }
          break;
        case 2:
          myRigidBody.velocity = new Vector2(0, -moveSpeed);
          if (hasWalkZone && transform.position.y < minWalkPoint.y)
          {
            isWalking = false;
            walkCounter = walkTime;
          }
          break;
        case 3:
          myRigidBody.velocity = new Vector2(-moveSpeed, 0);
          if (hasWalkZone && transform.position.x < minWalkPoint.x)
          {
            isWalking = false;
            walkCounter = walkTime;
          }
          break;
      }
    }
    else
    {
      waitCounter -= Time.deltaTime;

      myRigidBody.velocity = Vector2.zero;
      if (waitCounter < 0)
      {
        chooseDirection();
      }
    }
  }

  public void chooseDirection()
  {
    walkDirection = Random.Range(0, 4);
    isWalking = true;
    walkCounter = walkTime;

  }
}
