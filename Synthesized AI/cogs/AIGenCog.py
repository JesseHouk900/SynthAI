from discord.ext import commands
import AIGenBot

class AIGenCog(commands.Cog):
    def __init__(self, bot):
        self.bot = bot

    
    @commands.command(pass_context = True)
    async def test(self, ctx, arg):
        await ctx.send(arg)

    @commands.command(name = "landscape", aliases=["land", "l"], pass_context = True)
    async def landscape(self, ctx: commands.Context, *text):
        self.bot.queue.put_nowait(self.bot._landscape)

        await self.bot.handle_queue(ctx, text)
        
    @commands.command(name = "character", aliases=["char", "c"], pass_context = True)
    async def character(self, ctx: commands.Context, *text):
        self.bot.queue.put_nowait(self.bot._character)

        await self.bot.handle_queue(ctx, text)
        
    @commands.command(name = "scene", aliases=["scenery", "s"], pass_context = True)
    async def scene(self, ctx: commands.Context, *text):
        self.bot.queue.put_nowait(self.bot._scene)

        await self.bot.handle_queue(ctx, text)
        
def setup(bot):
    bot.add_cog(AIGenBot(bot))
