import socket,threading
import json
from datetime import datetime
import base64
import requests

BUFFER_SIZE = 5000000000

server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server_socket.bind(('', 9999))
server_socket.listen()

# 모델 uri 가져오면 끝에 uri를 image로 바꾸기
API_URL = "https://japaneast.api.cognitive.microsoft.com/customvision/v3.0/Prediction/d59d3856-7375-44b6-b4ea-45a532056e26/classify/iterations/Face_ID/image"
API_KEY = "1176a05d3fe147fe93399325dc9a92d0"
HEADERS = {"Prediction-Key": API_KEY, "Content-Type": "application/octet-stream"}

def face_id(file_fath):
    with open(file_fath, "rb") as file:
        response = requests.post(API_URL, headers=HEADERS, data=file.read())

    if response.status_code == 200:
        return response.json()
    else:
        return {"error":"Face request failed"}


def binder(client_socket, addr):
    print(f"Connecting client: {addr}")

    data = b""

    try:
        # while True:
        #     try:
        #         users = client_socket.recv(BUFFER_SIZE)
        #         if not users:
        #             break
        #         data += users
        #     except json.JSONDecodeError:
        #         continue

        while True:
            users = client_socket.recv(BUFFER_SIZE)
            mem = json.loads(users.decode())
            print("Received JSON:", mem)
            if mem["type"] == 'File' :
                now = datetime.now()
                filetime = now.strftime("%Y%m%d_%H%M%S")
                folder = (f"C:\\Users\\lms5\\Desktop\\TestFace\\{filetime}.png")
                f = open(folder, 'wb')
                file = base64.b64decode(mem["file"])

                f.write(file)
                f.close()
                print("이미지 저장 성공")

            elif mem["type"] == "Login" :
                now = datetime.now()
                filetime = now.strftime("%Y%m%d_%H%M%S")
                folder = (f"C:\\Users\\lms5\\Desktop\\TestFace\\{filetime}.png")
                f = open(folder, 'wb')
                file = base64.b64decode(mem["file"])
                f.write(file)
                f.close()

                result = face_id(folder)
                print(result)
                print("====================")
                face = result.get("predictions", [])
                print(face)

                for item in face:
                    if item['tagName'] == 'User' and item['probability'] >=0.5:
                        client_socket.send(json.dumps({"Num": 1}).encode('utf-8'))  # 키워드 인수로 전송해야 함.
                        break
                    else:
                        client_socket.send(json.dumps({"Num": 2}).encode('utf-8'))  # 키워드 인수로 전송해야 함.
                        break


    except Exception as e:
        print(f"Error with client {addr}: {e}")

    finally:
        client_socket.close()  # ✅ 클라이언트 소켓 닫기
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
