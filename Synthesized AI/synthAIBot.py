#### Taking inspiration from many people for various procedures as follows:
####    Craiyon: sa111n111, based-jace, Aedial
#### References: https://github.com/sa111n111/crayon.py/blob/core/crayon.py,
####    https://github.com/based-jace/craiyon-bot,
####    https://github.com/Aedial/novelai-api,
####    

#### Significant notes:
####    Some files downloaded via pip do not match files on github, use the ones from github, needs further documentation
####    map_meta_to_stories missing, found ????
####    transformers library was not installed as a dependant

#### Must have the following environment settings:
####    NAI_USERNAME: Username for active NovelAI account,
####    NAI_PASSWORD: Password for active NovelAI account
from sys import path
from os import environ as env
#from os.path import join, abspath, dirname
import os
from dotenv import load_dotenv
# Tensorflow minimum level for logs, prevents superfluous logging
os.environ['TF_CPP_MIN_LOG_LEVEL'] = '2'
# import ast

# path.insert(0, abspath(join(dirname(__file__), '..')))

#### For Craiyon
import sys
import aiohttp
import urllib.parse
import json
import asyncio
import aiofiles
#### For NovelAI
import logging
from logging import Logger, StreamHandler
import novelai_api
from novelai_api.utils import get_encryption_key, decrypt_user_data, map_meta_to_stories, assign_content_to_story
from novelai_api.GlobalSettings import GlobalSettings
from novelai_api.Preset import Preset, Model
from novelai_api.story import NovelAI_Story, NovelAI_StoryProxy
from typing import Union, Dict, Tuple, List, Any, NoReturn, Optional, Iterable
#### For AI Dungeon
from aidungeonapi import AIDungeonClient

from pprint import pprint
#user libraries
from imageConversion import webp_to_jpeg
credentials_location = ".env"

load_dotenv()

#### Low level functions, "private" so to speak
async def _get_username_password_session_nai(
        credentials_location: str, 
        is_logger: bool=True
    ) -> List:
    """Get novelAI username and password and connect to aiohttp.ClientSession.
        if is_logger is true, initialize and return that as well

    Args:
        credentials_location (str): the location ID for where to search for
            credentials.
        is_logger (bool, optional): _description_. Defaults to True.

    Raises:
        RuntimeError: Errors related to missing environment variables

    Returns:
        List: a collection of [username, password, session(, logger)]
    """
    username = ""
    password = ""
    if credentials_location == "env":
            
        if "NAI_USERNAME" not in env or "NAI_PASSWORD" not in env:
            raise RuntimeError("Please ensure that NAI_USERNAME and NAI_PASSWORD"
                + "are set in your system environment variables")
        username = env["NAI_USERNAME"]
        password = env["NAI_PASSWORD"]
    elif credentials_location == ".env":
        if "NAI_USERNAME" not in env or "NAI_PASSWORD" not in env:
            raise RuntimeError("Please ensure that NAI_USERNAME and NAI_PASSWORD"
                + "are set in your environments .env file")
        username = os.getenv("NAI_USERNAME")
        password = os.getenv("NAI_PASSWORD")
    if is_logger:
        logger = Logger("NovelAI")
        logger.addHandler(StreamHandler())
        logger.setLevel(logging.DEBUG)
        session = aiohttp.ClientSession()
        return [username, password, session, logger]

    session = aiohttp.ClientSession()
    return [username, password, session]
    
# async def _aid_generate_story(input_: str=None, action: str=None, client: AIDungeonClient=None):
#     adventures = await (await client).get_user_adventures()
    #print(adventures)


async def _sign_in_and_get_keystore_nai(
        session: aiohttp.ClientSession,
        username: str,
        password: str,
        logger: Logger=None
    ) -> Tuple:
    """Use username and password to get novelAI api object and keystore

    Args:
        session (aiohttp.ClientSession): active aoihttp session
        username (str): username used to sign in
        password (str): coresponding password
        logger (Logger, optional): used to log actions. Defaults to None.

    Returns:
        Tuple[novelai_api, novelai_api.Keystore]:
            -- novelAI API for high and low level methods
            -- ID used in place of username and passwords
    """    
    #### Connect account to site
    nai_api = novelai_api.NovelAI_API(session, logger = logger)
    login = await nai_api.high_level.login(username, password)
    try:
        logger.info(login)
    except:
        pass

    #### Get keys to stories and their content
    key = get_encryption_key(username, password)
    keystore = await nai_api.high_level.get_keystore(key)
    try:
        logger.info(keystore)
    except:
        pass

    return nai_api, keystore

