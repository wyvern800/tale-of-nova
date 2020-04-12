using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

  public Text nameText;
  public Text dialogueText;

  private Queue<string> sentences;

  public GameObject dialoguePanel;

  public Animator animator;

  // Start is called before the first frame update
  void Start()
  {
    sentences = new Queue<string>();
    setDialogueState(false);
  }

  public void StartDialogue(Dialogue dialogue)
  {
    setDialogueState(true);
    animator.SetBool("isOpen", true);

    nameText.text = dialogue.name;

    sentences.Clear();

    foreach (string sentence in dialogue.sentences)
    {
      sentences.Enqueue(sentence);
    }
    DisplayNextSentence();
  }

  public void DisplayNextSentence()
  {
    if (sentences.Count == 0)
    {
      EndDialogue();
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

  public void EndDialogue()
  {
    animator.SetBool("isOpen", false);
    setDialogueState(false);
  }

  public void setDialogueState(bool isVisible)
  {
    dialoguePanel.SetActive(isVisible);
  }
}
