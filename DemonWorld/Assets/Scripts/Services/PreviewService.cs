using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demonworld.Services
{
    public class PreviewService : MonoBehaviour
    {
        [SerializeField] LayerMask previewLayer;
        [SerializeField] Material canPlaceMaterial;
        [SerializeField] Material cannotPlaceMaterial;

        private Mesh previewMesh;
        private LevelService levelService;

        public void SetPreviewMesh(Mesh mesh)
        {
            this.previewMesh = mesh;
        }

        private void Update()
        {
            ShowPreview();   
        }

        public void Init(LevelService levelService)
        {
            this.levelService = levelService;
        }

        public void ShowPreview()
        {
            if (previewMesh == null) return;

            if (Input.GetMouseButton(1))
            {
               
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayCastHit;
                if(Physics.Raycast(ray,out rayCastHit,Mathf.Infinity,previewLayer))
                {
                    Debug.Log(rayCastHit.collider.gameObject.name);
                    if(levelService.IsInBounds(rayCastHit.point))
                    {
                        Vector3 previewPosition = levelService.GetCellPosition(rayCastHit.point);
                        if(levelService.GetCellData(previewPosition).isEmpty())
                        {
                            DrawPreviewMesh(previewPosition, true);
                            if (Input.GetMouseButtonDown(0))
                            {
                                levelService.AddCellData(previewPosition);
                            }
                        }
                        else
                        {
                            DrawPreviewMesh(previewPosition, false);
                        }

                    }

                }
            }
        }

        public void DrawPreviewMesh(Vector3 position,bool canPlace)
        {
            
            for (int submeshIndex = 0; submeshIndex < previewMesh.subMeshCount; submeshIndex++)
            {
                if(canPlace)
                  Graphics.DrawMesh(previewMesh, position,Quaternion.identity, canPlaceMaterial, 0, null, submeshIndex);
                else
                    Graphics.DrawMesh(previewMesh, position, Quaternion.identity, cannotPlaceMaterial, 0, null, submeshIndex);
            }
        }


    }
}


