using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FirstPerson
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(FirstPersonMovement))]
    [RequireComponent(typeof(FirstPersonAiming))]
    public class FirstPersonPlayer : MonoBehaviourPun
    {
        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject localPlayerInstance;

        #region Required Components
        private FirstPersonAiming aim;
        private FirstPersonMovement mov;
        private PlayerInput input;
        #endregion

        #region Player Stuff

        public Tool tool;

        #endregion
        
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

    
        // Start is called before the first frame update
        void Start()
        {
            aim = GetComponent<FirstPersonAiming>();
            mov = GetComponent<FirstPersonMovement>();
            input = GetComponent<PlayerInput>();
            
            if (!photonView.IsMine && PhotonNetwork.IsConnected)
            {
                input.enabled = false;
                //TODO: camera might not be initialized yet. Check
                aim.DisableCamera();
            }
            else
            {
                input.enabled = true;
            }
        }

        /// <summary>
        /// This is simply a wrapper for the Tool.UseTool method so we can tag it as a PunRPC.
        /// </summary>
        [PunRPC]
        public void UseTool()
        {
            Debug.Log("RPC UseTool called", this);
            tool.UseTool();
        }



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
            if(ctx.performed)
                if (PhotonNetwork.OfflineMode)
                    tool.UseTool();
                else
                    photonView.RPC("UseTool", RpcTarget.All);

        }
        
        #endregion
    }
}
