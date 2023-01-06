#### Taking inspiration from many people for various procedures as follows:
####    Craiyon: sa111n111, based-jace
#### References: https://github.com/sa111n111/crayon.py/blob/core/crayon.py,
####    https://github.com/based-jace/craiyon-bot/blob/main/bot.py,
####    
import aiohttp
import urllib.parse
import json
import asyncio
#import aiofiles
import chardet
#orc-in-shorts-meets-zombie-in-style-of-earthbound

async def generate_story_image(input: str = None, count: int = 1, output_str: str = None):
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
    async with aiohttp.ClientSession() as session:
        #print(await session.options(craiyon_url))
        async with session.post(craiyon_url, data = data, headers = headers) as resp:
            # run AI
            # wait
            # get output
            print(resp)
            to_json = await resp.json()

    # clean output
    data_url_template = "data:image/jpeg;base64,"
    
    # form our data-url
    for i in range(count):
        print(chardet.detect(to_json["images"][i+1]))
        # url_fmt = "{}{}".format(data_url_template, to_json["images"][i+1])
        
        # try:
        #     # Prepare image download. we simply use urlopen to open our data URI
        #     # and write its contents to an image.jpg file.
        #     response = urllib.request.urlopen(url_fmt)
        # except urllib.error.HTTPError as e:
        #     # Return code error (e.g. 404, 501, ...)
        #     # ...
        #     print('HTTPError: {}'.format(e.code))
        # except urllib.error.URLError as e:
        #     # Not an HTTP-specific error (e.g. connection refused)
        #     # ...
        #     print('URLError: {}'.format(e.reason))
        # else:
        #     # 200
        #     # ...
        #     print('good')
        # #img = response.file.read().decode('ISO-8859-1', errors='ignore')
        # print(response.status)
        # #async with response.file.read() as r:
        # #    await print(r)
        # # async with aiofiles.open('{}'.format(output_str), mode='wb') as f:
        # #   await f.write(response.file.read())


    # return output




def main():
    asyncio.get_event_loop().run_until_complete(
        generate_story_image('orc-in-shorts-meets-zombie-in-style-of-earthbound',
        count=1, output_str="img_img.jpg"))

if (__name__ == "__main__"):
    main()