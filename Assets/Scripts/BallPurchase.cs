using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BazaarInAppBilling;
using UnityEngine.UI;

public class BallPurchase : MonoBehaviour
{

    
    private int selectedProductIndex;

    void Start()
    {
        StoreHandler.instance.InitializeBillingService(OnServiceInitializationFailed, OnServiceInitializedSuccessfully);

    }

    

    public void BuyProduct(int index)
    {
        selectedProductIndex = index;
        StoreHandler.instance.Purchase(index, OnPurchaseFailed, OnPurchasedSuccessfully);
    }

    public void CheckInventory(int index)
    {
        selectedProductIndex = index;
        StoreHandler.instance.CheckInventory(index, OnInventoryCheckFailed, OnInventoryHadProduct);
    }

    public void SetValidatePurchasesState(bool state)
    {
        StoreHandler.instance.validatePurchases = state;
    }

    private void OnServiceInitializedSuccessfully()
    {
        StoreHandler.instance.LoadProductPrices(OnLoadingPricesFailed, OnPricesLoadedSuccessfully);
    }

    private void OnServiceInitializationFailed(int errorCode, string message)
    {
                // txtResult.text = "Initialization Failed. ErrorCode: " + errorCode + ", " + message + "\n" + txtResult.text;
    }
    
    private void OnPricesLoadedSuccessfully()
    {
        for(int i = 0; i < StoreHandler.instance.products.Length; i++)
        {
            string price = StoreHandler.instance.products[i].price;
                    }
    }

    private void OnLoadingPricesFailed(int errorCode, string message)
    {
        // txtResult.text = "Loading Prices Failed. ErrorCode: " + errorCode + ", " + message + "\n" + txtResult.text;
    }

    private void OnPurchasedSuccessfully(Purchase purchase, int productIndex)
    {
       
        switch (productIndex)
        {
            case 0: 
            UnlockThirdBall();
                break;
            
            default:
                throw new UnassignedReferenceException("You forgot to give user the product after purchase. product: " + purchase.productId + ", index: " + productIndex);
        }
    }

    private void OnPurchaseFailed(int errorCode, string message)
    {

        switch (errorCode)
        {
            case StoreHandler.SERVICE_IS_NOW_READY_RETRY_OPERATION:

                BuyProduct(selectedProductIndex);

                return;
            case StoreHandler.ERROR_WRONG_SETTINGS:

                break;
            case StoreHandler.ERROR_BAZAAR_NOT_INSTALLED:

                break;
            case StoreHandler.ERROR_SERVICE_NOT_INITIALIZED:

                break;
            case StoreHandler.ERROR_INTERNAL:

                break;
            case StoreHandler.ERROR_OPERATION_CANCELLED:

                break;
            case StoreHandler.ERROR_CONSUME_PURCHASE:

                break;
            case StoreHandler.ERROR_NOT_LOGGED_IN:

                break;
            case StoreHandler.ERROR_HAS_NOT_PRODUCT_IN_INVENTORY:

                break;
            case StoreHandler.ERROR_CONNECTING_VALIDATE_API:

                break;
            case StoreHandler.ERROR_PURCHASE_IS_REFUNDED:

                break;
            case StoreHandler.ERROR_NOT_SUPPORTED_IN_EDITOR:

                break;
            case StoreHandler.ERROR_WRONG_PRODUCT_INDEX:

                break;
            case StoreHandler.ERROR_WRONG_PRODUCT_ID:

                break;
        }

    }

    private void OnInventoryHadProduct(Purchase purchase, int productIndex)
    {
       
    }

    private void OnInventoryCheckFailed(int errorCode, string message)
    {
        switch (errorCode)
        {
            case StoreHandler.SERVICE_IS_NOW_READY_RETRY_OPERATION:

                CheckInventory(selectedProductIndex);

                return;
            case StoreHandler.ERROR_WRONG_SETTINGS:

                break;
            case StoreHandler.ERROR_BAZAAR_NOT_INSTALLED:

                break;
            case StoreHandler.ERROR_SERVICE_NOT_INITIALIZED:

                break;
            case StoreHandler.ERROR_INTERNAL:

                break;
            case StoreHandler.ERROR_OPERATION_CANCELLED:

                break;
            case StoreHandler.ERROR_CONSUME_PURCHASE:

                break;
            case StoreHandler.ERROR_NOT_LOGGED_IN:

                break;
            case StoreHandler.ERROR_HAS_NOT_PRODUCT_IN_INVENTORY:

                break;
            case StoreHandler.ERROR_CONNECTING_VALIDATE_API:

                break;
            case StoreHandler.ERROR_PURCHASE_IS_REFUNDED:

                break;
            case StoreHandler.ERROR_NOT_SUPPORTED_IN_EDITOR:

                break;
            case StoreHandler.ERROR_WRONG_PRODUCT_INDEX:

                break;
            case StoreHandler.ERROR_WRONG_PRODUCT_ID:

                break;
        }
        
           }
    
   
private void UnlockThirdBall()
{
    PlayerPrefs.SetInt("ThirdBallUnlocked", 1);
    PlayerPrefs.Save();
    
    // Update store UI if we're in the store scene
    StoreManager storeManager = FindObjectOfType<StoreManager>();
    if (storeManager != null)
    {
        storeManager.UpdateButtonStates();
    }
}
    
   

    private IEnumerator AnimateCountText(Text text, int preValue, int nextValue)
    {
        bool increase = true;
        if (nextValue < preValue)
        {
            increase = false;
        }

        float value = nextValue - preValue;

        float t = (Mathf.Abs(value) / 5) * 0.4f;
        if (t > 2.0f) t = 2.0f;

        if (value != 0)
        {
            float step = value / (t / 0.06f);
            float pre = preValue;

            value = Mathf.Abs(value);

            while (value > 0)
            {
                value -= Mathf.Abs(step);
                pre += (step);
                if ((increase && pre > nextValue) || (!increase && pre < nextValue))
                {
                    pre = nextValue;
                }

                text.text = (int)pre + "";
                yield return new WaitForSecondsRealtime(0.02f);
            }

            text.text = nextValue + "";
        }
        else
        {
            text.text = nextValue + "";
        }
    }
}
