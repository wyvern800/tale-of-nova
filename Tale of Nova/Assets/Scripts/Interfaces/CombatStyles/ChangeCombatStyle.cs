using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;// Required when using Event data.

public class ChangeCombatStyle : MonoBehaviour/* , ISelectHandler*/
{
  private PlayerController player;
  private ToggleGroup toggleGroupInstance;

  private Toggle[] toggles;

  public Toggle currentSelection
  {
    get { return toggleGroupInstance.ActiveToggles().FirstOrDefault(); }
  }

  // Start is called before the first frame update
  void Start()
  {
    player = FindObjectOfType<PlayerController>();
    toggleGroupInstance = GetComponent<ToggleGroup>();
    SelectToggle(0);
  }

  public void SelectToggle(int id)
  {
    var toggles = toggleGroupInstance.GetComponentsInChildren<Toggle>();
    toggles[id].isOn = true;
    //Debug.Log(id);
    changeCombatStyle(id);
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void changeCombatStyle(int id)
  {
    player.attackMode = id;
  }
}
