#include <iostream>
#include <vector>

using namespace std;

const long long MOD = 1000000007LL;

int main() {
    int n;
    cin >> n;

    vector<long long> dp(n + 1, 0);

    dp[0] = 0;
    if (n >= 1) {
        dp[1] = 1;
    }

    for (int i = 2; i <= n; i++) {
        dp[i] = (dp[i - 1] + dp[i - 2]) % MOD;
    }

    cout << dp[n];

    return 0;
}

/*
Пример входных данных:
10

Пример выходных данных:
55

Сложность:
по времени: O(n);
по памяти: O(n), для массива dp.
*/
