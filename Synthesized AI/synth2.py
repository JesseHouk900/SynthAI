import requests
import json

#base_url = "https://saharmor.me/dalle-playground"
#backend_url = "https://micro-trance-forever-theorem.trycloudflare.com"

#response = requests.post(f"{base_url}/?backendUrl={backend_url}")
#print(response.json())

def generate_story_image(input: str = None, count: int = 1, output_str: str = None):
    craiyon_url = "https://saharmor.me/dalle-playground"
    prompt = {
        'backendUrl': "https://micro-trance-forever-theorem.trycloudflare.com",
        'promptText': input
    }
    data = json.dumps(prompt)
    headers = {}
    headers["Content-Type"] = "application/json"
    # get client
    to_json = None
    with requests.post(craiyon_url, json = data, headers = headers) as resp:
        # run AI
        # wait
        # get output
        print(resp)
        to_json = resp.json()

generate_story_image("orc-in-shorts-meets-zombie-in-style-of-earthbound", count = 2)