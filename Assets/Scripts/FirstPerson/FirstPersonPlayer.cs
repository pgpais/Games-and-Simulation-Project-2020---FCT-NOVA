using System;
using Interactables;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

namespace FirstPerson
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(FirstPersonMovement))]
    [RequireComponent(typeof(FirstPersonAiming))]
    public class FirstPersonPlayer : MonoBehaviourPun
    {
        [Header("Interact Parameters")] [SerializeField]
        private float interactRange = 10f;

        [SerializeField] private LayerMask interactMask;

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject localPlayerInstance;

        #region Required Components
        
        private FirstPersonAiming aim;
        private FirstPersonMovement mov;
        private PlayerInput input;

        [SerializeField] private GameObject capsuleModel;
        [SerializeField] private GameObject animatedModel;
        
        #endregion

        #region Player Stuff

        public Tool tool;

        #endregion

        #region Carryable Stuff

        [Tooltip("Carrying stuff")] 
        [SerializeField] private float pullForce;
        [SerializeField] private float launchForce = 100f;
        [SerializeField] private Transform carryingPoint;
        private bool isCarrying;
        private GameObject carryingObject;
        private Transform carryingTrans;
        private Rigidbody carryingRb;
        

        #endregion

        private PauseMenu _pauseMenu;
        
        private void Awake()
        {
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine)
            {
                FirstPersonPlayer.localPlayerInstance = this.gameObject;
            }
            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(this.gameObject);
        }

    
        /// <summary>
        /// Start will be called for each players instance (4 in total) when the GameManager spawns the players through
        /// PhotonNetwork. 
        /// </summary>
        void Start()
        {
            aim = GetComponent<FirstPersonAiming>();
            mov = GetComponent<FirstPersonMovement>();
            input = GetComponent<PlayerInput>();
            
            Setup();
        }

        private void FixedUpdate()
        {
        }

        /// <summary>
        /// This is simply a wrapper for the Tool.UseTool method so we can tag it as a PunRPC.
        /// </summary>
        [PunRPC]
        public void UseTool(InputActionPhase phase)
        {
            //Debug.Log("RPC UseTool called", this);
            // Criar um ENUM que representa o Tipo de Input realizado
            // Investigar metodos de disntinção de Input
            tool.UseTool(phase);
        }

        #region Local VS Remote setup

        /// <summary>
        /// Setup players so there's no conflict between local and remote player
        /// </summary>
        void Setup()
        {
            _pauseMenu = GameManager.instance.SettingsSpawned.GetComponent<PauseMenu>();
            if (photonView.IsMine || !PhotonNetwork.IsConnected)
            {
                //TODO: Setup local player (change appearance and enable controls)
                input.enabled = true;
                ChangeModels(true);
            }
            else
            {
                //TODO: Setup object as remote player (show the humanoid model and disable controls)
                input.enabled = false;
                aim.DisableCamera(); // Maybe this is going away, since we're changing the whole model
                ChangeModels(false);
            }
        }

        void ChangeModels(bool isLocal)
        {
            capsuleModel.SetActive(isLocal);
            animatedModel.SetActive(!isLocal);
        }

        #endregion



        #region Handle Input
        
        public void OnMovement(InputAction.CallbackContext ctx)
        {
            Vector2 input = ctx.ReadValue<Vector2>();
            mov.ReceiveMovementInput(input.x, input.y);
        }
    
        public void OnJump(InputAction.CallbackContext ctx)
        {
            if(ctx.performed)
                mov.ReceiveJumpInput();
        }

        public void OnAim(InputAction.CallbackContext ctx)
        {
            Vector2 input = ctx.ReadValue<Vector2>();
            aim.ReceiveAimInput(input.x, input.y);
        }

        public void OnToolUse(InputAction.CallbackContext ctx)
        {
            if (isCarrying)
            {
                LaunchObject();
            } else if (PhotonNetwork.OfflineMode) 
                tool.UseTool(ctx.phase);
            else
                photonView.RPC("UseTool", RpcTarget.All, ctx.phase);

        }

        public void OnInteract(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                Transform camTransform = aim.camTransform;
                RaycastHit hit;
                Debug.DrawRay(camTransform.position, camTransform.forward, Color.red, 3f);
                
                if (isCarrying)
                {
                    DropObject();
                }
                else if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, interactRange, interactMask))
                {
                    if (hit.collider.CompareTag("Interactable"))
                    {
                        
                         hit.collider.GetComponentInParent<Interactable>().Interact();
                        
                    }

                    if (hit.collider.CompareTag("Carryable"))
                    {
                        CarryObject(hit.collider.gameObject);
                    }
                }
            }
        }

        public void OnMenu(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                if (_pauseMenu != null)
                {
                    _pauseMenu.MenuTrigger(); 
                    input.SwitchCurrentActionMap(_pauseMenu.GameIsPaused? "Menu" : "Player");
                }
                else
                {
                    Debug.LogError("COULDN'T FIND PAUSEMENU, WHERE IS IT?", this);
                }
            }
        }

        public void OnAnalytics(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                if (_pauseMenu != null)
                {
                    _pauseMenu.toggleAnalytics();
                }
                else
                {
                    Debug.LogError("COULDN'T FIND PAUSEMENU, WHERE IS IT?", this);
                }
            }
        }
        
        /// <summary>
        /// Shoots a Raycast forward to look for an Interactable
        /// </summary>
        [PunRPC]
        private void TryInteracting(GameObject interactable)
        {
            interactable.GetComponent<Interactable>().Interact();
        }

        private void CarryObject(GameObject carryAble)
        {
            isCarrying = true;
            carryingObject = carryAble;
            carryingRb = carryAble.GetComponent<Rigidbody>();
            carryingTrans = carryAble.transform;
            carryingTrans.parent = carryingPoint;
            carryingTrans.localPosition = Vector3.zero;
            carryingRb.isKinematic = true;
        }

        private void DropObject()
        {
            isCarrying = false;
            carryingTrans.parent = null;
            carryingRb.isKinematic = false;
            carryingRb = null;
            carryingTrans = null;
            carryingObject = null;
        }

        private void LaunchObject()
        {
            isCarrying = false;
            carryingTrans.parent = null;
            carryingRb.isKinematic = false;
            carryingRb.AddForce(aim.camTransform.forward*launchForce, ForceMode.Impulse);
            carryingRb = null;
            carryingTrans = null;
            carryingObject = null;
        }
        #endregion
    }
}
