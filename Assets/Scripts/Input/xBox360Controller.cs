using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using XInputDotNetPure;

public class xBox360Controller : MonoBehaviour, IInput {

	public int playerNumber;

	bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	public xBox360Controller(int playerNumber){
		playerNumber = playerNumber;
	}

	// Update is called once per frame
	void Update () {
		if (!playerIndexSet || !prevState.IsConnected)
		{
			PlayerIndex testPlayerIndex = (PlayerIndex)playerNumber;
			GamePadState testState = GamePad.GetState(testPlayerIndex);
			if (testState.IsConnected)
			{
				Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
				playerIndex = testPlayerIndex;
				playerIndexSet = true;
			}
		}
	}

	public float GetAxis(InputAxis axis){
		switch (axis)
		{
		case InputAxis.Horizontal:
			return GetAxisHorizontal();
		}
		return 0.0f;
	}

	public bool GetButtonDown(Inputs button){
		switch (button) {
		case Inputs.Jump:
			return GetButtonDownJump();
		case Inputs.Fire:
			return GetButtonDownFire();
		case Inputs.Start:
			return GetButtonDownStart();
		case Inputs.Start:
			return GetButtonDownBack();
		}
		return false;
	}

	public bool GetButtonUp(Inputs button){
		switch (button) {
		case Inputs.Jump:
			return GetButtonUpJump();
		case Inputs.Fire:
			return GetButtonUpFire();
		case Inputs.Start:
			return GetButtonUpStart();
		case Inputs.Back:
			return GetButtonUpBack();
		}
		return false;
	}

	private float GetAxisHorizontal(){
		if (state.DPad.Left == ButtonState.Pressed && state.DPad.Right == ButtonState.Released) {
			return -1.0f;
		} else if (state.DPad.Right == ButtonState.Pressed && state.DPad.Left == ButtonState.Released) {
			return 1.0f;
		}
		return state.ThumbSticks.Left.X;
	}

	private bool GetButtonDownJump(){
		return prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed;
	}

	private bool GetButtonUpJump(){
		return prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released;
	}

	private bool GetButtonDownFire(){
		return state.Triggers.Right > 0 && prevState.Triggers.Right == 0;
	}
	
	private bool GetButtonUpFire(){
		return state.Triggers.Right == 0 && prevState.Triggers.Right > 0;
	}

	private bool GetButtonDownStart(){
		return prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed;
	}

	private bool GetButtonUpStart(){
		return state.Buttons.Start == ButtonState.Released && prevState.Buttons.Start == ButtonState.Pressed;
	}

	private bool GetButtonDownBack(){
		return prevState.Buttons.Back == ButtonState.Released && state.Buttons.Back == ButtonState.Pressed;
	}
	
	private bool GetButtonUpBack(){
		return state.Buttons.Back == ButtonState.Released && prevState.Buttons.Back == ButtonState.Pressed;
	}
}
