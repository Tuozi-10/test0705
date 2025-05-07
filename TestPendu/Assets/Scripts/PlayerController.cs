using System;
using System.Collections.Generic;

public class PlayerController
{
    public enum Players { J1, J2 }

    public static readonly HashSet<Players> activePlayers = new HashSet<Players>();
    public readonly Players player;
    public readonly string playerName;

    public PlayerController(Players player, string name)
    {
        if (!activePlayers.Add(player))
            throw new Exception("Player Already Exist");
        this.player = player;
        playerName = name;
    }
}