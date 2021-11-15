using System;
using DG.Tweening;
using UI.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryPlot : MonoBehaviour
{
	public GameObject[] storyPics;
	public Button continue_btn;
	private int curIndex = 0;

	private void Start()
	{
		continue_btn.onClick.AddListener(OnClickContinue);
		DisplayStory();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && curIndex < storyPics.Length)
		{
			DisplayStory();
		}
	}

	private void DisplayStory()
	{
		GameObject curStory = storyPics[curIndex++];
		ImageEffects ie = curStory.GetComponentInChildren<ImageEffects>();
		ie.ActiveEffect();
	}

	private void OnClickContinue()
	{
		GameObject launchMusic = GameObject.Find("gameLaunchMusic");
		if (launchMusic != null)
		{
			AudioSource launchMusicSource = launchMusic.GetComponent<AudioSource>();
			launchMusicSource.DOFade(0, 2).OnComplete(() => Destroy(launchMusic.gameObject));
		}
		StartPage.isPlaying = false;
		SceneManager.LoadScene("Tutorial_Level");
	}
}