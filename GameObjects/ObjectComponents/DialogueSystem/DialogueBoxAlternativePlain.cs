﻿using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Gahame.GameUtils;
using System;

namespace Gahame.GameObjects.ObjectComponents.DialogueSystem
{
    public class DialogueBoxAlternativePlain : DialogueBox
    {
        // Alternatives
        public List<Alternative> Alternatives;
        public int CurrentAlternative;

        // Position of the Box
        public Vector2 Position;
        Vector2 origin;
        Vector2 size;

        // Constructor 
        public DialogueBoxAlternativePlain(DialogueBoxGroup group) : base(group)
        {
            Alternatives = new List<Alternative>();
            CurrentAlternative = 0;

            // Position stuff
            origin = Vector2.Zero;
            size = new Vector2(202, 50);
            Position = new Vector2(CameraController.ViewWidth() / 2 - 100, CameraController.ViewHeight() - 60);

            Font.LineSpacing = (int)(size.Y / 2) - FontSize;
        }

        // Updates stuff
        public override void Update(GameTime gameTime)
        {
            // next alternative
            if (GameControlls.RightPressed)
            {
                CurrentAlternative++;
                if (CurrentAlternative > Alternatives.Count - 1) CurrentAlternative = 0;
            }
            // previous alternative
            if (GameControlls.LeftPressed)
            {
                CurrentAlternative--;
                if (CurrentAlternative < 0) CurrentAlternative = Alternatives.Count - 1;
            }
            // next group
            if (GameControlls.E && !GameControlls.ActivateCD)
            {
                group.ClearGroup();
                group.dialogue.Key = Alternatives[CurrentAlternative].Key;
                CurrentAlternative = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Position.Y = CameraController.ViewHeight() - 60;

            // Draws the box
            ShapeRenderer.FillRectangle(
                spriteBatch,
                CameraController.PositionOnScreen(Position + new Vector2(-1, 0)),
                size,
                0.01f,
                Color.Black
                );

            // Draws the alternatives
            for (int i = 0; i < Alternatives.Count; i++)
            {
                Color col = Color.Gray;
                if (i == CurrentAlternative) col = Color.White;

                origin.X = Font.MeasureString(Alternatives[i].Text).X / 2;
                origin.Y = Font.MeasureString(Alternatives[i].Text).Y / 2;

                int rows = MyMaths.Round(Alternatives.Count / 2);
                int n = (((int)(i / 2)) * 2) + 1;
                float offsety = (50 / (2 * rows)) * ((n*2) / (2 * rows));

                Vector2 offset = new Vector2(50 + i%2 * 75, offsety);

                spriteBatch.DrawString(
                    Font,
                    Alternatives[i].Text,
                    CameraController.PositionOnScreen(Position + offset),
                    col,
                    0,
                    origin,
                    1,
                    SpriteEffects.None,
                    0
                    );
            }
        }

    }
}
