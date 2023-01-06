from PIL import Image

def webp_to_jpeg(image_address: str, new_image_address: str):
    """Convert an image at image_address to RGB and save it to new_image_address

    Args:
        image_address (str): old address
        new_image_address (str): new address
    """
    im = Image.open(image_address).convert("RGB")
    im.save(new_image_address,"jpeg")

if (__name__=="__main__"):
    webp_to_jpeg("character0.jpg", "character0.jpg")