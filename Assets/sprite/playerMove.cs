using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;


public class playerMove : MonoBehaviourPun
{
    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private bool maxJump;
    public Transform target; // 角色的Transform组件
    LayerMask jumpableGround;
    LayerMask downGround;
    public Vector3 offset; // 相机与角色之间的偏移量
    // Start is called before the first frame update
    private bool isFlagRaised = false;
    public GameObject flagPrefab; // 旗子的预制体
    private GameObject currentFlag; // 当前的旗子实例

    void Start()
    {
        maxJump = false;
        jumpableGround = LayerMask.GetMask("TransparentFX");
        downGround = LayerMask.GetMask("down");
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
            return;

        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrouded())
        {
            rb.velocity = new Vector2(rb.velocity.x, 7f);
            maxJump = true;
        }
        else if (Input.GetButtonDown("Jump") && maxJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 7f);
            maxJump = false;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && IsGrouded())
        {
            StartCoroutine(DisableColliderForTime(0.3f)); // 暂时禁用碰撞体积，持续0.5秒
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && !IsGrouded() && playerCollider.enabled == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, -3f);
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (isFlagRaised)
            {
                photonView.RPC("RemoveFlag", RpcTarget.All);
            }
            else if (IsGrouded())
            {
                photonView.RPC("PlaceFlag", RpcTarget.All);
            }
        }
        IEnumerator DisableColliderForTime(float duration)
        {
            playerCollider.enabled = false; // 禁用碰撞体积

            yield return new WaitForSeconds(duration); // 等待一段时间

            playerCollider.enabled = true; // 重新启用碰撞体积
        }
    }

    [PunRPC]
    void PlaceFlag()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.y += 0.5f;
        currentFlag = Instantiate(flagPrefab, spawnPosition, Quaternion.identity);
        currentFlag.transform.position = spawnPosition;
        isFlagRaised = true;
    }

    [PunRPC]
    void RemoveFlag()
    {
        Destroy(currentFlag);
        isFlagRaised = false;
    }

    bool IsGrouded()
    {
        return Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    bool IsGrouded2()
    {
        return Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, 0.1f, downGround);
    }
}
