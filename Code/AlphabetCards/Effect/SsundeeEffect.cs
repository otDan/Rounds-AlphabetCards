using Assets.AlphabetCards.Code.AlphabetCards.Util;
using ModdingUtils.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnboundLib;
using UnityEngine;

public class SsundeeEffect : MonoBehaviour
{
    private Player player;
    private GameObject particleTeleport;
    private Animator animator;
    private readonly List<GameObject> particles = new List<GameObject>();
    private Vector3 savedPosition;
    private bool canTeleport = true;
    public GameObject explosion;

    void Start()
    {
        player = base.transform.root.GetComponent<Player>();
        particleTeleport = AssetManager.ParticleTeleport;
        animator = GetComponent<Animator>();
        player.data.block.BlockAction += BlockAction;
    }

    void OnDestroy()
    {
        player.data.block.BlockAction -= BlockAction;
    }

    private void Update()
    {
        if (player == null) return;
        if (particles.Count > 0)
        {
            List<GameObject> toRemove = new List<GameObject>();
            foreach (GameObject particle in particles)
            {
                if (particle.transform.childCount == 0)
                {
                    Destroy(particle);
                    toRemove.Add(particle);
                }
            }
            toRemove.ForEach(p => particles.Remove(p));
            return;
        }
    }

    private void BlockAction(BlockTrigger.BlockTriggerType blockType)
    {
        if (!canTeleport) return;

        canTeleport = false;
        savedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        animator.Rebind();
        animator.Play("Effect_SSUNDEE");
    }

    public void RandomTeleport()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float randomX = Random.Range(0, screenWidth);
        float randomY = Random.Range(0, screenHeight);

        Vector3 randomPosition = new Vector3(randomX, randomY, 0);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(randomPosition);

        Teleport(worldPosition);
    }

    public void FinalTeleport()
    {
        Teleport(savedPosition);
        canTeleport = true;
    }

    private void Teleport(Vector3 teleportPosition)
    {
        GameObject newParticleTeleport = Instantiate(particleTeleport, player.transform.position, Quaternion.identity);
        particles.Add(newParticleTeleport);
        GameObject newExplosion = Instantiate(explosion, player.transform.position, Quaternion.identity);
        var enemyPlayers = PlayerManager.instance.players.Where(player => PlayerStatus.PlayerAliveAndSimulated(player) && (player.teamID != this.player.teamID)).ToList();

        foreach (Player enemyPlayer in enemyPlayers)
        {
            if (player.data.view.IsMine)
                enemyPlayer.data.healthHandler.CallTakeDamage(enemyPlayer.data.maxHealth * Vector2.one, enemyPlayer.transform.position);
        }

        newParticleTeleport = Instantiate(particleTeleport, teleportPosition, Quaternion.identity);
        particles.Add(newParticleTeleport);

        player.GetComponentInParent<PlayerCollision>().IgnoreWallForFrames(2);
        player.transform.position = teleportPosition;
        AudioController.Play(AssetManager.Sound_Teleport, player.transform, 0.15f);
    }
}