async def _get_stories_nai(
        nai_api: novelai_api,
        keystore: novelai_api.Keystore,
        logger: Logger=None
    ) -> Dict[str, Dict[str, Union[str, int]]]:
    """Get decrypted story data

    Args:
        nai_api (novelai_api): novel AI API object
        keystore (novelai_api.Keystore): key object for authentication and hashing
        logger (Logger, optional): logging object. Defaults to None.

    Returns:
        Dict[str, Dict[str, Union[str, int]]]: story data for all
            stories associated to keystore
    """
    stories = await nai_api.high_level.download_user_stories()
    decrypt_user_data(stories, keystore)
    try:
        logger.info(stories)
    except:
        pass
    return stories

async def _remove_nonce(obj: Iterable) -> NoReturn:
    """Remove nonce from given object

    Args:
        obj (List, Dict): iterable object of at least 1 dimension
    """
    for o in obj:
        #print(story)
        if ("nonce" in o):
            #pprint(o["nonce"])
            del o["nonce"]
     
async def _get_story_contents_nai(
        nai_api: novelai_api,
        keystore:novelai_api.Keystore
        ) -> Dict[str, Dict[str, Union[str, int]]]:
    """Get decrypted story contents

    Args:
        nai_api (novelai_api): novel AI API object
        keystore (novelai_api.Keystore): key object for authentication and hashing

    Returns:
        Dict[str, Dict[str, Union[str, int]]]: dict containing
            all contents of each story
    """        
    story_contents = await nai_api.high_level.download_user_story_contents()
    decrypt_user_data(story_contents, keystore)
    return story_contents

async def _pair_stories_to_content_nai(
        stories: Dict[str, Dict[str, Union[str, int]]],
        story_contents: Dict[str, Dict[str, Union[str, int]]],
        logger: Logger=None
        ) -> Dict[str, Dict[str, Union[str, int]]]:
    """Pair stories to their contents

    Args:
        stories (Dict[str, Dict[str, Union[str, int]]]): stories and their data
        story_contents (Dict[str, Dict[str, Union[str, int]]]): story contents 
            and their data
        logger (Logger, optional): logging object. Defaults to None.

    Returns:
        Dict[str, Dict[str, Union[str, int]]]: merged stories and their contents
    """
    try:
        logger.info(json.dumps(story_contents, indent=4))
    except:
        pass
    stories = map_meta_to_stories(stories)
    assign_content_to_story(stories, story_contents)
    try:
        logger.info(json.dumps(stories, indent=4))
    except:
        pass
    return stories


async def _get_names_from_stories_nai(
        stories: Dict[str, Dict[str, Union[str, int]]]
    ) -> Dict:
    """Get the names of all the stories and associate them with their keys 

    Args:
        stories (Dict[str, Dict[str, Union[str, int]]]): collection of novelai stories

    Returns:
        Dict[str, str]: story_ID: story_name pairs
    """
    names = {}
    for story in stories:
        names[story['meta']] = story['data']['title']
    return names

async def _get_cleaned_stories_nai(
        nai_api: novelai_api,
        keystore: novelai_api.Keystore
    ) -> Dict[str, Dict[str, Union[str, int]]]:
    """Get stories without nonce

    Args:
        nai_api (novelai_api): novel AI API object.
        keystore (novelai_api.Keystore): key object for authentication and hashing.

    Returns:
        Dict[str, Dict[str, Union[str, int]]]: cleaned dictionary of stories
    """
    _stories = await _get_stories_nai(nai_api, keystore)

    await _remove_nonce(_stories)

    return _stories

async def _get_cleaned_story_contents_nai(
        nai_api: novelai_api, 
        keystore: novelai_api.Keystore
    ) -> Dict[str, Dict[str, Union[str, int]]]:
    """Get story contents without nonce

    Args:
        nai_api (novelai_api): novel AI API object.
        keystore (novelai_api.Keystore): key object for authentication and hashing.

    Returns:
        Dict[str, Dict[str, Union[str, int]]]: cleaned dictionary of story contents
    """
    _story_contents = await _get_story_contents_nai(nai_api, keystore)

    await _remove_nonce(_story_contents)

    return _story_contents

