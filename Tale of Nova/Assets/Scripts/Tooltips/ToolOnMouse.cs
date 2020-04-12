using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolOnMouse : MonoBehaviour
{
  public Canvas myCanvas;
  public Vector3 offset;

  void Update()
  {
    transform.position = Input.mousePosition + offset;
  }
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per fram
}
