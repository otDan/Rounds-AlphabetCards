using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.GameModes;

public class GameController
{
    public static int Round = 0;
    public static List<CardInfo> AlreadySpawned = new List<CardInfo>();

    internal static IEnumerator GameStart(IGameModeHandler gameModeHandler)
    {
        Round = 0;
        AlreadySpawned = new List<CardInfo>();
        yield break;
    }

    public static Player GetHostPlayer()
    {
        return PlayerManager.instance.players.FirstOrDefault(loopPlayer => loopPlayer.data.view.OwnerActorNr == PhotonNetwork.MasterClient.ActorNumber);
    }

    public static int GetHostPlayerId()
    {
        return GetHostPlayer()?.playerID ?? -1;
    }
}
