namespace Algonquin1.Partials;

using Godot;

/// <summary>
/// Interface for objects that can collect items from a CollectionPoint or other locations.
/// </summary>
public partial class ProjectilePartial : RigidBody3D
{
  public string PlayerId { get; private set; }

  [Export] public int Damage = 100;

  [Export] public int Speed = 40;

  [Export] public int TimeToLiveInSeconds = 5;

  private float _timeAlive = 0.0f;

  private Vector3 _targetVelocity = Vector3.Zero;

  private float _currentSpeed = 0.0f;

  public override void _Ready()
  {
    BodyEntered += OnBodyEntered;
  }

  public void Launch(Vector3 direction, float launcherSpeed, string playerId)
  {
    PlayerId = playerId;
    direction.Y = 0;
    _targetVelocity = direction.Normalized();
    _currentSpeed = Speed + launcherSpeed;

    _targetVelocity.X *= _currentSpeed;
    _targetVelocity.Z *= _currentSpeed;
    LinearVelocity = _targetVelocity;
  }

  public override void _Process(double delta)
  {
    _timeAlive += (float)delta;
    if (_timeAlive >= TimeToLiveInSeconds)
    {
      QueueFree();
    }
  }

  private void OnBodyEntered(Node body)
  {
    GD.Print($"Cannonball hit: {body.Name}");
  }
}

