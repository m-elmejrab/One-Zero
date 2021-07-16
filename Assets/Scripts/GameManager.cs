using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    private bool isPlaying;
    float elapsedTime;
    float difficultyTimer;
    float difficultyIncrease;
    float totalPlayTime;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private class OneZero
    {
        public int index;
        public bool state;
        public GameObject gameObj;

        public OneZero() { }
        public OneZero(int i, bool s, GameObject obj)
        {
            index = i;
            state = s;
            gameObj = obj;
        }
    }

    

    //List<GameObject> objectsInCanvas = new List<GameObject>();
    List<OneZero> numbers = new List<OneZero>();

    // Use this for initialization
    void Start () {
        ResetGame();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if(isPlaying)
        {
            elapsedTime += Time.fixedDeltaTime;
            totalPlayTime += Time.fixedDeltaTime;

            if (elapsedTime > difficultyTimer)
                ChangeStateToZero(1);

        }

    }

    void ChangeStateToZero(int cellCount)
    {
        elapsedTime = 0;
        difficultyTimer -= difficultyIncrease;
        if(difficultyIncrease>0.003f)
            difficultyIncrease = difficultyIncrease - (difficultyIncrease * 0.1f);
        List<int> ones = new List<int>();
        foreach(OneZero o in numbers)
        {
            if (o.state == true)
                ones.Add(o.index);                         
        }
        Debug.Log("count of ones ->" + ones.Count);

        if (ones.Count>0 && ones.Count>=cellCount)
        {
            int target = Random.Range(0, ones.Count);

            int targetIndex = ones[target];
            Debug.Log("index for Change -> " + targetIndex);

            numbers[targetIndex].state = false;

            Text objText = numbers[targetIndex].gameObj.GetComponentInChildren<Text>();
            objText.text = "0";
        }
        else
        {
            isPlaying = false;
            foreach(OneZero o in numbers)
            {
                o.gameObj.SetActive(false);
            }

            GameObject myCanvasUI = GameObject.Find("CanvasUI");
            Text loseText = myCanvasUI.GetComponentInChildren<Text>();
            loseText.text = loseText.text + "\n But you survived for \n " + totalPlayTime;
            loseText.color = Color.white;
            Invoke("RestartGame", 2f);
        }

    }

    public void ChangeStateToOne(string objName)
    {
        int index=-1;

        foreach (OneZero o in numbers)
        {
            if (o.gameObj.name == objName)
                index = o.index;
        }

        if(index!=-1)
        {
            if(numbers[index].state != true)
            {
                numbers[index].state = true;
                Text objectText = numbers[index].gameObj.gameObject.GetComponentInChildren<Text>();
                objectText.text = "1";
                elapsedTime = 0;
            }
        }
        

    }

    void RestartGame()
    {

        SceneManager.LoadScene(0);
        Invoke("ResetGame", 1f);

        //ResetGame();
    }

    void ResetGame()
    {
        elapsedTime = 0f;
        difficultyTimer = 0.8f;
        difficultyIncrease = 0.05f;
        totalPlayTime = 0f;
        numbers.Clear();

        //GameObject myCanvasUI = GameObject.Find("CanvasUI");
        Canvas myCanvasUI = new Canvas();
        GameObject tempObject = GameObject.Find("CanvasUI");        
        myCanvasUI = tempObject.GetComponent<Canvas>(); 
        Text loseText = myCanvasUI.GetComponentInChildren<Text>();
        loseText.color = Color.clear;

        GameObject myCanvas = GameObject.FindGameObjectWithTag("Canvas");

        int canvasChildren = myCanvas.transform.childCount;

        for (int i = 0; i < canvasChildren; i++)
        {
            GameObject canvasObject = myCanvas.transform.GetChild(i).gameObject;

            OneZero obj = new OneZero(i, true, canvasObject);
            numbers.Add(obj);

            Debug.Log("index->" + obj.index + " State->" + obj.state + " Obj Name->" + obj.gameObj.name);
        }

        isPlaying = true;

    }
}
