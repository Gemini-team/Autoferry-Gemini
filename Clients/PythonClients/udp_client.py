import time
import socket

"""
client_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
client_socket.settimeout(5.0)
message = b'TEST!'
addr = ("127.0.0.1", 50090)

start = time.time()
for i in range(10):
    print("sending message ", i)
    client_socket.sendto(message, addr)
    try:
        data, server = client_socket.recvfrom(1024)
        end = time.time()
        elapsed = end - start
        print(f'{data} {elapsed}')
    except socket.timeout:
        print('REQUEST TIMED OUT')
"""

client_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
client_socket.settimeout(20.0)
addr = ("127.0.0.1", 50091)
client_socket.bind(addr)

num_buffers = 1
start = time.time()
while True:
    #try:
        #data, server = client_socket.recvfrom(1024)
        #data = client_socket.recv(1024)
        #num_buffers += 1
    #except socket.timeout:
        #print('REQUEST TIMED OUT')
        #break
    data = client_socket.recv(1024)
    num_buffers += 1
    now = time.time()
    if (now - start > 20): 
        break

print("num_buffers: ", num_buffers)