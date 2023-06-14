using Assets.AlphabetCards.Code.AlphabetCards.Util;
using ModdingUtils.MonoBehaviours;
using ModdingUtils.Utils;
using Photon.Realtime;
using System;
using System.Linq;
using UnboundLib.Networking;
using UnboundLib;
using UnityEngine;

public class FEffect : MonoBehaviour
{
    private Player player;
    private GameObject fSprite;
    private SpriteRenderer renderer;
    private ColorEffect colorEffect;
    private bool keyPressed = false;

    void Start()
    {
        this.player = this.transform.root.GetComponent<Player>();
        var art = this.player.transform.GetChild(0);
        var f = AssetManager.SpriteF;
        this.fSprite = Instantiate(f, art);
        this.renderer = this.fSprite.GetComponent<SpriteRenderer>();
        this.renderer.sortingLayerName = "MostFront";
        this.player.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        this.player.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        this.player.transform.GetChild(4).GetChild(0).transform.localPosition += new Vector3(0, 0.95f, 0);
        this.colorEffect = this.player.gameObject.AddComponent<ColorEffect>();
        this.colorEffect.SetColor(Color.black);
    }

    private void Update()
    {
        if (this.player == null) return;
        
        if (this.player.data.view.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.E) && !keyPressed)
            {
                Attack(this.player);
                keyPressed = true;
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                keyPressed = false;
            }
        }

        if (this.fSprite == null) return;
        if (this.renderer == null) return;

        var flipped = player.data.aimDirection.x >= 0;

        if (flipped)
        {
            this.fSprite.transform.localRotation = new Quaternion(0, 0, 0, 0);
            this.fSprite.transform.localPosition = new Vector3(0.75f, 0.5f, 0);
        }
        else
        {
            this.fSprite.transform.localRotation = new Quaternion(0, 180f, 0, 0);
            this.fSprite.transform.localPosition = new Vector3(-0.75f, 0.5f, 0);
        }
    }

    private void OnDestroy()
    {
        colorEffect.Destroy();
    }

    public static void Attack(Player attacker)
    {
        var enemyPlayers = PlayerManager.instance.players.Where(player => PlayerStatus.PlayerAliveAndSimulated(player) && (player.teamID != attacker.teamID)).Select(p => p.playerID).ToArray();
        NetworkingManager.RPC(typeof(FEffect), nameof(RPC_Attack), enemyPlayers);
    }

    [UnboundRPC]
    private static void RPC_Attack(int[] playerIds)
    { 
        foreach (Player enemyPlayer in PlayerManager.instance.players.Where(p => playerIds.Contains(p.playerID)))
        {
            var attack = Instantiate(AssetManager.A_F, enemyPlayer.transform);
            attack.transform.localScale = new Vector3(0.25f, 0.25f);

            float randomValue = UnityEngine.Random.Range(0f, 1f);
            if (randomValue < 0.5f)
            {
                attack.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
            }
            else
            {
                attack.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
            }

            attack.GetComponent<FBite>().enemyPlayer = enemyPlayer;
        }
    }
}