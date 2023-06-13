using Assets.AlphabetCards.Code.AlphabetCards.Util;
using ModdingUtils.MonoBehaviours;
using ModdingUtils.Utils;
using Photon.Realtime;
using System;
using System.Linq;
using UnityEngine;

public class FEffect : MonoBehaviour
{
    private Player player;
    private GameObject fSprite;
    private SpriteRenderer renderer;
    private ColorEffect colorEffect;

    void Start()
    {
        player = this.transform.root.GetComponent<Player>();
        var art = player.transform.GetChild(0);
        var f = AssetManager.SpriteF;
        fSprite = Instantiate(f, art);
        renderer = fSprite.GetComponent<SpriteRenderer>();
        renderer.sortingLayerName = "MostFront";
        player.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        player.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        player.transform.GetChild(4).GetChild(0).transform.localPosition += new Vector3(0, 0.95f, 0);
        colorEffect = player.gameObject.AddComponent<ColorEffect>();
        colorEffect.SetColor(Color.black); player.
        data.block.BlockAction += BlockAction;
    }

    private void Update()
    {
        if (player == null) return;
        if (fSprite == null) return;
        if (renderer == null) return;

        var flipped = player.data.aimDirection.x >= 0;

        if (flipped)
        {
            fSprite.transform.localRotation = new Quaternion(0, 0, 0, 0);
            fSprite.transform.localPosition = new Vector3(0.75f, 0.5f, 0);
        }
        else
        {
            fSprite.transform.localRotation = new Quaternion(0, 180f, 0, 0);
            fSprite.transform.localPosition = new Vector3(-0.75f, 0.5f, 0);
        }
    }

    private void OnDestroy()
    {
        player.data.block.BlockAction -= BlockAction;
        colorEffect.Destroy();
    }

    private void BlockAction(BlockTrigger.BlockTriggerType blockType)
    {
        var enemyPlayers = PlayerManager.instance.players.Where(player => PlayerStatus.PlayerAliveAndSimulated(player) && (player.teamID != this.player.teamID)).ToList();

        foreach (Player enemyPlayer in enemyPlayers)
        {
            if (player.data.view.IsMine)
                enemyPlayer.data.healthHandler.CallTakeDamage(((enemyPlayer.data.maxHealth / 3) * Vector2.one) - Vector2.one, enemyPlayer.transform.position);
            enemyPlayer.GetComponentInParent<PlayerCollision>().IgnoreWallForFrames(2);
            enemyPlayer.transform.position = player.transform.position;
            AudioController.Play(AssetManager.Sound_Bite, enemyPlayer.transform, 1.15f);
        }
    }
}