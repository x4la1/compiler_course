program CircleSquare;
const Pi = 3.14159;
var 
  radius, area: real;
begin
  writeln('Enter the radius of the circle:');
  read(radius);
  if radius < 0 
  then
    begin
      writeln('Error: Radius cannot be negative.');
    end
  else
    begin
      area := Pi * radius * radius;
      writeln('Area of the circle: ', area:0:2);
    end;
  readln;
end.