async def _get_paired_stories_and_contents_nai(
        nai_api: novelai_api,
        keystore: novelai_api.Keystore
    ) -> List:
    """Get stories and story contents using the novelAI api. Pair stories and 
    their contents as one object. Return paired story/story contents object and
    story contents as a list.

    Args:
        nai_api (novelai_api): novel AI API object
        keystore (novelai_api.Keystore): key object for authentication and hashing

    Returns:
        List[Dict[str, Dict[str, Union[str, int]]], 
            Dict[str, Dict[str, Union[str, int]]]]: 
            -- stories paired to their content
            -- contents of stories
    """
    _stories = await _get_stories_nai(nai_api, keystore)

    _story_contents = await _get_story_contents_nai(nai_api, keystore)

    stories = await _pair_stories_to_content_nai(_stories, _story_contents)

    return [stories, _story_contents]

async def _get_story_content_text_nai(
        story_contents: Dict[str, Dict[str, Union[str, int]]],
        story_id: str
    ) -> Dict[str, Dict[str, Union[str, int]]]:
    """Get the content of a specific story using the novelAI api

    Args:
        story_contents (Dict[str, Dict[str, Union[str, int]]]): content of all stories
        story_id (str): id of specific story

    Returns:
        Dict[str, Dict[str, Union[str, int]]]: the data of a specific story from
            story_contents
    """
    _story = await _find_story_contents_nai(story_contents, story_id)
    
    return _story
            
async def _find_story_contents_nai(
        story_contents: Dict[str, Dict[str, Union[str, int]]],
        story_id: str
    ) -> Dict[str, Dict[str, Union[str, int]]]:
    """Find the story contents that have a matching ID to story_id using the 
    novelAI api

    Args:
        story_contents (Dict[str, Dict[str, Union[str, int]]]): content of all
            stories
        story_id (str): id of specific story

    Returns:
        Dict[str, Dict[str, Union[str, int]]]: the data of a specific story 
            from story_contents
    """
    for _story in story_contents:
        if _story["meta"] == story_id:
            return _story

async def _find_story_and_contents_nai(
        stories: Dict[str, Dict[str, Union[str, int]]],
        story_contents: Dict[str, Dict[str, Union[str, int]]],
        story_id: str
    ) -> List:
    """Get the story and story contents for a specific story ID using the novelAI
    api

    Args:
        stories (Dict[str, Dict[str, Union[str, int]]]): all the stories and 
            their data
        story_contents (Dict[str, Dict[str, Union[str, int]]]): story contents 
            and all their data
        story_id (str): specific ID of a story

    Returns:
        List[Dict[str, Dict[str, Union[str, int]]],
            Dict[str, Dict[str, Union[str, int]]]]:
            -- the data of a specific story
            -- the data of a specific stories contents
    """
    return [
        await _find_story_nai(stories, story_id),
        await _find_story_contents_nai(story_contents, story_id)
    ]

async def _find_story_nai(
        stories: Dict[str, Dict[str, Union[str, int]]],
        story_id: Dict[str, Dict[str, Union[str, int]]]
    ) -> Dict[str, Dict[str, Union[str, int]]]:
    """Find specific story in the novelAI stories object

    Args:
        stories (Dict[str, Dict[str, Union[str, int]]]): all stories and their 
            data
        story_id (Dict[str, Dict[str, Union[str, int]]]): id of a specific story

    Returns:
        Dict[str, Dict[str, Union[str, int]]]: data of story[story_id]
    """
    for id, data in stories.items():
        if (id == story_id):
            return data

# async def _send_and_get_story_nai(input_: str=None, nai_api: novelai_api=None):
#     try:
#         presets = await nai_api.high_level.download_user_presets()
#         assert type(presets) is Preset

#     except:
#         preset = Preset("default", Model.Euterpe, {"min_length": 1, "max_length": 50})
#     global_settings = GlobalSettings()
#     response = await nai_api.high_level.generate(
#         str(input_),
#         Model.Euterpe,
#         preset,
#         global_settings)
    
#     return response

async def _make_story_object(
        nai_api: novelai_api,
        keystore: novelai_api.Keystore
    ) -> NovelAI_Story:
    """Create NovelAI_Story object. Enables saving and uploading new story
    content.
    
    Args:
        nai_api (novelai_api): novel AI API object
        keystore (novelai_api.Keystore): key object for hashing and authentication

    Returns:
        NovelAI_Story: object that manipulates story objects and interacts with
        the api and has an internal aiohttp.ClientSession 
    """
    global_settings = GlobalSettings()
    return NovelAI_Story(nai_api, keystore, global_settings)

