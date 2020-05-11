using Grpc.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Networking.Services
{
    public class VehicleController : MonoBehaviour
    {

        public string host = "localhost";

        public int port = ServicePortGenerator.GenPort();

        public WheelDrive wheelDrive;


        private Server server;
        private VehicleControllerImpl serviceImpl;

        // Start is called before the first frame update
        void Start()
        {
            if (wheelDrive)
                serviceImpl = new VehicleControllerImpl(wheelDrive);

            server = new Server
            {
                Services = { Vehiclecontroller.VehicleController.BindService(serviceImpl) },
                Ports = { new ServerPort(host, port, ServerCredentials.Insecure) }
            };

            Debug.Log("Vehiclecontroller server listening on port: " + port);
            server.Start();
        }

    }
    
}
