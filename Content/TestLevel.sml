OverworldScreen
	Width
		1920
	Height
		1080
	Background
		Path
			Backgrounds/testkek
		RepeatY
		RepeatX
	---
	Tileset
		AmountX
			4
		AmountY
			4
		Path
			Backgrounds/tile
		Tile
			X
				400
			Y
				50
			Row
				3
			Column
				1
		---
	---
	Player
		X
			400
		Y
			200
	---
	Wall
		X
			336
		Y
			160
		Width
			16
		Height
			96
	---
	Wall
		X
			336
		Y
			256
		Width
			304
		Height
			80
	---
	GameObject
		X
			544
		Y
			128
		Sprite
			Path
				Sprites/Test
		---
		HitBox
			Solid
			BoxCollider
				Width
					64
				Height
					128
				OffsetX
					32
				OffsetY
					0
			---
		---
		Physics
			Solid
		---
		Dialogue
			DialogueBranch
				DialogueBoxPlain
					Text
						hehe
				---
				DialogueBoxAlternativePlain
					Alternative
						Text
							hehe
						Key
							MemeGroup
					---
					Alternative
						Text
							Please no
						Key
							RealGroup
					---
				---
			---
			DialogueBranch
				Key
					MemeGroup
				DialogueBoxPlain
					Text
						hehehehee
				---
				DialogueBoxPlain
					Text
						Y O U|M E M E D|Y O U R S E L F
					NonSkippable
					TextSpeed
						0.05
				---
			---
			DialogueBranch
				Key
					RealGroup
				DialogueBoxPlain
					Text
						Okay
				---
				DialogueBoxPlain
					Text
						ieusfhiusehfuisehfusfuhirdgf|osiejfsoiefjseoifjoiijdfgrws|eiosfjosiejfoisejfoijgdrgwdgr
					NonSkippable
					TextSpeed
						1
				---
			---
		---
	---
	GameObject
		X
			700
		Y
			128
		Sprite
			Path
				Sprites/Test
			OriginX
				64
			OriginY
				64
		---
		HitBox
			Solid
			BoxCollider
				Width
					64
				Height
					64
				OffsetX
					-32
				OffsetY
					0
			---
		---
		Physics
			Solid
		---
		Dialogue
			DialogueBranch
				DialogueBoxPlain
					Text
						hehe
				---
				DialogueBoxAlternativePlain
					Alternative
						Text
							hehe
						Key
							MemeGroup
					---
					Alternative
						Text
							Please no
						Key
							RealGroup
					---
				---
			---
			DialogueBranch
				Key
					MemeGroup
				DialogueBoxPlain
					Text
						hehehehee
				---
				DialogueBoxPlain
					Text
						Y O U|M E M E D|Y O U R S E L F
					NonSkippable
					TextSpeed
						0.05
				---
			---
			DialogueBranch
				Key
					RealGroup
				DialogueBoxPlain
					Text
						Okay
				---
				DialogueBoxPlain
					Text
						ieusfhiusehfuisehfusfuhirdgf|osiejfsoiefjseoifjoiijdfgrws|eiosfjosiejfoisejfoijgdrgwdgr
					NonSkippable
					TextSpeed
						1
				---
			---
		---
		OverworldDepthFix
		LuaScript
			EmbeddedFile
				TestScript.lua
		---
	---
---