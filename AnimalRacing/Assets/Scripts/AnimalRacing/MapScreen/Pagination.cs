using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pagination : MonoBehaviour {
    [SerializeField]
    Transform[] pages;
    [SerializeField]
    Transform selection;

    int currentPage;
    private void Update()
    {
        if (Vector3.Distance(selection.position, GetCurrentTransform().position) > 0.1f)
        {
            selection.position = Vector3.Lerp(selection.position, GetCurrentTransform().position, Time.deltaTime * 1000);
        }
    }

    public void Set(int page)
    {
        currentPage = page;
    }

    Transform GetCurrentTransform()
    {
        return pages[Mathf.Clamp(currentPage, 0, pages.Length)];
    }
}
