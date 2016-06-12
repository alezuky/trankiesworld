using UnityEngine;
using System.Collections;


public class HiResScreenShots : MonoBehaviour {
	public int resWidth = 7680; 
	public int resHeight = 4320;

	public string path = "E:/OneDrive/HTW/ImagineCup/Final/Press_Kit/Hight_Res_Prints";

	public float printScreenTime = 10f;
	public bool isRecording = false;

	private bool record = false;
	private static string path2;
	private float timer = 0f;
	
	
	
	private bool takeHiResShot = false;
	public static string ScreenShotName(int width, int height) {
		return string.Format("{0}/{1}_{2}x{3}_{4}.png",
		                     path2,//Application.dataPath, 
		                     Application.loadedLevelName,
		                     width, height, 
		                     System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
	}
	public void TakeHiResShot() {
		takeHiResShot = true;
	}
	void LateUpdate() {
		//takeHiResShot |= Input.GetKeyDown("k");
		takeHiResShot |= (record && isRecording);
		if (takeHiResShot) {
			RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
			this.gameObject.GetComponent<Camera>().targetTexture = rt;
			Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
			this.gameObject.GetComponent<Camera>().Render();
			RenderTexture.active = rt;
			screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
			this.gameObject.GetComponent<Camera>().targetTexture = null;
			RenderTexture.active = null; // JC: added to avoid errors
			Destroy(rt);
			byte[] bytes = screenShot.EncodeToPNG();
			string filename = ScreenShotName(resWidth, resHeight);
			System.IO.File.WriteAllBytes(filename, bytes);
			Debug.Log(string.Format("Took screenshot to: {0}", filename));
			takeHiResShot = false;
			record = false;
		}
	}

	void Start()
	{
		path2 = path;
	}

	void Update()
	{
		timer += Time.deltaTime;
		if (timer > printScreenTime) {
			timer = 0f;
			record = true;
		}
	}


}
