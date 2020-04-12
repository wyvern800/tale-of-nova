using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour
{
  public string mapToLoad;
  public Vector2 position;
  public string exitPointName;

  private PlayerController player;


  // Start is called before the first frame update
  void Start()
  {
    player = FindObjectOfType<PlayerController>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.name == "Player")
    {
      SceneManager.LoadScene(mapToLoad);
      player.setStartPoint(exitPointName);
    }
  }
}
