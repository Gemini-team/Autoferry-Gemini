from __future__ import print_function

import os, sys, pygame
import pygame.camera
from pygame.locals import *


import grpc


from sensordata import sensordata_pb2
from sensordata import sensordata_pb2_grpc


if __name__ == '__main__':


    pygame.init()
    surface = pygame.display.set_mode((1700, 920))

    canvas = pygame.Surface((1600, 900))

    camera1 = pygame.Rect(0, 0, 800, 450)
    camera2 = pygame.Rect(800, 0, 800, 450)
    camera3 = pygame.Rect(0, 450, 800, 450)
    camera4 = pygame.Rect(800, 450, 800, 450)

    sub1 = canvas.subsurface(camera1)
    sub2 = canvas.subsurface(camera2)
    sub3 = canvas.subsurface(camera3)
    sub4 = canvas.subsurface(camera4)

    pygame.display.set_caption("Streaming")

    channel1 = grpc.insecure_channel('localhost:50101')
    stub1 = sensordata_pb2_grpc.SensordataStub(channel1)

    channel2 = grpc.insecure_channel('localhost:50100')
    stub2 = sensordata_pb2_grpc.SensordataStub(channel2)

    channel3 = grpc.insecure_channel('localhost:50098')
    stub3 = sensordata_pb2_grpc.SensordataStub(channel3)

    channel4 = grpc.insecure_channel('localhost:50107')
    stub4 = sensordata_pb2_grpc.SensordataStub(channel4)

    running = True
    while running:
        # Check for events
        events = pygame.event.get()
        for e in events:
            if e.type == QUIT or (e.type == KEYDOWN and e.key == K_ESCAPE):
                running = False     

        for imgChunk in stub1.StreamSensordata(sensordata_pb2.SensordataRequest(operation="streaming")):
            img = pygame.image.frombuffer(imgChunk.data, (800, 450), "RGB")

            sub1.blit(img, [100, 100])

            #surface.blit(sub1, (0, 0))
            surface.blit(pygame.transform.flip(sub1, False, True), (0, 0))

        for imgChunk in stub2.StreamSensordata(sensordata_pb2.SensordataRequest(operation="streaming")):

            img = pygame.image.frombuffer(imgChunk.data, (800, 450), "RGB")
            #img = pygame.transform.rotate(img, 180)

            sub2.blit(img, [100, 100])

            #surface.blit(sub2, (800, 0))

            surface.blit(pygame.transform.flip(sub2, False, True), (800, 0))

        for imgChunk in stub3.StreamSensordata(sensordata_pb2.SensordataRequest(operation="streaming")):
            img = pygame.image.frombuffer(imgChunk.data, (800, 450), "RGB")
            #img = pygame.transform.rotate(img, 180)

            sub3.blit(img, [100, 100])

            #surface.blit(sub3, (0, 450))

            surface.blit(pygame.transform.flip(sub3, False, True), (0, 450))

            #surface.blit(img, (0, 0), camera3)

        for imgChunk in stub4.StreamSensordata(sensordata_pb2.SensordataRequest(operation="streaming")):
            img = pygame.image.frombuffer(imgChunk.data, (800, 450), "RGB")
            #img = pygame.transform.rotate(img, 180)

            sub4.blit(img, [100, 100])

            #surface.blit(sub4, (800, 450))

            surface.blit(pygame.transform.flip(sub4, False, True), (800, 450))


        pygame.display.flip()
        pygame.display.update()
