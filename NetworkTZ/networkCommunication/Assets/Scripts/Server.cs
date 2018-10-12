using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class Server : MonoBehaviour
{
    private  List<ServerClient> clients;
    private List<ServerClient> disconnectedClients;

    public int port = 2018;
    private TcpListener listener;
    private bool isServerStarted = false;
    private StreamWriter writer;



    #region Unity LifeCycle
    void Start()
    {
        clients = new List<ServerClient>();
        disconnectedClients = new List<ServerClient>();

        try
        {

            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            StartListening();
            isServerStarted = true;

            Debug.Log("SERVER CHAT: Server has been started on port " + port);
        }
        catch(Exception ex)
        {
            Debug.Log(ex.Message);
        }


    }

    void Update()
    {
        if (!isServerStarted)
            return;

        foreach (ServerClient cs in clients)
        {

            if(!isConnected(cs.tcp))
            {
                cs.tcp.Close();
                disconnectedClients.Add(cs);
                continue;
            }
            else
            {
                NetworkStream stream = cs.tcp.GetStream();

                if(stream.DataAvailable)
                {
                    StreamReader reader = new StreamReader(stream, true);
                    string data = reader.ReadLine();


                    if(data != null)
                    {
                        OnIncomingData(cs,data);
                    }

                }

            }
        }



        if(Input.GetKey(KeyCode.S))
        {
            Broadcast("hello player", clients);

        }



    }


    #endregion


    #region Private methods

    private void Broadcast(string message, List<ServerClient> clients)
    {
        foreach(ServerClient sc in clients)
        {
            try
            {
                writer = new StreamWriter(sc.tcp.GetStream());
                writer.WriteLine(message);
                writer.Flush();


            }
            catch(Exception ex)
            {
                Debug.Log(ex);
            }



        }


    }

    private void OnIncomingData(ServerClient client,string data)
    {
        Debug.Log("SERVER CHAT: " + data);

        if(data == "turnLight")
        {
            EventController.InvokeEvent(EventController.Events.turnOnTheLigh);
            Broadcast("turnLight", clients);
        }

        if (data == "explosion")
        {
            EventController.InvokeEvent(EventController.Events.makeExplosion);
            Broadcast("explosion", clients);
        }

    }

    private bool isConnected(TcpClient tcp)
    {
        try
        {

            if (tcp != null && tcp.Client != null && tcp.Client.Connected)
            {
                //Danger zone
                return true;
            }
           

         return false;


        }
        catch(Exception ex)
        {
            Debug.Log(ex);
            return false;

        }
    }

    private void StartListening()
    {
        try
        {
            listener.BeginAcceptTcpClient(AcceptTCPClient, listener);
        }
        catch(Exception ex)
        {
            Debug.Log(ex.Message);
        }



    }

    private void AcceptTCPClient(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener)ar.AsyncState;

        clients.Add(new ServerClient(listener.EndAcceptTcpClient(ar)));

        StartListening();


    }

    #endregion

}



public class ServerClient
{
    public TcpClient tcp;
    public string clientName;
   public ServerClient(TcpClient value)
    {
        tcp = value;
        clientName = "Player";

    }


}