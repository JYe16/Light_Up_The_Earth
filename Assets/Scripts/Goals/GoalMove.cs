
using DG.Tweening;
using Player;
using UnityEngine;

public class GoalMove : MonoBehaviour
{
    [HideInInspector]public AudioClip destroyGoalAudio;
    [HideInInspector]public GameObject explosion;
    [HideInInspector]public bool isReturn = false;
    // params about fracture
    public GameObject fracturedPieces;
    public GameObject originalGoal;
    public float force;
    public float radius;
    public float moveSpeed;
    [HideInInspector] public float curSpeed;
    public bool isTutorial = false;
    
    private bool hideCompleteModel = false;
    private bool isDestroy = false;
    private GoalValue goalValue;
    private float destroyPiecesDuration = 5.0f;
    private LaserControl laserControl;
    private float maxSpeed;
    private Tweener moveTween;
    private Vector3 distroyPos;
    [HideInInspector]public LongClickButton shootBtn;
    
    void Start()
    {
        goalValue = GetComponent<GoalValue>();
        maxSpeed = 2.5f * moveSpeed;
        curSpeed = moveSpeed;
    }
    
    public void ReturnGoal(Vector3 distroyPos, LaserControl laserControl)
    {
        bool isPause = isTutorial
            ? SimpleGameManager.gm.pauseGame
            : GameManager.gm.gameState == GameManager.GameState.Pausing;
        if (isPause || goalValue.isCaptured) return;
        if (GetComponent<RotateBySelf>())
        {
            GetComponent<RotateBySelf>().enabled = false;
        }
        isReturn = true;
        // enable long click button
        shootBtn.isActive = true;
        this.laserControl = laserControl;
        this.distroyPos = distroyPos;
        laserControl.ChangeBackStatus(true);
        // move goal
        float distance = Vector3.Distance(transform.position, distroyPos);
        moveTween = gameObject.transform.DOMove(distroyPos, distance / curSpeed).OnComplete(AfterReturn);
    }

    private void AfterReturn()
    {
        laserControl.ChangeBackStatus(false);
        // disable long click button
        shootBtn.isActive = false;
        goalValue.isCaptured = true;
        if(!hideCompleteModel) goalValue.CapturedEffect();
        // hide laser line
        laserControl.HideLaserEnableMove();
        // send cool down message
        laserControl.SendDestroyMessage();
        DestroyGoal();
    }

    private void DestroyGoal()
    {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        Destroy(gameObject);
    }

    public void ExplosionAndHide()
    {
        originalGoal.GetComponent<Renderer>().enabled = false;
        hideCompleteModel = true;
        Explode();
    }

    private void Explode()
    {
        fracturedPieces.SetActive(true);
        Vector3 screenCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        // play explosion fire effect
        GameObject bombObj = Instantiate(explosion, screenCenter, Quaternion.identity);
        Destroy(bombObj, destroyPiecesDuration / 2);
        // change speed to lase line speed after explosion
        SpeedUp(maxSpeed);
        // play explosion sound effect
        if(destroyGoalAudio != null && PlayerPrefs.GetInt("sound") == 1) 
            AudioSource.PlayClipAtPoint(destroyGoalAudio, Camera.main.transform.position, 0.3f);
        // add force to pieces
        Vector3 boundCenter = originalGoal.GetComponent<MeshFilter>().sharedMesh.bounds.center;
        Vector3 center = fracturedPieces.transform.localToWorldMatrix.MultiplyPoint(boundCenter);
        Transform rootTrans = fracturedPieces.transform;
        for (int i = 0; i < rootTrans.childCount; i++)
        {
            Transform pieceTrans = rootTrans.GetChild(i);
            Rigidbody body = pieceTrans.GetComponent<Rigidbody>();
            if (body != null)
            {
                body.AddExplosionForce(force, center, radius);
                Destroy(rootTrans.GetChild(i).gameObject, destroyPiecesDuration);
            }
        }
    }

    public void SpeedUp(float targetSpeed)
    {
        if (curSpeed > maxSpeed) return;
        curSpeed = targetSpeed;
        float distance = Vector3.Distance(transform.position, distroyPos);
        moveTween.ChangeValues(transform.position, distroyPos, distance / curSpeed);
    }

    public void StopMove()
    {
        moveTween.Kill();
        curSpeed = moveSpeed;
    }
}
