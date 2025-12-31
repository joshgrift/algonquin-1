using Godot;
using System;

public partial class Play : Node3D
{
	[Export] private MultiplayerSpawner _playerSpawner;
	private PackedScene _playerScene = GD.Load<PackedScene>("res://player.tscn");

	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;

		if (Multiplayer.IsServer())
		{
			GD.Print($"Server ready. Connected peers: {string.Join(", ", Multiplayer.GetPeers())}");

			// Spawn for the host
			SpawnPlayer(Multiplayer.GetUniqueId());

			// Spawn for any peers that are already connected
			foreach (var peerId in Multiplayer.GetPeers())
			{
				GD.Print($"Spawning for already-connected peer {peerId}");
				SpawnPlayer(peerId);
			}

			// Listen for new peers and spawn for them
			Multiplayer.PeerConnected += OnPeerConnected;
		}
		else
		{
			GD.Print($"Client ready. My peer ID: {Multiplayer.GetUniqueId()}");
		}
	}

	private void OnPeerConnected(long peerId)
	{
		GD.Print($"Peer {peerId} connected, spawning their player");
		SpawnPlayer(peerId);
	}

	private void SpawnPlayer(long peerId)
	{
		var player = _playerScene.Instantiate<CharacterBody3D>();
		player.Name = $"Player{peerId}";

		// Position the player above ground (adjust Y value as needed)
		player.Position = new Vector3(0, 2, 0);

		// Set who controls this player
		player.SetMultiplayerAuthority((int)peerId);

		// Add to scene first
		_playerSpawner.GetParent().AddChild(player);

		// Tell the spawner to replicate this node
		_playerSpawner.Spawn(player);

		GD.Print($"Spawned player for peer {peerId} at position {player.Position}");
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent && keyEvent.Pressed && keyEvent.Keycode == Key.Escape)
		{
			GetTree().ChangeSceneToFile("res://menu.tscn");
		}
	}
}
