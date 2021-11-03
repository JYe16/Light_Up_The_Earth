using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoadSceneEvents : MonoBehaviour
{
        public void GotoShop()
        {
                SceneManager.LoadScene("ShopPage");
        }
        
        public void GotoScoreRank()
        {
                SceneManager.LoadScene("ScoreRank");
        }
        
        public void ContinueOnClick()
        {
                SceneManager.LoadScene("Level_00_Scene");
        }
        
        public void MainMenuOnClick()
        {
                SceneManager.LoadScene("StartPage");
        }
        
        public void PlayAgainOnClick()
        {
                GameObject launchMusic = GameObject.Find("gameLaunchMusic");
                if (launchMusic != null)
                {
                        AudioSource launchMusicSource = launchMusic.GetComponent<AudioSource>();
                        launchMusicSource.DOFade(0, 2).OnComplete(() => Destroy(launchMusic.gameObject));
                        StartPage.isPlaying = false;
                }
                SceneManager.LoadScene("LoadingPage");
        }
        
        public void GameOverOnClick()
        {
                SceneManager.LoadScene("EnterName");
        }
        
        /*
        public void StoryPlotOnClick()
        {
                SceneManager.LoadScene("Story_Plot");
        }
        */

}