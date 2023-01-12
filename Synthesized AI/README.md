Variables needed for .env file:
 - DISCORD_BOT - Token for discord bot on server, found on bot page

 - DISCORD_GUILD - The first number in the Discord URL

   -   i.e. https://discord.com/channels/DISCORD_GUILD/some_number

 - DISCORD_CHANNEL - The second number in the Discord URL

   -   i.e. https://discord.com/channels/some_number/DISCORD_CHANNEL

 - ROLE_ID - ID for the target role to moderate images passed through the bot
    found by querying the api from a bot command response with: 

   - guilds = self.guilds
      for g in guilds:
        if (GUILD_TOKEN == g.id):
          for r in g.roles:
            await ctx.send(r.id)
            await ctx.send(r.name)

 - MOD_ID - The id for the selected member of the given role
    found by querying the api from a bot command response with: 

   - guilds = self.guilds
      for g in guilds:
        if (GUILD_TOKEN == g.id):
          for r in g.roles:
            await ctx.send(r.members)

 - NAI_USERNAME - your username
 
 - NAI_PASSWORD - your password

