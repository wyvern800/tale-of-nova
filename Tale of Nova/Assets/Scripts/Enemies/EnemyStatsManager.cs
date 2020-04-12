using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatsManager : MonoBehaviour
{
  public int npcLevel;
  public int experience;
  private Text npcName;

  public int getExperience()
  {
    return this.experience;
  }

  public Image face;

  // Start is called before the first frame update
  void Start()
  {
    if (npcLevel <= 0)
      npcLevel = 1;

    //npcName = gameObject.GetComponentInChildren<Text>();
    //npcName.text = gameObject.name;
  }

  // Update is called once per frame
  void Update()
  {
  }

  public int getNpcLevel()
  {
    return npcLevel;
  }
}
