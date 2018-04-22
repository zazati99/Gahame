function Start()
	yStart = y;
	xStart = x;
end

function Update()

	--xSpeed = -0.5;
	--y = 5*math.sin(x/5) + yStart;

	ySpeed = math.sign(math.sin(t/10));
	xSpeed = 2*math.sin(t/10 + 30);
	
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
