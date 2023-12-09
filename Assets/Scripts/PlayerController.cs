using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public CharacterController charCon;
    private Vector3 moveInput;
    public Transform camTransf;

    public Animator anim;

    [Header("Gravedad")]
    public float gravityModifier;

    [Header("Player Controls")]
    public float moveSpeed;
    public float jumpPower;
    private bool canJump;
    public Transform groundCheckpoint;
    public LayerMask whatIsGround;

    [Header("Camara Controls")]
    public float mouseSensitivity;
    public bool invertX;
    public bool invertY;

    public GameObject bullet;
    public Transform firePoint;

    [Header("Gun")]
    public Gun activeGun;
    public List<Gun> allGuns = new List<Gun>();
    public List<Gun> unlockableGuns = new List<Gun>();
    public int currentGun;

    public Transform aimPoint, gunHolder;
    private Vector3 gunStartPos;
    public float aimSpeed = 2f;

    public GameObject muzzleflash;
    public AudioSource footStep;

    private float bounceAmount;
    private bool bounce;

    void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        gunStartPos = gunHolder.localPosition;

        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);
        UIController.Instance.ammoText.text = "Balas: " + activeGun.currentAmmo;
    }

    void Update()
    {
        if (!UIController.Instance.pauseScreen.activeInHierarchy)
        {

        
        //Guardar Y ,velocidad
        float yStore = moveInput.y;

        Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
        Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");

        moveInput = horiMove + vertMove;
        moveInput.Normalize();
        moveInput = moveInput * moveSpeed;
        moveInput.y = yStore;

        // Gravedad
        moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

        if (charCon.isGrounded)
        {
            moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        canJump = Physics.OverlapSphere(groundCheckpoint.position, .25f, whatIsGround).Length > 0;

        // Salto del jugador
        if (Input.GetButtonDown("Jump") && canJump)
        {
            moveInput.y = jumpPower;
            AudioManager.Instance.PlaySFX(7);
        }

        if(bounce)
            {
                bounce = false;
                moveInput.y = bounceAmount;
            }    

        charCon.Move(moveInput * Time.deltaTime);

        // Control Rotacion Camara
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;
        if (invertX)
        {
            mouseInput.x = -mouseInput.x;
        }
        if (invertY)
        {
            mouseInput.y = -mouseInput.y;
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.x);
        camTransf.rotation = Quaternion.Euler(camTransf.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));

        muzzleflash.SetActive(false);

        // shooting
        // singular
        if (Input.GetMouseButtonDown(0) && activeGun.fireCounter <= 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(camTransf.position, camTransf.forward, out hit, 50f))
            {
                if (Vector3.Distance(camTransf.position, hit.point) > 2f)
                {
                    firePoint.LookAt(hit.point);
                }
            }
            else
            {
                firePoint.LookAt(camTransf.position + (camTransf.forward * 30f));
            }

            FireShot();
        }
        //automatico
        if (Input.GetMouseButton(0) && activeGun.canAutoFire)
        {
            if (activeGun.fireCounter <= 0)
            {
                FireShot();
            }
        }

        if (Input.GetButtonDown("Switch Gun"))
        {
            SwitchGun();
        }

        if (Input.GetMouseButtonDown(1))
        {
            CameraControler.instance.ZoomIn(activeGun.zoomAmount);
        }

        if (Input.GetMouseButton(1))
        {
            gunHolder.position = Vector3.MoveTowards(gunHolder.position, aimPoint.position, aimSpeed * Time.deltaTime);
        }
        else
        {
            gunHolder.localPosition = Vector3.MoveTowards(gunHolder.localPosition, gunStartPos, aimSpeed * Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(1))
        {
            CameraControler.instance.ZoomOut();
        }

        // Animation
        anim.SetFloat("moveSpeed", moveInput.magnitude);
        anim.SetBool("onGround", canJump);
        }
    }
    public void FireShot()
    {
        if (activeGun.currentAmmo > 0)
        {
            activeGun.currentAmmo--;
            Instantiate(activeGun.bullet, firePoint.position, firePoint.rotation);
            activeGun.fireCounter = activeGun.fireRate;
            UIController.Instance.ammoText.text = "Balas: " + activeGun.currentAmmo;
            muzzleflash.SetActive(true);
        }
    }
    public void SwitchGun()
    {
        activeGun.gameObject.SetActive(false);
        currentGun++;

        if (currentGun >= allGuns.Count)
        {
            currentGun = 0;
        }
        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);

        UIController.Instance.ammoText.text = "Balas: " + activeGun.currentAmmo;
    }
    public void AddGun(string gunToAdd)
    {
        bool gunUnlocked = false;
        
        if(unlockableGuns.Count > 0)
        {
            for(int i = 0;i < unlockableGuns.Count; i++)
            {
                gunUnlocked = true;
                allGuns.Add(unlockableGuns[i]);

                unlockableGuns.RemoveAt(i);
                i = unlockableGuns.Count;
            }
        }
        if (gunUnlocked)
        {
            currentGun = allGuns.Count - 2;
            SwitchGun();
        }
    }

    public void Bounce (float bounceForce)
    {
        bounceAmount = bounceForce;
        bounce = true;
    }
}
