using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace QuickStart
{
    public class SceneScript : NetworkBehaviour
    {
        public SceneReference sceneReference;
        public TextMeshProUGUI canvasStatusText;
        private PlayerScript playerScript;

        [SyncVar(hook = nameof(OnStatusTextChanged))]
        public string statusText;

        public TextMeshProUGUI canvasAmmoText;

        public void UIAmmo(int _value)
        {
            canvasAmmoText.text = "Ammo: " + _value;
        }

        void OnStatusTextChanged(string _Old, string _New)
        {
            //called from sync var hook, to update info on screen for all players
            canvasStatusText.text = statusText;
        }

        public void ButtonSendMessage()
        {
            // Check if the local player is available
            if (NetworkClient.localPlayer != null)
            {
                // Get the PlayerScript attached to the local player
                playerScript = NetworkClient.localPlayer.GetComponent<PlayerScript>();

                if (playerScript == null)
                {
                    Debug.LogError("PlayerScript is not assigned or found!");
                }

                playerScript.CmdSendPlayerMessage();
            }

            else
            {
                Debug.LogError("Local player is not available.");
            }
            
        }

        public void ButtonChangeScene()
        {
            if (isServer)
            {
                Scene scene = SceneManager.GetActiveScene();
                if (scene.name == "MyScene")
                    NetworkManager.singleton.ServerChangeScene("MyOtherScene");
                else
                    NetworkManager.singleton.ServerChangeScene("MyScene");
            }
            else
                Debug.Log("You are not Host.");
        }

    }
}
