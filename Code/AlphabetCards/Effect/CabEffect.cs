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
    //private bool canUse = true;

    public UnityEngine.Color rayColor = UnityEngine.Color.magenta;
    public float lineWidth = 0.1f;

    //private LineRenderer lineRenderer;

    void Start()
    {
        player = base.transform.root.GetComponent<Player>();
        var art = player.transform.GetChild(0);
        var cab = AssetManager.SpriteCab;
        cabSprite = Instantiate(cab, art);
        renderer = cabSprite.GetComponent<SpriteRenderer>(); 
        player.data.block.BlockAction += BlockAction;

        //lineRenderer = gameObject.AddComponent<LineRenderer>();
        //lineRenderer.startWidth = lineWidth;
        //lineRenderer.endWidth = lineWidth;
        //lineRenderer.startColor = rayColor;
        //lineRenderer.endColor = rayColor;
    }

    void OnDestroy()
    {
        player.data.block.BlockAction -= BlockAction;
    }

    private void Update()
    {
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

    //private void FixedUpdate()
    //{
    //    if (canUse) return;
    //    //MoveTowards();
    //}

    private void MoveTowards()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 pointingPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = (pointingPosition - playerPosition).normalized;
        Vector2 force = direction * 100 * 150000f;

        Type type = typeof(PlayerVelocity);
        MethodInfo methodInfo = type.GetMethod("AddForce", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(Vector2), typeof(ForceMode2D) }, null);
        methodInfo.Invoke(player.data.playerVel, new object[] { force, ForceMode2D.Force });

        int layerMask = LayerMask.GetMask("Default", "IgnorePlayer", "Player");
        RaycastHit2D[] raycastHits = Physics2D.RaycastAll(player.transform.position, direction, 10, layerMask);
        //lineRenderer.SetPosition(0, player.transform.position);
        for (int i = 0; i < raycastHits.Length; i++)
        {
            //lineRenderer.SetPosition(1, raycastHits[i].point);

            NetworkPhysicsObject component = raycastHits[i].transform.GetComponent<NetworkPhysicsObject>();
            if ((bool) component)
            {
                component.BulletPush(force / 100, raycastHits[i].point, player.data);
            }
        }
    }

    private void BlockAction(BlockTrigger.BlockTriggerType blockType)
    {
        MoveTowards();
    }
}
