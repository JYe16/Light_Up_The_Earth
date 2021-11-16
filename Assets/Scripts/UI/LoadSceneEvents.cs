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
                GameObject overMusic = GameObject.Find("gameOverMusic");
                if (overMusic != null)
                {
                        AudioSource overMusicSource = overMusic.GetComponent<AudioSource>();
                        overMusicSource.DOFade(0, 2).OnComplete(() => Destroy(overMusic.gameObject));
                        StartPage.isPlaying = false;
                }
                SceneManager.LoadScene("StartPage");
        }
        
        public void PlayAgainOnClick()
        {
                GameObject overMusic = GameObject.Find("gameOverMusic");
                if (overMusic != null)
                {
                        AudioSource overMusicSource = overMusic.GetComponent<AudioSource>();
                        overMusicSource.DOFade(0, 2).OnComplete(() => Destroy(overMusic.gameObject));
                        StartPage.isPlaying = false;
                }
                
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
}