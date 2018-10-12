using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{

    #region Private fields

    private bool isSocketReady = false;
    private TcpClient socket;
    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;
    private string host;
    private int port;

    #endregion

    #region Serializable fields

    [SerializeField]
    Text ip_txt;

    [SerializeField]
    Text port_txt;

    [SerializeField]
    GameObject ConnectUI;

    #endregion

    #region Public fields

   

    #endregion

    public void ConnectToServer()
    {

        if (isSocketReady)
            return;       

        try
        {

            GetIPPort();

            if (host == null || port == 0)
            {
                Debug.Log(" Error! Set correct ip and port!");
                return;
            }

            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            isSocketReady = true;
            Broadcast("Client has been connected to " + host);
            EventController.InvokeEvent(EventController.Events.joinServer);

        }

        catch (Exception ex)
        {
            Debug.Log(ex.Message);

        }

    }



    void Update()
    {

        if(isSocketReady)
        {

            if(stream.DataAvailable)
            {
                string data = reader.ReadLine();

                if (data != null)
                    OnIncomingData(data);

            }
        }




      
            
       

       
        
        

    }



    private void GetIPPort()
    {
        host = ip_txt.text;


        if (!Int32.TryParse(port_txt.text, out port))
        {
            Debug.Log("Error! Can't convert ip");
            return;
        }
    }

    private void Broadcast(string message)
    {
        writer.WriteLine(message);
        writer.Flush();

    }



    private void OnIncomingData(string data)
    {
        Debug.Log("CLIENT CHAT: Server is send the following " + data);

        if(data == "turnLight")
        {
            EventController.InvokeEvent(EventController.Events.turnOnTheLigh);
        }

        if (data == "explosion")
        {
            EventController.InvokeEvent(EventController.Events.makeExplosion);
        }

    }



    public void TurnLightOn()
    {
        Broadcast("turnLight");


    }


    public void MakeExplosion()
    {
        Broadcast("explosion");
    }


}
