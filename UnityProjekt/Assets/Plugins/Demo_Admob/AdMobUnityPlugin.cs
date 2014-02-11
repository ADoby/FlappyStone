using UnityEngine;

public class AdMobUnityPlugin {

	private static string classname = "com.platoevolved.admobunity.UnityAndroidInterface";
	
	//Call once only, initialises the ad banner
	public static void StartAds(){
		try {
			AndroidJavaClass jc = new AndroidJavaClass(classname);
			jc.CallStatic("StartAds");
		} catch (System.Exception ex) {
			Debug.Log("Note, you must be deployed to an Android device! " + ex.Message);
		}
	}
	//Destroys the ad banner
	public static void StopAds(){
		try {
			AndroidJavaClass jc = new AndroidJavaClass(classname);
			jc.CallStatic("StopAds");		
		} catch (System.Exception ex) {
			Debug.Log("Note, you must be deployed to an Android device! " + ex.Message);
		}
	}
	//Positions the ad banner: xpos = "top", "bottom" ypos = "left", "middle","right"
	public static void SetPosition(string xpos,string ypos){
		try {
			AndroidJavaClass jc = new AndroidJavaClass(classname);
			jc.CallStatic("SetPosition",new string[] {xpos, ypos});			
		} catch (System.Exception ex) {
			Debug.Log("Note, you must be deployed to an Android device! " + ex.Message);
		}
	}

	public static void StartInterstitial(string interstitialUnitID){
		try {
			AndroidJavaClass jc = new AndroidJavaClass(classname);
			jc.CallStatic("StartInterstitial",interstitialUnitID);
		} catch (System.Exception ex) {	
			
		}
	}

	//Shows the ad banner if hidden
	public static void ShowAds(){
		try {
			AndroidJavaClass jc = new AndroidJavaClass(classname);
			jc.CallStatic("ShowAds");			
		} catch (System.Exception ex) {
			Debug.Log("Note, you must be deployed to an Android device! " + ex.Message);
		}
	}
	//Hides the ad banner if showing
	public static void HideAds(){
		try {
			AndroidJavaClass jc = new AndroidJavaClass(classname);
			jc.CallStatic("HideAds");			
		} catch (System.Exception ex) {
			Debug.Log("Note, you must be deployed to an Android device! " + ex.Message);
		}
	}
	//Forces an ad refresh
	public static void RefreshAd(){
		try {
			AndroidJavaClass jc = new AndroidJavaClass(classname);
			jc.CallStatic("RefreshAd");			
		} catch (System.Exception ex) {
			Debug.Log("Note, you must be deployed to an Android device! " + ex.Message);
		}
	}
	//Sets the banner type: "BANNER", "IAB_MRECT", "IAB_BANNER", "IAB_LEADERBOARD", "SMART_BANNER"
	public static void SetBanner(string bannertype){
		try {
			AndroidJavaClass jc = new AndroidJavaClass(classname);
			jc.CallStatic("SetBanner",bannertype);			
		} catch (System.Exception ex) {
			Debug.Log("Note, you must be deployed to an Android device! " + ex.Message);
		}
	}
	//Call before StartAds() to put into testmode
	public static void SetTestMode(string test_device_id){
		try {
			AndroidJavaClass jc = new AndroidJavaClass(classname);
			jc.CallStatic("SetTestMode",test_device_id);			
		} catch (System.Exception ex) {
			Debug.Log("Note, you must be deployed to an Android device! " + ex.Message);
		}
	}
}
