using System;
using System.Collections;
using System.Collections.Generic;
using MyNamespace;
using UnityEngine;

public class Corner : MonoBehaviour
{
    [SerializeField] private GameObject brick;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] List<Direct> directCanTurn = new List<Direct>();
    private Direct redirect;
    private Player player;
    bool isTaken = false;
    private InputManager InputManager => InputManager.Instance;
    private DataManager DataManager => DataManager.Instance;

    private void OnEnable()
    {
        OnInit();
    }

    private void OnDisable()
    {
        OnDespawn();
    }
    
    private void OnDespawn()
    {
        brick.SetActive(false);
    }

    public void OnInit()
    {
        isTaken = false;
        brick.SetActive(true);
        player = null;
        Invoke(nameof(GetDirectCanTurn), 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Variables.PLAYER_TAG))
        {
            player = other.GetComponent<Player>();
            anim.SetTrigger("take");
            redirect = GetReDirect(InputManager.Direct);
            if (!isTaken)
            {
                brick.SetActive(false);
                player.AddBrick();
                DataManager.AddScore(1);
                isTaken = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (player && IsPlayerAtCorner(player))
        {
            player.Move(redirect);
        }
    }
    private bool IsPlayerAtCorner(Player player)
    {
        return Vector3.Distance(transform.position, player.transform.position) < 0.1f;
    }

    private Direct GetDirectionFromPlayer(Direct direct)
    {
        switch (direct)
        {
            case Direct.Back:
                return Direct.Forward;
            case Direct.Forward:
                return Direct.Back;
            case Direct.Left:
                return Direct.Right;
            case Direct.Right:
                return Direct.Left;
        }
        return direct;
    }
    private void GetDirectCanTurn()
    {
        directCanTurn.Clear();

        if (!Physics.Raycast(transform.position, Vector3.forward, 1f, wallLayer))
        {
            directCanTurn.Add(Direct.Forward);
        }

        if (!Physics.Raycast(transform.position, Vector3.back, 1f, wallLayer))
        {
            directCanTurn.Add(Direct.Back);
        }

        if (!Physics.Raycast(transform.position, Vector3.right, 1f, wallLayer))
        {
            directCanTurn.Add(Direct.Right);
        }

        if (!Physics.Raycast(transform.position, Vector3.left, 1f, wallLayer))
        {
            directCanTurn.Add(Direct.Left);
        }
    }

    private Direct GetReDirect(Direct playerDirection)
    {
        var directFromPLayer = GetDirectionFromPlayer(playerDirection);
        foreach (var direction in directCanTurn)
        {
            if (direction != directFromPLayer)
            {
                return direction;
            }
        }
        return playerDirection;
    }
}