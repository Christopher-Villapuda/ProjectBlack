using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// Pauses the game/by setting timescale to 0.
    /// </summary>
    [CommandInfo("Scene",
                 "Pause Screen",
                 "Applies pause to the game. IMPORTANT: This command must be placed inside unconnected/independent blocks in flowcharts with Execute On Event in that block is null/None. (Optional) Design your custom pause menu inside Canvas. Only 2 supported events such as; KeyDown and KeyUp")]
    [AddComponentMenu("")]
    [ExecuteInEditMode]
    public class PauseScreen : Command
    {
        [Tooltip("The type of keypress to activate on")]
        [SerializeField] protected KeyPressType keyPressType;

        [Tooltip("Keycode of the key to activate on")]
        [SerializeField] protected KeyCode keyCode;
        [Tooltip("GameObject/Canvas as screen overlay")]
        [SerializeField] protected GameObject canvas;
        [Tooltip("Replace default text with custom")]
        [SerializeField] protected string customText = "Game Paused";
        [Tooltip("Disable background box layout")]
        [SerializeField] protected bool disableBoxOverlay = false;

        bool isPaused = false;
        void OnGUI()
        {
            GUIStyle style = new GUIStyle();

            if (isPaused)
            {
                //custom styling, otherwise, just leave it
                style.alignment = TextAnchor.MiddleCenter;
                style.fontSize = 50;
                style.normal.textColor = Color.white;
                style.fontStyle = FontStyle.BoldAndItalic;

                // Make a group on the center of the screen

                GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
                // Make a box so you can see where the group is on-screen.
                if (disableBoxOverlay)
                {
                    return;
                }
                else
                {
                    GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
                    GUI.color = new Color(0.6f, 0.6f, 0.6f);
                    GUI.Label(new Rect(0, 0, Screen.width, Screen.height), customText, style);
                }

                GUI.EndGroup();
            }
        }

        // Pause Screen
        protected void Pause()
        {
            Time.timeScale = 0;
            isPaused = true;
            GUI.enabled = true;
            SayDialog sayDialog = SayDialog.GetSayDialog();
            sayDialog.GetComponent<DialogInput>().enabled = false;
            if (canvas != null)
            {
                canvas.SetActive(true);
            }

        }
        protected void Resume()
        {
            Time.timeScale = 1;
            isPaused = false;
            GUI.enabled = false;
            SayDialog sayDialog = SayDialog.GetSayDialog();
            sayDialog.GetComponent<DialogInput>().enabled = true;
            if (canvas != null)
            {
                canvas.SetActive(false);
            }

        }
        protected virtual void OnComplete()
        {
            Continue();
        }
        protected virtual void Update()
        {
            switch (keyPressType)
            {
                case KeyPressType.KeyDown:
                    if (Input.GetKeyDown(keyCode))
                    {
                        if (!isPaused)
                        {
                            //changes box overlay state when GameObject exist
                            if (canvas != null)
                            {
                                disableBoxOverlay = true;
                            }
                            else
                            {
                                disableBoxOverlay = false;
                            }
                            Pause();
                        }
                        else
                        {
                            Resume();
                        }
                    }
                    break;
                case KeyPressType.KeyUp:
                    if (Input.GetKeyUp(keyCode))
                    {
                        if (!isPaused)
                        {
                            Pause();
                        }
                        else
                        {
                            Resume();
                        }
                    }
                    break;
            }
        }
        #region Public members
        public override string GetSummary()
        {
            string keycodesummary = "";
            if (keyCode == KeyCode.None)
            {
                return "Error: Assign your key";
            }

            return keycodesummary;
        }
        public override Color GetButtonColor()
        {
            return new Color32(221, 184, 169, 255);
        }
        public override void OnEnter()
        {
            Canvas.ForceUpdateCanvases();

            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
        public override void OnCommandAdded(Block parentBlock)
        {
            //Default to display type: KeyDown
            keyPressType = KeyPressType.KeyDown;
        }
        #endregion
    }
}