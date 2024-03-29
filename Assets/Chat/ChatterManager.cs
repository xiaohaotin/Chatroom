﻿using UnityEngine;
using UnityEngine.UI;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;


public class ChatterManager : ChatManagerBehavior
{
    public Transform chatContent;
    public GameObject chatMessage;

    public void WriteMessage(InputField sender)
    {
        if (!string.IsNullOrEmpty(sender.text) && sender.text.Trim().Length > 0)
        {
            sender.text = sender.text.Replace("\r", string.Empty).Replace("\n", string.Empty);
            networkObject.SendRpc(RPC_SEND_MESSAGE, Receivers.All, "Brent", sender.text.Trim());
            sender.text = string.Empty;
            sender.ActivateInputField();
        }
    }

    public override void SendMessage(RpcArgs args)
    {
        string username = args.GetNext<string>();
        string message = args.GetNext<string>();

        if(string.IsNullOrEmpty(username)|| string.IsNullOrEmpty(message))
        {
            // The message or username was empty so do not display this message to anyone
            return;
        }

        GameObject newMessage = Instantiate(chatMessage,chatContent);
        Text content = newMessage.GetComponent<Text>();

        content.text = string.Format(content.text, username, message);
    }

    

    

    
}
