using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoManager : MonoBehaviour
{
  public GameObject playerInfoObject;
  private Image healthFill;
  private GameObject manaFill;
  private PlayerHealthManager healthManager;
  private PlayerController player;

  private PlayerSkills playerSkills;
  private static bool UIExists;

  private Text labelHealth;
  private Text labelLevel;
  private Text labelName;
  private int combatLevel;

  private int currentHp;
  private int maxHp;
  private int maxHpWithOffset;

  private Sprite charFace;

  // Start is called before the first frame update
  void Start()
  {
    player = FindObjectOfType<PlayerController>();
    playerSkills = FindObjectOfType<PlayerSkills>();
    healthFill = GameObject.Find("CharacterInfo/HealthFill").GetComponent<Image>();
    labelLevel = GameObject.Find("CharacterInfo/LabelLevel").GetComponent<Text>();
    labelName = GameObject.Find("CharacterInfo/LabelName").GetComponent<Text>();
    labelHealth = playerInfoObject.GetComponentInChildren<Text>();
    checkIfUIExists();
  }

  // Update is called once per frame
  void Update()
  {
    currentHp = player.getPlayerHealthManager().getCurrentHealth();
    maxHp = player.getPlayerHealthManager().getMaxHealth();
    combatLevel = player.getPlayerSkills().getLevel(0);
    labelHealth.text = currentHp + "/" + maxHp;
    labelLevel.text = "" + combatLevel;
    labelName.text = "" + player.playerName;
    healthFill.fillAmount = (float)currentHp / maxHp;
  }

  private void checkIfUIExists()
  {
    if (!UIExists)
    {
      UIExists = true;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }
}
