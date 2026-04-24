using System;
using System.Collections;
using System.Collections.Generic;
using MyNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask wallLayer;
    
    private Vector3 startPos, endPos, targetPos;
    private Direct direct;
    private bool isMoving = false;
    
    private List<GameObject> bricks = new List<GameObject>();
    private Vector3 offset = new Vector3();

    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        isMoving = false;
        startPos = transform.position;
        endPos = transform.position;
        bricks.Clear();
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            direct = GetDirect(startPos, endPos);
            isMoving = true;
            Debug.Log(direct.ToString());
            Move(direct);
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                transform.position = targetPos;
                isMoving = false;
            }
        }
        
    }
    private Direct GetDirect(Vector3 startPos, Vector3 endPos)
    {
        Vector3 direction = (endPos - startPos).normalized;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            return direction.x > 0 ? Direct.Right : Direct.Left;
        }
        else
        {
            return direction.y > 0 ? Direct.Forward : Direct.Back;
        }
    }
    private Vector3 GetTargetPos(Direct direct)
    {
        RaycastHit hitInfo;
        switch (direct)
        {
            case Direct.Right:
                offset = Vector3.left;
                break;
            case Direct.Left:
                offset = Vector3.right;
                break;
            case Direct.Forward:
                offset = Vector3.back;
                break;
            case Direct.Back:
                offset = Vector3.forward;
                break;
        }
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, Mathf.Infinity, wallLayer))
        {
            Debug.Log(hitInfo.collider.name);
            if (hitInfo.collider.CompareTag("Wall"))
            {
                return hitInfo.transform.position + offset;
            }
        }

        return transform.position;
    }
    private void Rotate(Direct direct)
    {
        switch (direct)
        {
            case Direct.Right:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case Direct.Left:
                transform.rotation = Quaternion.Euler(0, -90, 0);
                break;
            case Direct.Forward:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Direct.Back:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
        }
    }
    private void Move(Direct direct)
    {
        Rotate(direct);
        Debug.Log("target pos before: "+ targetPos+" offser: "+ offset);
        targetPos = GetTargetPos(direct);
        Debug.Log("target pos after: "+ targetPos+" offser: "+ offset);
        offset = Vector3.zero;
        
    }
    public void AddBrick()
    {
        
    }
    public void RemoveBrick()
    {
        
    }
    public void ChangeAnim(int animIdx)
    {
        anim.SetInteger("renwu", animIdx);
    }
}
