namespace PiratesQuest;

using Godot;

public partial class Identity : Node
{
  private string _playerName = "Player";

  public override void _Ready()
  {
    GD.Print("Identity ready");
  }

  public string PlayerName
  {
    get => _playerName;
    set => _playerName = value;
  }
}