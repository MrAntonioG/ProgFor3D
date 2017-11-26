using System;
using Mogre;
using System.Collections;

namespace Coursework
{
    /// <summary>
    /// This class manages the inputs from keyboard and mouse
    /// </summary>
    class InputsManager
    {
        // Keyboard, mouse and inputs managers
        MOIS.Keyboard mKeyboard;
        MOIS.Mouse mMouse;
        MOIS.InputManager mInputMgr;
        PlayerController controller;                // Reference to an istance of the robot
        /// <summary>
        /// Read only. This property allow to set a reference to an istance of the robot
        /// </summary>
        public PlayerController Controller
        {
            set { controller = value; }
        }

        /// <summary>
        /// Private constructor (for singleton pattern)
        /// </summary>
        private InputsManager() { }

        private static InputsManager instance; // Private instance of the class
        /// <summary>
        /// Gives bak a new istance of the class if the instance field is null
        /// otherwise it gives back the istance already created
        /// </summary>
        public static InputsManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputsManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// This method set the reaction to each key stroke
        /// </summary>
        /// <param name="evt">Can be used to tune the reaction timings</param>
        /// <returns></returns>
        public bool ProcessInput(FrameEvent evt)
        {
            Vector3 displacements = Vector3.ZERO;
            Vector3 angles = Vector3.ZERO;
            mKeyboard.Capture();
            mMouse.Capture();

            if (mKeyboard.IsKeyDown(MOIS.KeyCode.KC_A)){
                controller.Left = true;
            }else{
                controller.Left = false;
            }
            if (mKeyboard.IsKeyDown(MOIS.KeyCode.KC_D))
            {
                controller.Right = true;
            }else
            {
                controller.Right = false;
            }
            if (mKeyboard.IsKeyDown(MOIS.KeyCode.KC_W))
            {
                controller.Forward = true;
            }
            else
            {
                controller.Forward = false;
            }
            if (mKeyboard.IsKeyDown(MOIS.KeyCode.KC_S))
            {
                controller.Backward = true;
            }
            else
            {
                controller.Backward = false;
            }
            
            if (mKeyboard.IsKeyDown(MOIS.KeyCode.KC_LSHIFT))
            {
                controller.Accellerate = true;
            }
            else
            {
                controller.Accellerate = false;
            }

            angles.y = -mMouse.MouseState.X.rel;
            controller.Angles = angles;
            return true;
        }

        /// <summary>
        /// Initializes the keyboad, mouse and the input manager systems
        /// </summary>
        /// <param name="windowHandle">An handle to the game windonw</param>
        public void InitInput(ref int windowHandle)
        {
            mInputMgr = MOIS.InputManager.CreateInputSystem((uint)windowHandle);
            mKeyboard = (MOIS.Keyboard)mInputMgr.CreateInputObject(MOIS.Type.OISKeyboard, true);
            mMouse = (MOIS.Mouse)mInputMgr.CreateInputObject(MOIS.Type.OISMouse, false);
            mKeyboard.KeyPressed += new MOIS.KeyListener.KeyPressedHandler(OnKeyPressed);
           
        }

        
        /// <summary>
        /// Buffered key listener
        /// </summary>
        /// <param name="arg">A keyboard event</param>
        /// <returns></returns>
        private bool OnKeyPressed(MOIS.KeyEvent arg)
        {
            switch (arg.key)
            {
                case MOIS.KeyCode.KC_SPACE:
                    {
                        controller.Shoot = true;
                        break;
                    }
                case MOIS.KeyCode.KC_E:
                    {
                        controller.SwapGun = true;
                        break;
                    }
                case MOIS.KeyCode.KC_Q:
                    {
                        controller.Reload = true;
                        return false;
                    }
            }
            return true;
        }
    }
}
