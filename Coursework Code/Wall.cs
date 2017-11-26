using System;
using Mogre;
using System.Collections.Generic;


namespace Coursework
{
    class Wall
    {
        ManualObject manual;
        SceneManager mSceneMgr;
        Vector3 v0,v1,v2,v3,v4,v5,v6,v7;
        public Wall(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
        }


        public MeshPtr getCube(string cubeName, string materialName, float width, float height, float depth)
        {
            manual = mSceneMgr.CreateManualObject(cubeName + "_ManObj");
            manual.Begin(materialName, RenderOperation.OperationTypes.OT_TRIANGLE_LIST);

            // --- Fills the Vertex buffer and define the texture coordinates for each vertex ---

            //--- Vertex 0 ---
            v0 = new Vector3(.5f * width, .5f * height, .5f * depth);
            manual.Position(v0);
            //Texture coordinates here!
            manual.TextureCoord(new Vector2(0, 0));

            //--- Vertex 1 ---
            v1 = new Vector3(.5f * width, -.5f * height, .5f * depth);
            manual.Position(v1);
            //Texture coordinates here!
            manual.TextureCoord(new Vector2(1, 0));

            //--- Vertex 2 ---
            v2 = new Vector3(.5f * width, .5f * height, -.5f * depth);
            manual.Position(v2);
            //Texture coordinates here!
            manual.TextureCoord(new Vector2(0, 1));

            //--- Vertex 3 ---
             v3 = new Vector3(.5f * width, -.5f * height, -.5f * depth);
            manual.Position(v3);
            //Texture coordinates here!
            manual.TextureCoord(new Vector2(1,1));


            //--- Vertex 4 ---
             v4 = new Vector3(-.5f * width, .5f * height, .5f * depth);
            manual.Position(v4);
            //Texture coordinates here!
            manual.TextureCoord(new Vector2(0, 0));

            //--- Vertex 5 ---
            v5 = new Vector3(-.5f * width, -.5f * height, .5f * depth);
            manual.Position(v5);
            //Texture coordinates here!
            manual.TextureCoord(new Vector2(-1, 0));

            //--- Vertex 6 ---
            v6 = new Vector3(-.5f * width, .5f * height, -.5f * depth);
            manual.Position(v6);
            //Texture coordinates here!
            manual.TextureCoord(new Vector2(0, -1));

            //--- Vertex 7 ---
            v7 = new Vector3(-.5f * width, -.5f * height, -.5f * depth);
            manual.Position(v7);
            //Texture coordinates here!
            manual.TextureCoord(new Vector2(-1, -1));


            // --- Fills the Index Buffer ---
            //--------Face 1----------
            manual.Index(2);
            manual.Index(1);
            manual.Index(0);

            manual.Index(3);
            manual.Index(1);
            manual.Index(2);

            //--------Face 2----------
            manual.Index(5);
            manual.Index(6);
            manual.Index(4);

            manual.Index(5);
            manual.Index(7);
            manual.Index(6);

            //--------Face 3----------
            manual.Index(1);
            manual.Index(4);
            manual.Index(0);

            manual.Index(5);
            manual.Index(4);
            manual.Index(1);

            ////--------Face 4----------
            //manual.Index(0);
            //manual.Index(6);
            //manual.Index(4);

            //manual.Index(0);
            //manual.Index(2);
            //manual.Index(6);

            //--------Face 5----------
            manual.Index(3);
            manual.Index(2);
            manual.Index(6);

            manual.Index(7);
            manual.Index(3);
            manual.Index(6);

            //--------Face 6----------
            //manual.Index(3);
            //manual.Index(1);
            //manual.Index(7);

            //manual.Index(1);
            //manual.Index(5);
            //manual.Index(7);

            manual.End();
            return manual.ConvertToMesh(cubeName);
        }
        
        public List<Plane> getPlanes(){
            List<Plane> list = new List<Plane>();

            Plane p = new Plane(v2, v1, v0);
            list.Add(p);

            p = new Plane(v5, v6, v4);
            list.Add(p);

            p = new Plane(v1, v4, v0);
            list.Add(p);

            p = new Plane(v3, v2, v6);
            list.Add(p);
            return list;
            }
        }
    }
    



