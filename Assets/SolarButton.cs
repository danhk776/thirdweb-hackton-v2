using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using UnityEngine.UI;

public class SolarButton : MonoBehaviour
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
        resultText.text = "Claiming...";

        // claim
        var contract = sdk.GetContract("0xc2B78F1872093Ac04C8A158Ee91aE780EfA04195"); // Edition Drop
        var canClaim = await contract.ERC1155.claimConditions.CanClaim("0", 1);
        if (canClaim)
        {
            try
            {
                var result = await contract.ERC1155.Claim("0", 1);
                var newSupply = await contract.ERC1155.TotalSupply("0");
                resultText.text = "Claim successful! New supply: " + newSupply;
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
