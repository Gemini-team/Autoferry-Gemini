using Grpc.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Networking.Services
{

    public class VesselController : MonoBehaviour
    {
        public string host = "localhost";

        // Change this to the generated after the thesis work is over
        //public int port = ServicePortGenerator.GenPort();
        public int port = 50081;

        public Vector3 forceVector { get; set; }
        public Vector3 torqueVector { get; set; }

        private Server server;
        private VesselControllerImpl serviceImpl;

        private Rigidbody rigidbody;

        // Start is called before the first frame update
        public void Start()
        {
            rigidbody = gameObject.GetComponent<Rigidbody>();
            serviceImpl = new VesselControllerImpl(this);

            server = new Server
            {
                Services = { Vesselcontroller.VesselController.BindService(serviceImpl) },
                Ports = { new ServerPort(host, port, ServerCredentials.Insecure) }
            };

            Debug.Log("Vesselcontroller server listening on port: " + port);
            server.Start();
        }

        public void FixedUpdate()
        {
            rigidbody.AddForce(forceVector);
            rigidbody.AddTorque(torqueVector);
        }
    }

}
