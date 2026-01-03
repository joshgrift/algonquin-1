using System;
using Godot;

partial class Configuration : Node
{
  public override void _Ready()
  {
    CallDeferred(MethodName.ConfigureWindowTitle);
  }

  private void ConfigureWindowTitle()
  {
    String title = $"Algonquin 1 {OS.GetCmdlineArgs().Join(" ")}";
    GD.Print($"setting title to {title}");
    DisplayServer.WindowSetTitle(title);
  }
}