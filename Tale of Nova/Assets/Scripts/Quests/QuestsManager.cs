using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestsManager : MonoBehaviour
{

  private enum QuestStatus
  {
    NON_STARTED, STARTED, DOING_STEP_1, DOING_STEP_2, DONE
  }

  public Text nameText;
  public Text continueButton;
  public Text dialogueText;


  public Text questName;
  public Text questDescription;

  private Queue<string> sentences;

  public GameObject dialoguePanel;
  public GameObject questPanel;

  public Animator animator;

  private PlayerController player;

  private Quest quest;

  public QuestsManager questsManager;

  public QuestsManager getQuestsManager()
  {
    return this.questsManager;
  }

  // Start is called before the first frame update
  void Start()
  {
    sentences = new Queue<string>();
    player = FindObjectOfType<PlayerController>();
    questPanel.SetActive(false);
    questsManager = FindObjectOfType<QuestsManager>();
  }


  // Erro começa aqui
  public void StartDialogue(Dialogue dialogue, Quest quest)
  {
    this.quest = quest;
    animator.SetBool("isOpen", true);
    nameText.text = dialogue.name;
    sentences.Clear();



    if (player.getQuestStatus(quest.questId) == (int)QuestStatus.DONE)
    {
      // Quest done
      sendTabs();
      StopAllCoroutines();
      StartCoroutine(TypeSentence("You already helped this person before =)"));
      Debug.Log("QUEST DONE");
    }
    else if (player.getQuestStatus(quest.questId) == (int)QuestStatus.STARTED && quest.isKillQuest)
    {
      // quest started
      if (player.getQuestKills(quest.questId) == quest.quantityToKill)
      {
        CompleteQuest();
      }
      else
      {
        Debug.Log("StartDialogue/isKillQuest && didnt kill all");
        StopAllCoroutines();
        StartCoroutine(TypeSentence("You still didn't do what I demanded, you killed: " + player.getQuestKills(quest.questId) + "/" + quest.quantityToKill + " " + quest.questObjectivesOrNpcToKillName + " !"));
      }
    }
    else if (player.getQuestStatus(quest.questId) == (int)QuestStatus.STARTED && !quest.isKillQuest)
    {
      // DOIng step 1
      if (player.getQuestObjectives(quest.questId) == 1)
      {
        CompleteQuest();
      }
      else
      {
        sendTabs();
        Debug.Log("StartDialogue/DOING_STEP_1");
        StopAllCoroutines();
        StartCoroutine(TypeSentence("You still didn't do what I demanded, your objectives are: " + quest.questObjectivesOrNpcToKillName));
      }
    }
    else
    {
      // normal dialogues
      Debug.Log("StartDialogue/normal dialogue");
      foreach (string sentence in dialogue.sentences)
      {
        sentences.Enqueue(sentence);
      }
      DisplayNextSentence();
    }
  }

  private void sendTabs()
  {
    questPanel.SetActive(false);
    dialoguePanel.SetActive(true);
  }

  //Termina aqui

  /*public void StartDialogue(Dialogue dialogue, Quest quest)
  {
    this.quest = quest;
    animator.SetBool("isOpen", true);
    nameText.text = dialogue.name;
    sentences.Clear();

    if (player.getQuestStatus(quest) == (int)QuestStatus.STARTED) // started the quest
    {
      if (quest.isKillQuest)
      {
        if (player.getQuestKills(quest.questId) < quest.quantityToKill) // is kill ques and didnt kill all
        {
          continueButton.text = "Click to Close";
          StopAllCoroutines();
          StartCoroutine(TypeSentence("You still didn't do what I demanded, you killed: " + player.getQuestKills(quest.questId) + "/" + quest.quantityToKill + " " + quest.questObjectivesOrNpcToKillName + " !"));
        }
        else
        {
          CompleteQuest();
        }
      }
      else
      {
        if (player.getQuestStatus(quest) != (int)QuestStatus.DOING_STEP_1) // done step 1
        {
          continueButton.text = "Click to Close";
          StopAllCoroutines();
          StartCoroutine(TypeSentence("You still didn't do what I demanded, your objectives are: " + quest.questObjectivesOrNpcToKillName));
        }
      }
    }
    else if (player.getQuestStatus(quest) == (int)QuestStatus.DOING_STEP_1)
    {
      CompleteQuest();
    }
    else if (player.getQuestStatus(quest) == (int)QuestStatus.DONE)
    { // Done
      continueButton.text = "Click to Close";
      StopAllCoroutines();
      StartCoroutine(TypeSentence("You already helped this person before =)"));
    }
    else
    {

      foreach (string sentence in dialogue.sentences)
      {
        sentences.Enqueue(sentence);
      }
      DisplayNextSentence();
    }

  }*/

  public void checkQuestKills(int questId)
  {
    if (player.playerQuests.ContainsKey(questId))
    {
      if (player.getQuestKills(questId) > quest.quantityToKill)
        return;

      player.setQuestKills(questId, 1);
      Debug.Log("[QuestManager/killedSufficient] Quest: " + questId + " increased 1 kill ");
    }
  }

  public void completeObjective(int questId)
  {
    if (player.playerQuests.ContainsKey(questId))
    {
      if (player.getQuestObjectives(questId) == 1)
        return;
      player.setQuestObjectiveComplete(questId);
      Debug.Log("[QuestManager/completeObjective] Quest: " + questId + " set to complete");
    }
  }

  public void DisplayNextSentence()
  {
    // If player has started the quest already, then the continue button will end his dialogue.
    if (hasStartedQuest())
    {
      EndDialogue();
      return;
    }

    if (sentences.Count == 0)
    {
      dialoguePanel.SetActive(false);
      sendQuestInterface(quest);
      return;
    }

    string sentence = sentences.Dequeue();
    StopAllCoroutines();
    StartCoroutine(TypeSentence(sentence));
  }

  IEnumerator TypeSentence(string sentence)
  {
    dialogueText.text = "";

    foreach (char letter in sentence.ToCharArray())
    {
      dialogueText.text += letter;
      yield return null;
    }
  }

  IEnumerator TypeQuestDescription(string sentence)
  {
    questDescription.text = "";

    foreach (char letter in sentence.ToCharArray())
    {
      questDescription.text += letter;
      yield return null;
    }
  }

  public void EndDialogue()
  {
    StopAllCoroutines();
    StartCoroutine(CloseTabs());
  }

  IEnumerator CloseTabs()
  {
    animator.SetBool("isOpen", false);
    yield return new WaitForSeconds(0.3f);
    questPanel.SetActive(false);
    dialoguePanel.SetActive(true);
  }


  public void sendQuestInterface(Quest quest)
  {
    questPanel.SetActive(true);
    questName.text = quest.questName;
    string sentence = quest.questDescription;
    StopAllCoroutines();
    StartCoroutine(TypeQuestDescription(sentence));
  }

  public void AcceptQuest()
  {
    player.setQuestStatus(quest, (int)QuestStatus.STARTED);
    EndDialogue();
  }

  public void DeclineQuest()
  {
    EndDialogue();
  }

  public void CompleteQuest()
  {
    questPanel.SetActive(false);
    dialoguePanel.SetActive(true);
    StopAllCoroutines();
    StartCoroutine(TypeQuestDescription("Thanks for helping me, now take your rewards as you deserve more than I: \n\n Reward: " + quest.experienceToGive + " Experience!"));
    player.setQuestStatus(quest, (int)QuestStatus.DONE);
    Debug.Log("[QuestManager/CompleteQuest] Quest: " + quest.questName + " is completed ");
  }

  private bool hasStartedQuest()
  {
    return (player.getQuestStatus(quest) == (int)QuestStatus.STARTED || player.getQuestStatus(quest) == (int)QuestStatus.DOING_STEP_1 /*|| player.getQuestStatus(quest) == (int)QuestStatus.DOING_STEP_2 */|| player.getQuestStatus(quest) == (int)QuestStatus.DONE);
  }

  public bool isDialogueActive()
  {
    return dialoguePanel.activeSelf || questPanel.activeSelf;
  }
}
