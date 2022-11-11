using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Pendopo.UI
{
    public class PanelCookingTools : NetworkBehaviour
    {
        [SerializeField] Image imgLoading;

        private bool onProgress;

        [ClientRpc]
        public void StartLoadingClientRpc()
        {
            Debug.Log("Client RPC Called");

            if (!onProgress)
            {
                StartCoroutine(CoroutineProcessLoading());
                onProgress = true;
            }
        }

        private IEnumerator CoroutineProcessLoading()
        {
            while (imgLoading.fillAmount < 1)
            {
                imgLoading.fillAmount += Time.deltaTime;
                yield return null;
            }

            imgLoading.fillAmount = 0;
            onProgress = false;
        }
    }
}