#include <iostream>
#include <vector>

using namespace std;

long long fibonachi(int n, vector<long long>& saved) {
    if (n == 0) {
        return 0;
    }

    if (n == 1) {
        return 1;
    }

    if (saved[n] != -1) {
        return saved[n];
    }

    saved[n] = fibonachi(n - 1, saved) + fibonachi(n - 2, saved);
    return saved[n];
}

int main() {
    int n;
    cin >> n;

    vector<long long> saved(n + 1, -1);

    cout << fibonachi(n, saved);

    return 0;
}

/*
Пример входных данных:
10

Пример выходных данных:
55

Сложность:
по времени: O(n), потому что каждое число Фибоначчи считается один раз;
по памяти: O(n), для массива сохраненных значений и стека рекурсии.
*/
