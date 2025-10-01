#include <iostream> 
#include <cmath> 
#include <iomanip> 
int main() { 
	double inputNumber;
	std::cout << "Enter a real number: "; 
	std::cin >> inputNumber;
	if (inputNumber < 0.0)
	{
		std::cout << "ERROR" << std::endl; 
	} 
	else 
	{ 
		double root = std::sqrt(inputNumber);
		std::cout << "Square root: " << std::fixed << std::setprecision(2) << root << std::endl; 
	} 
    return 0; 
}