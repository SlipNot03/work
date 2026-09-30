#include <iostream>

using namespace std;

int main() {
    long long n;
    cin >> n;

    int questions = 1;

    while (n > 2) {
        n = (n + 1) / 2;
        questions++;
    }

    cout << questions;

    return 0;
}

/*
Пример входных данных:
100

Пример выходных данных:
7

Сложность:
по времени: O(log n);
по памяти: O(1).
*/
