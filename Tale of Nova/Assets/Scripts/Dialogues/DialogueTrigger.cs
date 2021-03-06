﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
  private SpriteRenderer baloon;
  public Dialogue dialogue;

  // Start is called before the first frame update
  void Start()
  {
    baloon = GameObject.Find("InteractableNpc").GetComponent<SpriteRenderer>();
  }

  void Update()
  {

  }

  public void TriggerDialogue()
  {
    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

    if (transform.parent.GetComponent<VillagerMovement>() != null)
    {
      transform.parent.GetComponent<VillagerMovement>().canMove = false;
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.name == "Player")
    {
      StartCoroutine(SpriteFadeIn(baloon));
    }
  }

  void OnTriggerStay2D(Collider2D other)
  {
    if (other.gameObject.name == "Player")
    {
      if (Input.GetKeyUp(KeyCode.V))
      {
        TriggerDialogue();
      }
    }
  }

  void OnTriggerExit2D(Collider2D other)
  {
    if (other.gameObject.name == "Player")
    {
      FindObjectOfType<DialogueManager>().EndDialogue();
      StartCoroutine(SpriteFadeOut(baloon));
    }
  }



  IEnumerator SpriteFadeOut(SpriteRenderer _sprite)
  {

    Color tmpColor = _sprite.color;

    while (tmpColor.a > 0f)
    {
      tmpColor.a -= Time.deltaTime / 1.0f;
      _sprite.color = tmpColor;

      if (tmpColor.a <= 0f)
        tmpColor.a = 0.0f;

      yield return null;
    }

    _sprite.color = tmpColor;
  }

  IEnumerator SpriteFadeIn(SpriteRenderer _sprite)
  {

    Color tmpColor = _sprite.color;

    while (tmpColor.a < 1f)
    {
      tmpColor.a += Time.deltaTime / 1.0f;
      _sprite.color = tmpColor;

      if (tmpColor.a >= 1f)
        tmpColor.a = 1.0f;

      yield return null;
    }

    _sprite.color = tmpColor;
  }
}
