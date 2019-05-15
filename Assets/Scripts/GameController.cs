using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Advertisements;
using Prime31;

//This scripts controls the complete game logic.
public class GameController : MonoBehaviour {

	public static GameController Instance;

	//UI
	public GameObject gameOver;
	public GameObject ScoreText;
	
	public GameObject swipeAnywhere;

	//Interactions
	[Range(0.0f, 3.0f)]public float BlackSpawnRate;
	public GameObject blackDotLeftRightPrefab;
	public GameObject blackDotTopDownPrefab;
	public GameObject blueDotPrefab;
	public GameObject plusOnePrefab;
	Vector2[] positionsBlackSpawn;

	//bool
	bool controlsON;
	[HideInInspector]
	public int Score;

	//Tile
	Vector2 startPosition;
	GameObject tileTop;
	GameObject tileRight;

	//Move Vaules
	float horizontalValue;
	public float verticalValue;

	//PC Control 
	bool axisRelised = true; 
	//Mobile Controls
	private Vector2 touchOrigin = -Vector2.one; //Used to store location of screen touch origin for mobile controls.
	int horizontalT = 0;
	int verticalT = 0;

	public static bool isGameOver;
	//public static float scaleFactor;

    // Use this for initialization
    void Start () {
		
		Instance = this;
	
		controlsON = true;
		isGameOver = false;

        //define points
        tileTop = GameObject.Find ("TopPoint");
		tileRight = GameObject.Find ("RightPoint");

		//calucalte move values 
		horizontalValue = tileRight.transform.position.x -  transform.position.x;
		verticalValue = tileTop.transform.position.y - transform.position.y;
		startPosition = transform.position;

		//Start the creation of blue point and black dots on start.
		InvokeRepeating ("SpawnBlack", 0, BlackSpawnRate);
		Invoke ("SpawnBlue", 1);

		//difining spawn points for black dot.
		int offset = 11;
		positionsBlackSpawn = new Vector2[] {
			//top
			new Vector2(startPosition.x,startPosition.y + offset),
			new Vector2(startPosition.x+horizontalValue,startPosition.y + offset),
			new Vector2(startPosition.x-horizontalValue,startPosition.y + offset),
			//right
			new Vector2(startPosition.x+offset,startPosition.y+verticalValue),
			new Vector2(startPosition.x+offset,startPosition.y),
			new Vector2(startPosition.x+offset,startPosition.y-verticalValue),
			//bottom 
			new Vector2(startPosition.x,startPosition.y -offset),
			new Vector2(startPosition.x+horizontalValue,startPosition.y -offset),
			new Vector2(startPosition.x-horizontalValue,startPosition.y -offset),
			//left
			new Vector2(startPosition.x-offset,startPosition.y-verticalValue),
			new Vector2(startPosition.x-offset,startPosition.y),
			new Vector2(startPosition.x-offset,startPosition.y+verticalValue)
		};
	}

	void Update(){
		int horizontal = 0;
		int vertical = 0;

        if (!isGameOver) {

            //Check if Input has registered more than zero touches
            if (controlsON)
            {
                if (Input.touchCount > 0)
                {
                    //Store the first touch detected.
                    Touch myTouch = Input.touches[0];
                    swipeAnywhere.SetActive(false);
                    //Check if the phase of that touch equals Began
                    if (myTouch.phase == TouchPhase.Began)
                    {
                        //If so, set touchOrigin to the position of that touch
                        touchOrigin = myTouch.position;
                    }

                    //If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
                    else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
                    {
                        //Set touchEnd to equal the position of this touch
                        Vector2 touchEnd = myTouch.position;

                        //Calculate the difference between the beginning and end of the touch on the x axis.
                        float x = touchEnd.x - touchOrigin.x;

                        //Calculate the difference between the beginning and end of the touch on the y axis.
                        float y = touchEnd.y - touchOrigin.y;

                        //Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
                        touchOrigin.x = -1;

                        //Check if the difference along the x axis is greater than the difference along the y axis.
                        if (Mathf.Abs(x) > Mathf.Abs(y))
                        {
                            //If x is greater than zero, set horizontal to 1, otherwise set it to -1
                            horizontal = x > 0 ? 1 : -1;
                            horizontalT = x > 0 ? 1 : -1;
                        }
                        else
                        {
                            //If y is greater than zero, set horizontal to 1, otherwise set it to -1
                            vertical = y > 0 ? 1 : -1;
                            verticalT = y > 0 ? 1 : -1;
                        }
                    }

                    // WillSeee
                    if (vertical == 1)
                    {
                        if (transform.position.y <= startPosition.y)
                        {
                            transform.Translate(new Vector2(0, verticalValue));
                        }
                    }
                    if (vertical == -1)
                    {
                        if (transform.position.y >= startPosition.y)
                        {
                            transform.Translate(new Vector2(0, -1 * verticalValue));

                        }
                    }

                    //RIGHT & LEFT
                    if (horizontal == 1)
                    {
                        if (transform.position.x <= startPosition.x)
                        {
                            transform.Translate(new Vector2(horizontalValue, 0));

                        }
                    }
                    if (horizontal == -1)
                    {
                        if (transform.position.x >= startPosition.x)
                        {
                            transform.Translate(new Vector2(-1 * horizontalValue, 0));

                        }
                    }

                }
            }
		}
	}

