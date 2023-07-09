using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 10.0f;

    private bool _moveToDest = false;
    private Vector3 _destPos;
    private Animator anim;
    private Camera mainCamera;


    public enum PlayerState
    {
        Die,
        Idle,
        Moving
    }

    private PlayerState _state = PlayerState.Idle;

    void UpdateDie()
    {
    }

    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.0001f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            float moveDist = Math.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);

            transform.position += dir.normalized * moveDist;

            transform.rotation =
                Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
            transform.LookAt(_destPos);
        }

        // 애니메이션 (코드로 하는 경우)
        // wait_run_ratio = Mathf.Lerp(wait_run_ratio, 1, 10.0f * Time.deltaTime);
        // anim.SetFloat("wait_run_ratio", wait_run_ratio);
        // anim.Play("WAIT_RUN");
        
        //현재 게임 상태에 대한 정보를 Animator로 넘겨준다
        anim.SetFloat("speed", _speed);
    }

    void UpdateIdle()
    {
        // 애니메이션 (코드로 하는 경우)
        // wait_run_ratio = Mathf.Lerp(wait_run_ratio, 0, 10.0f * Time.deltaTime);
        // anim.SetFloat("wait_run_ratio", wait_run_ratio);
        // anim.Play("WAIT_RUN");
        
        anim.SetFloat("speed", 0);
    }


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        anim = GetComponent<Animator>();
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }

    // GameObject (Player)
    // Transform
    // PlayerController

    float _yAngle = 0.0f;


    private float wait_run_ratio = 0;

    void Update()
    {
        switch (_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
        }
    }

    void OnKeyboard()
    {
        Vector3 direction = Vector3.zero;
        //InverseTransformDirection �ݴ�

        _yAngle += Time.deltaTime * 100.0f;

        // ���� ȸ����
        //transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);

        // +-delta
        //transform.Rotate(new Vector3(0.0f, Time.deltaTime * 100.0f, 0.0f));

        //transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f));

        if (_state == PlayerState.Die) return;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }

        if (direction != Vector3.zero)
        {
            direction.Normalize();
            _destPos = transform.position + direction;
            _state = PlayerState.Moving;
        }
        else
        {
            _state = PlayerState.Idle;
        }
        //transform

        _moveToDest = false;
    }

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die) return;
        // if (evt != Define.MouseEvent.Click)
        //     return;

        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(mainCamera.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
        
        if (Physics.Raycast(ray, out RaycastHit hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            _state = PlayerState.Moving;
            // _moveToDest = true;
            // Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
        }
    }
}