using ModdingUtils.Utils;
using Photon.Pun;
using Sonigon.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using UnboundLib;
using UnityEngine;
using UnityEngine.UI;

public class CabEffect : MonoBehaviour
{
    private Player player;
    private GameObject cabSprite;
    private SpriteRenderer renderer;

    public UnityEngine.Color rayColor = UnityEngine.Color.magenta;
    public float lineWidth = 0.1f;

    void Start()
    {
        player = base.transform.root.GetComponent<Player>();
        var art = player.transform.GetChild(0);
        var cab = AssetManager.SpriteCab;
        cabSprite = Instantiate(cab, art);
        renderer = cabSprite.GetComponent<SpriteRenderer>();
        renderer.sortingLayerName = "MostFront";
        //player.data.block.BlockAction += BlockAction;
    }

    void OnDestroy()
    {
        //player.data.block.BlockAction -= BlockAction;
    }

    private void Update()
    {
        //PushPlayers();

        if (player == null) return;
        if (cabSprite == null) return;
        if (renderer == null) return;

        var flipped = player.data.aimDirection.x >= 0;

        if (flipped)
        {
            cabSprite.transform.localRotation = new Quaternion(0, 0, 0, 0);
            cabSprite.transform.localPosition = new Vector3(0.65f, 0, 0);
        }
        else
        {
            cabSprite.transform.localRotation = new Quaternion(0, 180f, 0, 0);
            cabSprite.transform.localPosition = new Vector3(-0.65f, 0, 0);
        }
    }

    //private void PushPlayers()
    //{
    //    Vector3 playerPosition = player.transform.position;

    //    var enemyPlayers = PlayerManager.instance.players.Where(player => PlayerStatus.PlayerAliveAndSimulated(player) && (player.teamID != this.player.teamID) && Vector3.Distance(player.transform.position, this.player.transform.position) < 5).ToList();

    //    foreach (Player enemyPlayer in enemyPlayers)
    //    {
    //        Vector3 direction = (playerPosition - enemyPlayer.transform.position).normalized;
    //        Vector2 force = direction * 100f * 150f;
    //        enemyPlayer.data.healthHandler.TakeForce(force * 10f, ForceMode2D.Force);
    //    }
            
    //}

    //private void MoveTowards()
    //{
    //    Vector3 playerPosition = player.transform.position;
    //    Vector3 pointingPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //    Vector3 direction = (pointingPosition - playerPosition).normalized;
    //    Vector2 force = direction * 100f * 15000f;

    //    Type type = typeof(PlayerVelocity);
    //    MethodInfo methodInfo = type.GetMethod("AddForce", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(Vector2), typeof(ForceMode2D) }, null);
    //    methodInfo.Invoke(player.data.playerVel, new object[] { force, ForceMode2D.Force });

    //    int layerMask = LayerMask.GetMask("Default", "IgnorePlayer", "Player");
    //    RaycastHit2D[] raycastHits = Physics2D.RaycastAll(player.transform.position, direction, 10, layerMask);
    //    for (int i = 0; i < raycastHits.Length; i++)
    //    {
    //        var hit = raycastHits[i];
    //        var position = hit.point;

    //        NetworkPhysicsObject component = hit.transform.GetComponent<NetworkPhysicsObject>();
    //        if ((bool) component)
    //        {
    //            component.BulletPush(force / 10f, hit.point, player.data);
    //        }

    //        Player hitPlayer = hit.transform.GetComponent<Player>();
    //        if ((bool) hitPlayer)
    //        {
    //            if (hitPlayer == player)
    //                continue;
    //            hitPlayer.data.healthHandler.TakeDamage(Vector2.one * 75f, position);
    //            hitPlayer.data.view.RPC("RPCA_AddSilence", RpcTarget.All, 2f);
    //            hitPlayer.data.healthHandler.TakeForce(force * 10f, ForceMode2D.Force);
    //        }
    //    }
    //}

    //private void BlockAction(BlockTrigger.BlockTriggerType blockType)
    //{
    //    MoveTowards();
    //}
}
