using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyScript : MonoBehaviour
{
    public PlayerScript playerScript;
    public GameObject player;
    public GameControllerScript gc;
    public GameObject pedestalPrefab;
    public GameObject rubyPrefab;
    private GameObject ruby;
    private GameObject pedestal;
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 1f;
    public Vector2 spawnAreaMin = new Vector2(-35, -35); // Defina valores padr�o se n�o for configurar via Inspector
    public Vector2 spawnAreaMax = new Vector2(35, 35);   // Defina valores padr�o se n�o for configurar via Inspector

    // Start is called before the first frame update
    void Start()
    {
        SpawnPedestalAndRuby();
    }

    void SpawnPedestalAndRuby()
    {
        // Define a posi��o de spawn do pedestal dentro da �rea de spawn
        Vector2 spawnPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        // Instancia o pedestal na posi��o de spawn
        pedestal = Instantiate(pedestalPrefab, spawnPosition, Quaternion.identity);

        // Instancia o rubi como filho do pedestal e posiciona acima do pedestal
        ruby = Instantiate(rubyPrefab, pedestal.transform);
        ruby.transform.localPosition = new Vector3(0, 1, 0);

        // Adiciona o script de flutua��o ao rubi e configura os par�metros
        RubyFloat rubyFloat = ruby.AddComponent<RubyFloat>();
        rubyFloat.amplitude = floatAmplitude;
        rubyFloat.frequency = floatFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < 2 && ruby.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerScript.hasRuby = true;
                gc.score += 5000;
                // Adicione aqui o c�digo para tocar um som, se necess�rio
                ruby.SetActive(false);
            }
        }
    }
}

public class RubyFloat : MonoBehaviour
{
    private Vector3 startPos;
    public float amplitude = 0.2f; // Amplitude da flutua��o
    public float frequency = 1f; // Frequ�ncia da flutua��o

    void Start()
    {
        startPos = transform.localPosition; // Posi��o inicial do rubi
    }

    void Update()
    {
        // Calcular nova posi��o do rubi
        float newY = startPos.y + amplitude * Mathf.Sin(Time.time * frequency);
        transform.localPosition = new Vector3(startPos.x, newY, startPos.z);
    }
}
