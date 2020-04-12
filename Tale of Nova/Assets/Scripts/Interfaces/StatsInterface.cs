using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsInterface : MonoBehaviour
{
  public GameObject statsInterface;
  private Image healthFill;
  private GameObject manaFill;
  private PlayerHealthManager healthManager;
  private PlayerController player;

  private PlayerSkills playerSkills;
  private static bool UIExists;
  private int maxHpWithOffset;
  public Text attackLevel;
  private int currentAttackExp;
  public Text blockLevel;
  private int currentBlockExp;
  public Text lifeLevel;
  private int currentLifeExp;
  public Text labelName;

  private Sprite charFace;

  private bool opened;

  public Image attackXP;
  public Image blockingXP;
  public Image lifeXP;

  // Start is called before the first frame update
  void Start()
  {
    player = FindObjectOfType<PlayerController>();
    playerSkills = FindObjectOfType<PlayerSkills>();
    /*attackLevel = GameObject.Find("Interfaces/Skills/AttackLevel").GetComponent<Text>();
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
    currentAttackExp = player.getPlayerSkills().getCurrentXP(1);
    currentBlockExp = player.getPlayerSkills().getCurrentXP(2);
    currentLifeExp = player.getPlayerSkills().getCurrentXP(3);

    attackLevel.text = "" + playerSkills.getLevel(1) + "/" + playerSkills.MAX_SKILL_LEVEL;
    blockLevel.text = "" + playerSkills.getLevel(2) + "/" + playerSkills.MAX_SKILL_LEVEL;
    lifeLevel.text = "" + playerSkills.getLevel(3) + "/" + playerSkills.MAX_SKILL_LEVEL;
    labelName.text = "" + player.playerName + "'s Stats";
    attackXP.fillAmount = (float)currentAttackExp / (float)player.getPlayerSkills().getRequiredXP(player.getPlayerSkills().getLevel(1));
    blockingXP.fillAmount = (float)currentBlockExp / (float)player.getPlayerSkills().getRequiredXP(player.getPlayerSkills().getLevel(2));
    lifeXP.fillAmount = (float)currentLifeExp / player.getPlayerSkills().getRequiredXP(player.getPlayerSkills().getLevel(3));
  }

  public void sendInterface()
  {
    if (opened)
    {
      statsInterface.SetActive(false);
      opened = false;
    }
    else
    {
      statsInterface.SetActive(true);
      opened = true;
    }
  }

  public void closeInterface()
  {
    statsInterface.SetActive(false);
  }
}
