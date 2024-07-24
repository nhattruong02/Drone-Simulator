// Decompiled with JetBrains decompiler
// Type: DroneController.Profiles.Profile
// Assembly: DroneControllerPlugin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F0498C7-D6E3-4EA5-898B-1106F4644D80
// Assembly location: C:\Project Unity\New Unity Project\New Unity Project\Assets\DroneControllerPlugin.dll

using System;

namespace DroneController.Profiles
{
  [Serializable]
  public class Profile
  {
    public int maxForwardSpeed;
    public int maxSidewaySpeed;
    public float slowDownTime;
    public float forceUpHover;
    public float forceDownHover;
    public float sideMovementSpeed;
    public float movementForwardSpeed;
    public float rotationAmount;
    public float tiltMovementSpeed;
    public float tiltNoMovementSpeed;
    public float wantedForwardTilt;
    public float wantedSideTilt;
    public float droneSoundAmplifier;
  }
}
