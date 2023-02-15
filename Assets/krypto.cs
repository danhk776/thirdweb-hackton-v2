using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using UnityEngine.UI;

public class krypto : MonoBehaviour
{
    private ThirdwebSDK sdk;
    private int count;
    public Text resultText;
    
    void Start()
    {
        sdk = new ThirdwebSDK("goerli");
    }

    public async void OnMouseUpAsButton()
    {
        Debug.Log("Claim button clicked");
        resultText.text = "Claiming kryptonite...";

        // claim
        var contract = sdk.GetContract("0xeb1A0c9A5f35215A22D7a3FDB61be3513e234411"); // Edition Drop
        var canClaim = await contract.ERC1155.claimConditions.CanClaim("3", 1);
        if (canClaim)
        {
            try
            {
                var result = await contract.ERC1155.Claim("3", 1);
                var newSupply = await contract.ERC1155.TotalSupply("3");
                resultText.text = "Claim successful!";
            }
            catch (System.Exception e)
            {
                resultText.text = "Claim Failed: " + e.Message;
            }
        }
        else
        {
            resultText.text = "Can't claim";
        }

        // sig mint additional supply
        // var contract = sdk.GetContract("0xdb9AAb1cB8336CCd50aF8aFd7d75769CD19E5FEc"); // Edition
        // var payload = new ERC1155MintAdditionalPayload("0xE79ee09bD47F4F5381dbbACaCff2040f2FbC5803", "1");
        // payload.quantity = 3;
        // var p = await contract.ERC1155.signature.GenerateFromTokenId(payload);
        // var result = await contract.ERC1155.signature.Mint(p);
        // resultText.text = "sigminted tokenId: " + result.id;
    }

}

