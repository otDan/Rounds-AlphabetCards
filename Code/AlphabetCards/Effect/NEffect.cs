using Photon.Pun;
using Sonigon.Internal;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

internal class NEffect : MonoBehaviour
{
    private Player _player;
    private Gun _gun;
    public LineRenderer lineRenderer;
    public GameObject beam;
    private GameObject _beam;
    private GameObject startEffect;
    private GameObject staticEffect;
    private GameObject endEffect;
    private const float MaximumLength = 95;
    private Quaternion rotation;

    public float cycleSpeed = 0.45f;
    public float colorOffset = 0.5f;
    private float hue = 0f;
    private float startHue;
    private float endHue;


    private void Start()
    {
        _player = base.transform.root.GetComponent<Player>();
        _gun = _player.data.weaponHandler.gun;
        _beam = Instantiate(beam);

        lineRenderer = _beam.GetComponentInChildren<LineRenderer>();
        startEffect = _beam.gameObject.transform.GetChild(0).gameObject;
        staticEffect = _beam.gameObject.transform.GetChild(1).gameObject;
        endEffect = _beam.gameObject.transform.GetChild(2).gameObject;
        _beam.SetActive(false);
        _player.data.block.BlockAction += BlockAction;

        startHue = hue;
        endHue = (hue + colorOffset) % 1f;

        SetLineRendererColors();
    }

    void OnDestroy()
    {
        _player.data.block.BlockAction -= BlockAction;
    }

    private void Update()
    {
        if (!ModdingUtils.Utils.PlayerStatus.PlayerAliveAndSimulated(_player))
        {
            _beam.SetActive(false);
            return;
        }
        
        if (!_beam.activeInHierarchy) return;

        UpdateColor();

        int layerMask = LayerMask.GetMask("Default", "IgnorePlayer", "Player");
        Vector3 shootPosition = _gun.shootPosition.position;
        staticEffect.transform.position = shootPosition;
        Vector3 direction = _gun.shootPosition.forward;
        Vector2 force = direction * 100 * 1.75f;
        _beam.transform.position = shootPosition;

        RotateToMouse(direction);
        RaycastHit2D hit = Physics2D.CircleCast(shootPosition, 0.15f, direction, MaximumLength, layerMask);

        lineRenderer.SetPosition(0, shootPosition);
        if (!hit)
        {
            var endPoint = shootPosition + (direction * MaximumLength);
            lineRenderer.SetPosition(1, endPoint);
            return;
        }
        Vector3 position = hit.point;
        lineRenderer.SetPosition(1, position);

        var hitEffect = Instantiate(endEffect, position, Quaternion.identity);
        hitEffect.SetActive(true);
        
        NetworkPhysicsObject component = hit.transform.GetComponent<NetworkPhysicsObject>();
        if ((bool) component)
        {
            component.BulletPush(force, hit.point, _player.data);
        }
        Player player = hit.transform.GetComponent<Player>();
        if ((bool) player)
        {
            player.data.healthHandler.TakeDamage(Vector2.one, position);
            player.data.view.RPC("RPCA_AddSilence", RpcTarget.All, 1f);
            player.data.healthHandler.TakeForce(force);
        }
    }

    private void UpdateColor()
    {
        hue += cycleSpeed * Time.deltaTime;
        if (hue > 1f)
        {
            hue -= 1f;
        }

        startHue = hue;
        endHue = (hue + colorOffset) % 1f;

        SetLineRendererColors();
    }

    private void SetLineRendererColors()
    {
        ParticleSystem.MainModule settingsStatic = staticEffect.GetComponent<ParticleSystem>().main;
        ParticleSystem.MainModule settingsStart = startEffect.GetComponent<ParticleSystem>().main;
        ParticleSystem.MainModule settingsEnd = endEffect.GetComponent<ParticleSystem>().main;

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Color startColor = Color.HSVToRGB(startHue, 1f, 1f);
            Color endColor = Color.HSVToRGB(endHue, 1f, 1f);
            
            lineRenderer.startColor = startColor;
            lineRenderer.endColor = endColor;

            settingsStatic.startColor = startColor;
            settingsStart.startColor = startColor;
            settingsEnd.startColor = endColor;
        }
    }

    private void ShootBeam()
    {
        _beam.SetActive(true);
        StartCoroutine(StopBeam());
    }

    private IEnumerator StopBeam()
    {
        yield return new WaitForSeconds(2f);
        _beam.SetActive(false);
    }

    private void RotateToMouse(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotation.eulerAngles = new Vector3(0, 0, angle);
        _beam.transform.rotation = rotation;
    }

    private void BlockAction(BlockTrigger.BlockTriggerType blockType)
    {
        if (_beam.activeInHierarchy) return;
        ShootBeam();
    }
}