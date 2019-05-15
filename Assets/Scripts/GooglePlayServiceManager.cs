using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class GooglePlayServiceManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayGameServices.authenticate ();
		print ("PlayGameServices.authenticate");
		PlayGameServices.enableDebugLog( true );
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onLeaderboardBtn(){
		PlayGameServices.showLeaderboard (Constants.leadaerboard_id);
	}

	public void onScorePostBtn(){
		PlayGameServices.submitScore ("CgkI1pHE_s4JEAIQAQ", 100);
	}

	public void onUnlockAchievements(){
		PlayGameServices.unlockAchievement ("CgkI1pHE_s4JEAIQAg");
	}
}
