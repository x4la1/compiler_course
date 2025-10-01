program Reverse;
var
  str, reversed: string;
  i: integer;
begin
  writeln('Enter a string:');
  read(str);
  reversed := '';
  for i := length(str) downto 1
  do
    begin
      reversed := reversed + str[i];
    end;
  writeln('Reversed string: ', reversed);
  readln;
end.
