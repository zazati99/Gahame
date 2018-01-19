function Start()

	yoyo = 900
	speed = 1

end

function Update()
    
	x = x + speed

	-- Det är så här man skriver kommentarer lol
	if yoyo == 900 and x >= yoyo then
		yoyo = 700
		speed = -1
	end

	-- varför ?
	if yoyo == 700 and x <= yoyo then 
		yoyo = 900
		speed = 1
	end
	
end