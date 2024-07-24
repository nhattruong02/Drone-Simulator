// Decompiled with JetBrains decompiler
// Type: DroneCollision
// Assembly: DroneControllerPlugin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F0498C7-D6E3-4EA5-898B-1106F4644D80
// Assembly location: C:\Project Unity\New Unity Project\New Unity Project\Assets\DroneControllerPlugin.dll

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DroneCollision : MonoBehaviour
{
  [Tooltip("Sparks GameObject prefab that will be created when crashing the drone.")]
    public GameObject sparks;
    public Logic logic;
    private void Awake()
  {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>();
        if (this.sparks != null)
            return;
    MonoBehaviour.print((object) "Missing sparks particle prefab!");
  }

  private void OnCollisionStay(Collision other)
  {
    if (!other.transform)
      return;
    ContactPoint contact = other.contacts[0];
    Quaternion quaternion = Quaternion.FromToRotation(Vector3.up, ((ContactPoint)  contact).normal)* Quaternion.Euler(-90f, 0.0f, 0.0f);
    Vector3 point = ((ContactPoint) contact).point;
    if ( this.sparks != null)
    {
      logic.ReducedDurability();
      GameObject _spark = Object.Instantiate<GameObject>(this.sparks, point, quaternion);
      _spark.transform.localScale = transform.localScale * 2f;
      foreach (Transform transform in _spark.transform)
        transform.localScale = transform.localScale * 2f;
      this.StartCoroutine(this.SparksCleaner(_spark));
    }
    else
      Debug.LogError((object) "You did not assign a spark prefab effect, default is located in DroneController/Prefabs/...");
  }

  private IEnumerator SparksCleaner(GameObject _spark)
  {
    yield return (object) new WaitForSeconds(0.1f);
    Object.Destroy((Object) _spark);
  }
}
