using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
  public Dictionary<int, int> playerQuests;

  private Dictionary<int, int> playerQuestKills;
  private Dictionary<int, int> playerQuestObjectives;

  public string playerName;
  public float moveSpeed;
  private float currentMoveSpeed;
  public int attackMode;
  public float diagonalMoveModifier;
  private Vector3 scaleToChange;

  private Animator anim;
  private Rigidbody2D myRigidBody;
  private GameObject portalEntrance;

  private bool isPlayerMoving;
  private bool isPlayerRunning;

  public Vector2 lastMove;
  private static bool playerExist;

  private bool isPlayerAttacking;
  private PlayerHealthManager playerHealthManager;

  private StatsInterface statsInterface;

  private CombatStylesInterface combatStylesInterface;

  public PlayerHealthManager getPlayerHealthManager()
  {
    return this.playerHealthManager;
  }

  public bool isAttacking()
  {
    return isPlayerAttacking;
  }
  public void setPlayerAttacking(bool attacking)
  {
    this.isPlayerAttacking = attacking;
  }

  public float attackTime;
  private float attackTimeCounter;

  public string startPoint;
  public string getStartPoint()
  {
    return startPoint;
  }
  public void setStartPoint(string startPointName)
  {
    this.startPoint = startPointName;
  }

  private PlayerSkills playerSkills;

  public PlayerSkills getPlayerSkills()
  {
    return this.playerSkills;
  }

  private Text playerLabelll;

  private GameObject sword;
  private GameObject shield;

  // Start is called before the first frame update
  void Start()
  {
    playerSkills = GameObject.FindObjectOfType<PlayerSkills>();
    playerHealthManager = GameObject.FindObjectOfType<PlayerHealthManager>();
    statsInterface = GameObject.FindObjectOfType<StatsInterface>();
    combatStylesInterface = GameObject.FindObjectOfType<CombatStylesInterface>();

    playerQuests = new Dictionary<int, int>();
    playerQuestKills = new Dictionary<int, int>();
    playerQuestObjectives = new Dictionary<int, int>();

    anim = GetComponent<Animator>(); // Getting the component with animator
    myRigidBody = GetComponent<Rigidbody2D>();
    checkPlayerExistence();
    hideInterfaces();
    //playerLabelll = gameObject.GetComponentInChildren<Text>();
    //playerLabelll.text = playerName;
  }

  // Update is called once per frame
  void Update()
  {
    isPlayerMoving = false; // At start of every frame, it will tell that the player isn't moving

    if (!isAttacking())
    {
      // Character movements
      checkPlayerMovement();
      // Checks to make sure player is stopping movement
      checkPlayerControls();
    }
    // Check player attack
    checkPlayerAttack();
    // START - Sprites treating based on direction
    sendAnimationsSettings();
    // END
    sendInputs();
  }

  private void checkPlayerControls()
  {
    if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
    {
      myRigidBody.velocity = new Vector2(0f, myRigidBody.velocity.y);
    }
    if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
    {
      myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0f);
    }
    if (Input.GetMouseButton(0))
    {
      attackTimeCounter = attackTime;
      setPlayerAttacking(true);
      myRigidBody.velocity = Vector2.zero; // Frozen Player
      anim.SetBool("isPlayerAttacking", true);
    }
    if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
    {
      currentMoveSpeed = moveSpeed * diagonalMoveModifier;
    }
    else
    {
      currentMoveSpeed = moveSpeed;
    }
  }

  private void checkPlayerMovement()
  {
    if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
    {
      //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
      myRigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * currentMoveSpeed, myRigidBody.velocity.y);
      isPlayerMoving = true;
      lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
    }
    if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
    {
      //transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
      myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, Input.GetAxisRaw("Vertical") * currentMoveSpeed);
      isPlayerMoving = true;
      lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
    }
  }

  public void sendDeath()
  {
    // TODO
  }

  private void checkPlayerAttack()
  {
    if (attackTimeCounter > 0)
    {
      attackTimeCounter -= Time.deltaTime;
    }
    if (attackTimeCounter <= 0)
    {
      setPlayerAttacking(false);
      anim.SetBool("isPlayerAttacking", false);
    }
  }

  void checkPlayerExistence()
  {
    if (!playerExist)
    {
      playerExist = true;
      DontDestroyOnLoad(transform.gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }

  private void sendInputs()
  {
    //Character stats
    if (Input.GetKeyDown(KeyCode.C))
    {
      statsInterface.sendInterface();
    }
    //Combat style
    if (Input.GetKeyDown(KeyCode.P))
    {
      combatStylesInterface.sendInterface();
    }
  }

  private void hideInterfaces()
  {
    combatStylesInterface.closeInterface();
    statsInterface.closeInterface();
  }

  private void sendAnimationsSettings()
  {
    anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
    anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
    anim.SetBool("isPlayerMoving", isPlayerMoving);
    anim.SetFloat("LastMoveX", lastMove.x);
    anim.SetFloat("LastMoveY", lastMove.y);
  }

  public void setQuestStatus(Quest quest, int status)
  {
    string questStatus = (status == 0 ? "STARTED" : status == 1 ? "STARTED" : status == 2 ? "STEP 1" : "DONE");

    addOrUpdate(playerQuests, quest.questId, status);
    Debug.Log("[PlayerController/SetQuestStatus] QuestId: " + quest.questId + " - VALUE: " + status + " - Status: " + questStatus + ".");

    /*foreach (KeyValuePair<int, int> pair in playerQuests)
    {
      if (pair.Key == quest.questId)
      {
        Debug.Log("[SetQuestStatus] QuestId: " + pair.Key + " - VALUE: " + pair.Value + " - QuestStatus: " + questStatus);
      }
    }*/
  }

  public void setQuestStatus(int questId, int status) //Quest
  {
    Debug.Log("[SetQuestStatus] QuestId: " + questId + " - Status: " + status);
    addOrUpdate(playerQuests, questId, status);
  }

  public int getQuestStatus(Quest quest)
  {
    foreach (KeyValuePair<int, int> pair in playerQuests)
    {
      if (pair.Key == quest.questId)
      { // if quest is the same
        Debug.Log("[GetQuestStatus] QuestId: " + pair.Key + " VALUE: " + pair.Value);
        return pair.Value;
      }
    }
    return 0;
  }

  public int getQuestStatus(int questId)
  {
    foreach (KeyValuePair<int, int> pair in playerQuests)
    {
      if (pair.Key == questId)
      { // if quest is the same
        Debug.Log("[PlayerController/getQuestKills] QuestId: " + pair.Key + " VALUE: " + pair.Value);
        return pair.Value;
      }
    }
    return 0;
  }

  public void setQuestKills(int questId, int quantity) //Quest
  {
    Debug.Log("[PlayerController/setQuestStatus] QuestId: " + questId + " - Quantity: " + quantity);
    addOrUpdate(playerQuestKills, questId, quantity);
  }

  public void setQuestObjectiveComplete(int questId) //Quest
  {
    Debug.Log("[PlayerController/setQuestStatus] QuestId: " + questId + " - Quantity: " + 1);
    addOrUpdate(playerQuestObjectives, questId, 1);
  }

  void addOrUpdate(Dictionary<int, int> dic, int key, int newValue)
  {
    int val;
    if (dic.TryGetValue(key, out val))
    {
      // yay, value exists!
      dic[key] = val + newValue;
    }
    else
    {
      // darn, lets add the value
      dic.Add(key, newValue);
    }
  }

  public int getQuestObjectives(int questId)
  {
    foreach (KeyValuePair<int, int> pair in playerQuestObjectives)
    {
      if (pair.Key == questId)
      { // if quest is the same
        Debug.Log("[PlayerController/getQuestObjectives] QuestId: " + pair.Key + " VALUE: " + pair.Value);
        return pair.Value;
      }
    }
    return 0;
  }

  public int getQuestKills(int questId)
  {
    foreach (KeyValuePair<int, int> pair in playerQuestKills)
    {
      if (pair.Key == questId)
      { // if quest is the same
        Debug.Log("[PlayerController/getQuestKills] QuestId: " + pair.Key + " VALUE: " + pair.Value);
        return pair.Value;
      }
    }
    return 0;
  }
}
