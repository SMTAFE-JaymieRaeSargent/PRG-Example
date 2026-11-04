namespace Hangman
{
    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;
    using System.Collections.Generic;

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
        public GameSaveData saveAndLoadGame;
        public LoadWordList wordListLoader;

        private void LoadOrCreateWord()
        {
            saveAndLoadGame.Load();
            // no saved word means this is a new game, so pick a word and save it
            if (wordToGuess == "" || wordToGuess == null)
            {
                wordToGuess = wordListLoader.ChooseNewWord();
                guessedLetters.Clear();
                saveAndLoadGame.Save();
            }
        }

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
        void RestoreGuessedLetters()
        {
            // copy the loaded letters, clear the list, then guess each one again
            // so the word and keyboard colours show the saved progress
            List<char> lettersToRestore = new List<char>(guessedLetters);
            guessedLetters.Clear();
            foreach (char letter in lettersToRestore)
            {
                Guess(letter);
            }
        }
        private void Start()
        {
            LoadOrCreateWord();
            LettersInWord();
            SpawnUIPerLetter();
            GetOnscreenKeys();
            RestoreGuessedLetters();
        }

        public void PressedLetter(string letter)
        {
            Guess(letter[0]);
        }
        public void Guess(char letter)
        {
            // ignore keys like space, enter and backspace
            if (!char.IsLetter(letter))
            {
                return;
            }
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

            // save after every guess so Continue picks up where we left off
            saveAndLoadGame.Save();
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
