using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bazaar.Poolakey;
using Bazaar.Data;
using System.Threading.Tasks;
using Bazaar.Poolakey.Data;

public class PurchaseManager : MonoBehaviour
{
    [SerializeField] private string appkey = "MIHNMA0GCSqGSIb3DQEBAQUAA4G7ADCBtwKBrwDWaOoz/OJG04qz2Vl78cDTIvIUs4oczC5htnwTTcKW1WLMEP56kq9ocSN6xpeX78mPWPCVGOVTsy2M8XkM/AGG834XQnCdzo6AZQ9yps38UloorByAjvKNT6qyXm6fvrwUp80Tdxu43d1FDUj5rgG7kJSjU4hHK6SC+rNOc0uP8JtQnLIJj8gcvsfQ7v8JLOQj1zz+FwYGgspoO8+yCxeDuWHbgilZfwZXLQQCns0CAwEAAQ==";
    private Payment _payment;

    public async Task<bool> init()
    {
        var securityCheck = SecurityCheck.Enable(appkey);
        var paymentConfiguration = new PaymentConfiguration(securityCheck);
        _payment = new Payment(paymentConfiguration);
        var result = await _payment.Connect();
        return result.status == Status.Success; 
    }

    public async Task<Result<PurchaseInfo>> Purchase(string productId)
    {
    
        var result = await _payment.Purchase(productId);
        return result;
    }

    public async Task<Result<bool>> Consume(string purchaseToken)
    {
        var result = await _payment.Consume(purchaseToken);
        return result;
    }
    private void OnApplicationQuit()
    {
        _payment.Disconnect();
    }

}
