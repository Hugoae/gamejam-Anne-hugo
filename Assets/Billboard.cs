using UnityEngine;
public class Billboard : MonoBehaviour
{
  void LateUpdate()
  {
    var camera = Camera.main;
    if (camera != null)
        transform.LookAt(transform.position + camera.transform.forward);
  }
}
