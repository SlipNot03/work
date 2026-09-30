#include <iostream>

using namespace std;

int main() {
    ios::sync_with_stdio(false);
    cin.tie(nullptr);

    int n;
    cin >> n;

    long long currentSum = 0;
    long long bestSum = 0;

    int currentLeft = 1;
    int bestLeft = 1;
    int bestRight = 1;

    for (int i = 1; i <= n; i++) {
        long long x;
        cin >> x;

        if (i == 1) {
            currentSum = x;
            bestSum = x;
            currentLeft = 1;
            bestLeft = 1;
            bestRight = 1;
            continue;
        }

        if (currentSum <= 0) {
            currentSum = x;
            currentLeft = i;
        } else {
            currentSum += x;
        }

        if (currentSum > bestSum) {
            bestSum = currentSum;
            bestLeft = currentLeft;
            bestRight = i;
        }
    }

    cout << bestLeft << ' ' << bestRight;

    return 0;
}

/*
Пример входных данных:
5
1 -3 2 4 -5

Пример выходных данных:
3 4

Сложность:
по времени: O(n), массив просматривается один раз;
по памяти: O(1).
*/
