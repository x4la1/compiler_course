# Примеры программ на языке Init

### 1. `SumNumbers` — сложение двух чисел

```
float a;
float b;
input(a);
input(b);
float sum = a + b;
print(sum);
```

---

### 2. `CircleSquare` — площадь круга

```
float radius;
input(radius);
float area = Pi * radius ^ 2;
print(area);
```

---

### 3. `FahrenheitToCelsius` — перевод температуры

```
float fahrenheit;
input(fahrenheit);
float celsius = (fahrenheit - 32.0) * 5.0 / 9.0;
print(celsius);
```

### 4. `Factorial` — вычисление факториала с помощью цикла while

```
float n;
print("Enter N:");
input(n);

float result = 1;
float i = 1;
while (i <= n) {
    result = result * i;
    i = i + 1;
}
print("Factorial:", result);
```

### 5. `IsPrime` — проверка простоты числа

```
func sqrt(x: float): float {
    float guess = x / 2;
    float prev = 0;
    while (guess != prev) {
        prev = guess;
        guess = (guess + x / guess) / 2;
    }
    return guess;
}

float n;
print("Enter N:");
input(n);

float is_prime = 1;
if (n < 2) {
    is_prime = 0;
} else {
    float limit = sqrt(n);
    float d = 2;
    while (d <= limit) {
        if (n % d == 0) {  
            is_prime = 0;
            break;
        }
        d = d + 1;
    }
}
print(is_prime);
```

### 6. `Pow` — возведение в степень с помощью цикла

```
float a;
float b;
print("Enter A and B:");
input(a);
input(b);

float result = 1;
float i = 0;
while (i < b) {
    result = result * a;
    i = i + 1;
}
print("A^B:", result);
```

### 7. `Pow` с repeat-until

```
float a;
float b;
print("Enter A and B:");
input(a);
input(b);

float result = 1;
float i = 0;
repeat {
    result = result * a;
    i = i + 1;
} until (i >= b);
print("A^B:", result);
```