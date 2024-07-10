using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AISceneChanger : MonoBehaviour
{
    private Animator animator;
    public string nextScene; //���� ��
    private AsyncOperation op; //�ε��� ���� ������ ��

    [SerializeField]
    private bool IsAIloading = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(LoadAI());
    }

    IEnumerator LoadAI()
    {
        yield return null;
        op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;    //�ε��� �� �Ǵ��� �ϴ� 0.9���� ���� 1�̸� ����
        while (!op.isDone)
        {
            yield return null;

            //������� �ε��� ������ ai�� �غ� �ƴٸ�
            if (op.progress >= 0.9f)
            {
                if (IsAIloading)
                {
                    animator.SetTrigger("LoadingDone"); //�ε� �����Ű�� �����غ�
                    yield break;
                }
            }
        }
    }

    public void LoadAIScene()
    {
        op.allowSceneActivation = true;
    }

}
