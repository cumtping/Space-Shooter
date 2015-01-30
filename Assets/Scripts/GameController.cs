using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	public GameObject hazard;
	public Vector3 spawnVector;
	public float spawnCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	private GUIText scoreText;
	private GUIText restartText;
	private GUIText gameoverText;
	public Vector3 scoreVector;
	private int score;

	private bool restart;
	private bool gameover;

	void Start()
	{
		StartCoroutine (SpawnWaves ());
		audio.Play ();
		score = 0;

		SetupGUIText ();

		restart = false;
		gameover = false;
		restartText.guiText.text = "";
		gameoverText.guiText.text = "";
		
		UpdateScore ();
	}

	void Update()
	{
		if(restart)
		{
			if(Input.GetKeyDown(KeyCode.R))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	void SetupGUIText()
	{
		GameObject obj1 = new GameObject("Score text");
		Instantiate(obj1);
		scoreText = obj1.AddComponent<GUIText>();
		scoreText.transform.position = new Vector3(0f, 1f, 0f);//new Vector3(scoreVector.x,scoreVector.y,scoreVector.z);
		scoreText.anchor = TextAnchor.UpperLeft;
		scoreText.alignment =  TextAlignment.Left;
		scoreText.pixelOffset = new Vector2(10, -10);
		scoreText.fontSize = 20;

		GameObject obj2 = new GameObject ("Restart text");
		Instantiate (obj2);
		restartText = obj2.AddComponent<GUIText>();
		restartText.transform.position = new Vector3(1f, 1f, 0f);
		restartText.anchor = TextAnchor.UpperRight;
		restartText.alignment =  TextAlignment.Right;
		restartText.pixelOffset = new Vector2(-10, -10);
		restartText.fontSize = 20;
		
		GameObject obj3 = new GameObject ("Gameover text");
		Instantiate (obj3);
		gameoverText = obj3.AddComponent<GUIText>();
		gameoverText.transform.position = new Vector3(0.5f, 0.6f, 0f);
		gameoverText.anchor = TextAnchor.MiddleCenter;
		gameoverText.alignment =  TextAlignment.Center;
		gameoverText.fontSize = 25;
	}

	IEnumerator SpawnWaves()
	{
		while(true)
		{
			yield return new WaitForSeconds(startWait);
			for (int i=0; i<spawnCount; i++) 
			{
				Instantiate (hazard, new Vector3 (Random.Range (-spawnVector.x, spawnVector.x), spawnVector.y, spawnVector.z),
				             Quaternion.identity);
				yield return new WaitForSeconds(spawnWait);
			}

			yield return new WaitForSeconds(waveWait);

			if(gameover)
			{
				Debug.Log ("GameOver3");
				restart = true;
				restartText.guiText.text = "Press 'R' to restart game.";
				break;
			}
		}
	}

	public void AddScore(int newScore)
	{
		//Debug.Log ("AddScore: " + newScore);
		score += newScore;
		UpdateScore ();
	}

	void UpdateScore()
	{
		//Debug.Log ("Score: " + score);
		scoreText.guiText.text =  "Score: " + score;
	}

	public void GameOver()
	{
		Debug.Log ("GameOver2");
		gameover = true;
		gameoverText.guiText.text = "Game Over";
	}
}