async def _make_story_proxy(
        story_obj: NovelAI_Story,
        story: Dict[str, Dict[str, Union[str, int]]], 
        story_content: Dict[str, Dict[str, Union[str, int]]]
    ) -> NovelAI_StoryProxy:
    """Create a Story Proxy object by loading a story-content paired dict
    and the content it's paired to via a Story object.

    Args:
        story_obj (NovelAI_Story): object that manipulates story objects and 
            interacts with the api and has an internal aiohttp.ClientSession 
        story (Dict[str, Dict[str, Union[str, int]]]): all stories and their 
            data
        story_content (Dict[str, Dict[str, Union[str, int]]]): story contents 
            and all their data

    Returns:
        NovelAI_StoryProxy: object that manipulates story objects, child of
            NovelAI_Story, can alter contents of story and control how new
            content is handled/filtered. Operates on a specific story
    """
    return story_obj.load(story, story_content)

async def _add_story_content_nai(
        story_obj: NovelAI_StoryProxy,
        input_: str):
    """Create a new datablock associated to the user and add it to the story_obj
    using the novelAI api

    Args:
        story_obj (NovelAI_StoryProxy): object that manipulates story objects,
            child of NovelAI_Story, can alter contents of story and control how
            new content is handled/filtered. Operates on a specific story
        input_ (str): Content of the data being added to the story
    """
    fragment = {"data": input_, "origin": "user"}
    story_obj._create_datablock(fragment, 0)
    
async def _generate_story_nai(story_obj: NovelAI_StoryProxy):
    """Using the NovelAI_StoryProxy object, generate a response from the AI
    to continue the story

    Args:
        story_obj (NovelAI_StoryProxy): object that manipulates story objects,
            child of NovelAI_Story, can alter contents of story and control how
            new content is handled/filtered. Operates on a specific story
    """
    await story_obj.generate()

async def _get_current_story_fragment(story_obj: NovelAI_StoryProxy
    ) -> dict:
    """Get the most recent data fragment from the story object

    Args:
        story_obj (NovelAI_StoryProxy): object that manipulates story objects,
            child of NovelAI_Story, can alter contents of story and control how
            new content is handled/filtered. Operates on a specific story

    Returns:
        dict: a story fragment
    """
    return story_obj._storycontent["data"]["story"]["fragments"][-1]

async def _save_story_nai(story_obj: NovelAI_StoryProxy):
    """Publish the changes made to the story object to novelAI

    Args:
        story_obj (NovelAI_StoryProxy): object that manipulates story objects,
            child of NovelAI_Story, can alter contents of story and control how
            new content is handled/filtered. Operates on a specific story
    """
    await story_obj.save(True)


#### High level functions, "public"

async def generate_story_image(
        input_: str, 
        output_str: str, 
        count: int = 1,
        model: str = "craiyon"):
    """Generate count number of images from the given model using input_
    saving as output_str

    Args:
        input_ (str): Description of what to generate an image of
        output_str (str): The name the image will be saved with
        count (int, optional): The number of images generated. Defaults to 1.
        model (str, optional): Which image generator to use. Defaults to "craiyon".
    """
    if (model == "craiyon"):
        craiyon_url = "https://backend.craiyon.com/generate"
        prompt = {
            'prompt': input_
        }
        data = json.dumps(prompt)
        headers = {}
        headers["Content-Type"] = "application/json"
        # get client
        to_json = None
        async with aiohttp.ClientSession() as session:
            async with session.post(craiyon_url, data = data, headers = headers) as resp:
                # run AI
                # wait
                # get output
                to_json = await resp.json()
                
        # clean output
        data_url_template = "data:image/jpeg;base64,"
        
        # form our data-url
        for i in range(count):
            #print(chardet.detect(to_json["images"][i+1]))
            url_fmt = "{}{}".format(data_url_template, to_json["images"][i+1])
            
            urllib.request.urlretrieve(url_fmt, output_str)

            webp_to_jpeg(output_str, output_str)
            #Original code
            # response = urllib.request.urlopen(url_fmt)

            # if (not (os.path.exists(output_str) and os.path.isfile(output_str))):
            #     f = open(output_str,"w")
            #     f.close()
            # async with aiofiles.open('{}'.format(output_str), mode='wb') as f:
            #     await f.write(response.file.read())



