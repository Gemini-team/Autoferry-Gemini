using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using System.Threading;
using System.Threading.Tasks;
using Vesselcontroller;

namespace Assets.Networking.Services
{
    public class VesselControllerImpl : Vesselcontroller.VesselController.VesselControllerBase
    {
        private VesselController _vesselController;

        public VesselControllerImpl(VesselController vesselController)
        {
            _vesselController = vesselController;
        }

        public override async Task<ControlResponse> Forward(ControlRequest request, ServerCallContext context)
        {
            Debug.Log("Vesselcontroller Forward!");
            ManualResetEvent signalEvent = new ManualResetEvent(false);
            ThreadManager.ExecuteOnMainThread(() =>
            {

                _vesselController.forceVector = new Vector3(0, 0, 10_000);
                

                // Need to set signal event such that it wont block forever.
                signalEvent.Set();
            });

            // Wait for the event to be triggered from the action, signaling that the action is finished
            signalEvent.WaitOne();
            signalEvent.Close();

            return await Task.FromResult(new ControlResponse
            {
                Success = true
            });
        }

        public override async Task<ControlResponse> Backward(ControlRequest request, ServerCallContext context)
        {
            ManualResetEvent signalEvent = new ManualResetEvent(false);
            ThreadManager.ExecuteOnMainThread(() =>
            {

                _vesselController.forceVector = new Vector3(0, 0, -10_000);
                

                // Need to set signal event such that it wont block forever.
                signalEvent.Set();
            });

            // Wait for the event to be triggered from the action, signaling that the action is finished
            signalEvent.WaitOne();
            signalEvent.Close();

            return await Task.FromResult(new ControlResponse
            {
                Success = true
            });
        }

        public override async Task<ControlResponse> Portside(ControlRequest request, ServerCallContext context)
        {
            ManualResetEvent signalEvent = new ManualResetEvent(false);
            ThreadManager.ExecuteOnMainThread(() =>
            {

                _vesselController.forceVector = new Vector3(-10_000, 0, 0);
                

                // Need to set signal event such that it wont block forever.
                signalEvent.Set();
            });

            // Wait for the event to be triggered from the action, signaling that the action is finished
            signalEvent.WaitOne();
            signalEvent.Close();

            return await Task.FromResult(new ControlResponse
            {
                Success = true
            });
        }
        public override async Task<ControlResponse> Starboard(ControlRequest request, ServerCallContext context)
        {
            ManualResetEvent signalEvent = new ManualResetEvent(false);
            ThreadManager.ExecuteOnMainThread(() =>
            {

                _vesselController.forceVector = new Vector3(10_000, 0, 0);
                

                // Need to set signal event such that it wont block forever.
                signalEvent.Set();
            });

            // Wait for the event to be triggered from the action, signaling that the action is finished
            signalEvent.WaitOne();
            signalEvent.Close();

            return await Task.FromResult(new ControlResponse
            {
                Success = true
            });
        }
        public override async Task<ControlResponse> RotateClockwise(ControlRequest request, ServerCallContext context)
        {
            ManualResetEvent signalEvent = new ManualResetEvent(false);
            ThreadManager.ExecuteOnMainThread(() =>
            {

                _vesselController.torqueVector = new Vector3(0, 10_000, 0);
                

                // Need to set signal event such that it wont block forever.
                signalEvent.Set();
            });

            // Wait for the event to be triggered from the action, signaling that the action is finished
            signalEvent.WaitOne();
            signalEvent.Close();

            return await Task.FromResult(new ControlResponse
            {
                Success = true
            });
        }
        public override async Task<ControlResponse> RotateCounterClockwise(ControlRequest request, ServerCallContext context)
        {
            ManualResetEvent signalEvent = new ManualResetEvent(false);
            ThreadManager.ExecuteOnMainThread(() =>
            {

                _vesselController.torqueVector = new Vector3(0, -10_000, 0);
                

                // Need to set signal event such that it wont block forever.
                signalEvent.Set();
            });

            // Wait for the event to be triggered from the action, signaling that the action is finished
            signalEvent.WaitOne();
            signalEvent.Close();

            return await Task.FromResult(new ControlResponse
            {
                Success = true
            });
        }
        public override async Task<ControlResponse> Idle(ControlRequest request, ServerCallContext context)
        {
            ManualResetEvent signalEvent = new ManualResetEvent(false);
            ThreadManager.ExecuteOnMainThread(() =>
            {

                _vesselController.forceVector = new Vector3(0, 0, 0);
                _vesselController.torqueVector = new Vector3(0, 0, 0);
                

                // Need to set signal event such that it wont block forever.
                signalEvent.Set();
            });

            // Wait for the event to be triggered from the action, signaling that the action is finished
            signalEvent.WaitOne();
            signalEvent.Close();

            return await Task.FromResult(new ControlResponse
            {
                Success = true
            });
        }

    }
}
