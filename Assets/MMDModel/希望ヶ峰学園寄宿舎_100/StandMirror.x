xof 0303txt 0032
template Vector {
 <3d82ab5e-62da-11cf-ab39-0020af71e433>
 FLOAT x;
 FLOAT y;
 FLOAT z;
}

template MeshFace {
 <3d82ab5f-62da-11cf-ab39-0020af71e433>
 DWORD nFaceVertexIndices;
 array DWORD faceVertexIndices[nFaceVertexIndices];
}

template Mesh {
 <3d82ab44-62da-11cf-ab39-0020af71e433>
 DWORD nVertices;
 array Vector vertices[nVertices];
 DWORD nFaces;
 array MeshFace faces[nFaces];
 [...]
}

template MeshNormals {
 <f6f23f43-7686-11cf-8f52-0040333594a3>
 DWORD nNormals;
 array Vector normals[nNormals];
 DWORD nFaceNormals;
 array MeshFace faceNormals[nFaceNormals];
}

template Coords2d {
 <f6f23f44-7686-11cf-8f52-0040333594a3>
 FLOAT u;
 FLOAT v;
}

template MeshTextureCoords {
 <f6f23f40-7686-11cf-8f52-0040333594a3>
 DWORD nTextureCoords;
 array Coords2d textureCoords[nTextureCoords];
}

template ColorRGBA {
 <35ff44e0-6c7c-11cf-8f52-0040333594a3>
 FLOAT red;
 FLOAT green;
 FLOAT blue;
 FLOAT alpha;
}

template ColorRGB {
 <d3e16e81-7835-11cf-8f52-0040333594a3>
 FLOAT red;
 FLOAT green;
 FLOAT blue;
}

template Material {
 <3d82ab4d-62da-11cf-ab39-0020af71e433>
 ColorRGBA faceColor;
 FLOAT power;
 ColorRGB specularColor;
 ColorRGB emissiveColor;
 [...]
}

template MeshMaterialList {
 <f6f23f42-7686-11cf-8f52-0040333594a3>
 DWORD nMaterials;
 DWORD nFaceIndexes;
 array DWORD faceIndexes[nFaceIndexes];
 [Material <3d82ab4d-62da-11cf-ab39-0020af71e433>]
}

template TextureFilename {
 <a42790e1-7810-11cf-8f52-0040333594a3>
 STRING filename;
}


Mesh {
 4;
 0.416887;1.943319;0.064994;,
 0.202521;0.051973;-0.190478;,
 -0.018678;1.943319;0.430478;,
 -0.233045;0.051973;0.175006;;
 2;
 3;0,3,2;,
 3;0,1,3;;

 MeshNormals {
  4;
  -0.633030;0.173624;-0.754405;,
  -0.633030;0.173624;-0.754405;,
  -0.633030;0.173624;-0.754405;,
  -0.633030;0.173624;-0.754405;;
  2;
  3;0,3,2;,
  3;0,1,3;;
 }

 MeshTextureCoords {
  4;
  0.987694;0.007957;,
  0.987694;0.334089;,
  0.891139;0.007957;,
  0.891139;0.334089;;
 }

 MeshMaterialList {
  1;
  2;
  0,
  0;

  Material {
   1.000000;1.000000;1.000000;1.000000;;
   50.000000;
   0.000000;0.000000;0.000000;;
   0.400000;0.400000;0.400000;;

   TextureFilename {
    "texRoom\\\\\\\\\\\\\\\\shower.png";
   }
  }
 }
}