# def generate_new_story(
#         input_: str, 
#         action: str,
#         model: str = "aidungeon"):    
#     if (model == "aidungeon"):
#         aidc = AIDungeonClient(username='jessejameshouk@gmail.com', 
#             password='AISynthesisPW2', debug=True)
#         asyncio.get_event_loop().run_until_complete(_aid_generate_story(
#             input_=input_, action=action, client=aidc))

# async def generate_new_story_async(
#         input_: str,
#         action: str,
#         model: str = "novelai"):    
#     if (model == "aidungeon"):
#         pass
#         # aidc = await AIDungeonClient(debug=True)
#         # adventure = await aidc.connect_to_public_adventure('51e86616-507f-49f7-b07d-9a58b3261781')
#         # await adventure.register_actions_callback(aidc_callback)
#         # await asyncio.create_task(adic_blocking_task())

#     elif (model == "novelai"):
#         username, password, session, logger = await _get_username_password_session_nai(credentials_location)

#         nai_api, keystore = await _sign_in_and_get_keystore_nai(session, username, password, logger)

#         _stories = await _get_stories_nai(nai_api, keystore)
        
#         _story_contents = await _get_story_contents_nai()

#         stories = await _pair_stories_to_content_nai(_stories, _story_contents, logger)

#         await session.close()



async def get_story_names(model: str="novelai") -> List:
    """Get the names of stories associated to the story telling model

    Args:
        model (str, optional): The story telling model. Defaults to "novelai".

    Returns:
        List: list of story names and story keys alternating
    """
    if model == "aidungeon":
        pass
    elif model == "novelai":
        return await get_story_names_nai()


async def get_story_names_nai() -> List:
    """Get the names of stories using the novelAI api

    Returns:
        List: list of story names and story keys alternating
    """
    username, password, session = await _get_username_password_session_nai(credentials_location, False)

    nai_api, keystore = await _sign_in_and_get_keystore_nai(session, username, password)

    _stories = await _get_stories_nai(nai_api, keystore)

    names = await _get_names_from_stories_nai(_stories)
    
    await session.close()

    return names        

async def get_story_contents(story: str, model: str) -> List:
    """Get the contents of story using model story objects

    Args:
        story (str): id of the story
        model (str): name of the model the story is stored in

    Returns:
        List: the individual elements of story
    """
    if (model == "aidungeon"):
        pass
    elif (model == "novelai"):
        data_fragments = await get_story_contents_nai(story)
        story_fragments = await get_story_from_fragments_nai(data_fragments)
        return await package_story_contents(story_fragments)

#### Not in use currently, replaced by get_story_from_fragments_nai
# async def get_story_from_data_fragments_nai(data_fragments):
#     story_fragments = []
#     for dataFragment in data_fragments["data"]["story"]["datablocks"][1:]:
#         story_fragments.append(dataFragment["dataFragment"]["data"])


#     return story_fragments

async def get_story_from_fragments_nai(
        fragments: Dict[str, str]
    ) -> List:
    """Extract the story contents as fragments in a list using the novelAI API
    including what entity produced that content

    Args:
        fragments (Dict[str, str]): the fragments dict from the story object

    Returns:
        List: a list of story fragments and their originators
    """
    story_fragments = []
    for fragment in fragments["data"]["story"]["fragments"][1:]:
        story_fragments.append(fragment["origin"])
        story_fragments.append(fragment["data"])

    return story_fragments

async def package_story_contents(story_fragments: List) -> List:
    """Prepare story fragments for being processed as individual entities
    and the content they produced

    Args:
        story_fragments (List): a list of story fragments and their originators

    Returns:
        List: formatted list of story contents and their originators
    """
    index = 0
    story_contents = [""]
    delimiters = ["prompt", "ai", "user", "edit"]
    for frag in story_fragments:
        if (frag not in delimiters):
            # print("frag")
            # print(frag)
            story_contents[index] += (frag  + '`')
        else:
            # print("attr")
            # print(frag)
            story_contents.append(frag + '`')
            index += 2
            story_contents.append("")

    return story_contents

