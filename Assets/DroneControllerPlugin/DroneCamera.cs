// Decompiled with JetBrains decompiler
// Type: DroneCamera
// Assembly: DroneControllerPlugin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F0498C7-D6E3-4EA5-898B-1106F4644D80
// Assembly location: C:\Project Unity\New Unity Project\New Unity Project\Assets\DroneControllerPlugin.dll

using DroneController.CameraMovement;

public class DroneCamera : CameraScript
{
  public override void Awake() => base.Awake();

  private void FixedUpdate()
  {
    this.FPVTPSCamera();
    this.ScrollMath();
  }

  private void Update() => this.PickDroneToControl();
}
