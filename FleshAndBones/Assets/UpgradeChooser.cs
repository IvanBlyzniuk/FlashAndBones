using UnityEngine;

public class UpgradeChooser : MonoBehaviour
{
    [SerializeField] private UpgradeChoiceItem item1;
    [SerializeField] private UpgradeChoiceItem item2;
    [SerializeField] private UpgradeChoiceItem item3;

    private void Start()
    {
        //Disappear();
    }

    public void Appear()
    {
        Show(item1.gameObject);
        Show(item2.gameObject);
        Show(item3.gameObject);
    }

    public void Disappear()
    {
        Hide(item1.gameObject);
        Hide(item2.gameObject);
        Hide(item3.gameObject);
    }

    private void Show(GameObject go) => go.SetActive(true);

    private void Hide(GameObject go) => go.SetActive(false);
}
