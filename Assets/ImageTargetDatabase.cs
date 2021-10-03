using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTargetDatabase : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public List<ImageTarget> targets;

    private void Awake()
    {
        if (trackedImageManager == null)
        {
            trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
            if (trackedImageManager == null)
            {
                Debug.LogError("No AR tracked image manager");
                return;
            }

            foreach (ImageTarget target in targets)
            {
                target.instantiatedObject = Instantiate(target.prefab);
                target.instantiatedObject.name = target.name;
            }
        }
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageTargetChanged;
    }
    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageTargetChanged;
    }

    private void ImageTargetChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage image in eventArgs.added)
        {
            UpdateImagePosition(image);
        }
        foreach (ARTrackedImage image in eventArgs.updated)
        {
            UpdateImagePosition(image);
        }
        foreach (ARTrackedImage image in eventArgs.removed)
        {
            RemoveImageTargetByName(image.referenceImage.name);
        }
    }

    private void UpdateImagePosition(ARTrackedImage image)
    {
        foreach (ImageTarget target in targets)
        {
            if (target.name == image.referenceImage.name)
            {
                target.instantiatedObject.transform.position = image.transform.position;

                if (target.instantiatedObject.activeSelf == false)
                {
                    target.instantiatedObject.SetActive(true);
                    target.OnEnabled.Invoke();
                }
            }
            else
            {
                target.instantiatedObject.SetActive(false);
                target.OnRemoved.Invoke();
            }
        }
    }
    
    private void RemoveImageTargetByName(string image_name)
    {
        foreach (ImageTarget target in targets)
        {
            if (target.name == image_name)
            {
                target.instantiatedObject.SetActive(false);
                target.OnRemoved.Invoke();
                return;
            }
        }

        Debug.LogWarning("not found an image with that name");
    }
}

[System.Serializable]
public class ImageTarget
{
    public string name;
    public XRReferenceImage image_target;
    public GameObject prefab;
    internal GameObject instantiatedObject;
    public UnityEvent OnEnabled;
    public UnityEvent OnRemoved;
}