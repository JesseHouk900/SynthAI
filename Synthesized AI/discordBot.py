
import os

import discord
from discord.ext import commands
from dotenv import load_dotenv
#from synthAIBot import generate_story_image
import AIGenBot

print("Discord bot initiating")
load_dotenv()

# token is the bot ID
TOKEN = os.getenv("DISCORD_TOKEN")
CHANNEL = os.getenv("DISCORD_CHANNEL")


action_queue = {}

# give all permissions to bot
intents = discord.Intents.all()
bot = AIGenBot.AIGenBot(
    command_prefix = '!', 
    intents = intents,
    channel = CHANNEL)


bot.run(TOKEN)