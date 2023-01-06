from PIL import Image
import os

image = Image.open(os.getcwd() + "/character0.jpg")

print(image.format)