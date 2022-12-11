#!/bin/sh

ffmpeg -f image2 -framerate 96 -i img/%d.png -pix_fmt yuv420p day08.mp4
