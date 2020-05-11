using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using UnityEngine;
using Vehiclecontroller;

namespace Assets.Networking.Services
{
    public class VehicleControllerImpl : Vehiclecontroller.VehicleController.VehicleControllerBase 
    {

        private WheelDrive _wheelDrive;

        public VehicleControllerImpl(WheelDrive wheelDrive)
        {
            _wheelDrive = wheelDrive;
        }

        public override async Task<DriveResponse> Drive(DriveRequest request, ServerCallContext context)
        {
    
            // TODO: Move the code execution part into own function.
            if (request.Torque > 0.0f)
            {


                ManualResetEvent signalEvent = new ManualResetEvent(false);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    _wheelDrive.torque = _wheelDrive.maxTorque;

                    // Need to set signal event such that it wont block forever.
                    signalEvent.Set();
                });

                // Wait for the event to be triggered from the action, signaling that the action is finished
                signalEvent.WaitOne();
                signalEvent.Close();

            }
            else if (request.Torque < -0.0f)
            {

                ManualResetEvent signalEvent = new ManualResetEvent(false);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    _wheelDrive.torque = -1 * _wheelDrive.maxTorque;

                    // Need to set signal event such that it wont block forever.
                    signalEvent.Set();
                });

                // Wait for the event to be triggered from the action, signaling that the action is finished
                signalEvent.WaitOne();
                signalEvent.Close();
            }
            else
            {
                 
                ManualResetEvent signalEvent = new ManualResetEvent(false);
                // We apply no torque
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    _wheelDrive.torque = 0.0f;

                    // Need to set signal event such that it wont block forever.
                    signalEvent.Set();
                });

                // Wait for the event to be triggered from the action, signaling that the action is finished
                signalEvent.WaitOne();
                signalEvent.Close();

            }

            if (request.Angle > 0.0f)
            {
                ManualResetEvent signalEvent = new ManualResetEvent(false);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    _wheelDrive.angle = _wheelDrive.maxAngle;

                    // Need to set signal event such that it wont block forever.
                    signalEvent.Set();
                });

                // Wait for the event to be triggered from the action, signaling that the action is finished
                signalEvent.WaitOne();
                signalEvent.Close();

            } else if (request.Angle < 0.0f)
            {

                ManualResetEvent signalEvent = new ManualResetEvent(false);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    _wheelDrive.angle = -1 * _wheelDrive.maxAngle;

                    // Need to set signal event such that it wont block forever.
                    signalEvent.Set();
                });

                // Wait for the event to be triggered from the action, signaling that the action is finished
                signalEvent.WaitOne();
                signalEvent.Close();

            } else
            {

                ManualResetEvent signalEvent = new ManualResetEvent(false);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    _wheelDrive.angle = 0;

                    // Need to set signal event such that it wont block forever.
                    signalEvent.Set();
                });

                // Wait for the event to be triggered from the action, signaling that the action is finished
                signalEvent.WaitOne();
                signalEvent.Close();
            }
            

            return await Task.FromResult(new DriveResponse
            {
                Success = true
            });

        }

    }

}
