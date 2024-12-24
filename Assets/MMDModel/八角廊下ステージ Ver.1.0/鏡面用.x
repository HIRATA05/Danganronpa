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


Mesh {
 12;
 -56.683104;0.000000;-2.646090;,
 -56.683104;0.000000;2.646090;,
 -7.890500;0.000000;-2.646090;,
 -7.890500;0.000000;2.646090;,
 -2.950090;0.000000;7.586510;,
 -2.950000;0.000000;-7.586590;,
 2.950090;0.000000;7.586510;,
 2.950000;0.000000;-7.586590;,
 7.890500;0.000000;-2.646090;,
 7.890500;0.000000;2.646090;,
 56.683104;0.000000;-2.646090;,
 56.683104;0.000000;2.646090;;
 10;
 3;0,1,2;,
 3;3,2,1;,
 3;4,2,3;,
 3;5,2,4;,
 3;6,5,4;,
 3;7,5,6;,
 3;8,7,6;,
 3;9,8,6;,
 3;10,8,9;,
 3;11,10,9;;

 MeshNormals {
  12;
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;1.000000;0.000000;;
  10;
  3;0,1,2;,
  3;3,2,1;,
  3;4,2,3;,
  3;5,2,4;,
  3;6,5,4;,
  3;7,5,6;,
  3;8,7,6;,
  3;9,8,6;,
  3;10,8,9;,
  3;11,10,9;;
 }

 MeshTextureCoords {
  12;
  4.082127;0.984499;,
  -0.071327;0.984499;,
  4.082127;-37.309251;,
  -0.071337;-37.309251;,
  -3.948696;-41.186624;,
  7.959548;-41.186685;,
  -3.948696;-45.817222;,
  7.959548;-45.817162;,
  4.082127;-49.694595;,
  -0.071337;-49.694595;,
  4.082117;-87.988282;,
  -0.071337;-87.988282;;
 }

 MeshMaterialList {
  1;
  10;
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0;

  Material {
   0.752941;0.752941;0.752941;1.000000;;
   0.000000;
   0.000000;0.000000;0.000000;;
   0.752941;0.752941;0.752941;;
  }
 }
}