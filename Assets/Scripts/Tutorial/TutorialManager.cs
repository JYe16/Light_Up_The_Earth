using System;
using System.Collections;
using DG.Tweening;
using UI.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
	public Image mask;
	public GameObject gamePlayMusic;
	public Canvas controlsCanvas;
	public Canvas propsCanvas;
	public Canvas infoCanvas;
	public Canvas loadingBar;
	
	public GameObject controlsGuide;
	public GameObject infoGuide;
	public GameObject[] propsGuides;
	
	public Text centerText;
	public Button skipBtn;
	public Button goBtn;
	public Button startBtn;

	private int curStep = 0;
	private string WELCOME_INTRO = "Hi, stranger.\r\nWelcome to the training ground.";
	private string OPERATIONS_INTRO = "Firstly, there are some operations you need to know.";
	private string INFO_INTRO = "You must achieve target score within limited time ! ! !";
	private string EXPLORE_INTRO = "now, try to control your ship and explore the whole space !";
	private string PROPS_INTRO = "Did you notice these props below? Let's learn their effects.";
	private string PROPS_USE_INTRO = "try to use them!";
	private string GOAL_GUIDE_1 = "crystals always have higher value than stones and they move faster.";
	private string GOAL_GUIDE_2 = "you can get a random prop from the prop package. Also you can buy them in the shop after passing a level.";
	private string FINISH_INTRO = "Now, it is time to start your trip !";

	private bool canClickBlank = false;
	public static bool isPlaying = false;
	private bool isShowFullText = false;
	private bool isPause = false;
	
	void Start()
	{
		StartType(WELCOME_INTRO);

		skipBtn.gameObject.SetActive(false);
		skipBtn.onClick.AddListener(SkipTutorial);
		goBtn.gameObject.SetActive(false);
		goBtn.onClick.AddListener(NextStep);
		startBtn.gameObject.SetActive(false);
		startBtn.onClick.AddListener(SkipTutorial);
		
		//start playing background music
		if(!isPlaying)
		{
			DontDestroyOnLoad(gamePlayMusic.gameObject);
			if(PlayerPrefs.GetInt("music") == 1)
			{
				gamePlayMusic.gameObject.GetComponent<AudioSource>().Play();
			}
			isPlaying = true;
		}
	}
	
	private void Update()
	{
		if (!isPause && isShowFullText && Input.GetMouseButtonDown(0))
		{
			if (canClickBlank)
			{
				NextStep();
			}
			else
			{
				BeforeNextStep();
			}
		}
	}

	public void TypeFinish()
	{
		isShowFullText = true;
	}

	private void BeforeNextStep()
	{
		switch (curStep) 
		{
			case 0:
				skipBtn.gameObject.SetActive(true);
				goBtn.gameObject.SetActive(true);
				break;
			case 5:
				ShowHighlights(propsCanvas);
				canClickBlank = true;
				break;
			case 10:
				HideMaskAndStart();
				break;
			default:
				canClickBlank = true;
				break;
		}
	}
	
	private void SkipTutorial()
	{
		loadingBar.gameObject.SetActive(true); 
	}

	private void NextStep()
	{
		canClickBlank = false;
		switch (curStep)
		{
			case 0:
				skipBtn.gameObject.SetActive(false);
				goBtn.gameObject.SetActive(false);
				StartType(OPERATIONS_INTRO);
				break;
			case 1:
				ClearContent();
				ShowHighlights(controlsCanvas, controlsGuide);
				canClickBlank = true;
				break;
			case 2:
				HideHighlights(controlsCanvas, controlsGuide);
				StartType(INFO_INTRO);
				ShowHighlights(infoCanvas, infoGuide);
				break;
			case 3:
				HideHighlights(infoCanvas, infoGuide);
				StartType(EXPLORE_INTRO);
				break;
			case 4:
				HideMaskAndStart();
				break;
			case 5:
				ClearContent();
				PropsGuide();
				break;
			case 6:
			case 7:
			case 8:
			case 9:
				PropsGuide();
				break;
			case 11:
				StartType(GOAL_GUIDE_2);
				break;
			case 12:
				StartType(FINISH_INTRO);
				break;
			case 13: // end of tutorial
				startBtn.gameObject.SetActive(true);
				break;
		}
		curStep++;
	}

	private void StartType(string info)
	{
		isShowFullText = false;
		centerText.GetComponent<TypeWriterEffect>().StartType(info);
	}

	private void ClearContent()
	{
		centerText.GetComponent<TypeWriterEffect>().ClearContent();
	}

	private void HideMaskAndStart()
	{
		ClearContent();
		mask.DOFade(0, 0.4f).OnComplete(() =>
		{
			mask.enabled = false;
			isPause = true;
			ChangeOverrideSortings(false);
			SimpleGameManager.gm.PauseGame(false);
		});
	}

	private void PropsGuide()
	{
		switch (curStep)
		{
			case 5:
				propsGuides[0].SetActive(true);
				canClickBlank = true;
				break;
			case 6:
				propsGuides[0].SetActive(false);
				canClickBlank = true;
				propsGuides[1].SetActive(true);
				break;
			case 7:
				propsGuides[1].SetActive(false);
				canClickBlank = true;
				propsGuides[2].SetActive(true);
				break;
			case 8:
				propsGuides[2].SetActive(false);
				canClickBlank = true;
				propsGuides[3].SetActive(true);
				break;
			case 9:
				HideHighlights(propsCanvas, propsGuides[3]);
				StartType(PROPS_USE_INTRO);
				break;
		}
	}

	public void DisplayPropsGuide()
	{
		mask.DOFade(0.8f, 0.4f).OnComplete(() =>
		{
			mask.enabled = true;
			isPause = false;
			ChangeOverrideSortings(true);
			StartType(PROPS_INTRO);
		});
	}

	public void DisplayGoalGuide()
	{
		mask.DOFade(0.8f, 0.4f).OnComplete(() =>
		{
			mask.enabled = true;
			isPause = false;
			ChangeOverrideSortings(true);
			StartType(GOAL_GUIDE_1);
			curStep++;
		});
	}

	private void HideHighlights(Canvas canvas, GameObject guide = null)
	{
		canvas.overrideSorting = true;
		if(guide != null)
		{
			guide.SetActive(false);
		}
	}

	private void ShowHighlights(Canvas canvas, GameObject guide = null)
	{
		canvas.overrideSorting = false;
		if (guide != null)
		{
			guide.SetActive(true);
		}
	}

	private void ChangeOverrideSortings(bool val)
	{
		controlsCanvas.overrideSorting = val;
		infoCanvas.overrideSorting = val;
		propsCanvas.overrideSorting = val;
	}
}