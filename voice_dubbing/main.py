import argparse
import sounddevice as sd
import numpy as np
import requests as requests

authorize_root = 'https://ngw.devices.sberbank.ru:9443/api/v2/oauth'
authorize_headers = {
    'Authorization': ''
}

root = 'https://smartspeech.sber.ru/rest/v1/text:synthesize'
params = {
    'format': 'wav16'
}
headers = {
    'Content-Type': 'application/text',
    'RqUID': ''
}

def authorize():
    global token
    response = requests.post(authorize_root, headers=authorize_headers, data={
        'scope': '',
        'authorization': '',
        'rquid': ''
    }, verify=False)
    try:
        return response.json()['access_token']
    except:
        print("Authorize error")


def synthesize(token:str, voice:str, text:str):
    try:
        headers["Authorization"] = f'Bearer {token}'
        params['voice'] = voice
        voice = requests.post(root, params=params, data=text.encode('utf-8'), headers=headers, verify=False)
        say(voice.content)
    except:
        print("sythesize error")


def say(content):
    audio_array = np.frombuffer(content, dtype=np.int16)
    sd.play(audio_array, samplerate=22000)
    sd.wait()


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--voice", required=True)
    parser.add_argument("--text", required=True)

    args = parser.parse_args()
    text = args.text
    voice = args.voice
    synthesize(authorize(), voice, text)


if __name__ == '__main__':
    main()


