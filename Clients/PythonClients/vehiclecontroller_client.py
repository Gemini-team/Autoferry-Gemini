
from __future__ import print_function

import grpc

from vehiclecontroller import vehiclecontroller_pb2
from vehiclecontroller import vehiclecontroller_pb2_grpc

import sys

if __name__ == '__main__':

    # Default port number if not overwritten by argument
    port = '50080'

    if len(sys.argv) == 1:
        sys.exit("No IP argument given, quitting...")
    elif len(sys.argv) == 2:
        ip = sys.argv[1]
        print("One argument given, IP: ", ip)
    elif len(sys.argv) == 3:
        ip = sys.argv[1]
        port = sys.argv[2]
        print("Two arguments given, IP: ", ip, " port: ", port)


    vehiclecontroller_channel = grpc.insecure_channel(ip + ':' + port)

    #vehiclecontroller_channel = grpc.insecure_channel('192.168.1.106:50080')

    vehiclecontroller_stub = vehiclecontroller_pb2_grpc.VehicleControllerStub(vehiclecontroller_channel)

    command = ""
    while True:
        command = input()
        if command == "w":
            success = vehiclecontroller_stub.DriveForward(vehiclecontroller_pb2.DriveRequest(torque=1.0, angle = 0.0, brakeTorque = 0.0))
        elif command == "a":
            success = vehiclecontroller_stub.Steer(vehiclecontroller_pb2.DriveRequest(torque = 0.0, angle = -1.0, brakeTorque = 0.0))
        elif command == "s":
            success = vehiclecontroller_stub.DriveBackward(vehiclecontroller_pb2.DriveRequest(torque=-1.0, angle = 0.0, brakeTorque = 0.0))
        elif command == "d":
            success = vehiclecontroller_stub.Steer(vehiclecontroller_pb2.DriveRequest(torque = 0.0, angle = 1.0, brakeTorque = 0.0))
        elif command == " ":
            success = vehiclecontroller_stub.Idle(vehiclecontroller_pb2.DriveRequest(torque = 0.0, angle = 0.0, brakeTorque = 0.0))
        elif command == "x":
            success = vehiclecontroller_stub.Brake(vehiclecontroller_pb2.DriveRequest(torque = 0.0, angle = 0.0, brakeTorque = 30000.0))