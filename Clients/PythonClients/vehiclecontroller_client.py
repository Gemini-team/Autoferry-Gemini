
from __future__ import print_function

import grpc

from vehiclecontroller import vehiclecontroller_pb2
from vehiclecontroller import vehiclecontroller_pb2_grpc

if __name__ == '__main__':

    vehiclecontroller_channel = grpc.insecure_channel('192.168.1.106:50080')

    vehiclecontroller_stub = vehiclecontroller_pb2_grpc.VehicleControllerStub(vehiclecontroller_channel)

    command = ""
    while True:
        command = input()
        if command == "w":
            #success = vehiclecontroller_stub.Drive(vehiclecontroller_pb2.DriveRequest(torque=1.0, angle = 0.0))
            success = vehiclecontroller_stub.DriveForward(vehiclecontroller_pb2.DriveForwardRequest(value = 1.0))
        elif command == "a":
            success = vehiclecontroller_stub.Drive(vehiclecontroller_pb2.DriveRequest(torque=0.0, angle = -1.0))
        elif command == "s":
            #success = vehiclecontroller_stub.Drive(vehiclecontroller_pb2.DriveRequest(torque=-1.0, angle = 0.0))
            success = vehiclecontroller_stub.DriveBackward(vehiclecontroller_pb2.DriveBackwardRequest(value = -1.0))
        elif command == "d":
            success = vehiclecontroller_stub.Drive(vehiclecontroller_pb2.DriveRequest(torque=0.0, angle = 1.0))
        elif command == " ":
            success = vehiclecontroller_stub.Drive(vehiclecontroller_pb2.DriveRequest(torque=0.0, angle = 0.0))