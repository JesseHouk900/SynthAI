import sys
import asyncio
sys.path.append("C:\\Users\\jesse\\Repos\\Synthesized AI\\Synthesized AI")
from AIGenBot import get_image_and_update_id

asyncio.get_event_loop().run_until_complete(get_image_and_update_id(
    "!c a tall man with a wide brimmed hat with dark rough features wearing a duster",
    "character",
    0))