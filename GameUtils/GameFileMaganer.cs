using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Gahame.GameScreens;
using Gahame.GameObjects;
using Gahame.GameObjects.ObjectComponents;
using Gahame.GameObjects.ObjectComponents.Colliders;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;

namespace Gahame.GameUtils
{
    public class GameFileMaganer
    {

        // Load Object with a reader
        public static GameObject LoadGameObject(StreamReader reader, GameScreen screen)
        {
            EmptyObject o = new EmptyObject();
            o.screen = screen;
            o.Position = new Vector2(0, 0);

            string line;
            while ((line = reader.ReadLine()) != "---")
            {
                switch (line)
                {
                    case "X":
                        o.Position.X = float.Parse(reader.ReadLine());
                        break;
                    case "Y":
                        o.Position.Y = float.Parse(reader.ReadLine());
                        break;
                    case "Tag":
                        o.Tag = reader.ReadLine();
                        break;
                    case "Sprite":
                        o.Components.Add(LoadSprite(reader, o));
                        break;
                    case "SpriteFile":
                        // Loads Sprite from file on next line
                        StreamReader temp = new StreamReader(reader.ReadLine());
                        o.Components.Add(LoadSprite(temp, o));
                        temp.Close();
                        break;
                    case "HitBox":
                        o.Components.Add(LoadHitBox(reader, o));
                        break;
                    case "Physics":
                        o.Components.Add(LoadPhysics(reader, o));
                        break;
                }
            }
            o.Initialize();
            return o;
        }

        // Load a hitbox from a reader
        public static HitBox LoadHitBox(StreamReader reader, GameObject o)
        {
            HitBox hb = new HitBox(o);

            string line;
            while((line = reader.ReadLine()) != "---")
            {
                if(line == "BoxCollider")
                {
                    BoxCollider b = new BoxCollider();
                    while((line = reader.ReadLine()) != "---")
                    {
                        if (line == "Width") b.Size.X = float.Parse(reader.ReadLine());
                        if (line == "Height") b.Size.Y = float.Parse(reader.ReadLine());
                        if (line == "OffsetX") b.Offset.X = float.Parse(reader.ReadLine());
                        if (line == "OffsetY") b.Offset.Y = float.Parse(reader.ReadLine());
                    }
                    hb.Colliders.Add(b);
                }
                if (line == "Solid")
                {
                    hb.Solid = true;
                }
                
            }
            return hb;
        }

        // Load Physics
        public static Physics LoadPhysics(StreamReader reader, GameObject o)
        {
            Physics p = new Physics(o);

            string line;
            while ((line = reader.ReadLine()) != "---")
            {
                if (line == "GravityEnabled") p.GravityEnabled = true;
                if (line == "Solid") p.Solid = true;
            }
            return p;
        }

        // Load Player
        public static PlayerObject LoadPlayer(StreamReader reader, GameScreen screen)
        {
            PlayerObject p = new PlayerObject(screen);

            string line;
            while((line = reader.ReadLine()) != "---")
            {
                if (line == "X") p.Position.X = float.Parse(reader.ReadLine());
                if (line == "Y") p.Position.Y = float.Parse(reader.ReadLine());
            }
            return p;
        }

        // Load Wall
        public static WallObject LoadWall(StreamReader reader, GameScreen screen)
        {
            WallObject wall = new WallObject();
            wall.screen = screen;

            BoxCollider col = new BoxCollider();

            string line;
            while ((line = reader.ReadLine()) != "---")
            {
                if (line == "X") wall.Position.X = float.Parse(reader.ReadLine());
                if (line == "Y") wall.Position.Y = float.Parse(reader.ReadLine());
                if (line == "Width") col.Size.X = float.Parse(reader.ReadLine());
                if (line == "Height") col.Size.Y = float.Parse(reader.ReadLine());
            }

            wall.GetComponent<HitBox>().Colliders.Add(col);
            return wall;
        }

        // Loads Sprite from reader
        public static Sprite LoadSprite(StreamReader reader, GameObject o)
        {
            Sprite s = new Sprite(o);

            string line;
            while ((line = reader.ReadLine()) != "---")
            {
                if (line == "Path") s.AddImage(reader.ReadLine());
                if (line == "ImageSpeed") s.ImageSpeed = float.Parse(reader.ReadLine());
                if (line == "Depth") s.Depth = float.Parse(reader.ReadLine());
            }

            return s;
        }

        // Load GameScreen
        public static GenericGameScreen LoadScreen(string path)
        {
            GenericGameScreen screen = new GenericGameScreen();
            StreamReader reader = new StreamReader(path);

            string line;
            while((line = reader.ReadLine()) != null)
            {
                switch (line)
                {
                    case "GameObject":
                        screen.GameObjects.Add(LoadGameObject(reader, screen));
                        break;
                    case "GameObjectFile":
                        StreamReader temp = new StreamReader(reader.ReadLine());
                        screen.GameObjects.Add(LoadGameObject(temp, screen));
                        temp.Close();
                        break;
                    case "Player":
                        screen.GameObjects.Add(LoadPlayer(reader, screen));
                        break;
                    case "Wall":
                        screen.GameObjects.Add(LoadWall(reader, screen));
                        break;
                }
            }

            reader.Close();
            return screen;
        }

        // Load Encrypted GameScreen
        public static GenericGameScreen LoadScreenEncrypted(string path)
        {
            GenericGameScreen screen = new GenericGameScreen();

            byte[] bytes = File.ReadAllBytes(path);
            for (int i = 0; i < bytes.Length; i++) bytes[i] ^= 0x7b;
            Stream stream = new MemoryStream(bytes);
            StreamReader reader = new StreamReader(stream);

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                switch (line)
                {
                    case "GameObject":
                        screen.GameObjects.Add(LoadGameObject(reader, screen));
                        break;
                    case "GameObjectFile":
                        StreamReader temp = new StreamReader(reader.ReadLine());
                        screen.GameObjects.Add(LoadGameObject(temp, screen));
                        temp.Close();
                        break;
                }
            }

            reader.Close();
            stream.Close();
            return screen;
        }

        // Streamreader that will decrypt File (does not work)
        static StreamReader Decrypt(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            for (int i = 0; i < bytes.Length; i++) bytes[i] ^= 0x7b;

            Stream stream = new MemoryStream(bytes);
            StreamReader reader = new StreamReader(stream);
            stream.Close();

            return reader;
        }

        // Encrypt a file (probablt wont need this)
        public void EncryptFile(string path, string newPath)
        {
            byte[] bytes = File.ReadAllBytes(path);
            for (int i = 0; i < bytes.Length; i++) bytes[i] ^= 0x7b;
            FileStream stream = new FileStream(newPath, FileMode.Create, FileAccess.Write);
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
        }

    }
}
