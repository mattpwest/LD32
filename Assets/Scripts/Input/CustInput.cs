using UnityEngine;
using System.Collections;
using AssemblyCSharp;

namespace AssemblyCSharp
{
	public interface Input {
		float GetAxis(InputAxis axis);
		bool GetButtonDown(Inputs button);
		bool GetButtonUp(Inputs button);
	}
}
