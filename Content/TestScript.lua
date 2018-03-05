function Start()
	yStart = y;
	xStart = x;
end

function Update()

	--xSpeed = -0.5;
	--y = 5*math.sin(x/5) + yStart;

	ySpeed = 2.5*math.sign(math.sin(t/10));
	xSpeed = math.sin(t/10);
	
end


function math.sign(x)
   if x<0 then
     return -1
   elseif x>0 then
     return 1
   else
     return 0
   end
end
