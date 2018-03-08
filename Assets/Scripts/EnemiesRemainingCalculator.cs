using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemiesRemainingCalculator : MonoBehaviour {

    private Text _remainingTextBox;

    private Color _textColor;

    public Color TextColor
    {
        get { return _textColor; }
        set { _textColor = value; _remainingTextBox.color = value; }
    }


    [SerializeField]
    private int _enemiesRemaining;
  

    public int EnemiesRemaining
    {
        get
        {
            return _enemiesRemaining;
        }
        set
        {
          _enemiesRemaining = value;
          _remainingTextBox.text = string.Format("Enemies Remaining: {0}", _enemiesRemaining);
        }
    }
  
    // Use this for initialization
    void Start () {
        _remainingTextBox = GetComponent<Text>();
        _textColor = _remainingTextBox.color;
        if(SceneManager.GetActiveScene().name == "Easy"){
            EnemiesRemaining = 22;
        } else if (SceneManager.GetActiveScene().name == "Intermediate"){
            EnemiesRemaining = 31;
        } else if (SceneManager.GetActiveScene().name == "Hard"){
            EnemiesRemaining = 40;
        } else if (SceneManager.GetActiveScene().name == "Impossible"){
            EnemiesRemaining = 49;
        }
	}

    private void FixedUpdate()
    {
        if (EnemiesRemaining == 0)
        {
            StartCoroutine(DarkenToGreen());
        }
    }

    public IEnumerator DarkenToRed()
    {
        for (float f = TextColor.g; f > 0 ; f -= 0.1f)
        {
            TextColor = new Color(1, f, f);
            yield return new WaitForSeconds(0.005f);
        }
    }
    public IEnumerator LightenToWhiteFromRed()
    {
        for (float f = TextColor.g; f < 1; f += 0.1f)
        {
            TextColor = new Color(1, f, f);
            yield return new WaitForSeconds(0.005f);
        }
    }

    public IEnumerator DarkenToGreen()
    {
        for (float f = TextColor.r; f > 0; f -= 0.1f)
        {
            TextColor = new Color(f, 1, f);
            yield return new WaitForSeconds(0.005f);
        }
    }

    public IEnumerator LightenToWhiteFromGreen()
    {
        for (float f = TextColor.r; f > 0; f -= 0.1f)
        {
            TextColor = new Color(f, 1, f);
            yield return new WaitForSeconds(0.005f);
        }
    }
}
