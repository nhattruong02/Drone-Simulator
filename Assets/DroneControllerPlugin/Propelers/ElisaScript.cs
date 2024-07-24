// Decompiled with JetBrains decompiler
// Type: DroneController.Propelers.ElisaScript
// Assembly: DroneControllerPlugin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F0498C7-D6E3-4EA5-898B-1106F4644D80
// Assembly location: C:\Project Unity\New Unity Project\New Unity Project\Assets\DroneControllerPlugin.dll

using DroneController.Physics;
using System.Drawing;
using UnityEngine;

namespace DroneController.Propelers
{
  public class ElisaScript : MonoBehaviour
  {
    [Tooltip("Propellers from your drone. Assing cross propellers. X ")]
    public GameObject[] elisa;
    [Tooltip("How fast propellers will rotate when they are idle.")]
    public float idleRotationSpeed = 1000f;
    [Tooltip("How fast propellers will rotate when we are moving.")]
    public float movingRotationSpeed = 2000f;
    [Tooltip("Fixing propellers rotation if somethign went wrong during import from Blender or 3DsMax.")]
    public float elisaAngle;
    public bool spinDifference = true;
    [HideInInspector]
    public float atWhatSpeedsShowWingtipVortices = 20f;
    private float currentYRotation;
    private float rotationSpeed = 1000f;
    private int amountOfWingtipVorticesOnElisas = 0;
    private ParticleSystem[] wingtipVortices;
    private float wantedAlpha;
    private float currentAlpha;
    private DroneMovementScript droneMovementScript;

    public virtual void Awake() => this.droneMovementScript = ((Component) this).GetComponent<DroneMovementScript>();

    public void RotationInputs()
    {
      if (Input.GetKey(this.droneMovementScript.forward) || Input.GetKey(this.droneMovementScript.downward) || Input.GetKey(this.droneMovementScript.downButton) || Input.GetKey(this.droneMovementScript.leftward) || Input.GetKey(this.droneMovementScript.rightward) || Input.GetKey(this.droneMovementScript.upward) || Input.GetKey(this.droneMovementScript.downButton) || Input.GetKey(this.droneMovementScript.downward))
        this.rotationSpeed = this.movingRotationSpeed;
      else
        this.rotationSpeed = this.idleRotationSpeed;
    }

    public void RotationDifferentials()
    {
      this.currentYRotation += Time.deltaTime * this.rotationSpeed;
      for (int index = 0; index < this.elisa.Length; ++index)
        this.elisa[index].transform.localRotation = !this.spinDifference ? Quaternion.Euler(new Vector3(this.elisaAngle, this.currentYRotation, ((Component) this).transform.rotation.z)) : (index % 2 != 0 ? Quaternion.Euler(new Vector3(this.elisaAngle, -this.currentYRotation, ((Component) this).transform.rotation.z)) : Quaternion.Euler(new Vector3(this.elisaAngle, this.currentYRotation, ((Component) this).transform.rotation.z)));
    }

    private void LocateWintipParticles()
    {
      this.amountOfWingtipVorticesOnElisas = 0;
      for (int index = 0; index < this.elisa.Length; ++index)
      {
        if ( this.elisa[index].GetComponent<ParticleSystem>() != null)
          ++this.amountOfWingtipVorticesOnElisas;
      }
      this.wingtipVortices = new ParticleSystem[this.amountOfWingtipVorticesOnElisas];
      for (int index = 0; index < this.amountOfWingtipVorticesOnElisas; ++index)
        this.wingtipVortices[index] = this.elisa[index].GetComponent<ParticleSystem>();
    }

    private void WingtipVortices()
    {
      this.wantedAlpha = (double) ((Component) this).GetComponent<DroneMovementScript>().velocity < (double) this.atWhatSpeedsShowWingtipVortices ? 0.0f : 0.2f;
      this.currentAlpha = Mathf.Lerp(this.currentAlpha, this.wantedAlpha, Time.deltaTime * 3f);
      if (this.wingtipVortices.Length == 0)
        return;
      foreach (ParticleSystem wingtipVortex in this.wingtipVortices)
      {
        wingtipVortex.Play();
        ParticleSystem.MainModule main1 = wingtipVortex.main;
        ref ParticleSystem.MainModule local = ref main1;
        ParticleSystem.MainModule main2 = wingtipVortex.main;
        ParticleSystem.MinMaxGradient startColor = ((ParticleSystem.MainModule)  main2).startColor;
        double r = (double) ((ParticleSystem.MinMaxGradient)  startColor).color.r;
        main2 = wingtipVortex.main;
        startColor = ((ParticleSystem.MainModule)  main2).startColor;
        double g = (double) ((ParticleSystem.MinMaxGradient)  startColor).color.g;
        main2 = wingtipVortex.main;
        startColor = ((ParticleSystem.MainModule)  main2).startColor;
        double b = (double) ((ParticleSystem.MinMaxGradient)  startColor).color.b;
        double currentAlpha = (double) this.currentAlpha;
        ParticleSystem.MinMaxGradient minMaxGradient = new ParticleSystem.MinMaxGradient(new UnityEngine.Color((float) r, (float) g, (float) b, (float) currentAlpha));
/*        ((ParticleSystem.MainModule)  local).startColor = minMaxGradient;*/
      }
    }
  }
}
