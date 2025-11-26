#include <iostream> 
#include <iomanip> 

int main() { 
	const double PI = 3.14159;
	double radius; 
	std::cout << "Enter the radius of the circle: "; 
	std::cin >> radius; 
	if (radius < 0) { 
		std::cout << "Error: Radius cannot be negative." << std::endl; 
	} 
	else { 
		double area = PI * radius * radius;
		std::cout << "Area of the circle: " << std::fixed << std::setprecision(2) << area << std::endl; 
	} 
return 0; 
}