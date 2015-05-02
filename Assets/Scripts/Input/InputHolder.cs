using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class InputHolder : MonoBehaviour {
	private IInputUpdate[] playerInputs;
	private int numOfPlayers = 4;
	// Use this for initialization
	void Start () {
		playerInputs = new IInputUpdate[numOfPlayers];
		for (var i = 0; i < numOfPlayers; i++) {
			playerInputs[i] = new xBox360Controller(i);
		}
	}

	void Update() {
		foreach (var input in playerInputs) {
			input.Update();
		}
	}

	public IInput GetPlayerInput(Player player){
		return (IInput) playerInputs [(int)player];
	}
}
