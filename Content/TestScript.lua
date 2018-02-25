function Start()
	yStart = y;
end

function Update()
    
	xSpeed = 1
	y = 5*math.sin(x/5) + yStart;
	
end