async def get_story_contents_nai(story: str) -> Dict[str, Dict[str, Union[str, int]]]:
    """Get the contents of a specific story within novelAI

    Args:
        story (str): the id of the story

    Returns:
        Dict[str, Dict[str, Union[str, int]]]: the story contents of a specific
        story
    """
    username, password, session = await _get_username_password_session_nai(credentials_location, False)
    
    #global authorizationToken

    # authorizationToken = getAuth(username, password)
    # print("got auth")

    nai_api, keystore = await _sign_in_and_get_keystore_nai(session, username, password)
    
    stories, story_contents = await _get_paired_stories_and_contents_nai(nai_api, keystore)
    
    story_content_text = await _get_story_content_text_nai(story_contents, story)
    
    await session.close()

    return story_content_text

async def send_and_get_story(
        input_: str, 
        model: str, 
        story: str
    ) -> List:
    """Send input_ to model story, save the contents, and generate a response

    Args:
        input_ (str): new content for the story
        model (str): the model the story is saved on
        story (str): the id of the story

    Returns:
        List: an encapsulated response generated by the model
    """
    if (model == "novelai"):
        story_response = await send_and_get_story_nai(input_, story)

    return story_response

async def send_and_get_story_nai(
        input_: str, 
        _story: str
    ) -> List:
    """Send input_ to story, save the contents, and generate a response using
    NovelAI

    Args:
        input_ (str): new content for the story
        _story (str): the id of the story

    Returns:
        List: an encapsulated response generated by the model
    """
    username, password, session = await _get_username_password_session_nai(credentials_location, False)
    
    nai_api, keystore = await _sign_in_and_get_keystore_nai(session, username, password)
    
    story_obj = await _make_story_object(nai_api, keystore)
    
    stories, story_contents = await _get_paired_stories_and_contents_nai(nai_api, keystore)    
    
    story, contents = await _find_story_and_contents_nai(stories, story_contents, _story)
    
    story_proxy = await _make_story_proxy(story_obj, story, contents)
    
    await _add_story_content_nai(story_proxy, input_)
    
    await _generate_story_nai(story_proxy)
    
    await _save_story_nai(story_proxy)
    
    response = await _get_current_story_fragment(story_proxy)

    await session.close()
    
    return [response["data"]]
    

async def determine_and_execute_action(args: Dict):
    """Determine what action to execute and pass necessary parameters

    Args:
        args (Dict): general object containing action to be performed. May also
        contain input, model, story_id 
    """
    if (args["action"] == "getStoryNames"):
        story_names = await get_story_names(args["model"])
        await deliver_dict(story_names)
    elif (args["action"] == "sendAndGetStory"):
        response = await send_and_get_story(
            args["input"], 
            args["model"], 
            args["story_id"])
        await deliver_list(response)
    elif (args["action"] == "getStoryContents"):
        story_contents = await get_story_contents(
            args["story_id"],
            args["model"])
        await deliver_list(story_contents)

# async def aidc_callback(result):
#     #print(result)
#     pass

# async def aidc_blocking_task():
#     while True:
#         await asyncio.sleep(10)

async def deliver_dict(obj: Dict):
    """Prints the key, value pairs of the dict object

    Args:
        obj (Dict): a traditional dictionary object, preferably the values are not
        objects
    """
    for key, value in obj.items():
        print(key)
        print(value)

async def deliver_list(obj: List):
    """Prints the elements of the list object

    Args:
        obj (List): a traditional list object, preferably the values are not objects
    """
    for o in obj:
        print(o)

async def deliver_string(obj: str):
    """Prints the string object

    Args:
        obj (str): just a string
    """
    print(obj)

# async def deliver_story(_story):
#     for dataFragment in _story["data"]["story"]["datablocks"][1:]:
#         print(dataFragment["dataFragment"]["data"])

def main():
    aiType = sys.argv[1]
    aiModel = sys.argv[2]
    action = sys.argv[3]
    story_id = sys.argv[4]
    appInput = " ".join(sys.argv[5:])
    if (aiType == "image"):
        asyncio.get_event_loop().run_until_complete(
            generate_story_image(input_=appInput, output_str="img_img.jpg", 
                count=1, model=aiModel))
    elif (aiType == "story"):
        #try:
        if (aiModel == "aidungeon"):
            pass
            #generate_new_story_async(input_=appInput, model=aiModel)
        elif (aiModel == "novelai"):

            asyncio.get_event_loop().run_until_complete(
                determine_and_execute_action(
                    {   "input": appInput,
                        "model": aiModel,
                        "action": action,
                        "story_id": story_id}))

if (__name__ == "__main__"):
    main()