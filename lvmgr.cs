using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class lvmgr : MonoBehaviour {

    public int killcivil;
    public int maxkill;

    void Awake()
    {


    }

    void Update()
    {


        if(killcivil==maxkill)
        {

            if (maxkill == 2)
            {
                StartCoroutine(Stageclear());

            }else if (maxkill == 5)
            {
                StartCoroutine(Stageclear2());
            }else if(maxkill == 7)
            {

                StartCoroutine(Stageclear3());
            }

        }// if 문 end

        
    }// update end

    IEnumerator Stageclear()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("capstone2");
    }

    IEnumerator Stageclear2()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("capstone3");
    }

    IEnumerator Stageclear3()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("MainScene");
    }
}
