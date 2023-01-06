import asyncio
import os
import discord
from discord.ext import commands
import random
import synthAIBot
from cogs.AIGenCog import AIGenCog
import sys
from typing import Union, Dict, Tuple, List, Any, NoReturn, Optional, Iterable
_DEBUG_ = True

class AIGenBot(commands.Bot):
    """Bot that listens on a channel and generates an image based on the specific
    command and prompt

    """
    def __init__(self, 
            command_prefix: str, 
            intents: discord.Intents, 
            channel: str):
        """Initialize components of class

        Args:
            command_prefix (str): Indicator for the bot to know when a message
                is a command
            intents (discord.Intents): Permissions the bot has for discord
            channel (str): ID of the channel the bot will listen to
        """
        super().__init__(command_prefix = command_prefix, intents = intents)
        self.queue = asyncio.Queue()
        self.call_limiter = asyncio.Semaphore(1)
        self.conformation_phrase_bank = [
            "Heard!",
            "Roger, Roger",
            "It shall be done",
            "As you wish",
            "You have my ear",
            "Doing it",
            "If you say so",
            "Let me try that",
            "I'm sworn to carry your burdens :rolling_eyes:",
            "On it"
        ]
        self.CHANNEL = channel
        self.IMAGE_DIR = os.getcwd().replace("\\", "/") + "/images/"
        self.img_id = 0
        self.lID = 0
        self.sID = 0
        self.cID = 0
       
        asyncio.get_event_loop().run_until_complete(
                    self._init_cog_())


    async def _init_cog_(self):
        """Add a new instance of AIGenCog to the bot

        """
        await self.add_cog(AIGenCog(self))
        
    async def _landscape(self, ctx: commands.Context, text: List):
        """The command for landscape. Read discord message, check channel, confirm
        receiving the message, generate image, and send confirmation

        Args:
            ctx (commands.Context): Active command context
            text (List): list of strings
        """
        input_ = self.convert_to_prompt(text)
        if self.channel_check(ctx.channel.id):
            await self.send_conformation_message(ctx)
            async with self.call_limiter:
                self.lID = await get_image_and_update_id(input_, "landscape", self.lID)
            
                
    async def _character(self, ctx: commands.Context, text: List):
        """The command for character. Read discord message, check channel, confirm
        receiving the message, generate image, and send confirmation

        Args:
            ctx (commands.Context): Active command context
            text (List): list of strings
        """
        input_ = self.convert_to_prompt(text)
        if self.channel_check(ctx.channel.id):
            await self.send_conformation_message(ctx)
            async with self.call_limiter:
                self.cID = await get_image_and_update_id(input_, "character", self.cID)
        
    async def _scene(self, ctx: commands.Context, text: List):
        """The command for scene. Read discord message, check channel, confirm
        receiving the message, generate image, and send confirmation

        Args:
            ctx (commands.Context): Active command context
            text (List): list of strings
        """
        input_ = self.convert_to_prompt(text)
        if self.channel_check(ctx.channel.id):
            await self.send_conformation_message(ctx)
            async with self.call_limiter:
                self.sID = await get_image_and_update_id(input_, "scene", self.sID)

    def convert_to_prompt(self, text: List):
        """Take a discord text component and process it into a single string

        Args:
            text (List): list of strings

        Returns:
            string: concatenated string
        """
        input_ = ""
        for e in text:
            input_ += e + " "
        input_.strip(" ")
        return input_

    def channel_check(self, id: int):
        """Compare id given to assigned channel id

        Args:
            id (int): id to compare

        Returns:
            bool: result of comparison
        """
        return str(id) == self.CHANNEL

    async def send_conformation_message(self, ctx: commands.Context, text: str=None):
        """Send a message in discord to acknowledge command given

        Args:
            ctx (commands.Context): Active command context
            text (str, optional): Custom message. Defaults to None and preset
                message will be used.
        """
        message = ""
        if text:
            message = text
        else:
            message = self.conformation_phrase_bank[
                random.randrange(0, len(self.conformation_phrase_bank))]
        await ctx.send(message)

    async def handle_queue(self, ctx: commands.Context, text: List):
        """Get next in the queue, await the execution of the function, and signal
        for the next in the queue

        Args:
            ctx (commands.Context): Active command context
            text (List): List of strings
        """
        f = await self.queue.get()

        await f(ctx, text)
        
        self.queue.task_done()

    
async def get_image_and_update_id(input_: str, file_prefix: str, id: int):
    """If path exists, remove file. Then generate the image from synthAIBot.
    Update id, send name of file, and return new id.

    Args:
        input_ (str): prompt for the ai generation
        file_prefix (str): type of image. Will also be the start to the file name
        id (int): id of the image type

    Returns:
        int: the updated id for the next image
    """
    file_name =  str(id) + ".jpg"
    path = file_prefix + file_name
    if (os.path.isfile(path)):
        os.remove(path)
    await synthAIBot.generate_story_image(input_=input_, count=1, 
                output_str=path, model="craiyon")
                #output_str="character" + str(self.img_id) + ".jpg", model="craiyon")
    # await self.update_id()
    id = (id + 1) % 2
    if (_DEBUG_):
        print(path, file=sys.stderr)
    return id