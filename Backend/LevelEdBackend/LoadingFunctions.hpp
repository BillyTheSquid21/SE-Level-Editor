#pragma once
#ifndef LOAD_FUNCS_HPP
#define LOAD_FUNCS_HPP

#include <stdint.h>
#include <iostream>
#include <fstream>
#include <string>
#include <algorithm>
#include "vendor/stb/stb_image.h"

extern "C" {

	struct Tile
	{
		uint32_t x;
		uint32_t y;
	};

	struct ImageData
	{
		int width;
		int height;
		int bpp;
	};

	//External
	__declspec(dllexport) void LoadTileset(const char* path, uint8_t* data, int size, ImageData& dim);
	__declspec(dllexport) void LoadTile(uint8_t* tilesetData, int tilesetSize, ImageData& tilesetDim, uint8_t* tileIconData, int tileIconSize, Tile& tile);

	void LoadTileset(const char* path, uint8_t* data, int size, ImageData& dim)
	{
		unsigned char* tmp = (unsigned char*)stbi_load(path, &dim.width, &dim.height, &dim.bpp, 4);
		for (int i = 0; i < size; i+=4)
		{
			//Reverses channels - writes as bgr otherwise - I do not know why
			data[i] = tmp[i + 2];
			data[i + 1] = tmp[i + 1];
			data[i + 2] = tmp[i];
			data[i + 3] = tmp[i + 3];
		}
	}

}

#endif