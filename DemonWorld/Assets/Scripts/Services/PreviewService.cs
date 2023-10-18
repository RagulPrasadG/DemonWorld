using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demonworld.Services
{
    public class PreviewService : MonoBehaviour
    {
        [SerializeField] Material canPlaceMaterial;
        [SerializeField] Material cannotPlaceMaterial;
        private Mesh previewMesh;

        public void SetPreviewMesh(Mesh mesh)
        {
            this.previewMesh = mesh;
        }

        private void Update()
        {
            ShowPreview();   
        }

        public void ShowPreview()
        {
            if (Input.GetMouseButton(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayCastHit;
                if(Physics.Raycast(ray,out rayCastHit,Mathf.Infinity))
                {
                    if(GameService.Instance.GetLevelService().IsInBounds(rayCastHit.point))
                    {
                        Vector3 previewPosition = GameService.Instance.GetLevelService().GetCellPosition(rayCastHit.point);
                        if(GameService.Instance.GetLevelService().GetCellData(previewPosition).isEmpty())
                        {
                            DrawPreviewMesh(previewPosition, true);
                            if (Input.GetMouseButtonDown(0))
                            {
                                GameService.Instance.GetLevelService().AddCellData(previewPosition);
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


