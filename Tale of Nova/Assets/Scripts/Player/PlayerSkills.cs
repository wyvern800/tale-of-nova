using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
  public enum Skills
  {
    COMBAT
  }
  private PlayerController player;
  private int MAXIMUM_EXP = 900;
  public int MAX_SKILL_LEVEL = 99;
  public int MAX_COMBAT_LEVEL = 10;
  public int[] level;
  public int[] xp;

  private string[] skillNames = { "Combat", "Attacking", "Blocking", "Life" };


  public string getSkillName(int skillId)
  {
    return skillNames[skillId];
  }
  public int getXp(int i)
  {
    return this.xp[i];
  }

  public int[] xpRequiredPerLevel;
  public GameObject levelUpParticle;

  //public string[] skillName = { "Attack", "Herblore", "Woodcutting" };

  // Start is called before the first frame update
  void Start()
  {
    player = GameObject.FindObjectOfType<PlayerController>();
    level = new int[4];
    xp = new int[4];

    for (int i = 0; i < level.Length; i++)
    {
      if (getLevel(i) < 1)
      {
        level[i] = 1;
      }
    }
  }

  // Update is called once per frame
  void Update()
  {
  }

  public void addXp(int skill, int exp)
  {
    //Check if its max skill level
    if ((level[skill] >= MAX_SKILL_LEVEL) && skill != 0)
    {
      level[skill] = MAX_SKILL_LEVEL;
      Debug.Log("Max skill level reached");
      return;
    }
    //Check if its max combat level
    if (((level[skill] >= MAX_COMBAT_LEVEL) && skill == 0))
    {
      level[skill] = MAX_COMBAT_LEVEL;
      Debug.Log("Max combat level reached");
      return;
    }
    //check if xp is the maximum
    if (xp[skill] >= MAXIMUM_EXP)
    {
      xp[skill] = MAXIMUM_EXP;
      Debug.Log("Max XP reached");
      return;
    }
    //Check if is a level up
    if (xp[skill] >= xpRequiredPerLevel[level[skill]])
    {
      xp[skill] = 0;
      level[skill]++;
      sendLevelUp(skillNames[skill], level[skill]);
      Debug.Log("Level up");
    }
    //give the actual xp
    xp[skill] += exp;
  }

  public int getRequiredXP(int level)
  {
    return xpRequiredPerLevel[level];
  }

  /*public int getRequiredXP(int level, int skill)
  {
    return xpRequiredPerLevel[level[skill]];
  }*/

  public int getCurrentXP(int skill)
  {
    int value = xp[skill];
    return value;
  }

  private void sendLevelUp(string skill, int level)
  {
    Debug.Log("A player leveled " + skill + " up to level " + level + "!");
    player.getPlayerHealthManager().SetMaxHealth();
    Instantiate(levelUpParticle, new Vector2(player.transform.position.x, player.transform.position.y + 0.9f), player.transform.rotation);
  }

  public int[] getLevels()
  {
    return level;
  }

  public int getLevel(int skill)
  {
    return level[skill];
  }
}
