// Decompiled with JetBrains decompiler
// Type: DroneMovement
// Assembly: DroneControllerPlugin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F0498C7-D6E3-4EA5-898B-1106F4644D80
// Assembly location: C:\Project Unity\New Unity Project\New Unity Project\Assets\DroneControllerPlugin.dll

using DroneController.Physics;

public class DroneMovement : DroneMovementScript
{
  public override void Awake() => base.Awake();

  private void FixedUpdate()
  {
    this.GetVelocity();
    this.ClampingSpeedValues();
    this.MovementUpDown();
    this.MovementLeftRight();
    this.Rotation();
    this.MovementForward();
    this.SettingControllerToInputSettings();
    this.BasicDroneHoverAndRotation();
  }

  private void Update()
  {
    this.RotationUpdateLoop_TrickRotation();
    this.Animations();
    this.DroneSound();
    this.CameraCorrectPickAndTranslatingInputToWSAD();
  }
}
