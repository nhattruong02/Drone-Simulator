// Decompiled with JetBrains decompiler
// Type: DroneController.CustomGUI.DrawGUI
// Assembly: DroneControllerPlugin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F0498C7-D6E3-4EA5-898B-1106F4644D80
// Assembly location: C:\Project Unity\New Unity Project\New Unity Project\Assets\DroneControllerPlugin.dll

using UnityEngine;

namespace DroneController.CustomGUI
{
  public class DrawGUI : MonoBehaviour
  {
    public static void DrawButton(float x, float y, float w, float h, string txt) => GUI.Button(new Rect(DrawGUI.position_x(x), DrawGUI.position_y(y), DrawGUI.size_x(w), DrawGUI.size_y(h)), txt);

    public static void DrawButton(
      float x,
      float y,
      float w,
      float h,
      string txt,
      GUIStyle style)
    {
      GUI.Button(new Rect(DrawGUI.position_x(x), DrawGUI.position_y(y), DrawGUI.size_x(w), DrawGUI.size_y(h)), txt, style);
    }

    public static void DrawButton(
      float x,
      float y,
      float w,
      float h,
      string txt,
      string hoverTxt,
      GUIStyle style)
    {
      GUI.Button(new Rect(DrawGUI.position_x(x), DrawGUI.position_y(y), DrawGUI.size_x(w), DrawGUI.size_y(h)), new GUIContent(txt, hoverTxt), style);
    }

    public static bool DrawButtonReturn(float x, float y, float w, float h, string txt) => GUI.RepeatButton(new Rect(DrawGUI.position_x(x), DrawGUI.position_y(y), DrawGUI.size_x(w), DrawGUI.size_y(h)), txt);

    public static bool DrawButtonReturn(
      float x,
      float y,
      float w,
      float h,
      string txt,
      string hoverTxt,
      GUIStyle style)
    {
      return GUI.Button(new Rect(DrawGUI.position_x(x), DrawGUI.position_y(y), DrawGUI.size_x(w), DrawGUI.size_y(h)), new GUIContent(txt, hoverTxt), style);
    }

    public static void DrawTexture(float x, float y, float w, float h, Texture texture) => GUI.DrawTexture(new Rect(DrawGUI.position_x(x), DrawGUI.position_y(y), DrawGUI.size_x(w), DrawGUI.size_y(h)), texture);

    public static Rect Percentages(Rect rect) => new Rect(DrawGUI.position_x(((Rect)  rect).x), DrawGUI.position_y(((Rect)  rect).y), DrawGUI.size_x(((Rect)  rect).width), DrawGUI.size_y(((Rect)  rect).height));

    public static Vector2 Percentages(Vector2 rect) => new Vector2((float) ((double) rect.x / (double) Screen.width * 100.0), (float) ((double) rect.y / (double) Screen.height * 100.0));

    private static float position_x(float var) => (float) ((double) Screen.width * (double) var / 100.0);

    private static float position_y(float var) => (float) ((double) Screen.height * (double) var / 100.0);

    private static float size_x(float var) => (float) ((double) Screen.width * (double) var / 100.0);

    private static float size_y(float var) => (float) ((double) Screen.height * (double) var / 100.0);
  }
}
