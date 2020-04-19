using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameManager Manager;

    [Header("Movement")]
    public Transform PlayerHead;
    public float LookSpeed;
    public float MoveSpeed;
    public float MoveMod;
    public bool InvertY = false;

    [Header("Interaction")]
    public float Range = 1;
    public float Damage = 1;
    public Image CursorImage;
    public Sprite NormalCursor;
    public Sprite GrabCursor;
    public Sprite HarvestCursor;
    public Sprite CraftCursor;
    public Sprite NoItemCursor;
    public Inventory Inventory;
    public GameObject HitParticles;

    [Header("Item Management")]
    public bool AxeEquipped;
    public bool TorchEquipped;
    public Animator HandAnimation;
    public Animator HeadAnimation;

    [Header("Crafting")]
    public CraftMenu CraftMenu;
    


    private Camera cam;
    private float vertLook;
    private float sideLook;
    private Vector3 moveVector;
    private float yMod;
    private float index;

    void Start()
    {
        
        SetInvert();
        cam = PlayerHead.GetComponent<Camera>();
    }

    void Update()
    {
        if (!Manager.Paused)
        {
            CursorImage.transform.localPosition = Vector3.zero;
            moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
            //transform.Translate(moveVector * MoveSpeed * Time.deltaTime);
            transform.position += transform.forward * moveVector.z * MoveSpeed * Time.deltaTime;
            transform.position += transform.right * moveVector.x * MoveSpeed * Time.deltaTime;

            vertLook += Input.GetAxis("Mouse Y") * LookSpeed * Time.deltaTime * yMod;
            vertLook = Mathf.Clamp(vertLook, -90, 90);
            sideLook += Input.GetAxis("Mouse X") * LookSpeed * Time.deltaTime;


            transform.eulerAngles = new Vector3(0, sideLook, 0);
            PlayerHead.localEulerAngles = new Vector3(vertLook, 0, 0);
            index += moveVector.magnitude * Time.deltaTime;
            HeadAnimation.Play("Walk", 0, index);

        }
        else
        {
            HeadAnimation.Play("Walk", 0, 0);
            CursorImage.transform.position = Input.mousePosition;

           
        }
        transform.eulerAngles = new Vector3(0, sideLook, 0);
        PlayerHead.localEulerAngles = new Vector3(vertLook, 0, 0);


        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height/2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance < Range)
            {
                if (hit.transform.tag == "Resource")
                {
                    if(hit.transform.GetComponent<Resource>().RequiresAxe)
                    {
                        if (AxeEquipped)
                        {
                            CursorImage.sprite = HarvestCursor;
                            if (Input.GetMouseButtonDown(0))
                            {
                                var hitResource = hit.transform.GetComponent<Resource>();
                                hitResource.Harvest(Damage);
                                var hitParts = Instantiate(HitParticles, hit.point, transform.rotation);
                                hitParts.GetComponent<HitParticles>().StartColour = hitResource.ParticleColour;

                            }
                        }
                        else
                        {
                            CursorImage.sprite = NoItemCursor;
                        }
                        
                    }
                    
                    else
                    {
                        CursorImage.sprite = HarvestCursor;
                        if (Input.GetMouseButtonDown(0))
                        {
                            var hitResource = hit.transform.GetComponent<Resource>();
                            hitResource.Harvest(Damage);
                            var hitParts = Instantiate(HitParticles, hit.point, transform.rotation);
                            hitParts.GetComponent<HitParticles>().StartColour = hitResource.ParticleColour;

                        }
                    }
                    
                }
                else if (hit.transform.tag == "Item")
                {
                    CursorImage.sprite = GrabCursor;
                    if (Input.GetMouseButtonDown(0))
                    {
                        var hitItem = hit.transform.GetComponent<Item>();
                        Inventory.Slots[hitItem.Slot].ItemCount ++;
                        Destroy(hit.transform.gameObject);
                        
                    }
                }
                else if(hit.transform.tag == "Craft")
                {
                    if (!CraftMenu.Open)
                    {
                        CursorImage.sprite = CraftCursor;
                        if (Input.GetMouseButtonDown(0))
                        {
                            CraftMenu.OpenMenu();
                            Manager.PauseMenu();

                        }
                    }
                    else
                    {
                        CursorImage.sprite = NormalCursor;
                    }

                }
                else
                {
                    CursorImage.sprite = NormalCursor;
                }
            }
            else
            {
                CursorImage.sprite = NormalCursor;
            }
        }
    }

    public void SetInvert()
    {
        if (InvertY)
        {
            yMod = 1;
        }
        else
        {
            yMod = -1;
        }
    }

    public void AttackAnimation()
    {
        HandAnimation.Play("Attack", 0, 0);
    }
    
}
