#include <iostream> 
#include <string> 
#include <algorithm> 
int main() { 
	std::string input; 
	std::cout << "Enter a string: "; 
	std::getline(std::cin, input); 
	std::string reversed = input; 
	std::reverse(reversed.begin(), reversed.end()); 
	std::cout << "Reversed string: " << reversed << std::endl; 
	return 0; 
}