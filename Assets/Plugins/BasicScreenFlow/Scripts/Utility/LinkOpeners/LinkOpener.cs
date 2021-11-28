using UnityEngine;

public class LinkOpener : MonoBehaviour, ILinkOpener
{
    [SerializeField] private string message = "Isso irá te redirecionar para um link externo. Deseja continuar?";

    private string _url;

    public void OpenURL(string url)
    {
        _url = url;

        var popup = PopupManager.GetInstance().ShowPopup<ConfirmationPopup>();
        popup.SetPositiveCase(OpenLink);
        popup.SetText(message);
    }

    private void OpenLink()
    {
        Application.OpenURL(_url);
    }
}
