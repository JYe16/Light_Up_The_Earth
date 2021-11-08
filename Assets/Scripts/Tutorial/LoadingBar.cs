using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

	public class LoadingBar : MonoBehaviour
	{
		public Slider slider;
		public Text text;
		public Text headingText;
		
		private int curProgressValue = 0;
		private bool isReady = false;
		private AsyncOperation loadOperation;

		private void Start()
		{
			StartCoroutine(LoadLevel());
		}

		void Update()
		{
			if (curProgressValue < 100)
			{
				curProgressValue++;
			}
        
			text.text = curProgressValue + " %";
			slider.value = curProgressValue / 100f;

			if (curProgressValue == 100)
			{
				if (loadOperation.progress >= 0.9f)
				{
					slider.value = 1;
					text.text = "Tap anywhere to continue";
					headingText.text = "It's ready!";
					if (Input.GetMouseButton(0))
					{
						loadOperation.allowSceneActivation = true;
					}
				}
			}
		}
		
		IEnumerator LoadLevel()
		{
			loadOperation = SceneManager.LoadSceneAsync("Level_00_Scene");
			loadOperation.allowSceneActivation = false;
			yield return loadOperation;
		}

	}