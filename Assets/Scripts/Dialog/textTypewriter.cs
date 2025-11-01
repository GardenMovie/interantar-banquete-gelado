using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class legendaAnimada : MonoBehaviour
{
    [Header("Configurações de Velocidade")]
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float timeBetweenPhrases = 2f;
    [SerializeField] private float delayAfterPunctuation = 0.2f;

    [Header("Componentes UI")]
    [SerializeField] private Text textComponent;

    [Header("Frases (Insira no Inspector)")]
    [SerializeField] private string[] phrases;

    [Header("Configurações de Áudio")]
    [SerializeField] private AudioClip typingSound;
    [SerializeField] private AudioClip eraseSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float typingSoundVolume = 0.5f;
    [SerializeField] private float eraseSoundVolume = 0.3f;

    [Header("Transição de Cena (Opcional)")]
    [SerializeField] private string nextSceneName;
    [SerializeField] private float sceneTransitionDelay = 1f;

    private int currentPhraseIndex = 0;
    private bool isTyping = false;
    public bool waitingForInput = true;

    void Start()
    {
        textComponent = gameObject.GetComponent<Text>();
        
        if (phrases == null || phrases.Length == 0)
        {
            Debug.LogError("Nenhuma frase definida no Inspector!");
            return;
        }

        for (int i = 0; i < GameManager.Instance.globeTargetIndex+1; i++)
        {
            SkipToNextPhrase();
        }

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }

        StartTypingSequence();
    }

    public void StartTypingSequence()
    {
        currentPhraseIndex = 0;
        StopAllCoroutines();
        StartCoroutine(TypePhrases());
    }

    public void clickSkip()
    {
        charIndex = phrases[currentPhraseIndex].Length;
        //currentPhraseIndex++;
    }

    public void OnBtnContinue()
    {
        waitingForInput = false;
    }

    IEnumerator TypePhrases()
    {
        /*         for (int i = -1; i < gameManager.Instance.globeTargetIndex; i++)
                {
                    Debug.Log("Skipped")
                    SkipToNextPhrase();
                } */
        currentPhraseIndex = GameManager.Instance.globeTargetIndex+1;
        while (currentPhraseIndex < phrases.Length)
        {
            isTyping = true;

            // Digitar a frase atual
            yield return StartCoroutine(TypeText(phrases[currentPhraseIndex]));

            isTyping = false;

            // Esperar antes de apagar (exceto na última frase)
/*             if (currentPhraseIndex < phrases.Length - 1)
            {
                yield return new WaitForSeconds(timeBetweenPhrases);

                // Apagar o texto instantaneamente
                EraseTextImmediately();
            } */

            yield return new WaitUntil(() => waitingForInput == false);
            GameManager.Instance.incrementIndex();
            currentPhraseIndex++;
            waitingForInput = true;
        }

        // Transição para próxima cena se configurado
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            yield return new WaitForSeconds(sceneTransitionDelay);
            SceneManager.LoadScene(nextSceneName);
        }
    }

    public int charIndex = 0;
    IEnumerator TypeText(string text)
    {
        textComponent.text = "";

        for (charIndex = 0; charIndex < text.Length; charIndex++)
        {
            textComponent.text += text[charIndex];

            if (typingSound != null && !char.IsWhiteSpace(text[charIndex]))
            {
                audioSource.PlayOneShot(typingSound, typingSoundVolume);
            }

            if (char.IsPunctuation(text[charIndex]))
            {
                yield return new WaitForSeconds(delayAfterPunctuation);
            }
            else
            {
                yield return new WaitForSeconds(typingSpeed);
            }
        }
    }

    // Novo método para apagar o texto instantaneamente
    void EraseTextImmediately()
    {
        if (eraseSound != null)
        {
            audioSource.PlayOneShot(eraseSound, eraseSoundVolume);
        }
        textComponent.text = "";
    }

    public void SkipToNextPhrase()
    {
        if (!isTyping) return;

        GameManager.Instance.incrementIndex();
        StopAllCoroutines();

        if (currentPhraseIndex < phrases.Length - 1)
        {
            textComponent.text = "";
            currentPhraseIndex++;
            StartCoroutine(TypeText(phrases[currentPhraseIndex]));
        }

        else
        {
            textComponent.text = phrases[currentPhraseIndex];

            if (!string.IsNullOrEmpty(nextSceneName))
            {
                StartCoroutine(LoadNextSceneAfterDelay());
            }
        }
    }

    private IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(sceneTransitionDelay);
        SceneManager.LoadScene(nextSceneName);
    }
}
