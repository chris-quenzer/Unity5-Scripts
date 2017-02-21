using UnityEngine;
using System.Collections;

public class webGL : MonoBehaviour {

	void Update ()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
            WebGLInput.captureAllKeyboardInput = true;
        #endif
    }
}
