
import os

import discord
from discord.ext import commands
from dotenv import load_dotenv
#from synthAIBot import generate_story_image
import AIGenBot

print("Discord bot initiating", flush=True)
load_dotenv()

# token is the bot ID
BOT_TOKEN = os.getenv("DISCORD_BOT")
GUILD_TOKEN = os.getenv("DISCORD_GUILD")
CHANNEL_TOKEN = os.getenv("DISCORD_CHANNEL")
ROLE_ID_TOKEN = os.getenv("ROLE_ID")
MOD_ID_TOKEN = os.getenv("MOD_ID")


action_queue = {}

# give all permissions to bot
intents = discord.Intents.all()
bot = AIGenBot.AIGenBot(
    command_prefix = '!', 
    intents = intents,
    channel = CHANNEL_TOKEN)


bot.run(BOT_TOKEN)