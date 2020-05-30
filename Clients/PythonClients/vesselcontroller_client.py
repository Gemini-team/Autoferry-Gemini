

from __future__ import print_function

import grpc

from vesselcontroller import vesselcontroller_pb2
from vesselcontroller import vesselcontroller_pb2_grpc

import sys

if __name__ == '__main__':

    # Default port number if not overwritten by argument
    port = '50081'

    if len(sys.argv) == 1:
        sys.exit("No IP argument given, quitting...")
    elif len(sys.argv) == 2:
        ip = sys.argv[1]
        print("One argument given, IP: ", ip)
    elif len(sys.argv) == 3:
        ip = sys.argv[1]
        port = sys.argv[2]
        print("Two arguments given, IP: ", ip, " port: ", port)


    vesselcontroller_channel = grpc.insecure_channel(ip + ':' + port)

    vesselcontroller_stub = vesselcontroller_pb2_grpc.VesselControllerStub(vesselcontroller_channel)

    command = ""
    while True:
        command = input()
        if command == "w":
            success = vesselcontroller_stub.Forward(vesselcontroller_pb2.ControlRequest(throttle=1.0))
        elif command == "a":
            success = vesselcontroller_stub.Portside(vesselcontroller_pb2.ControlRequest(throttle=1.0))
        elif command == "s":
            success = vesselcontroller_stub.Backward(vesselcontroller_pb2.ControlRequest(throttle=1.0))
        elif command == "d":
            success = vesselcontroller_stub.Starboard(vesselcontroller_pb2.ControlRequest(throttle=1.0))
        elif command == "e":
            success = vesselcontroller_stub.RotateClockwise(vesselcontroller_pb2.ControlRequest(throttle=1.0))
        elif command == "q":
            success = vesselcontroller_stub.RotateCounterClockwise(vesselcontroller_pb2.ControlRequest(throttle=1.0))
        elif command == " ":
            success = vesselcontroller_stub.Idle(vesselcontroller_pb2.ControlRequest(throttle=1.0))
        elif command == "quit":
            break