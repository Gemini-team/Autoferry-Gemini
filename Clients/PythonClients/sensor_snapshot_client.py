from __future__ import print_function
import grpc

from sensordata import sensordata_pb2
from sensordata import sensordata_pb2_grpc

import PIL.Image as image
import PIL.ImageOps as imageops

import sys

if __name__ == '__main__':


    # Default port number if not overwritten by argument
    port = '50060'

    if len(sys.argv) == 1:
        sys.exit("No IP argument given, quitting...")
    elif len(sys.argv) == 2:
        ip = sys.argv[1]
        print("One argument given, IP: ", ip)
    elif len(sys.argv) == 3:
        ip = sys.argv[1]
        port = sys.argv[2]
        print("Two arguments given, IP: ", ip, " port: ", port)


    channel = grpc.insecure_channel(ip + ':' + port)


    stub = sensordata_pb2_grpc.SensordataStub(channel)
    for imgChunk in stub.StreamSensordata(sensordata_pb2.SensordataRequest(operation="streaming")):

        img = image.frombytes("RGB",(800, 450), imgChunk.data, 'raw')
        img_flip = imageops.flip(img)
        img_flip.save("test.bmp")