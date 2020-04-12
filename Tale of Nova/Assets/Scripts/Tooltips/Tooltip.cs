using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
  private PlayerController player;
  private Text tooltip;
  public GameObject toolTipOriginal;
  private GameObject toolTipClone;
  private Vector3 toolTipPosition;
  private Text tooltipText;

  private Image toolTipBg;

  // Start is called before the first frame update
  void Start()
  {
    player = FindObjectOfType<PlayerController>();

  }


  // Update is called once per frame
  void Update()
  {
    toolTipPosition = Input.mousePosition;
  }

  public void sendTooltip(int tooltipId)
  {
    player = FindObjectOfType<PlayerController>();
    toolTipOriginal.SetActive(true);
    switch (tooltipId)
    {
      case 0:
        int currentXp = player.getPlayerSkills().getCurrentXP(1);
        int requiredXP = player.getPlayerSkills().getRequiredXP(1);

        StartCoroutine(TooltipOnMouse("- Attacking -\n\nCurrent XP: " + currentXp + "\nXP Left: " + requiredXP));
        return;
      case 1:
        StartCoroutine(TooltipOnMouse("Billy"));
        break;
    }
  }

  public void sendTooltip(string tooltipMessage)
  {
    toolTipOriginal.SetActive(true);
    StartCoroutine(TooltipOnMouse(tooltipMessage));
  }

  public void destroyToolTip()
  {
    //toolTipOriginal.SetActive(false);
    DestroyImmediate(toolTipClone);
  }

  IEnumerator TooltipOnMouse(string text)
  {
    toolTipClone = Instantiate(toolTipOriginal, toolTipPosition, Quaternion.identity);
    tooltipText = toolTipClone.GetComponentInChildren<Text>();
    toolTipBg = toolTipClone.GetComponentInChildren<Image>();
    tooltipText.enabled = false;
    toolTipBg.enabled = false;
    yield return new WaitForSeconds(0.001f);
    if (toolTipClone != null)
    {
      tooltipText.text = text;
      tooltipText.enabled = true;
      toolTipBg.enabled = true;
    }
    yield return null;
  }
}
