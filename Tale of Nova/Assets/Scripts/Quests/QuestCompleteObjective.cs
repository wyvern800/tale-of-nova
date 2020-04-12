using UnityEngine;

public class QuestCompleteObjective : MonoBehaviour
{


  public int questId;

  private void Start()
  {
  }

  void OnTriggerStay2D(Collider2D other)
  {
    if (other.gameObject.name == "Player")
    {
      //if (gameObject.tag == "QuestObject")
      //{
      if (Input.GetKeyUp(KeyCode.V))
      {
        FindObjectOfType<QuestsManager>().completeObjective(questId);
      }
      //}
    }
  }
}