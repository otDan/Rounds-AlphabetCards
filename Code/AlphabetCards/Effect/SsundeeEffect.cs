using Assets.AlphabetCards.Code.AlphabetCards.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SsundeeEffect : MonoBehaviour
{
    private Player player;
    private GameObject particleTeleport;
    private Animator animator;
    private readonly List<GameObject> particles = new List<GameObject>();
    private Vector3 savedPosition;
    private bool canTeleport = true;

    void Start()
    {
        player = base.transform.root.GetComponent<Player>();
        particleTeleport = AssetManager.Particle_Teleport;
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
        newParticleTeleport = Instantiate(particleTeleport, teleportPosition, Quaternion.identity);
        particles.Add(newParticleTeleport);

        player.GetComponentInParent<PlayerCollision>().IgnoreWallForFrames(2);
        player.transform.position = teleportPosition;
        AudioController.Play(AssetManager.Sound_Teleport, player.transform, 0.15f);
    }
}
