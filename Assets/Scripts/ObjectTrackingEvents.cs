using UnityEngine;
using UnityEngine.UI;
using Vuforia;
//Attach to the image tracker
public class ObjectTrackingEvents : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour trackableBehaviour;
    public string objectName;

    public Text statusText;

    void Start()
    {
        trackableBehaviour = GetComponent<TrackableBehaviour>();
        if (trackableBehaviour)
            trackableBehaviour.RegisterTrackableEventHandler(this);
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            OnTrackingFound();
        else
            onTrackingLost();
    }
    private void OnTrackingFound()
    {
        // update status text 
        statusText.text = getStatusFound();

        if (transform.childCount > 0)
            SetChildrenActive(true);
    }
    private void onTrackingLost()
    {
        // update status text
        statusText.text = getStatusEmpty();
        if (transform.childCount > 0)
            SetChildrenActive(false);
    }
    private void SetChildrenActive(bool activeState)
    {
        for (int i = 0; i <= transform.childCount; i++)
            transform.GetChild(i++).gameObject.SetActive(activeState);
    }

    private string getStatusFound()
    {
        return "Vous avez trouvé : " + objectName + " !";
    }

    private string getStatusEmpty()
    {
        return "à la recherche d'objets...";
    }
}