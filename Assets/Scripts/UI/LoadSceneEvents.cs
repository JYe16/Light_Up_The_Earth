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
                SceneManager.LoadScene("Level_00_Scene");
        }
        
        public void NewGameOnClick()
        {
                SceneManager.LoadScene("Level_00_Scene");
        }
        
        public void GameOverOnClick()
        {
                SceneManager.LoadScene("EnterName");
        }





}