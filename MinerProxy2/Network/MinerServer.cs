﻿using MinerProxy2.Interfaces;
using MinerProxy2.Network.Connections;
using MinerProxy2.Network.Sockets;
using Serilog;

namespace MinerProxy2.Network
{
    public class MinerServer
    {
        private readonly Server minerServer;
        public ICoinHandlerMiner _coinHandler;
        private readonly PoolClient _pool;

        private int port;

        public MinerServer(int port, PoolClient pool, ICoinHandlerMiner coinHandler)
        {
            Log.Information("MinerServer initialized.");
            _pool = pool;
            _coinHandler = coinHandler;
            coinHandler.SetMinerServer(this);

            this.port = port;
            minerServer = new Server();
            minerServer.OnClientDataReceived += MinerServer_OnClientDataReceived;
            minerServer.OnClientConnected += MinerServer_OnClientConnected;
        }

        public void ListenForMiners()
        {
            Log.Information("Starting MinerServer on " + port);
            minerServer.Start(port);
        }

        public void SendToPool(byte[] data)
        {
            Log.Debug("MinerServer SendToPool");
            _pool.SendToPool(data);
        }

        public void SendToMiner(byte[] data, TcpConnection connection)
        {
            connection.socket.Send(data);
        }

        public void BroadcastToMiners(byte[] data)
        {
            //Log.Debug("MinerServer broadcast");
            minerServer.BroadcastToMiners(data);
        }

        private void MinerServer_OnClientConnected(object sender, ClientConnectedArgs e)
        {
            Log.Information(e.connection.endPoint.ToString() + " has connected!");
            //_coinHandler.MinerConnected
        }

        private void MinerServer_OnClientDataReceived(object sender, ClientDataReceivedArgs e)
        {
            //Log.Information(Encoding.ASCII.GetString(e.Data));
            _coinHandler.MinerDataReceived(e.Data, e.connection);
        }
    }
}