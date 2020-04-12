using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatStylesInterface : MonoBehaviour
{
  public GameObject combatStylesInterface;
  private PlayerController player;

  private bool opened;

  // Start is called before the first frame update
  void Start()
  {
    player = FindObjectOfType<PlayerController>();
    /*playerSkills = FindObjectOfType<PlayerSkills>();
    attackLevel = GameObject.Find("Interfaces/Skills/AttackLevel").GetComponent<Text>();
    blockLevel = GameObject.Find("Interfaces/Skills/BlockLevel").GetComponent<Text>();
    lifeLevel = GameObject.Find("Interfaces/Skills/LifeLevel").GetComponent<Text>();
    labelName = GameObject.Find("Skills/PlayerName").GetComponent<Text>();
    attackXP = GameObject.Find("Interfaces/Skills/AttackXP").GetComponent<Image>();
    blockingXP = GameObject.Find("Interfaces/Skills/BlockingXP").GetComponent<Image>();
    lifeXP = GameObject.Find("Interfaces/Skills/LifeXP").GetComponent<Image>();*/
  }

  // Update is called once per frame
  void Update()
  {
    /*currentAttackExp = player.getPlayerSkills().getCurrentXP(1);
    currentBlockExp = player.getPlayerSkills().getCurrentXP(2);
    currentLifeExp = player.getPlayerSkills().getCurrentXP(3);

    attackLevel.text = "" + player.getPlayerSkills().getLevel(1) + "/" + playerSkills.MAX_SKILL_LEVEL;
    blockLevel.text = "" + player.getPlayerSkills().getLevel(2) + "/" + playerSkills.MAX_SKILL_LEVEL;
    lifeLevel.text = "" + player.getPlayerSkills().getLevel(3) + "/" + playerSkills.MAX_SKILL_LEVEL;
    labelName.text = "" + player.playerName + "'s Stats";
    attackXP.fillAmount = (float)currentAttackExp / (float)player.getPlayerSkills().getRequiredXP(player.getPlayerSkills().getLevel(1));
    blockingXP.fillAmount = (float)currentBlockExp / (float)player.getPlayerSkills().getRequiredXP(player.getPlayerSkills().getLevel(2));
    lifeXP.fillAmount = (float)currentLifeExp / player.getPlayerSkills().getRequiredXP(player.getPlayerSkills().getLevel(3));*/
  }

  public void sendInterface()
  {
    if (opened)
    {
      combatStylesInterface.SetActive(false);
      opened = false;
    }
    else
    {
      combatStylesInterface.SetActive(true);
      opened = true;
    }
  }

  public void closeInterface()
  {
    combatStylesInterface.SetActive(false);
  }
}
