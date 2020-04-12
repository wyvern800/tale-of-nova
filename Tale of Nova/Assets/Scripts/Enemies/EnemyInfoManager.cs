using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfoManager : MonoBehaviour
{
  public GameObject enemyInfoObject;
  private Image healthFill;
  private GameObject manaFill;
  private GameObject npcInfo;
  private static bool UIExists;

  private Text labelHealth;
  private Text labelName;
  private Text labelLevel;
  private Image face;

  // Start is called before the first frame update
  void Start()
  {
    healthFill = GameObject.Find("NpcInfo/HealthFill").GetComponent<Image>();
    labelHealth = enemyInfoObject.GetComponentInChildren<Text>();
    labelName = GameObject.Find("NpcInfo/LabelName").GetComponent<Text>();
    labelLevel = GameObject.Find("NpcInfo/LabelLevel").GetComponent<Text>();
    enemyInfoObject.SetActive(false);
  }

  // Update is called once per frame
  void Update()
  {
  }

  public void updateInterface(int currentHealth, int targetMaxHp, string name, int level)
  {
    enemyInfoObject.SetActive(true);
    healthFill.fillAmount = (float)currentHealth / targetMaxHp;
    labelHealth.text = currentHealth + "/" + targetMaxHp;
    labelName.text = "" + name;
    labelLevel.text = "" + level;
  }

  public void destroyInterface()
  {
    enemyInfoObject.SetActive(false);
    healthFill.fillAmount = 0f;
    labelHealth.text = "";
  }
}
