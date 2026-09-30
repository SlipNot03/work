#include <iostream>
#include <iomanip>

using namespace std;

int main() {
    long double x;
    cin >> x;

    long double left = 0;
    long double right = x;

    if (right < 1) {
        right = 1;
    }

    for (int i = 0; i < 200; i++) {
        long double middle = (left + right) / 2;

        if (middle * middle <= x) {
            left = middle;
        } else {
            right = middle;
        }
    }

    cout << fixed << setprecision(12) << (double)left;

    return 0;
}

/*
Пример входных данных:
2

Пример выходных данных:
1.414213562373

Сложность:
по времени: O(1), потому что количество итераций фиксировано;
по памяти: O(1).
*/
