using UnityEngine;
using UnityEngine.SceneManagement;

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
                // SceneManager.LoadScene("Level_00_Scene");
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