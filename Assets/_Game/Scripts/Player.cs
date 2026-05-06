using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using MyNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private GameObject model;
    [SerializeField] private Vector3 brickHeight;
    [SerializeField] private BrickPool brickPool;
    
    private Vector3 targetPos,raycastDirect,rotation;
    private bool isMoving = false;
    private bool isAddBrick = false;
    private Vector3 offset = new Vector3();
    private Stack<GameObject> stack = new Stack<GameObject>();
    private string animName = "idle";
    private GameManager manager => GameManager.Instance;
    private InputManager input => InputManager.Instance;
    public bool IsMoving
    {
        get => isMoving;
    }

    private void Awake()
    {
        rotation = model.transform.rotation.eulerAngles;
    }

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        isMoving = false;
        isAddBrick = false;
        input.OnInit();
        RemoveAllBrick();
        transform.position = Vector3.zero;
        RotateModel(rotation);
        ChangeAnim("idle");
        manager.onWinGame.AddListener(RemoveAllBrick);
        manager.onWinGame.AddListener(RotateToChess);
        manager.onWinGame.AddListener(ChangeWinAnim);
    }
    public void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
            ChangeAnim("idle");
            if (Vector3.Distance(transform.position, targetPos) < 0.1f && manager.State == GameState.Playing)
            {
                transform.position = targetPos;
                isMoving = false;
                if (isAddBrick)
                {
                    ChangeAnim("jump");
                    isAddBrick = false;
                }
            }
        }
        
    }
   
    private Vector3 GetTargetPos(Direct direct)
    {
        RaycastHit hitInfo;
        switch (direct)
        {
            case Direct.Right:
                offset = Vector3.left;
                raycastDirect = Vector3.right;
                break;
            case Direct.Left:
                offset = Vector3.right;
                raycastDirect = Vector3.left;
                break;
            case Direct.Forward:
                offset = Vector3.back;
                raycastDirect = Vector3.forward;
                break;
            case Direct.Back:
                offset = Vector3.forward;
                raycastDirect = Vector3.back;
                break;
        }
        if (Physics.Raycast(transform.position, raycastDirect, out hitInfo, Mathf.Infinity, wallLayer))
        {
            if (hitInfo.collider.CompareTag("Wall"))
            {
                return hitInfo.transform.position + offset;
            }
        }

        return transform.position;
    }
    public void Move(Direct direct)
    {
        isMoving = true;
        targetPos = GetTargetPos(direct);
        offset = Vector3.zero;
        
    }
    public void AddBrick()
    {
        var newBrick = brickPool.GetBrick();
        newBrick.transform.SetParent(this.transform);
        newBrick.transform.position = transform.position + brickHeight * stack.Count;
        stack.Push(newBrick);
        model.transform.localPosition = brickHeight * stack.Count;
        isAddBrick = true;
    }
    public void RemoveBrick()
    {
        if (stack.Count <= 0)
        {
            manager.OnLoseGame();
            return;
        }
        GameObject brick = stack.Pop();
        brickPool.ReturnBrick(brick);
        model.transform.localPosition = brickHeight * stack.Count;
        ChangeAnim("idle");
    }
    public void RemoveAllBrick()
    {
        if (stack.Count <= 0)
        {
            return;
        }
        while(stack.Count > 0)
        {
            GameObject brick = stack.Pop();
            brickPool.ReturnBrick(brick);
            model.transform.localPosition = brickHeight * stack.Count;
        }
        isMoving = false;
    }
    public void ChangeAnim(String animName)
    {
        if (this.animName == animName)
        {
            return;
        }
        this.animName = animName;
        anim.SetTrigger(this.animName);
    }
    public void RotateToChess()
    {
        RotateModel(Vector3.zero);
    }

    private void RotateModel(Vector3 dicrection)
    {
        model.transform.rotation = Quaternion.Euler(dicrection);
    }
    private void ChangeWinAnim()
    {
        ChangeAnim("win");
    }
    private void OnDisable()
    {
        OnDespawn();
    }
    public void OnDespawn()
    {
        manager.onWinGame.RemoveListener(RemoveAllBrick);
        manager.onWinGame.RemoveListener(RotateToChess);
        manager.onWinGame.RemoveListener(ChangeWinAnim);
    }
}
