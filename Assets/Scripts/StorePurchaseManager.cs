using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Bazaar.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bazaar.Poolakey;
using Bazaar.Poolakey.Data;

public class StorePurchaseManager : MonoBehaviour
{
    private Payment payment;

    [SerializeField] private string thirdBallProductId = "third_ball";
    [SerializeField] private string rsaKey = "MIHNMA0GCSqGSIb3DQEBAQUAA4G7ADCBtwKBrwDWaOoz/OJG04qz2Vl78cDTIvIUs4oczC5htnwTTcKW1WLMEP56kq9ocSN6xpeX78mPWPCVGOVTsy2M8XkM/AGG834XQnCdzo6AZQ9yps38UloorByAjvKNT6qyXm6fvrwUp80Tdxu43d1FDUj5rgG7kJSjU4hHK6SC+rNOc0uP8JtQnLIJj8gcvsfQ7v8JLOQj1zz+FwYGgspoO8+yCxeDuWHbgilZfwZXLQQCns0CAwEAAQ==";

    [SerializeField] private Button buyButton;
    [SerializeField] private Text buyButtonText;

    private bool isBallSelected = false;

    private async void Start()
    {
        SecurityCheck securityCheck = SecurityCheck.Enable(rsaKey);
        PaymentConfiguration config = new PaymentConfiguration(securityCheck);
        payment = new Payment(config);

        var connectResult = await payment.Connect();
        Debug.Log($"Poolakey Connect: {connectResult.status}");

        UpdateButtonState();
    }

    public async void OnBuyButtonClicked()
    {
        if (PlayerPrefs.GetInt("ThirdBallUnlocked", 0) == 1)
        {
            // Already purchased — just toggle selection
            isBallSelected = !isBallSelected;
            PlayerPrefs.SetInt("SelectedBall", isBallSelected ? 2 : 0);
            PlayerPrefs.Save();
            
            Debug.Log($"Ball selection toggled. Selected: {isBallSelected}");
            return;
        }

        // Not purchased — start purchase
        var purchaseResult = await payment.Purchase(thirdBallProductId);
        if (purchaseResult.status != Status.Success)
        {
            Debug.LogError("❌ Purchase failed: " + purchaseResult.message);
            return;
        }

        var token = purchaseResult.data.purchaseToken;
        var consumeResult = await payment.Consume(token);
        if (consumeResult.status != Status.Success)
        {
            Debug.LogError("⚠️ Consume failed: " + consumeResult.message);
            return;
        }

        PlayerPrefs.SetInt("ThirdBallUnlocked", 1);
        PlayerPrefs.SetInt("SelectedBall", 2);
        PlayerPrefs.Save();
        isBallSelected = true;

        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        bool unlocked = PlayerPrefs.GetInt("ThirdBallUnlocked", 0) == 1;
        int selectedBall = PlayerPrefs.GetInt("SelectedBall", 0);
        isBallSelected = (selectedBall == 2);

        buyButtonText.text = unlocked ? "Bought" : "Buy";
    }

    private void OnApplicationQuit()
    {
        payment?.Disconnect();
    }
}

