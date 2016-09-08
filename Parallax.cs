using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	public Transform[] backgrounds;       //Array of all of the back- and fore- grounds to be parallaxed
	private float[] parallaxScales;       //Proportion of the camera's movement to move the backgrounds by
	public float smoothing = 1f;          //How smooth the parallax will be **MUST BE >0

	private Transform cam;                //Reference to the Camera's transform
	private Vector3 previousCamPos;       //store position of camera in previous frame

	//Awake is called before Start().  Great for references
	void Awake () {
		// set up camera reference
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () {
		// The previous frame had the current frame's camera position
		previousCamPos = cam.position;
		
		// asigning coresponding parallaxScales
		parallaxScales = new float[backgrounds.Length];
		for (int i = 0; i < backgrounds.Length; i++) {
			parallaxScales[i] = backgrounds[i].position.z*-1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//for each background...  
		for (int i = 0; i < backgrounds.Length; i++) {
			// the parallax is the opposite of the camera movement because the previous frame multiplied by the scale
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
			
			// set a target x position which is the current position plus the parallax
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;
			
			// create a target position which is the background's current position with it's target x position
			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
			
			// fade between current position and the target position using lerp
			backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}
		//set the previousCamPos to the camera's positon at the end of the frame
		previousCamPos = cam.position;

	}
}
