using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
  public int enemyCurrentHealth;
  public int enemyMaxHealth;
  private GameObject thePlayer;
  private EnemyInfoManager enemyInfoManager;
  private EnemyStatsManager enemyStats;
  private PlayerController player;
  private RespawnController respawnController;
  private bool isDead;

  private QuestKillsIncrease qki;

  private QuestsManager questsManager;
  private Quest quest;

  public void setIsDead(bool isDead)
  {
    this.isDead = isDead;
  }


  // Start is called before the first frame update
  void Start()
  {
    player = FindObjectOfType<PlayerController>();
    enemyCurrentHealth = enemyMaxHealth;
    enemyInfoManager = FindObjectOfType<EnemyInfoManager>();
    enemyStats = FindObjectOfType<EnemyStatsManager>();
    respawnController = FindObjectOfType<RespawnController>();
    qki = FindObjectOfType<QuestKillsIncrease>();
    questsManager = FindObjectOfType<QuestsManager>();
  }

  // Update is called once per frame
  void Update()
  {
    if (enemyCurrentHealth <= 0)
    {
      enemyCurrentHealth = 0;
      isDead = true;
      enemyCurrentHealth = enemyMaxHealth;
      StartCoroutine(respawnController.RespawnNpc(gameObject));
      player.getPlayerSkills().addXp(0, enemyStats.getExperience());
      if (player.attackMode == 0)
      { // Xp in Attacking
        player.getPlayerSkills().addXp(1, enemyStats.getExperience());
      }
      else if (player.attackMode == 1)
      { // Xp in Defence
        player.getPlayerSkills().addXp(2, enemyStats.getExperience());
      }
      else
      { // Xp in attack and defence / 2
        player.getPlayerSkills().addXp(1, enemyStats.getExperience() / 2);
        player.getPlayerSkills().addXp(2, enemyStats.getExperience() / 2);
      }

      questsManager.checkQuestKills(qki.questId);

      player.getPlayerSkills().addXp(3, enemyMaxHealth / 2);
      enemyInfoManager.destroyInterface();
      Debug.Log("Exp gained: " + enemyStats.getExperience() + " -  Total: " + player.getPlayerSkills().getXp(0));
      Debug.Log("Life XP gained: " + enemyStats.getExperience());
    }
  }

  public void HurtEnemy(int damage)
  {
    if (damage > 0)
    {
      enemyCurrentHealth -= damage;
    }
    enemyInfoManager.updateInterface(getCurrentHealth(), getMaxHealth(), this.gameObject.name, enemyStats.getNpcLevel());
    //Debug.Log("Enemy hurt with " + damage + " dps");
  }

  public void SetMaxHealth()
  {
    enemyCurrentHealth = enemyMaxHealth;
  }

  public int getMaxHealth()
  {
    return enemyMaxHealth;
  }

  public int getCurrentHealth()
  {
    return enemyCurrentHealth;
  }
}

