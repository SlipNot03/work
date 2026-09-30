#include <iostream>
#include <iomanip>

using namespace std;

long double getValue(long double a, long double b, long double c, long double x) {
    return a * x * x + b * x + c;
}

long double ternarySearch(long double a, long double b, long double c,
                          long double left, long double right) {
    for (int i = 0; i < 200; i++) {
        long double m1 = left + (right - left) / 3;
        long double m2 = right - (right - left) / 3;

        if (getValue(a, b, c, m1) < getValue(a, b, c, m2)) {
            right = m2;
        } else {
            left = m1;
        }
    }

    return (left + right) / 2;
}

int main() {
    long double a, b, c;
    cin >> a >> b >> c;

    long double left, right;
    cin >> left >> right;

    if (left > right) {
        long double temp = left;
        left = right;
        right = temp;
    }

    long double answer;

    if (a < 0) {
        long double leftValue = getValue(a, b, c, left);
        long double rightValue = getValue(a, b, c, right);

        if (leftValue <= rightValue) {
            answer = left;
        } else {
            answer = right;
        }
    } else {
        answer = ternarySearch(a, b, c, left, right);
    }

    cout << fixed << setprecision(3) << (double)answer;

    return 0;
}

/*
Пример входных данных:
1 4 3
-3 3

Пример выходных данных:
-2.000

Сложность:
по времени: O(1), потому что количество итераций тернарного поиска фиксировано;
по памяти: O(1).
*/
