using UnityEngine;

[System.Serializable]
public class Quest
{
  public int questId;
  public string questName;

  [TextArea(3, 8)]
  public string questDescription;

  [TextArea(3, 8)]
  public string questObjectivesOrNpcToKillName;

  public int experienceToGive;

  public bool isKillQuest;
  public int quantityToKill;

}