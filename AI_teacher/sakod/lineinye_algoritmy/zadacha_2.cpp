#include <iostream>
#include <vector>

using namespace std;

const long long MOD = 1000000009LL;

long long extendedGcd(long long a, long long b, long long& x, long long& y) {
    if (b == 0) {
        x = 1;
        y = 0;
        return a;
    }

    long long x1, y1;
    long long gcd = extendedGcd(b, a % b, x1, y1);

    x = y1;
    y = x1 - (a / b) * y1;

    return gcd;
}

long long inverseModulo(long long number) {
    long long x, y;
    extendedGcd(number, MOD, x, y);

    x %= MOD;
    if (x < 0) {
        x += MOD;
    }

    return x;
}

int main() {
    ios::sync_with_stdio(false);
    cin.tie(nullptr);

    int n;
    cin >> n;

    vector<long long> prefix(n + 1, 1);

    for (int i = 1; i <= n; i++) {
        long long x;
        cin >> x;
        prefix[i] = (prefix[i - 1] * x) % MOD;
    }

    int m;
    cin >> m;

    for (int i = 0; i < m; i++) {
        int left, right;
        cin >> left >> right;

        long long divider = prefix[left - 1];
        long long answer = prefix[right] * inverseModulo(divider) % MOD;

        cout << answer;
        if (i + 1 < m) {
            cout << '\n';
        }
    }

    return 0;
}

/*
Пример входных данных:
5
1 2 3 4 5
3
1 5
2 4
3 3

Пример выходных данных:
120
24
3

Сложность:
по времени: O(n + m log MOD), потому что для каждого запроса ищется обратный элемент;
по памяти: O(n), для массива префиксных произведений.
*/
