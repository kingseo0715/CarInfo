import socket
import threading
import json
from datetime import datetime
import base64

BUFFER_SIZE = 1024

server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server_socket.bind(('', 9999))
server_socket.listen()


def binder(client_socket, addr):
    print(f"Connecting client: {addr}")

    data = b""

    try:
        while True:
            chunk = client_socket.recv(BUFFER_SIZE)

            data += chunk
            if not data: break
            # if b"\n" in chunk:  # 개행 문자 기준으로 데이터 완전성 확인
            #     break
        server_socket.close()

        try:
            mem = json.loads(data.decode().strip())  # JSON 변환
            print("Received JSON:", mem)

            if mem["type"] == 'File':
                now = datetime.now()
                filetime = now.strftime("%Y%m%d_%H%M%S")
                folder = f"C:\\Users\\lms5\\Desktop\\TestFace\\{filetime}.png"

                file_data = base64.b64decode(mem["file"])
                with open(folder, 'wb') as f:
                    f.write(file_data)

                print("이미지 저장 성공:", folder)
                server_socket.close()

        except json.JSONDecodeError as e:
            print("JSON 파싱 실패:", e)

    except Exception as e:
        print(f"Error with client {addr}: {e}")

    finally:
        client_socket.close()
        print(f"Client {addr} disconnected")


try:
    while True:
        client_socket, addr = server_socket.accept()
        th = threading.Thread(target=binder, args=(client_socket, addr))
        th.start()
except Exception as e:
    print("Server error:", e)
finally:
    server_socket.close()