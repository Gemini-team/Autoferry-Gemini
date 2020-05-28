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


        public override async Task<DriveResponse> DriveForward(DriveRequest request, ServerCallContext context)
        {
            ManualResetEvent signalEvent = new ManualResetEvent(false);
            ThreadManager.ExecuteOnMainThread(() =>
            {
                _wheelDrive.torque = _wheelDrive.maxTorque;
                _wheelDrive.angle = request.Angle * _wheelDrive.maxAngle;
                _wheelDrive.handBrake = request.BrakeTorque;

                // Need to set signal event such that it wont block forever.
                signalEvent.Set();
            });

            // Wait for the event to be triggered from the action, signaling that the action is finished
            signalEvent.WaitOne();
            signalEvent.Close();

            return await Task.FromResult(new DriveResponse
            {
                Success = true
            });
        }

        public override async Task<DriveResponse> DriveBackward(DriveRequest request, ServerCallContext context)
        {
            ManualResetEvent signalEvent = new ManualResetEvent(false);
            ThreadManager.ExecuteOnMainThread(() =>
            {
                _wheelDrive.torque = -1 * _wheelDrive.maxTorque;
                _wheelDrive.angle = request.Angle * _wheelDrive.maxAngle;
                _wheelDrive.handBrake = request.BrakeTorque;

                // Need to set signal event such that it wont block forever.
                signalEvent.Set();
            });

            // Wait for the event to be triggered from the action, signaling that the action is finished
            signalEvent.WaitOne();
            signalEvent.Close();

            return await Task.FromResult(new DriveResponse
            {
                Success = true
            });
        }
        public override async Task<DriveResponse> Steer(DriveRequest request, ServerCallContext context)
        {

            ManualResetEvent signalEvent = new ManualResetEvent(false);
            ThreadManager.ExecuteOnMainThread(() =>
            {
                _wheelDrive.angle = request.Angle * _wheelDrive.maxAngle;
                _wheelDrive.handBrake = request.BrakeTorque;

                // Need to set signal event such that it wont block forever.
                signalEvent.Set();
            });

            // Wait for the event to be triggered from the action, signaling that the action is finished
            signalEvent.WaitOne();
            signalEvent.Close();

            return await Task.FromResult(new DriveResponse
            {
                Success = true
            });
        }

        public override async Task<DriveResponse> Idle(DriveRequest request, ServerCallContext context)
        {

            ManualResetEvent signalEvent = new ManualResetEvent(false);
            ThreadManager.ExecuteOnMainThread(() =>
            {
                _wheelDrive.torque = request.Torque * _wheelDrive.maxTorque;
                _wheelDrive.angle = request.Angle * _wheelDrive.maxAngle;
                _wheelDrive.handBrake = request.BrakeTorque;

                // Need to set signal event such that it wont block forever.
                signalEvent.Set();
            });

            // Wait for the event to be triggered from the action, signaling that the action is finished
            signalEvent.WaitOne();
            signalEvent.Close();

            return await Task.FromResult(new DriveResponse
            {
                Success = true
            });
        }

        public override async Task<DriveResponse> Brake(DriveRequest request, ServerCallContext context)
        {

            ManualResetEvent signalEvent = new ManualResetEvent(false);
            ThreadManager.ExecuteOnMainThread(() =>
            {
                
                _wheelDrive.torque = request.Torque * _wheelDrive.maxTorque;
                _wheelDrive.angle = request.Angle * _wheelDrive.maxAngle;
                _wheelDrive.handBrake = request.BrakeTorque;

                // Need to set signal event such that it wont block forever.
                signalEvent.Set();
            });

            // Wait for the event to be triggered from the action, signaling that the action is finished
            signalEvent.WaitOne();
            signalEvent.Close();

            return await Task.FromResult(new DriveResponse
            {
                Success = true
            });
        }
    }

}
