using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour
{
  private PlayerController player;
  private CameraController myCamera;

  public Vector2 startDirection;

  public string pointName;

  void Awake()
  {
    player = FindObjectOfType<PlayerController>();
  }
  // Start is called before the first frame update
  void Start()
  {
    if (player.startPoint == pointName)
    {
      player.transform.position = transform.position;
      player.lastMove = startDirection;

      myCamera = FindObjectOfType<CameraController>();
      myCamera.transform.position = new Vector3(transform.position.x, transform.position.y, myCamera.transform.position.z);
    }
    /*if (player.startPoint == null)
    {
      player.startPoint = "Portal to Out";
    }*/
  }

  // Update is called once per frame
  void Update()
  {

  }
}
