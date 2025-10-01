program SquareRoot;
var
  inputNumber, root: real;
begin
  writeln('Enter a real number: ');
  read(inputNumber);
  if inputNumber < 0
  then
    begin
      writeln('ERROR');
    end
  else
    begin
      root := sqrt(inputNumber);
      writeln('Square root: ', root:0:2);
    end;
  readln;
end.
