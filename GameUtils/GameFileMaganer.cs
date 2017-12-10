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
using Gahame.GameObjects.ObjectComponents.DialogueSystem;

using System.IO;
using System.Reflection;
using System.Globalization;

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
                    case "Dialogue":
                        o.Components.Add(LoadDialogue(reader, o));
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
                switch (line)
                {
                    case "BoxCollider":
                        hb.Colliders.Add(LoadBoxCollider(reader));
                        break;
                    case "Solid":
                        hb.Solid = true;
                        break;
                }  
            }

            return hb;
        }

        // Load BoxCollider
        public static BoxCollider LoadBoxCollider(StreamReader reader)
        {
            BoxCollider col = new BoxCollider();

            string line;
            while ((line = reader.ReadLine()) != "---")
            {
                switch (line)
                {
                    case "Width":
                        col.Size.X = float.Parse(reader.ReadLine());
                        break;
                    case "Height":
                        col.Size.Y = float.Parse(reader.ReadLine());
                        break;
                    case "OffsetX":
                        col.Offset.X = float.Parse(reader.ReadLine());
                        break;
                    case "OffsetY":
                        col.Offset.Y = float.Parse(reader.ReadLine());
                        break;
                }
            }

            return col;
        }

        // YOU NOW ENTER DIALOGUE HELL
        // Load dialogue mee
        public static Dialogue LoadDialogue(StreamReader reader, GameObject o)
        {
            Dialogue d = new Dialogue(o);

            string line;
            while((line = reader.ReadLine()) != "---"){
                switch(line){
                    case "DialogueBoxGroup":
                        DialogueBoxGroup group = LoadDialogueBoxGroup(reader, d);
                        d.BoxGroups.Add(group.Key, group);
                        break;
                }   
            }

            return d;
        }

        // Load Dialogue box group
        public static DialogueBoxGroup LoadDialogueBoxGroup(StreamReader reader, Dialogue d)
        {
            DialogueBoxGroup group = new DialogueBoxGroup(d);

            string line;
            while ((line = reader.ReadLine()) != "---")
            {
                switch (line)
                {
                    case "DialogueBoxPlain":
                        group.Boxes.Add(LoadDialogueBoxPlain(reader, group));
                        break;
                    case "DialogueBoxAlternativePlain":
                        group.Boxes.Add(LoadDialogueBoxAlternativePlain(reader, group));
                        break;
                    case "Key":
                        group.Key = reader.ReadLine();
                        break;
                }
            }
            return group;
        }

        // Load a Dialogue box
        public static DialogueBoxPlain LoadDialogueBoxPlain(StreamReader reader, DialogueBoxGroup group)
        {
            DialogueBoxPlain box = new DialogueBoxPlain(group);

            string line;
            while ((line = reader.ReadLine()) != "---")
            {
                switch(line){
                    case "Text":
                        box.Text = reader.ReadLine().Replace("|", "\n");
                        break;
                    case "TextSpeed":
                        box.UpdateSpeed = float.Parse(reader.ReadLine(), CultureInfo.InvariantCulture);
                        break;
                    case "NonSkippable":
                        box.Skippable = false;
                        break;
                }
            }
            return box;
        }

        // Load a plain choice box
        public static DialogueBoxAlternativePlain LoadDialogueBoxAlternativePlain(StreamReader reader, DialogueBoxGroup group)
        {
            DialogueBoxAlternativePlain box = new DialogueBoxAlternativePlain(group);

            string line;
            while ((line = reader.ReadLine()) != "---")
            {
                switch (line)
                {
                    case "Alternative":
                        box.Alternatives.Add(LoadAlternative(reader));
                        break;
                }
            }
            return box;
        }

        // Load the alternatives for a choice box
        public static Alternative LoadAlternative(StreamReader reader)
        {
            Alternative alternative = new Alternative();

            string line;
            while ((line = reader.ReadLine()) != "---")
            {
                switch (line)
                {
                    case "Text":
                        alternative.Text = reader.ReadLine();
                        break;
                    case "Key":
                        alternative.Key = reader.ReadLine();
                        break;
                }
            }
            return alternative;
        }
        // END OF ALL OF THE DIALOGUE MEMES HERE

        // Load Physics
        public static Physics LoadPhysics(StreamReader reader, GameObject o)
        {
            Physics p = new Physics(o);

            string line;
            while ((line = reader.ReadLine()) != "---")
            {
                switch (line)
                {
                    case "GravityEnabled":
                        p.GravityEnabled = true;
                        break;
                    case "Solid":
                        p.Solid = true;
                        break;
                }
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
                switch (line)
                {
                    case "X":
                        p.Position.X = float.Parse(reader.ReadLine());
                        break;
                    case "Y":
                        p.Position.Y = float.Parse(reader.ReadLine());
                        break;
                }
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
                switch (line)
                {
                    case "X":
                        wall.Position.X = float.Parse(reader.ReadLine());
                        break;
                    case "Y":
                        wall.Position.Y = float.Parse(reader.ReadLine());
                        break;
                    case "Width":
                        col.Size.X = float.Parse(reader.ReadLine());
                        break;
                    case "Height":
                        col.Size.Y = float.Parse(reader.ReadLine());
                        break;
                }

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
                switch (line)
                {
                    case "Path":
                        s.AddImage(reader.ReadLine());
                        break;
                    case "ImageSpeed":
                        s.ImageSpeed = float.Parse(reader.ReadLine(), CultureInfo.InvariantCulture);
                        break;
                    case "Depth":
                        s.Depth = float.Parse(reader.ReadLine(), CultureInfo.InvariantCulture);
                        break;
                }
            }

            return s;
        }

        // Load GameScreen witth a Streamreader
        public static BattleScreen LoadBattleScreen(StreamReader reader)
        {
            BattleScreen screen = new BattleScreen();

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                switch (line)
                {
                    case "Width":
                        screen.ScreenSize.X = float.Parse(reader.ReadLine());
                        break;
                    case "Height":
                        screen.ScreenSize.Y = float.Parse(reader.ReadLine());
                        break;
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

        // Load Embedded screen
        public static GameScreen LoadScreenEmbedded(string path)
        {
            GameScreen screen = null;

            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(Program.ENBEDDEDCONTENT + path));

            switch (reader.ReadLine())
            {
                case "BattleScreen":
                    screen = LoadBattleScreen(reader);
                    break;
            }

            reader.Close();
            return screen;
        }

        // Load GameScreen from a path
        public static GameScreen LoadScreen(string path)
        {
            GameScreen screen = null;
            StreamReader reader = new StreamReader(path);

            switch (reader.ReadLine())
            {
                case "BattleScreen":
                    screen = LoadBattleScreen(reader);
                    break;
            }

            reader.Close();
            return screen;
        }

        // Write to log file
        public static void SaveLog(string[] log)
        {
            StreamWriter writer = new StreamWriter("log.txt");
            for (int i = 0; i < log.Length; i++)
            {
                writer.WriteLine(log[i]);
            }
            writer.Close();
        }

        // Encrypt a file (probablt wont need this)
        public static void EncryptFile(string path, string newPath)
        {
            byte[] bytes = File.ReadAllBytes(path);
            for (int i = 0; i < bytes.Length; i++) bytes[i] ^= 0x7b;
            FileStream stream = new FileStream(newPath, FileMode.Create, FileAccess.Write);
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
        }

    }
}
