using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    [SerializeField] private GameObject Square;
    [SerializeField] private GameObject EndPanel;
    [SerializeField] private Text TimeTxt;
    [SerializeField] private Text ThisScoreTxt;
    [SerializeField] private Text MaxScoreTxt;
    [SerializeField] private Animator Anim;

    private bool _isRunning = true;
    private float _alive = 0f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        InvokeRepeating("MakeSquare", 0.0f, 0.5f);
    }

    void MakeSquare()
    {
        Instantiate(Square);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRunning)
        {
            _alive += Time.deltaTime;
            TimeTxt.text = _alive.ToString("N2");
        }
    }

    public void GameOver()
    {
        _isRunning = false;
        Anim.SetBool("isDie", true);

        Invoke("timeStop", 0.5f);

        ThisScoreTxt.text = _alive.ToString("N2");

        EndPanel.SetActive(true);

        if(PlayerPrefs.HasKey("bestscore") == false)
        {
            PlayerPrefs.SetFloat("bestscore", _alive);
        }
        else
        {
            if(_alive > PlayerPrefs.GetFloat("bestscore"))
            {
                PlayerPrefs.SetFloat("bestscore", _alive);
            }
        }
        MaxScoreTxt.text = PlayerPrefs.GetFloat("bestscore").ToString("N2");
    }

    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void TimeStop()
    {
        Time.timeScale = 0f;
    }
}
