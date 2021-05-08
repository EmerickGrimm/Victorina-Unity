using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public InputField Field;
    public Text Score, QuestionText;

    public GameObject ButtonsAnswers, TextField, GameField, WinScene, MenuScene;

    public Button[] buttons;


    private int n, fieldsComplete = 0, ButtonsComplete = 0;

    private string[] questions = { "Какое время года больше всего любил А.С.Пушкин?", "Старый домен VK? (ответ).ru", "2 x 2", "4 - 1", "Сколько месяцев в году содержат по 28 дней?", "Уличный термометр показывает 15 градусов. Сколько градусов покажут два таких термометра?", "Год рождения Путина?", "Крупнейший известный объект пояса Койпера", "Как называется это существо, полученное путем добавления человеческой спермы в куриное яйцо?", "Откуда фраза <<Нужно больше золота>>?", "Где нас просили охладить траханье?", "Какая страна имеет выход к Каспийскому морю?", "Какая пустыня расположена в южном полушарии?", "Фестиваль в пустыне Black-Rock", "Шотландский праздник, фестиваль огня, когда все переодеваются в викингов?", "Монумент в центре Трафлальгарской площади:", "Глава правительства Великобритании", "День провозглашения корейского алфавита", "В какой стране находится город Цинциннати", "Компания - создатель PlayStation"};
    private string[] answers = { "Осень", "vkontakte", "4", "3", "12", "15", "1952", "Плутон", "Гомункул", "Warcraft 3", "GTA: San Andreas", "Туркменистан", "Атакама", "Burning Man", "Апхеллио", "Колона Нельсона", "Премьер - министр", "Хангула", "США", "Sony" };

    private List<int> complete = new List<int>();

    private int minrange = 0, maxrange = 1;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            Button button = buttons[i];
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => AnsweredWithButton(button.transform.gameObject.GetComponentInChildren<Text>().text));
        }

        NewQuestion();
    }



    void NewQuestion()
    {

        if (fieldsComplete == 10 && ButtonsComplete == 10)
        {
            gameWon();
        }
        else
        {
            if (Random.Range(minrange, maxrange) == 1)
            {
                n = Random.Range(10, (questions.Length));

                if (complete != null)
                {

                    while (complete.Contains(n))
                    {
                        NewQuestion();
                    }
                }
                QuestionText.text = questions[n];
                EnableButtons();

            }
            else
            {
                ButtonsAnswers.SetActive(false);
                TextField.SetActive(true);
                n = Random.Range(0, 10);
                QuestionText.text = questions[n];
                if (complete != null)
                {
                    while (complete.Contains(n))
                    {
                        NewQuestion();
                    }
                }

            }

        }

    }

    void EnableButtons()
    {
        int correct;
        int i = 0;
        TextField.SetActive(false);
        ButtonsAnswers.SetActive(true);

        correct = Random.Range(0, 3);

        buttons[correct].transform.gameObject.GetComponentInChildren<Text>().text = answers[n];
        while (i != buttons.Length - 1)
        {

            if (buttons[i] != buttons[correct])
            {
                buttons[i].transform.gameObject.GetComponentInChildren<Text>().text = answers[Random.Range(0, answers.Length)];
            }
            i++;
        }
    }

    public void Answered()
    {
        if (answers[n].ToLower() == Field.text.ToLower())
        {
            Score.text = $"{(int.Parse(Score.text) + 1).ToString()}";
            complete.Add(n);
            Field.text = null;
            ++fieldsComplete;
            Debug.Log("Field completed: " + fieldsComplete);
            Debug.Log("Buttons completed: " + ButtonsComplete);

            if (fieldsComplete == 10 && ButtonsComplete == 10)
            {
                Debug.Log("won");
                gameWon();
            }
            if (fieldsComplete == 10 && ButtonsComplete != 10)
            {
                Debug.Log("Buttons mod");
                buttonsOnly();
            }
            else
            {
                NewQuestion();
                Debug.Log("new question");
            }

        }
        else
        {
            NewQuestion();
            Field.text = null;
        }
    }

    void AnsweredWithButton(string answer)
    {

        if (answers[n].ToLower() == answer.ToLower())
        {
            Score.text = $"{(int.Parse(Score.text) + 1).ToString()}";
            complete.Add(n);
            Field.text = null;
            ++ButtonsComplete;
            Debug.Log("Field completed: " + fieldsComplete);
            Debug.Log("Buttons completed: " + ButtonsComplete);
            if (fieldsComplete == 10 && ButtonsComplete == 10)
            {
                Debug.Log("won");

                gameWon();
            }
            if (ButtonsComplete == 10 && fieldsComplete != 10)
            {
                Debug.Log("field mod");

                fieldOnly();
            }
            else
            {
                Debug.Log("new question");
                NewQuestion();
            }

        }
        else
        {
            NewQuestion();
        }
    }

    void gameWon()
    {
        GameField.SetActive(false);
        WinScene.SetActive(true);
    }
    public void randomMode()
    {
        minrange = 0;
        maxrange = 1;
        NewQuestion();
    }
    public void fieldOnly()
    {
        minrange = 0;
        maxrange = 0;
        ButtonsComplete = 10;
        Score.transform.gameObject.SetActive(true);
        NewQuestion();
    }

    public void buttonsOnly()
    {
        minrange = 1;
        maxrange = 1;
        fieldsComplete = 10;
        Score.transform.gameObject.SetActive(true);
        NewQuestion();
    }
    public void Restart()
    {
        WinScene.SetActive(false);
        Score.transform.gameObject.SetActive(false);
        GameField.SetActive(false);
        MenuScene.SetActive(true);
        complete.Clear();
        Score.text = "0";
        fieldsComplete = 0;
        ButtonsComplete = 0;
        NewQuestion();
    }
    public void allRandom()
    {
        complete.Clear();
        Score.transform.gameObject.SetActive(true);
        Score.text = "0";
        fieldsComplete = 0;
        ButtonsComplete = 0;
        NewQuestion();
    }
}
