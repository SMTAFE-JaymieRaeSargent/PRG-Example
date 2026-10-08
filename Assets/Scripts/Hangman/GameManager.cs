namespace Hangman
{
    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;
    using System.Collections.Generic;
    using Unity.VisualScripting;

    public class GameManager : MonoBehaviour
    {
        public string wordToGuess;
        public char[] lettersInWord;
        public GameObject letterPrefab;
        public Transform spawnPoint;
        public TMP_Text[] letters;

        public Transform keyboardContainer;
        public Color correctColour;
        public Color incorrectColour;
        public Button[] keyButton;
        public List<char> guessedLetters = new List<char>();

        void LettersInWord()
        {
            lettersInWord = wordToGuess.ToCharArray();
        }
        void SpawnUIPerLetter()
        {
            letters = new TMP_Text[wordToGuess.Length];
            //letters = new TMP_Text[lettersInWord.Length];
            for (int currentLetter = 0; currentLetter < wordToGuess.Length; currentLetter++)
            {
                GameObject spawnedLetterPrefab = Instantiate(letterPrefab, spawnPoint);
                letters[currentLetter] = spawnedLetterPrefab.GetComponentInChildren<TMP_Text>();
            }
        }
        void GetOnscreenKeys()
        {
            keyButton = keyboardContainer.GetComponentsInChildren<Button>();
        }
        private void Start()
        {
            LettersInWord();
            SpawnUIPerLetter();
            GetOnscreenKeys();
        }

        public void PressedLetter(string letter)
        {
            Guess(letter[0]);
        }   
        public void Guess(char letter)
        {
            letter = char.ToUpper(letter);

            if (guessedLetters.Contains(letter))
            {
                return;
            }
            guessedLetters.Add(letter);

            bool isCorrect = false;

            for (int i = 0; i < lettersInWord.Length; i++)
            {
                if (char.ToUpper(lettersInWord[i]) == letter)
                {
                    letters[i].text = letter.ToString();
                    isCorrect = true;
                }
            }
            //find key and then change according to colour
            Button button = FindKey(letter);
            if (button != null)
            {
                if (isCorrect)
                {
                    button.image.color = correctColour;
                }
                else
                {
                    button.image.color = incorrectColour;
                }
                button.interactable = false;
            }
        }
        Button FindKey(char letter)
        {
            foreach (Button button in keyButton)
            {
                if (button.name == letter.ToString())
                {
                    return button;
                }
            }
            return null;
        }
        private void Update()
        {
            foreach (char c in Input.inputString)
            {
                Guess(c);
            }
        }
    }
}