	//CODE FOR SPAWNING BLACK DOTS OF DEATH
	void SpawnBlack(){
        if (!isGameOver)
        {
            int posIndex = Random.Range(0, 11);
            Vector2 bPlace = positionsBlackSpawn[posIndex];

            /// handle face direction of dragon
            if (posIndex == 0 || posIndex == 1 || posIndex == 2 || posIndex == 6
                || posIndex == 7 || posIndex == 8)
            {
                GameObject blackDragonObj = Instantiate(blackDotTopDownPrefab, bPlace, Quaternion.identity);

                if (blackDragonObj.transform.position.y > 0)
                {
                    blackDragonObj.GetComponent<SpriteRenderer>().flipY = true;
                }
            }
            else
            {
                GameObject blackDragonObj = Instantiate(blackDotLeftRightPrefab, bPlace, Quaternion.identity);

                if (blackDragonObj.transform.position.x < 0)
                {
                    blackDragonObj.GetComponent<SpriteRenderer>().flipX = true;
                }
            }
        }
	}

	//CODE FOR SPAWNING BLUE POINTS
	void SpawnBlue(){
		int randomed1 = Random.Range (-1, 2);//randoms between -1, 0 , 1
		int randomed2 = Random.Range (-1, 2);//randoms between -1, 0 , 1
		Vector2 rPlace = new Vector2 (startPosition.x + randomed1*verticalValue,startPosition.y + randomed2*horizontalValue);
		if (new Vector2 (rPlace.x, rPlace.y) != new Vector2 (transform.position.x, transform.position.y)) { //check so it wont spawn on player position
			Instantiate (blueDotPrefab, rPlace, Quaternion.identity);
		} else
			SpawnBlue ();
	}

	//CODE FOR COLLECTING POINTS AND GAME OVER
	void OnTriggerEnter2D (Collider2D whatHited){
		
		if(whatHited.CompareTag("Point")){

			string ScoreString = ScoreText.GetComponent<Text> ().text;
			Score = int.Parse (ScoreString);
			Score = Score + 1;
			ScoreText.GetComponent<Text> ().text = Score.ToString ();

            CheckNUnlockAchievement();

            //Creating the plus one sign here and destroying the blue point.
            Instantiate (plusOnePrefab, whatHited.transform.position, Quaternion.identity);
			Destroy (whatHited.gameObject);
			Invoke ("SpawnBlue",0);
		}

		//GAMEOVER
		if (whatHited.CompareTag ("Black") && !isGameOver) {
                isGameOver = true;
                gameOver.SetActive(true);
                PlayGameServices.submitScore (Constants.leadaerboard_id, Score);
            }
            }

    public void onRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void onMenu()
    {
        SceneManager.LoadScene(0);
    }

    void CheckNUnlockAchievement()
    {
        if (Score >= 5)
        {
            PlayGameServices.unlockAchievement(Constants.achievement_1);
        }
        else if(Score >= 10)
        {
            PlayGameServices.unlockAchievement(Constants.achievement_2);
        }
        else if (Score >= 15)
        {
            PlayGameServices.unlockAchievement(Constants.achievement_3);
        }
    }
}
