using System;

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
        #region Objects
        // Load values for a GameObject
        public static GameObject LoadGameObjectValues(GameObject o, StreamReader reader)
        {
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
                    case "OverworldDepthFix":
                        o.Components.Add(new OverworldDepthFix(o));
                    break;
                }
            }
            return o;
        }

        // Load Object with a reader
        public static GameObject LoadGameObject(StreamReader reader, GameScreen screen)
        {
            // creates empty object
            GameObject o = new GameObject();
            o.screen = screen;

            // Load values with help of cool gameObject function
            LoadGameObjectValues(o, reader);
            
            // Initialize objeect and the return it
            o.Initialize();
            return o;
        }

        // Load existing object
        public static GameObject LoadExistingObject(StreamReader reader, GameScreen screen)
        {
            // Creates object with help of advanced stuff
            Type t = Type.GetType(reader.ReadLine());
            GameObject o = (GameObject)Activator.CreateInstance(t);
            o.screen = screen;

            // Load values for the object
            LoadGameObjectValues(o, reader);
            
            // Initialize object and return it
            o.Initialize();
            return o;
        }
        #endregion

        #region HitBox
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
        #endregion

        #region Sprite
        // Loads Sprite from reader
        public static Sprite LoadSprite(StreamReader reader, GameObject o)
        {
            Sprite sprite = new Sprite(o);

            string line;
            while ((line = reader.ReadLine()) != "---")
            {
                switch (line)
                {
                    case "Path":
                        sprite.AddImage(reader.ReadLine());
                        break;
                    case "ImageSpeed":
                        sprite.ImageSpeed = float.Parse(reader.ReadLine(), CultureInfo.InvariantCulture);
                        break;
                    case "Depth":
                        sprite.Depth = float.Parse(reader.ReadLine(), CultureInfo.InvariantCulture);
                        break;
                    case "OriginX":
                        sprite.SpriteOrigin.X = float.Parse(reader.ReadLine(), CultureInfo.InvariantCulture);
                        break;
                    case "OriginY":
                        sprite.SpriteOrigin.Y = float.Parse(reader.ReadLine(), CultureInfo.InvariantCulture);
                        break;
                }
            }
            return sprite;
        }
        #endregion

        #region Dialogue
        // Load dialogue mee
        public static Dialogue LoadDialogue(StreamReader reader, GameObject o)
        {
            Dialogue d = new Dialogue(o);

            string line;
            while((line = reader.ReadLine()) != "---"){
                switch(line){
                    case "DialogueBranch":
                        LoadDialogueBranch(reader, d);
                        break;
                }   
            }
            return d;
        }

        // Load Dialogue box group
        public static void LoadDialogueBranch(StreamReader reader, Dialogue d)
        {
            DialogueBranch branch = new DialogueBranch(d);
            string Key = "";

            string line;
            while ((line = reader.ReadLine()) != "---")
            {
                switch (line)
                {
                    case "DialogueBoxPlain":
                        branch.Boxes.Add(LoadDialogueBoxPlain(reader, branch));
                        break;
                    case "DialogueBoxAlternativePlain":
                        branch.Boxes.Add(LoadDialogueBoxAlternativePlain(reader, branch));
                        break;
                    case "Key":
                        Key = reader.ReadLine();
                        break;
                }
            }
            d.DialogueBranches.Add(Key, branch);
        }

        // Load a Dialogue box
        public static DialogueBoxPlain LoadDialogueBoxPlain(StreamReader reader, DialogueBranch group)
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
        public static DialogueBoxAlternativePlain LoadDialogueBoxAlternativePlain(StreamReader reader, DialogueBranch group)
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
        #endregion

        #region Physics
        // Load Physics
        public static Physics LoadPhysics(StreamReader reader, GameObject o)
        {
            Physics physics = new Physics(o);

            string line;
            while ((line = reader.ReadLine()) != "---")
            {
                switch (line)
                {
                    case "GravityEnabled":
                        physics.GravityEnabled = true;
                        break;
                    case "Solid":
                        physics.Solid = true;
                        break;
                }
            }
            return physics;
        }
        #endregion

        #region Player
        // Load Player
        public static PlayerObjectBattle LoadPlayerBattle(StreamReader reader, GameScreen screen)
        {
            PlayerObjectBattle player = new PlayerObjectBattle(screen);

            string line;
            while((line = reader.ReadLine()) != "---")
            {
                switch (line)
                {
                    case "X":
                        player.Position.X = float.Parse(reader.ReadLine());
                        break;
                    case "Y":
                        player.Position.Y = float.Parse(reader.ReadLine());
                        break;
                }
            }
            return player;
        }

        // Load Player Overworld
        public static PlayerObjectOverworld LoadPlayerOverworld(StreamReader reader, GameScreen screen)
        {
            PlayerObjectOverworld player = new PlayerObjectOverworld(screen);

            string line;
            while ((line = reader.ReadLine()) != "---")
            {
                switch (line)
                {
                    case "X":
                        player.Position.X = float.Parse(reader.ReadLine());
                        break;
                    case "Y":
                        player.Position.Y = float.Parse(reader.ReadLine());
                        break;
                }
            }
            return player;
        }

        // Load Player Overworld
        public static PlayerObjectOverworldSidePerspective LoadPlayerOverworldSidePerspective(StreamReader reader, GameScreen screen)
        {
            PlayerObjectOverworldSidePerspective player = new PlayerObjectOverworldSidePerspective(screen);

            string line;
            while ((line = reader.ReadLine()) != "---")
            {
                switch (line)
                {
                    case "X":
                        player.Position.X = float.Parse(reader.ReadLine());
                        break;
                    case "Y":
                        player.Position.Y = float.Parse(reader.ReadLine());
                        break;
                }
            }
            return player;
        }
        #endregion

        #region Walls
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
        #endregion

        #region GameScreens
        // Load common screen information
        public static GameScreen LoadScreenInformation(GameScreen screen, StreamReader reader, string info)
        {
            switch (info)
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
                case "Wall":
                    screen.GameObjects.Add(LoadWall(reader, screen));
                    break;
                case "ExistingObject":
                    screen.GameObjects.Add(LoadExistingObject(reader, screen));
                    break;
            }
            return screen;
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
                    case "Player":
                        screen.GameObjects.Add(LoadPlayerBattle(reader, screen));
                        break;
                    default:
                        screen = (BattleScreen)LoadScreenInformation(screen, reader, line);
                        break;
                }
            }
            return screen;
        }

        // Load an Overworld screen with streamreader
        public static OverworldScreen LoadOverworldScreen(StreamReader reader)
        {
            OverworldScreen screen = new OverworldScreen();

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                switch (line)
                {
                    case "Player":
                        screen.GameObjects.Add(LoadPlayerOverworld(reader, screen));
                        break;
                    default:
                        screen = (OverworldScreen)LoadScreenInformation(screen, reader, line);
                        break;
                }
            }
            return screen;
        }

        // Load an Overworld screen side perspective edition with streamreader
        public static OverworldScreenSidePerspective LoadOverworldScreenSidePerspective(StreamReader reader)
        {
            OverworldScreenSidePerspective screen = new OverworldScreenSidePerspective();

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                switch (line)
                {
                    case "Player":
                        screen.GameObjects.Add(LoadPlayerOverworldSidePerspective(reader, screen));
                        break;
                    default:
                        screen = (OverworldScreenSidePerspective)LoadScreenInformation(screen, reader, line);
                        break;
                }
            }
            return screen;
        }

        // Load Screen
        public static GameScreen LoadGameScreen(StreamReader reader)
        {
            // Creates screen and loads it
            GameScreen screen = null;
            switch (reader.ReadLine())
            {
                case "BattleScreen":
                    screen = LoadBattleScreen(reader);
                    break;
                case "OverworldScreen":
                    screen = LoadOverworldScreen(reader);
                    break;
                case "OverworldScreenSidePerspective":
                    screen = LoadOverworldScreenSidePerspective(reader);
                    break;
            }
            // return the screen
            return screen;
        }

        // Load Embedded screen
        public static GameScreen LoadScreenFromEmbeddedPath(string path)
        {
            // gets the embedded file and creates StreamReader
            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(Program.ENBEDDEDCONTENT + path));

            // Loads the Screen file
            GameScreen screen = LoadGameScreen(reader);

            // Gives it the same name as path
            screen.Name = path;

            // Close the reader and return the screen
            reader.Close();
            return screen;
        }

        // Load GameScreen from a path
        public static GameScreen LoadScreenFromPath(string path)
        {
            // Creates a StreamReader from the path
            StreamReader reader = new StreamReader(path);

            // Loads the screen file
            GameScreen screen = LoadGameScreen(reader);

            // Gives it the same name as path
            screen.Name = path;

            // Close the reader and return screen
            reader.Close();
            return screen;
        }
        #endregion

        #region Dumb Stuff
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

        // TEST STUFF
        // SML 2.0 interpreter
        public static T LoadStuff<T>(string path)
        {
            T obj = default(T);

            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(Program.ENBEDDEDCONTENT + path));

            obj = createInstance<T>(reader.ReadLine());

            string line;
            while ((line = reader.ReadLine()) != "---")
            {
                
            }
            reader.Close();
            return obj;
        }

        // Load stuff with StreamReader
        public static T LoadStuff<T>(StreamReader reader)
        {
            T obj = default(T);

            string line;
            while ((line = reader.ReadLine()) != "---")
            {

            }

            return obj;
        }

        private static T createInstance<T>(string type)
        {
            Type t = Type.GetType(type);
            return (T)Activator.CreateInstance(t);
        }
        #endregion
    }
}
